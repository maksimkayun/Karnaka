namespace Karnaka.Data.Models;

public class ErrorCode
{
    public int? Id { get; set; }
    
    public string? ExternalErrorMessage { get; set; }
    public string? ExternalErrorCode { get; set; }
        
    public string? InternalErrorMessage { get; set; }
    public string? InternalErrorCode { get; set; }
}