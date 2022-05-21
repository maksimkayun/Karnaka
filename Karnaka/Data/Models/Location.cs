using System.ComponentModel.DataAnnotations;

namespace Karnaka.Data.Models;

public class Location
{
    public Location()
    {
        Conspirators = new HashSet<Conspirator>();
    }

    public Location(string? fullnamae)
    {
        Name = fullnamae.Split(", ")[2];
        City = fullnamae.Split(", ")[1];
        Island = fullnamae.Split(", ")[0];
    }

    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string? City { get; set; }
    public string? Island { get; set; }
    
    public ICollection<Conspirator>? Conspirators { get; set; }
}