
using Karnaka.Data;
using Karnaka.Data.Models;
using Karnaka.Services;
using Karnaka.Services.Dto;
using Microsoft.EntityFrameworkCore;

namespace Karnaka.GraphQL.ServicesGraphQL;

public class ConspiratorServiceGraphQL : IConspiratorServiceGraphQL
{
    private readonly KarnakaContext _context;

    public ConspiratorServiceGraphQL(KarnakaContext context)
    {
        _context = context;
    }

    public Conspirator GetConspirator(int id)
    {
        return _context.Conspirators.Include(e=>e.Location)
            .Include(e=>e.Location!.Conspirators)
            .Include(e=>e.PartPlan).SingleOrDefault(e => e.Id == id)!;
    }

    public List<Conspirator> GetAllConspirators()
    {
        return _context.Conspirators.Include(e=>e.Location)
            .Include(e=>e.Location!.Conspirators)
            .Include(e=>e.PartPlan).Select(e => e).ToList();
    }

    public Conspirator UpdateConspirator(Conspirator conspirator, int id)
    {
        throw new NotImplementedException();
    }

    public Conspirator AddConspirator(ConspiratorDto conspirator)
    {
        throw new NotImplementedException();
    }

    public Conspirator DeleteConspirator(int id)
    {
        throw new NotImplementedException();
    }
}