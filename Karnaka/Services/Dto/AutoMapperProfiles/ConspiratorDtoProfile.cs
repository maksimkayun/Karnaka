using AutoMapper;
using Karnaka.Data.Models;

namespace Karnaka.Services.Dto.AutoMapperProfiles;

public class ConspiratorDtoProfile : Profile
{
    public ConspiratorDtoProfile()
    {
        CreateMap<Conspirator, ConspiratorDto>().ForPath(
                e => e.Location,
                i =>
                    i.MapFrom(j => j.Location.Name))
            .ForPath(e => e.PartPlan,
                i
                    => i.MapFrom(j => j.PartPlan.Description)).ReverseMap()
            .ForPath(e => e.Location.Name,
                i => i.MapFrom(j => j.Location))
            .ForPath(e => e.PartPlan.Description, i
                => i.MapFrom(j => j.PartPlan));

    }
}