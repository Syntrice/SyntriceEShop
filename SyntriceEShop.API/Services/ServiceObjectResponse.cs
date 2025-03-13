namespace SyntriceEShop.API.Services;

public class ServiceObjectResponse<T> : ServiceResponse
{
    public T? Value { get; set; } = default(T);
}