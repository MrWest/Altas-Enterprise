using System.Collections;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
   

    public interface ISubExpenseConcept : ICodedNomenclator, IOwnable
    {
        IExpenseConcept ExpenseConcept { get; set; }
        string ExpenseConceptId { get; set; }
    }
}