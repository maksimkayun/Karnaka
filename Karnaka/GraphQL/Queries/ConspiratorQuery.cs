﻿using GraphQL;
using GraphQL.Types;
using Karnaka.Data.Models;
using Karnaka.GraphQL.GraphTypes;
using Karnaka.GraphQL.ServicesGraphQL;

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
    }
    
    private QueryArgument MakeNonNullStringArgument(string name, string description) {
        return new QueryArgument<NonNullGraphType<StringGraphType>> {
            Name = name, Description = description
        };
    }

    private IEnumerable<Conspirator> GetAllConspirators(IResolveFieldContext<object?> arg)
    {
        var t = _service.GetAllConspirators();
        return t;
    }
    
    private Conspirator GetConspirator(IResolveFieldContext<object?> arg)
    {
        return _service.GetConspirator(int.Parse(arg.GetArgument<string>("id")));
    }
}