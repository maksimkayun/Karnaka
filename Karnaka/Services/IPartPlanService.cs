using Karnaka.Services.Dto;

namespace Karnaka.Services;

public interface IPartPlanService
{
    IEnumerable<dynamic> GetPartPlan(int id);
    ICollection<PartPlanDto> GetAllPartsPlan();
    ICollection<PartPlanDto> GetAllPartsPlan(int index, int count);
    PartPlanDto UpdatePartPlan(PartPlanDto plan, int id);
    PartPlanDto AddPartPlan(PartPlanDto plan);
    PartPlanDto DeletePartPlan(int id);
}