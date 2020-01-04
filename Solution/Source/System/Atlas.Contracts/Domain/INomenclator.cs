namespace CompanyName.Atlas.Contracts.Domain
{
    /// <summary>
    /// This is the contract implemented by the domain entities representing nomenclators (or entities being described
    /// by a name and a description.
    /// </summary>
    public interface INomenclator : IEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description { get; set; }
    }
}
