using System.ComponentModel;
using System.Dynamic;
using System.Text;
using Karnaka.Data.Models;
using Karnaka.Services.Dto;

namespace Karnaka.HAL;

public static class HAL
{
    public static dynamic PaginateAsDynamic(string baseUrl, int index, int count, int total)
    {
        dynamic links = new ExpandoObject();
        links.self = new { href = $"{baseUrl}" };
        if (index < total) {
            links.next = new { href = $"{baseUrl}?index={index + count}" };
            links.final = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
        }
        if (index > 0) {
            links.prev = new { href = $"{baseUrl}?index={index - count}" };
            links.first = new { href = $"{baseUrl}?index=0" };
        }
        return links;
    }
    
    public static Dictionary<string, string> PaginateAsDynamicQL(string baseUrl, int index, int count, int total)
    {
        Dictionary<string, string> links = new Dictionary<string, string>();
        links.Add("self", $"{baseUrl}");
        
        //links.self = new { href = $"{baseUrl}" };
        if (index < total) {
            links.Add("next", $"{baseUrl}?index={index + count}");
            links.Add("final", $"{baseUrl}?index={total - (total % count)}&count={count}");

            //links.next = new { href = $"{baseUrl}?index={index + count}" };
            //links.final = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
        }
        if (index > 0) {
            links.Add("prev", $"{baseUrl}?index={index - count}");
            links.Add("first", $"{baseUrl}?index=0");
            //links.prev = new { href = $"{baseUrl}?index={index - count}" };
            //links.first = new { href = $"{baseUrl}?index=0" };
        }
        return links;
    }

    public static dynamic ToResource(this ConspiratorDto conspirator, int idLocation, int idPlan = -1) {
        var resource =  conspirator.ToDynamic();
        string location = idLocation != -1 ? idLocation.ToString() : "";
        string plan = idPlan != -1 ? idPlan.ToString() : "";
        resource._links = new {
            self = new {
                href = $"/hal/conspirators/{conspirator.Id}"
            },
            location = new {
                href = $"/api/locations/{location}"
            },
            partPlan = new
            {
                href = $"/hal/partsplan/{plan}"
            }
        };
        return resource;
    }
    
    public static dynamic ToResource(this PartPlanDto plan, int idPerson = -1) {
        var resource =  plan.ToDynamic();
        var person = idPerson != -1 ? idPerson.ToString() : "";
       
        resource._links = new {
            self = new {
                href = $"/hal/partsplan/{plan.Id}"
            },
            conspirator = new {
                href = $"/hal/conspirators/{person}"
            }
        };
        return resource;
    }
    
    public static dynamic ToDynamic(this object value) {
        IDictionary<string, object> result = new ExpandoObject();
        var properties = TypeDescriptor.GetProperties(value.GetType());
        foreach (PropertyDescriptor prop in properties) {
            if (Ignore(prop)) continue;
            result.Add(prop.Name, prop.GetValue(value));
        }
        return result;
    }
    
    private static bool Ignore(PropertyDescriptor prop) {
        return prop.Attributes.OfType<System.Text.Json.Serialization.JsonIgnoreAttribute>().Any();
    }
}