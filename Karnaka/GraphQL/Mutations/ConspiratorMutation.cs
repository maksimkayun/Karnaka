using GraphQL;
using GraphQL.Types;
using Karnaka.Data;
using Karnaka.Data.Helpers;
using Karnaka.Data.Models;
using Karnaka.GraphQL.GraphTypes;
using Karnaka.GraphQL.ServicesGraphQL;
using Karnaka.Services;
using Karnaka.Services.Dto;

namespace Karnaka.GraphQL.Mutations;

public class ConspiratorMutation : ObjectGraphType
{
    private readonly IConspiratorServiceGraphQL _service;

    public ConspiratorMutation(IConspiratorServiceGraphQL service)
    {
        _service = service;

        Field<ConspiratorGraphType>("createConspirator",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                new QueryArgument<StringGraphType> { Name = "location" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "partPlan" }
                ),
            resolve: tContext =>
            {
                var name = tContext.GetArgument<string>("name");
                var location = tContext.GetArgument<string>("location");
                var plan = tContext.GetArgument<string>("partPlan");

                var newConspirator = new ConspiratorDto()
                {
                    Name = name,
                    Location = location,
                    PartPlan = plan
                };
                var conspirator = _service.AddConspirator(newConspirator);
                return conspirator;
            });
    }
}