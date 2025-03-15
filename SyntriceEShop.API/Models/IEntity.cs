namespace SyntriceEShop.Common.Models;

/// <summary>
/// Interface for all entities. Uses integer based Id for simplicity over GUID.
/// </summary>
public interface IEntity 
{ 
    int Id { get; set; }
}