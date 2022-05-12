using GraphQL.Types;
using Karnaka.Data.Models;

namespace Karnaka.GraphQL.GraphTypes;

public class ConspiratorGraphType : ObjectGraphType<Conspirator>
{
    public ConspiratorGraphType()
    {
        Name = "conspirator";
        Field(c => c.Id, true).Description("ID заговорщика (генерится само)");
        Field(c => c.Name, false).Description("Имя участника заговора");
        Field(c => c.Location, true, type: typeof(LocationGraphType))
            .Description("Локация заговорщика (где его можно встретить");
        Field(c => c.PartPlan, false, type: typeof(PartPlanGraphType))
            .Description("Часть плана заговора, исполняемая этим заговорщиком");
        
    }
}