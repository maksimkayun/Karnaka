using GraphQL.Types;
using Karnaka.Data.Models;
using System.Collections.Generic;

namespace Karnaka.GraphQL.GraphTypes;

public class LocationGraphType: ObjectGraphType<Location>
{
    public LocationGraphType()
    {
        Name = "location";
        Field(e => e.Id, true).Description("ID локации");
        Field(e => e.Island, false).Description("Остров");
        Field(e => e.City, false).Description("Город на острове");
        Field(e => e.Name, false).Description("Имя локации");
        Field(e => e.Conspirators, true, typeof(ListGraphType<ConspiratorGraphType>))
            .Description("Заговорщики, встречающиеся в этой локации");
    }
}