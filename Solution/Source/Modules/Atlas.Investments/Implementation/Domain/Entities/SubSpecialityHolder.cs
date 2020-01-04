using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public abstract class SubSpecialityHolder: CodedNomenclatorBase, ISubSpecialityHolder
    {
        
        public string SubSpeciality { get; set; }
       
        public IBudgetComponent BudgetComponent { get; set; }
        public string Category { get; set; }
        public string SubExpenseConcept { get; set; }
        public string BudgetComponentId { get; set; }
    }
}