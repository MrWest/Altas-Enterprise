using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    ///     Implementation of the contract <see cref="IExpenseConcept" /> for the domain entity: "ExpenseConcept".
    /// </summary>
    public class ExpenseConcept : CodedNomenclatorBase, IExpenseConcept
    {
        ///// <summary>
        /////     Gets or sets the code of the current <see cref="IExpenseConcept" />.
        ///// </summary>
        //public string Code { get; set; }

        ///// <summary>
        /////     Gets or sets the name of the current <see cref="IExpenseConcept" />.
        ///// </summary>
        //public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the type of the current <see cref="IExpenseConcept" />.
        /// </summary>
        public ExpenseConceptType Type { get; set; }

        private IList<ISubExpenseConcept> _subExpenseConcept;
        public IList<ISubExpenseConcept> SubExpenseConcept {
            get
            {
                return _subExpenseConcept ?? new List<ISubExpenseConcept>();
            } 
        }


        /// <summary>
        ///     Returns a string that represents the current <see cref="IExpenseConcept" />.
        /// </summary>
        /// <returns>
        ///     A string that represents the current <see cref="IExpenseConcept" />.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

      //  public IEntity OwnerEntity { get; set; }
    }
}