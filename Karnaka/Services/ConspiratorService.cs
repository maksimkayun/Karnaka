using AutoMapper;
using Karnaka.Data;
using Karnaka.Data.Helpers;
using Karnaka.Data.Models;
using Karnaka.HAL;
using Karnaka.Services.Dto;
using Microsoft.EntityFrameworkCore;

namespace Karnaka.Services;

public class ConspiratorService : IConspiratorService
{
    private readonly KarnakaContext _context;
    private readonly IMapper _mapper;

    public ConspiratorService(KarnakaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ConspiratorDto GetConspirator(int id)
    {
        return _mapper.Map<ConspiratorDto>(_context.Conspirators.Include(e => e.Location)
            .Include(e => e.PartPlan).SingleOrDefault(e => e.Id == id));
    }

    public ICollection<ConspiratorDto> GetAllConspirators()
    {
        return _mapper.Map<ICollection<ConspiratorDto>>(_context.Conspirators.Include(e => e.Location)
            .Include(e => e.PartPlan).Select(e => e));
    }

    public IEnumerable<dynamic> GetAllConspirators(int index, int count)
    {
        var persons = _context.Conspirators
            .Include(e => e.Location)
            .Select(e => e).Skip(index).Take(count);
        var locations = persons
            .Where(e=>e.Location != default)
            .Select(v => v.Location);

        var dictLocations = 
            locations.ToDictionary(key => key.Id, value=> GetFullNameLocation(value));

        var items = _mapper.Map<IEnumerable<ConspiratorDto>>(
            persons.Include(e=>e.PartPlan)
            .Select(v => v)).Select(v => GetResource(v, dictLocations));
        int total = _context.Conspirators.Count();
        var _links = HAL.HAL.PaginateAsDynamic("/hal/conspirators", index, count, total);
        IEnumerable<dynamic> result = new[]
        {
            _links,
            count,
            total,
            index,
            items
        };
        return result;
    }

    private dynamic GetResource(ConspiratorDto conspiratorDto, Dictionary<int, string> locations)
    {
        if (conspiratorDto.Location != default)
        {
            try
            {
                var id = locations.First(e => e.Value.Equals(conspiratorDto.Location)).Key;
                return conspiratorDto.ToResource(id);
            }
            catch (Exception e)
            {
            }
        }

        return conspiratorDto.ToResource(-1);
    }

    private string GetFullNameLocation(Location location)
    {
        return $"{location.Island}, {location.City}, {location.Name}";
    }
    
    public ConspiratorDto UpdateConspirator(ConspiratorDto conspirator, int id)
    {
        var upd = _context.Conspirators.SingleOrDefault(e => e.Id == id);
        upd = _mapper.Map<Conspirator>(conspirator);
        upd.Id = id;
        _context.Conspirators.Update(upd);
        _context.SaveChanges();
        return _mapper.Map<ConspiratorDto>(upd);
    }

    public ConspiratorDto AddConspirator(ConspiratorDto conspirator)
    {
        Conspirator cons = _mapper.Map<Conspirator>(conspirator);
        if (_context.Locations.SingleOrDefault(e =>
                e.Name.Equals(cons.Location.Name) &&
                e.Island.Equals(cons.Location.Island) &&
                e.City.Equals(cons.Location.City)) != default)
        {
            cons.Location = _context.Locations.SingleOrDefault(e =>
                e.Name.Equals(cons.Location.Name) &&
                e.Island.Equals(cons.Location.Island) &&
                e.City.Equals(cons.Location.City));

            cons = _context.Conspirators.Add(cons).Entity;
        }
        else
        {
            cons = _context.Conspirators.Add(_mapper.Map<Conspirator>(conspirator)).Entity;
        }

        _context.SaveChanges();
        return _mapper.Map<ConspiratorDto>(cons);
    }

    public ConspiratorDto DeleteConspirator(int id)
    {
        var cons = _context.Conspirators.SingleOrDefault(e => e.Id == id);
        _context.Conspirators.Remove(cons);
        _context.SaveChanges();
        return _mapper.Map<ConspiratorDto>(cons);
    }
}