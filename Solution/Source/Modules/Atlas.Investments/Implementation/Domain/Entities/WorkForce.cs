using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWorkForce" /> for the domain entity: "Work Force".
    /// </summary>
    public class WorkForce : EntityBase, IWorkForce
    {
        public WorkForce()
        {
            WageScale = new WageScale();
        }
        /// <summary>
        ///     Gets or sets the code of the <see cref="WorkForce" />.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Gets or sets the name of the <see cref="WorkForce" />.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the wage scale of the <see cref="WorkForce" />.
        /// </summary>
        public object WageScale { get; set; }


        /// <summary>
        ///     Returns a string that represents the current <see cref="WorkForce" />.
        /// </summary>
        /// <returns>
        ///     A string that represents the current <see cref="WorkForce" />.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}