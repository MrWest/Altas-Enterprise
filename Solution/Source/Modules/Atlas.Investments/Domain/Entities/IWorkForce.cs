using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    ///     Contract of the domain entity: "Work Force" in abbreviation. Used to describe investment elements.
    /// </summary>
    public interface IWorkForce : IEntity
    {
        /// <summary>
        ///     Gets or sets the code of the <see cref="IWorkForce" />.
        /// </summary>
        string Code { get; set; }

        /// <summary>
        ///     Gets or sets the name of the <see cref="IWorkForce" />.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the wage scale of the <see cref="IWorkForce" />.
        /// </summary>
        object WageScale { get; set; }
    }
}