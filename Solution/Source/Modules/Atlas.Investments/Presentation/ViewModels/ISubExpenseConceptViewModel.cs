using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public  interface ISubExpenseConceptViewModel:ICrudViewModel<ISubExpenseConcept,ISubExpenseConceptPresenter>
    {
        IExpenseConceptPresenter ExpenseConcept { get; set; }
    }
}
