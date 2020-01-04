using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    /// <summary>
    /// Represents the base class of the domain entities being nomenclators.
    /// </summary>
    public abstract class NomenclatorBase : EntityBase, INomenclator
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public virtual string Description { get; set; }


        /// <summary>
        /// Gets the string representation of the current nomenclator.
        /// </summary>
        /// <returns>A string being the representation in such format of the current nomenclator.</returns>
        public override string ToString()
        {
            return Name??"";
        }
    }
}
