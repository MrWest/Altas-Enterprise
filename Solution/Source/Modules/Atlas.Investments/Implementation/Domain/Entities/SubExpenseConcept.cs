using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class SubExpenseConcept : CodedNomenclatorBase, ISubExpenseConcept
    {
        public IEntity OwnerEntity { get; set; }
        [ForeignKey("ExpenseConceptId")]
        public IExpenseConcept ExpenseConcept { get; set; }
        public string OwnerEntityId { get; set; }
        public string ExpenseConceptId { get; set; }
    }
}
