using System.ComponentModel.DataAnnotations;

namespace Karnaka.Data.Models;

public class PartPlan
{
    [Key]
    public int Id { get; set; }
    
    public string Description { get; set; }
}