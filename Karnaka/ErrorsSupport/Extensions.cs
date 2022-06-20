using Karnaka.Data;

namespace Karnaka.ErrorsSupport;

public static class Extensions
{
    public static ErrorObject ToErrorObject(this Exception exception, KarnakaContext context)
    {
        return new ErrorObject(exception, context);
    }
}