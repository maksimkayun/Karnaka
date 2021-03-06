using Karnaka.Data.Models;
using Karnaka.Services.Dto;

namespace Karnaka.GraphQL.ServicesGraphQL;

public interface IConspiratorServiceGraphQL
{
    Conspirator GetConspirator(int id);
    IQueryable<Conspirator> GetAllConspirators();
    Conspirator UpdateConspirator(Conspirator conspirator, int id);
    Conspirator AddConspirator(ConspiratorDto conspirator);
    Conspirator DeleteConspirator(int id);
    
    Conspirator GetByName(string name);
}