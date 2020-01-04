using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWageScale" /> for the domain entity: "Wage Scale".
    /// </summary>
    public class WageScale : EntityBase, IWageScale
    {
        /// <summary>
        ///     Gets or sets the name of the current <see cref="IWageScale" />.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the retribution of the current <see cref="IWageScale" />.
        /// </summary>
        public decimal Retribution { get; set; }


        /// <summary>
        ///     Returns a string that represents the current <see cref="IWageScale" />.
        /// </summary>
        /// <returns>
        ///     A string that represents the current <see cref="IWageScale" />.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}