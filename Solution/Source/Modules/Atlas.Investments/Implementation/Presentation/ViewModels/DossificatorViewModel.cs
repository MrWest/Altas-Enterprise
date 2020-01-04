using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    /// Implements CRUD operation over the Dossificator
    /// </summary>
//    public class DossificatorViewModel :  InvestmentElementViewModelBase<IDossificator, IDossificatorPresenter, IDossificatorApplicationService>, IDossificatorViewModel
//    {
//        /// <summary>
//        ///     Creates a new presenter view model decorating the given investment element.
//        /// </summary>
//        /// <param name="dossificator">The <see cref="IInvestmentElement" /> to wrap into an presenter view model.</param>
//        /// <returns>
//        ///     A new instance of <see cref="IInvestmentElementPresenter" /> decorating
//        ///     <paramref name="dossificator" />.
//        /// </returns>
//        private IDossificator Prepare(IDossificator dossificator)
//        {
//            if (Equals(dossificator, null))
//                throw new ArgumentNullException("dossificator");



//            var budget = ServiceLocator.Current.GetInstance<IBudget>();

//            budget.InvestmentElement = dossificator;
//            dossificator.Budget = budget;

//            var equipment = ServiceLocator.Current.GetInstance<IEquipmentComponent>();

//            equipment.Budget = budget;
//            budget.EquipmentComponent = equipment;

//            var construction = ServiceLocator.Current.GetInstance<IConstructionComponent>();

//            construction.Budget = budget;
//            budget.ConstructionComponent = construction;

//            var otherExpenses = ServiceLocator.Current.GetInstance<IOtherExpensesComponent>();

//            otherExpenses.Budget = budget;
//            budget.OtherExpensesComponent = otherExpenses;

//            var workCapital = ServiceLocator.Current.GetInstance<IWorkCapitalComponent>();

//            workCapital.Budget = budget;
//            budget.WorkCapitalComponent = workCapital;



//            return dossificator;
//        }
//        public void Load()
//        {
//            base.Load();

//          // Delete(Dossificator);
//            if (Items.Count==0)
//            {

//                var dosificator = ServiceLocator.Current.GetInstance<IDossificator>();

//                Add(CreatePresenterFor(Prepare(dosificator)));
//                base.Load();
//            }
//        }

//        public IDossificatorPresenter Dossificator
//        {
//            get { return Items[0]; }
//        }
//    }
//}
}