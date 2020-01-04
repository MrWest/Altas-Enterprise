using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    ///     Contract of the domain entity: "Wage Scale" in abbreviation. Used to describe investment elements.
    /// </summary>
    public interface IWageScale : IEntity
    {
        /// <summary>
        ///     Gets or sets the name of the <see cref="IWageScale" />.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the retribution of the current <see cref="IWageScale" />.
        /// </summary>
        decimal Retribution { get; set; }
    }
}