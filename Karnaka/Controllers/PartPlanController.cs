using AutoMapper;
using Karnaka.Data.Models;
using Karnaka.Services;
using Karnaka.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Karnaka.Controllers;

[ApiController]
public class PartPlanController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPartPlanService _service;
    private const int PAGE_SIZE = 3;

    public PartPlanController(IMapper mapper, IPartPlanService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpGet("/hal/partsplan")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<dynamic>))]
    [ProducesResponseType(404)]
    [Produces("application/hal+json")]
    public IActionResult Get(int index = 0, int count = PAGE_SIZE)
    {
        return Ok(_service.GetAllPartsPlan(index, count));
    }

    [HttpGet("/hal/partsplan/{id}")]
    [ProducesResponseType(200, Type = typeof(PartPlanDto))]
    [ProducesResponseType(404)]
    public ActionResult<PartPlanDto> GetPartPlanHal(int id)
    {
        return Ok(_service.GetPartPlan(id));
    }
}