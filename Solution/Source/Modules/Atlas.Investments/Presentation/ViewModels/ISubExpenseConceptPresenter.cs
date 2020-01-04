using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface ISubExpenseConceptPresenter:IPresenter<ISubExpenseConcept>
    {
        IExpenseConceptPresenter ExpenseConcept { get; set; }
        string Name { get; }
        string Code { get; }
    }
}
