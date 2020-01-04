using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    /// Default implementation of the contract <see cref="IInvestmentComponentDomainServices"/>, representing the contract
    /// for the domain services provider assisting the assurance of the business rules in the investment elements the
    /// system handles.
    /// </summary>
    /// <typeparam name="T">The type of the investment elements which business rules are handled here.</typeparam>
    public abstract class InvestmentElementDomainServicesBase<T> : CodedNomenclatorDomainServicesBase<T>
        where T : class, IInvestmentElement
    {
        /// <summary>
        /// Creates a new instance of an investment element.
        /// </summary>
        /// <returns>A new <see cref="IInvestmentElement"/>.</returns>
        public override T Create()
        {
            T element = base.Create();
            element.Name = Resources.NewInvestmentElementName;
            element.Description = Resources.NewInvestmentElementDescription;

            var equipmentComponent = ServiceLocator.Current.GetInstance<IEquipmentComponent>();
            var constructionComponent = ServiceLocator.Current.GetInstance<IConstructionComponent>();
            var otherExpensesComponent = ServiceLocator.Current.GetInstance<IOtherExpensesComponent>();
            var workCapitalComponent = ServiceLocator.Current.GetInstance<IWorkCapitalComponent>();
            var workCapitalCashFlow = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlow>();
            workCapitalCashFlow.WorkCapital = workCapitalComponent;
            workCapitalComponent.WorkCapitalCashFlow = workCapitalCashFlow;
            var workCapitalCashFlow2 = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlow>();
            workCapitalCashFlow2.WorkCapital = workCapitalComponent;
            workCapitalComponent.ExecutedWorkCapitalCashFlow = workCapitalCashFlow2;

            var budget = ServiceLocator.Current.GetInstance<IBudget>();
            budget.EquipmentComponent = equipmentComponent;
            budget.ConstructionComponent = constructionComponent;
            budget.OtherExpensesComponent = otherExpensesComponent;
            budget.WorkCapitalComponent = workCapitalComponent;
            budget.InvestmentElement = element;

            equipmentComponent.Budget = constructionComponent.Budget = otherExpensesComponent.Budget = workCapitalComponent.Budget = budget;

            element.Budget = budget;

            var period = ServiceLocator.Current.GetInstance<IPeriod>();
            period.Holder = element;
            element.Period = period;

            return element;
        }
    }
}
