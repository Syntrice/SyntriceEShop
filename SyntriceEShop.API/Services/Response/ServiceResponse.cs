namespace SyntriceEShop.API.Services.Response;

public class ServiceResponse
{
    public ServiceResponseType Type { get; set; } = ServiceResponseType.Failure;
    public string Message { get; set; } = String.Empty;
}