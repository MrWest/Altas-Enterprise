using System.Collections;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    ///     Contract of the domain entity: "Expense Concept" in abbreviation. Used to describe investment elements.
    /// </summary>
    public interface IExpenseConcept : ICodedNomenclator //, IOwnable
    {
        ///// <summary>
        /////     Gets or sets the code of the current <see cref="IExpenseConcept" />.
        ///// </summary>
        //string Code { get; set; }

        ///// <summary>
        /////     Gets or sets the name of the current <see cref="IExpenseConcept" />.
        ///// </summary>
        //string Name { get; set; }

        /// <summary>
        ///     Gets or sets the type of the current <see cref="IExpenseConcept" />.
        /// </summary>
        ExpenseConceptType Type { get; set; }

        IList<ISubExpenseConcept> SubExpenseConcept { get; }
    }
}