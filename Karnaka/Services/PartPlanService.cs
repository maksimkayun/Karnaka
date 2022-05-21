using AutoMapper;
using Karnaka.Data;
using Karnaka.Data.Models;
using Karnaka.HAL;
using Karnaka.Services.Dto;
using Microsoft.EntityFrameworkCore;

namespace Karnaka.Services;

public class PartPlanService : IPartPlanService
{
    private readonly KarnakaContext _context;
    private readonly IMapper _mapper;

    public PartPlanService(KarnakaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<dynamic> GetPartPlan(int id)
    {
        var plan = _mapper.Map<PartPlanDto>(_context.PartPlans.FirstOrDefault(e=>e.Id==id));
        var person = _context.Conspirators
            .Include(e => e.PartPlan)
            .Where(e => e.PartPlan != default)
            .FirstOrDefault(e => e.PartPlan.Id == id);
        var personId = -1;
        if (person != default)
        {
            personId = person.Id;
        }

        var item = plan.ToResource(personId);
        return new[]
        {
            id,
            item
        };
    }

    public ICollection<dynamic> GetAllPartsPlan(int index, int count)
    {
        Dictionary<int, int> dictPersons = _context.Conspirators
            .Include(e => e.PartPlan)
            .Where(e => e.PartPlan != default).Select(e => e)
            .ToDictionary(key => key.PartPlan.Id, value => value.Id);
        var items = _mapper.Map<ICollection<PartPlanDto>>(_context.PartPlans.Select(e => e).Skip(index).Take(count))
            .Select(e => GetRecourse(e, dictPersons));
        var total = _context.PartPlans.Count();
        var _links = HAL.HAL.PaginateAsDynamic("/hal/partsplan", index, count, total);
        return new[]
        {
            _links,
            count,
            total,
            index,
            items
        };
    }

    private dynamic GetRecourse(PartPlanDto plan, Dictionary<int, int> dictPersons)
    {

        var idPerson = -1;
        if (dictPersons.Keys.FirstOrDefault(e=>e == plan.Id) != default)
        {
            idPerson = dictPersons.FirstOrDefault(e => e.Key == plan.Id).Value;
        }
        try
        {
            return plan.ToResource(idPerson);
        }
        catch (Exception e)
        {
        }

        return null;
    }

    public PartPlanDto UpdatePartPlan(PartPlanDto plan, int id)
    {
        throw new NotImplementedException();
    }

    public PartPlanDto AddPartPlan(PartPlanDto plan)
    {
        throw new NotImplementedException();
    }

    public PartPlanDto DeletePartPlan(int id)
    {
        throw new NotImplementedException();
    }
}