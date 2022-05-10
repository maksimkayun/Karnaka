﻿using System.ComponentModel;
using System.Dynamic;
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
    
    public static dynamic ToResource(this ConspiratorDto conspirator, int idLocation) {
        var resource =  conspirator.ToDynamic();
        string location = idLocation != -1 ? idLocation.ToString() : "";
        resource._links = new {
            self = new {
                href = $"/hal/conspirators/{conspirator.Id}"
            },
            location = new {
                href = $"/hal/conspirators/{location}"
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