using Karnaka.Services.Dto;

namespace Karnaka.Services;

public interface IConspiratorService
{
    [Obsolete]
    ConspiratorDto GetConspirator(int id);
    ICollection<ConspiratorDto> GetAllConspirators();
    IEnumerable<dynamic> GetAllConspirators(int index, int count);
    ConspiratorDto UpdateConspirator(ConspiratorDto conspirator, int id);
    ConspiratorDto AddConspirator(ConspiratorDto conspirator);
    ConspiratorDto DeleteConspirator(int id);
    IEnumerable<dynamic> GetConspiratorHal(int id);
}