namespace SyntriceEShop.API.Models;

/// <summary>
/// Interface for entities with a primary key.
/// </summary>
/// <typeparam name="T">The type of the primary key (e.g. int, Guid).</typeparam>
public interface IEntity<T>
{ 
    T Id { get; set; }
}