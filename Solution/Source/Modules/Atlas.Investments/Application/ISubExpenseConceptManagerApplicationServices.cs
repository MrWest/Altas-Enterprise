using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface ISubExpenseConceptManagerApplicationServices:IItemManagerApplicationServices<ISubExpenseConcept>,IExportable<ISubExpenseConcept>
    {
        IExpenseConcept ExpenseConcept { get; set; }
    }
}
