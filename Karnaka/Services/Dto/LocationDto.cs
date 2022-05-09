namespace Karnaka.Services.Dto;

public class LocationDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public ICollection<string>? Encountereds { get; set; }
}