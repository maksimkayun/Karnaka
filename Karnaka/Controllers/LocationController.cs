using AutoMapper;
using Karnaka.Services;
using Karnaka.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Karnaka.Controllers;

[Route(("api/locations"))]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationService _service;
    private readonly IMapper _mapper;

    public LocationController(ILocationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LocationDto>))]
    [ProducesResponseType(404)]
    public ActionResult<IEnumerable<LocationDto>> GetLocations()
    {
        return Ok(_service.GetAllLocations());
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(LocationDto))]
    [ProducesResponseType(404)]
    public ActionResult<LocationDto> GetLocation(int id)
    {
        return Ok(_service.GetLocation(id));
    }

    [HttpPost]
    public ActionResult<LocationDto> PostLocation(LocationDto location)
    {
        var loc = _service.AddLocation(location);
        return CreatedAtAction("GetLocation", new { id = loc.Id }, loc);
    }
    
    [HttpPut("{id}")]
    public ActionResult<LocationDto> UpdateLocation(LocationDto location, int id)
    {
        var loc = _service.UpdateLocation(location, id);
        return CreatedAtAction("GetLocation", new { id = loc.Id }, loc);
    }

    [HttpDelete("{id}")]
    public ActionResult<LocationDto> DeleteLocation(int id)
    {
        var loc = _mapper.Map<LocationDto>(_service.DeleteLocation(id));
        return Ok(loc);
    }
}