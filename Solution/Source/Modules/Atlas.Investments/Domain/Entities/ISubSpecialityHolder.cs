using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface ISubSpecialityHolder:ICodedNomenclator
    {
        IBudgetComponent BudgetComponent { get; set; }
        string BudgetComponentId { get; set; }
        string SubSpeciality { get; set; }

        /// <summary>
        /// Get or sets the Category id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        string SubExpenseConcept { get; set; }


    }
}