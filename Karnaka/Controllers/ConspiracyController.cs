using AutoMapper;
using Karnaka.Services;
using Karnaka.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Karnaka.Controllers;

[Route("api/conspirators")]
[ApiController]
public class ConspiracyController : ControllerBase
{
    private readonly ILogger<ConspiracyController> _logger;
    private readonly IConspiratorService _service;
    private readonly IMapper _mapper;
    private const int PAGE_SIZE = 3;

    public ConspiracyController(ILogger<ConspiracyController> logger, IConspiratorService service, IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ConspiratorDto>))]
    [ProducesResponseType(404)]
    public ActionResult<IEnumerable<ConspiratorDto>> GetConspirators()
    {
        return Ok(_service.GetAllConspirators());
    }

    [HttpGet("/hal/conspirators")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ConspiratorDto>))]
    [ProducesResponseType(404)]
    [Produces("application/hal+json")]
    public IActionResult Get(int index = 0, int count = PAGE_SIZE)
    {
        return Ok(_service.GetAllConspirators(index, count));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(ConspiratorDto))]
    [ProducesResponseType(404)]
    public ActionResult<ConspiratorDto> GetConspirator(int id)
    {
        return Ok(_service.GetConspirator(id));
    }

    [HttpPost]
    public ActionResult<ConspiratorDto> PostConspirator(ConspiratorDto conspirator)
    {
        var cons = _service.AddConspirator(conspirator);
        return CreatedAtAction("GetConspirator", new { id = cons.Id }, cons);
    }

    [HttpDelete("{id}")]
    public ActionResult<ConspiratorDto> DeleteConspirator(int id)
    {
        var cons = _mapper.Map<ConspiratorDto>(_service.DeleteConspirator(id));
        return Ok(cons);
    }
}