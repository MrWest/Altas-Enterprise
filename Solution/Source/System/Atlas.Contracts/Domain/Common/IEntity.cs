using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    /// <summary>
    /// Represents the contract to be implemented by an domain entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the current entity.
        /// </summary>
        [Index (IsUnique = true)]
        string Id { get; set; }

        /// <summary>
        /// Gets the ToString method's value.
        /// </summary>
        string FullName { get; }
    }
}
