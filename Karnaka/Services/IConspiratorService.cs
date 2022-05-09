using Karnaka.Services.Dto;

namespace Karnaka.Services;

public interface IConspiratorService
{
    ConspiratorDto GetConspirator(int id);
    ICollection<ConspiratorDto> GetAllConspirators();
    ConspiratorDto UpdateConspirator(ConspiratorDto conspirator, int id);
    ConspiratorDto AddConspirator(ConspiratorDto conspirator);
    ConspiratorDto DeleteConspirator(int id);
}