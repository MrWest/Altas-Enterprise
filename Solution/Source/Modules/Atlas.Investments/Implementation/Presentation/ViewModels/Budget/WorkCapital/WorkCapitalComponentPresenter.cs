using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWorkCapitalComponentPresenter" />, representing a presenter view model
    ///     used to decorate and impersonate instances <see cref="IWorkCapitalComponent" /> in the UI.
    /// </summary>
    public class WorkCapitalComponentPresenter :
        BudgetComponentPresenterBase<
            IWorkCapitalComponent,
            IWorkCapitalPlannedSubSpecialityHolderViewModel,
             IWorkCapitalPlannedSubSpecialityHolderPresenter,
              IWorkCapitalExecutedSubSpecialityHolderViewModel,
             IWorkCapitalExecutedSubSpecialityHolderPresenter>,
        IWorkCapitalComponentPresenter
    {
        private IWorkCapitalCashFlowPresenter _worlCapitalCashFlow;
        private IWorkCapitalCashFlowPresenter _executedworlCapitalCashFlow;
        /// <summary>
        /// Cash flow for work capital
        /// </summary>
       public IWorkCapitalCashFlowPresenter PlannedWorkCapitalCashFlow { 
           get
           {
               return _worlCapitalCashFlow ;
           }
           set
           {
               _worlCapitalCashFlow = value;
           }
       }
       /// <summary>
       ///  executed Cash flow for work capital
       /// </summary>
       public IWorkCapitalCashFlowPresenter ExecutedWorkCapitalCashFlow
       {
           get
           {
               return _executedworlCapitalCashFlow;
           }
           set
           {
               _executedworlCapitalCashFlow = value;
           }
       }

        public override decimal PlannedCost
        {
            get
            {

                return PlannedWorkCapitalCashFlow.WorkCapital;
            }
        }

        public override decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period)
        {

            return PlannedCost;
        }
    }
}