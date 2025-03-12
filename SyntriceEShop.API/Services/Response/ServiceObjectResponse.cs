namespace SyntriceEShop.API.Services.Response;

public class ServiceObjectResponse<T> : ServiceResponse
{
    public T? Value { get; set; } = default(T);
}