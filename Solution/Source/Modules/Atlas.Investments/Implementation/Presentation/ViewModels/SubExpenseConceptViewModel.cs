using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class SubExpenseConceptViewModel : CrudViewModelBase<ISubExpenseConcept, ISubExpenseConceptPresenter, ISubExpenseConceptManagerApplicationServices>, ISubExpenseConceptViewModel
    {
        public IExpenseConceptPresenter ExpenseConcept { get; set; }
        protected override ISubExpenseConceptPresenter CreatePresenterFor(ISubExpenseConcept item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.ExpenseConcept = ExpenseConcept;
            return presenter;
        }
        protected override ISubExpenseConceptManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.ExpenseConcept = ExpenseConcept.Object;
            return service;
        }

        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
            OnPropertyChanged(() => Items);
        }
    }
}
