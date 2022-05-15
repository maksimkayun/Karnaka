using AutoMapper;
using GraphQL.Types;
using Karnaka.Data;
using Karnaka.GraphQL.Mutations;
using Karnaka.GraphQL.Queries;
using Karnaka.GraphQL.ServicesGraphQL;
using Karnaka.Services;

namespace Karnaka.GraphQL.Schemas;

public class ConspiratorSchema : Schema
{
    public ConspiratorSchema(IConspiratorServiceGraphQL service)
    {
        Query = new ConspiratorQuery(service);
        Mutation = new ConspiratorMutation(service);
    }
}