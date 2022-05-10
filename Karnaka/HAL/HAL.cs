﻿using System.ComponentModel;
using System.Dynamic;
using Karnaka.Data.Models;

namespace Karnaka.HAL;

public static class HAL
{
    public static dynamic PaginateAsDynamic(string baseUrl, int index, int count, int total)
    {
        dynamic links = new ExpandoObject();
        links.self = new { href = "/api/conspirators" };
        if (index < total) {
            links.next = new { href = $"/api/conspirators?index={index + count}" };
            links.final = new { href = $"{baseUrl}?index={total - (total % count)}&count={count}" };
        }
        if (index > 0) {
            links.prev = new { href = $"/api/conspirators?index={index - count}" };
            links.first = new { href = $"/api/conspirators?index=0" };
        }
        return links;
    }
    
    public static dynamic ToResource(this Conspirator conspirator) {
        var resource =  conspirator.ToDynamic();
        resource._links = new {
            self = new {
                href = $"/api/conspirators/{conspirator.Id}"
            },
            location = new {
                href = $"/api/locations/{conspirator.Location?.Id}"
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