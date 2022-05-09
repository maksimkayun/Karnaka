using System.Reflection;
using AutoMapper;
using Karnaka.Data;
using Karnaka.Data.Models;

namespace Karnaka.Services.Dto.AutoMapperProfiles;

public class LocationDtoProfiler : Profile
{
    public LocationDtoProfiler()
    {
        CreateMap<Location, LocationDto>().ForPath(e => e.Encountereds,
                i =>
                    i.MapFrom(j => j.Conspirators.Select(e => e.Name)))
            .ForPath(e => e.Name, i
                => i.MapFrom(j => $"{j.Island}, {j.City}, {j.Name}"))
            .ReverseMap()
            .ForPath(e => e.Conspirators,
                i => i.MapFrom(j => GetConspirators(j.Encountereds)))
            .ForPath(e => e.Island, i
                => i.MapFrom(j => GetString(j, 0)))
            .ForPath(e => e.City, i
                => i.MapFrom(j => GetString(j, 1)))
            .ForPath(e => e.Name, i
                => i.MapFrom(j => GetString(j, 2)));
    }

    private ICollection<Conspirator> GetConspirators(ICollection<string> names)
    {
        List<Conspirator> conspirators = new List<Conspirator>();
        (names as List<string>)!.ForEach(e =>
        {
            conspirators.Add(new Conspirator()
            {
                Name = e
            });
            
        });
        return conspirators;
    }
    
    private string GetString(LocationDto s, int i)
    {
        return s.Name.Split(", ")[i];
    }
}