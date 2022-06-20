using Karnaka.Data;

namespace Karnaka.ErrorsSupport;

public class ErrorObject
{
    public string ErrorMessage { get; private set; }
    public string? ErrorData { get; private set; }
    public string ErrorCode { get; private set; }
    public string ErrorCodeDb { get; private set; }
    public ErrorObject(Exception e, KarnakaContext context)
    {
        ErrorMessage = e.Message;
        ErrorData = e.StackTrace;
        BindViaDbAndMapError(context);
    }

    private void BindViaDbAndMapError(KarnakaContext context)
    {
        var externalErrorCode = context.ErrorCodes.FirstOrDefault(e => e.InternalErrorMessage == ErrorMessage)
            ?.ExternalErrorCode;
        if (externalErrorCode != null)
            ErrorCode = externalErrorCode;
        
        var externalErrorMessage = context.ErrorCodes.FirstOrDefault(e => e.InternalErrorMessage == ErrorMessage)
            ?.ExternalErrorMessage;
        if (externalErrorMessage != null)
            ErrorCode = externalErrorMessage;

        ErrorCodeDb = context.ErrorCodes.FirstOrDefault(e => e.InternalErrorMessage == ErrorMessage)
            ?.Id.ToString()!;
    }
}