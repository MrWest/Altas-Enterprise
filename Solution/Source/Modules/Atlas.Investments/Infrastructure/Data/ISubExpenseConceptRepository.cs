using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    public interface ISubExpenseConceptRepository : IRepository<ISubExpenseConcept>
    {
        IExpenseConcept ExpenseConcept { get; set; }
    }
}
