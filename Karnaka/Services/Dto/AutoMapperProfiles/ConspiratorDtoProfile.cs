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
                    i.MapFrom(j => $"{j.Location.Island}, {j.Location.City}, {j.Location.Name}"))
            .ForPath(e => e.PartPlan,
                i
                    => i.MapFrom(j => j.PartPlan.Description)).ReverseMap()
            .ForPath(e => e.Location.Island,
                i => i.MapFrom(j =>  GetString(j, 0)))
            .ForPath(e => e.Location.City,
                i => i.MapFrom(j => GetString(j, 1)))
            .ForPath(e => e.Location.Name,
                i => i.MapFrom(j => GetString(j, 2)))
            .ForPath(e => e.PartPlan.Description, i
                => i.MapFrom(j => j.PartPlan));

    }
    
    private string GetString(ConspiratorDto s, int i)
    {
        return s.Location.Split(", ")[i];
    }
}