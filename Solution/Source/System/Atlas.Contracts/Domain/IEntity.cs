namespace CompanyName.Atlas.Contracts.Domain
{
    /// <summary>
    /// Represents the contract to be implemented by an domain entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the current entity.
        /// </summary>
        object Id { get; set; }

        /// <summary>
        /// Gets the ToString method's value.
        /// </summary>
        string FullName { get; }
    }
}
