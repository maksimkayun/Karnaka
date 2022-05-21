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

    public ICollection<PartPlanDto> GetAllPartsPlan()
    {
        return _mapper.Map<ICollection<PartPlanDto>>(_context.PartPlans.Select(e => e));
    }

    public ICollection<PartPlanDto> GetAllPartsPlan(int index, int count)
    {
        throw new NotImplementedException();
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