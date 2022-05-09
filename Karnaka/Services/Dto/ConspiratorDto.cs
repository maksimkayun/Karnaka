using Karnaka.Data.Models;

namespace Karnaka.Services.Dto;

public class ConspiratorDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string? Location { get; set; }
    public string PartPlan { get; set; }
}