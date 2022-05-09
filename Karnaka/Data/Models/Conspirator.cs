using System.ComponentModel.DataAnnotations;

namespace Karnaka.Data.Models;

public class Conspirator
{
    public Conspirator()
    {
    }

    public Conspirator(string name, string street, string partPlan)
    {
        Name = name;
        Location = new Location()
        {
            Name = street
        };
        PartPlan = new PartPlan()
        {
            Description = partPlan
        };
    }

    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public Location Location { get; set; }
    public PartPlan PartPlan { get; set; }
}