using AutoMapper;
using Karnaka.Data;
using Karnaka.Data.Models;
using Karnaka.Services.Dto;
using Microsoft.EntityFrameworkCore;

namespace Karnaka.Services;

public class LocationService : ILocationService
{
    private readonly KarnakaContext _context;
    private readonly IMapper _mapper;

    public LocationService(KarnakaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public LocationDto GetLocation(int id)
    {
        return _mapper.Map<LocationDto>(_context.Locations.Include(e=>e.Conspirators).SingleOrDefault(e => e.Id == id));
    }

    public ICollection<LocationDto> GetAllLocations()
    {
        return _mapper.Map<ICollection<LocationDto>>(_context.Locations.Include(e=>e.Conspirators).Select(e => e));
    }

    public LocationDto UpdateLocation(LocationDto location, int id)
    {
        var parseLoc = _mapper.Map<Location>(location);
        List<string> names = parseLoc.Conspirators.Select(e => e.Name).ToList();
        var cons = _context.Conspirators.Where(e => names.Contains(e.Name)).Select(e => e).ToList();
        var newLoc = _context.Locations.Include(e=>e.Conspirators).SingleOrDefault(e => e.Id == id);

        foreach (var person in newLoc.Conspirators)
        {
            person.Location = null;
        }
        
        newLoc.Name = parseLoc.Name;
        newLoc.City = parseLoc.City;
        newLoc.Island = parseLoc.Island;
        
        foreach (var person in cons)
        {
            person.Location = newLoc;
        }
        
        _context.Locations.Update(newLoc);
        _context.SaveChanges();
        return _mapper.Map<LocationDto>(newLoc);
    }

    public LocationDto AddLocation(LocationDto location)
    {
        var newLoc = _mapper.Map<Location>(location);
        List<string> names = location.Encountereds!.ToList();
        newLoc.Conspirators = _context.Conspirators.Where(e => names.Contains(e.Name)).Select(e => e).ToList();
        newLoc = _context.Locations.Add(newLoc).Entity;
        _context.SaveChanges();
        return _mapper.Map<LocationDto>(newLoc);
    }

    public LocationDto DeleteLocation(int id)
    {
        throw new NotImplementedException();
    }
}