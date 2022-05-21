using AutoMapper;
using Karnaka.Data.Models;

namespace Karnaka.Services.Dto.AutoMapperProfiles;

public class PartPlanDtoProfile : Profile
{
    public PartPlanDtoProfile()
    {
        CreateMap<PartPlan, PartPlanDto>().ForMember(e => e.Id,
                i => i.MapFrom(j => j.Id))
            .ForMember(e => e.Description, 
                i => i.MapFrom(j => j.Description))
            .ReverseMap();
    }
}