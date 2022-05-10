using GraphQL.Types;
using Karnaka.Data.Models;

namespace Karnaka.GraphQL.GraphTypes;

public class PartPlanGraphType : ObjectGraphType<PartPlan>
{
    public PartPlanGraphType()
    {
        Name = "partPlan";
        Field(e => e.Id, true).Description("ID плана");
        Field(e => e.Description, true).Description("Описание плана");
    }
}