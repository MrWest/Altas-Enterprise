using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class SubExpenseConceptPresenter : CodedNomenclatorPresenterBase<ISubExpenseConcept,ISubExpenseConceptManagerApplicationServices>, ISubExpenseConceptPresenter
    {
        public SubExpenseConceptPresenter(ISubExpenseConcept nomenclator) : base(nomenclator)
        {
        }

        public IExpenseConceptPresenter ExpenseConcept { get; set; }
        protected override ISubExpenseConceptManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.ExpenseConcept = ExpenseConcept.Object;
            return service;
        }
    }
}
