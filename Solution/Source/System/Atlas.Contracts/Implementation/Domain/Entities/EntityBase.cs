using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    /// <summary>
    /// This is the base class of the domain entities in the system.
    /// </summary>
    public abstract class EntityBase : IEntity
    {
        #region IEntity Members

        /// <summary>
        /// Gets or sets the unique identifier of the current entity.
        /// </summary>
        [Index( IsUnique = true)]
        public string Id { get; set; }

        /// <summary>
        /// Gets the ToString method's value.
        /// </summary>
        public string FullName { get { return ToString(); } }

        #endregion
    }
}
