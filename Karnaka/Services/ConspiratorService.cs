using AutoMapper;
using EasyNetQ.Internals;
using Karnaka.Data;
using Karnaka.Data.Helpers;
using Karnaka.Data.Models;
using Karnaka.ErrorsSupport;
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
    
    [Obsolete]
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
            .Include(e=>e.PartPlan)
            .Select(e => e).Skip(index).Take(count);
        
        Dictionary<int, int> plans = persons.Where(e => e.PartPlan != default).Select(e => e)
            .ToDictionary(key => key.Id, value => value.PartPlan.Id);

        var dictLocations = persons.Where(e => e.Location != default).Select(e => e)
            .ToDictionary(key => key.Id, value => value.Location.Id);

        var items = _mapper.Map<IEnumerable<ConspiratorDto>>(
            persons.Include(e=>e.PartPlan)
            .Select(v => v)).Select(v => GetResource(v, dictLocations, plans));
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

    public IEnumerable<dynamic> GetConspiratorHal(int id)
    {
        try
        {
            var person = _context.Conspirators
                .Include(e=>e.Location)
                .Include(e=>e.PartPlan)
                .SingleOrDefault(e => e.Id == id);
            if (person == default)
            {
                throw new NullReferenceException("Запрашиваемый объект в базе не найден");
            }

            var location = _context.Locations.Include(e=>e.Conspirators)
                .Where(e=>e.Conspirators != default).SingleOrDefault(e => e.Conspirators.Contains(person));

            Dictionary<int, int> dictLocations = new Dictionary<int, int>();
            if (location != default)
            {
                dictLocations.Add(person.Id, location.Id);
            }

            Dictionary<int, int> dictPartsPlan = new Dictionary<int, int>();
            if (person.PartPlan != default)
            {
                dictPartsPlan.Add(person.Id, person.PartPlan.Id);
            }
            var item = GetResource(_mapper.Map<ConspiratorDto>(person), dictLocations, dictPartsPlan); 
            IEnumerable<dynamic> result = new[]
            {
                id,
                item
            };
            return result;
        }
        catch (Exception e)
        {
            var error = e.ToErrorObject(_context);
            return new[]
            {
                error.ErrorCode,
                error.ErrorMessage,
                error.ErrorCodeDb
            };
        }
    }

    private dynamic GetResource(ConspiratorDto conspiratorDto, Dictionary<int, int> locations, Dictionary<int, int> partsPlan)
    {
        var idplan = -1;
        if (conspiratorDto.PartPlan != default)
        {
            idplan = partsPlan.First(e => e.Key.Equals(conspiratorDto.Id)).Value;
        }
        if (conspiratorDto.Location != default)
        {
            try
            {
                var idlocation = locations.First(e => e.Key == conspiratorDto.Id).Value;
                
                if (conspiratorDto.PartPlan != default)
                {
                    idplan = partsPlan.First(e => e.Key.Equals(conspiratorDto.Id)).Value;
                }
                return conspiratorDto.ToResource(idlocation, idplan);
            }
            catch (Exception e)
            {
            }
        }
        return conspiratorDto.ToResource(-1, idplan);
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