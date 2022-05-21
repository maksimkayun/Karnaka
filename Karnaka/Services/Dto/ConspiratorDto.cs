using Newtonsoft.Json;

namespace Karnaka.Services.Dto;

public class ConspiratorDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnoreAttribute]
    public string? Location { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnoreAttribute]
    public string? PartPlan { get; set; }
}