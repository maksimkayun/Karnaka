using Karnaka.Services.Dto;

namespace Karnaka.Services;

public interface ILocationService
{
    LocationDto GetLocation(int id);
    ICollection<LocationDto> GetAllLocations();
    LocationDto UpdateLocation(LocationDto location, int id);
    LocationDto AddLocation(LocationDto location);
    LocationDto DeleteLocation(int id);
}