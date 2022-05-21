using AutoMapper;
using Karnaka.Data;
using Karnaka.Data.Models;
using Karnaka.Services;
using Karnaka.Services.Dto;
using Microsoft.EntityFrameworkCore;

namespace Karnaka.GraphQL.ServicesGraphQL;

public class ConspiratorServiceGraphQL : IConspiratorServiceGraphQL
{
    private readonly KarnakaContext _context;
    private readonly IMapper _mapper;

    public ConspiratorServiceGraphQL(IMapper mapper, KarnakaContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public Conspirator GetConspirator(int id)
    {
        return _context.Conspirators.Include(e => e.Location)
            .Include(e => e.Location!.Conspirators)
            .Include(e => e.PartPlan).SingleOrDefault(e => e.Id == id)!;
    }

    public IQueryable<Conspirator> GetAllConspirators()
    {
        return _context.Conspirators.Include(e => e.Location)
            .Include(e => e.Location!.Conspirators)
            .Include(e => e.PartPlan).Select(e => e);
    }

    public Conspirator UpdateConspirator(Conspirator conspirator, int id)
    {
        var personForUpdate = _context.Conspirators.FirstOrDefault(e => e.Id == id);
        if (personForUpdate != default)
        {
            personForUpdate = conspirator;
            _context.Conspirators.Update(personForUpdate);
            _context.SaveChanges();
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(id), $"Заговорщика с id = {id} нет в базе");
        }

        return personForUpdate;
    }

    public Conspirator AddConspirator(ConspiratorDto conspirator)
    {
        var person = _mapper.Map<Conspirator>(conspirator);
        if (!string.IsNullOrEmpty(GetFullNameLocation(person.Location)) &&
            _context.Locations.FirstOrDefault(e =>
                e.Name.Equals(person.Location.Name) &&
                e.City.Equals(person.Location.City) &&
                e.Island.Equals(person.Location.Island)) != default)
        {
            person.Location = _context.Locations.FirstOrDefault(e =>
                e.Name.Equals(person.Location.Name) &&
                e.City.Equals(person.Location.City) &&
                e.Island.Equals(person.Location.Island));
        } 
        else if (!string.IsNullOrEmpty(GetFullNameLocation(person.Location)))
        {
            person.Location = new Location(GetFullNameLocation(person.Location));
        }

        _context.Conspirators.Add(person);
        _context.SaveChanges();

        return person;
    }

    private string? GetFullNameLocation(Location personLocation)
    {
        if (personLocation != default)
        {
            return $"{personLocation.Island}, {personLocation.City}, {personLocation.Name}";
        }

        return "";
    }

    public Conspirator DeleteConspirator(int id)
    {
        throw new NotImplementedException();
    }

    public Conspirator GetByName(string name)
    {
        return _context.Conspirators.Include(e => e.Location)
            .Include(e => e.Location)
            .Include(e => e.PartPlan)
            .SingleOrDefault(e => e.Name.Equals(name))!;
    }
}