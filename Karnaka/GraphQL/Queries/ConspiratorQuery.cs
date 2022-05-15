using AutoMapper;
using GraphQL;
using GraphQL.Types;
using Karnaka.Data.Models;
using Karnaka.GraphQL.GraphTypes;
using Karnaka.GraphQL.ServicesGraphQL;
using Karnaka.HAL;
using Karnaka.Services.Dto;

namespace Karnaka.GraphQL.Queries;

public class ConspiratorQuery : ObjectGraphType
{
    private readonly IConspiratorServiceGraphQL _service;

    public ConspiratorQuery(IConspiratorServiceGraphQL service)
    {
        _service = service;

        Field<ListGraphType<ConspiratorGraphType>>("conspirators", "Запрос для получения списка всех заговорщиков",
            resolve: GetAllConspirators);
        Field<ConspiratorGraphType>("conspirator", "Запрос для получения данных о заговорщике",
            new QueryArguments(MakeNonNullStringArgument("id", "ID искомого заговорщика")),
            resolve: GetConspirator);
        Field<ConspiratorGraphType>("conspiratorByName", "Запрос для получения данных о заговорщике по имени",
            new QueryArguments(MakeNonNullStringArgument("name", "ID искомого заговорщика")),
            resolve: GetConspiratorByName);
        
        // with pagination
        Field<ListGraphType<ConspiratorGraphType>>("conspiratorsPag", "Запрос для получения списка всех заговорщиков",
            new QueryArguments(MakeNonNullStringArgument("index", "С какой страницы начинаем"),
                MakeNonNullStringArgument("count", "Сколько записей на странице")),
            resolve: GetAllConspiratorsPag);
    }

    private IEnumerable<Conspirator> GetAllConspiratorsPag(IResolveFieldContext<object?> arg)
    {
        var index = int.Parse(arg.GetArgument<string>("index"));
        var count = int.Parse(arg.GetArgument<string>("count"));

        var items = _service.GetAllConspirators()
            .Skip(index)
            .Take(count).ToList();
        return items;
    }

    private Conspirator GetConspiratorByName(IResolveFieldContext<object?> arg)
    {
        var name = arg.GetArgument<string>("name");
        return _service.GetByName(name);
    }

    private QueryArgument MakeNonNullStringArgument(string name, string description) {
        return new QueryArgument<NonNullGraphType<StringGraphType>> {
            Name = name, Description = description
        };
    }

    private IEnumerable<Conspirator> GetAllConspirators(IResolveFieldContext<object?> arg)
    {
        return _service.GetAllConspirators();
    }
    
    private Conspirator GetConspirator(IResolveFieldContext<object?> arg)
    {
        return _service.GetConspirator(int.Parse(arg.GetArgument<string>("id")));
    }
}