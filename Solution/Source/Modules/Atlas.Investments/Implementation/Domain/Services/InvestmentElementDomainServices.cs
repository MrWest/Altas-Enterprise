using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    /// Default implementation of the contract <see cref="IInvestmentElementDomainServices"/>, representing the contract
    /// for the domain services provider assisting the assurance of the business rules in the investment elements the
    /// system handles.
    /// </summary>
    public class InvestmentElementDomainServices : DomainServicesBase<IInvestmentElement>, IInvestmentElementDomainServices
    {
        /// <summary>
        /// Gets or sets the <see cref="IInvestmentElement"/> being the parent of those handled in the current
        /// <see cref="InvestmentElementDomainServices"/>.
        /// </summary>
        public IInvestmentElement Parent { get; set; }


        /// <summary>
        /// Creates a new instance of an investment element.
        /// </summary>
        /// <returns>A new <see cref="IInvestmentElement"/>.</returns>
        public override IInvestmentElement Create()
        {
            IInvestmentElement element = base.Create();
            element.Name = Resources.NewInvestmentElementName;
            element.Description = Resources.NewInvestmentElementDescription;

            var equipmentComponent = ServiceLocator.Current.GetInstance<IEquipmentComponent>();
            var constructionComponent = ServiceLocator.Current.GetInstance<IConstructionComponent>();
            var otherExpensesComponent = ServiceLocator.Current.GetInstance<IOtherExpensesComponent>();
            var workCapitalComponent = ServiceLocator.Current.GetInstance<IWorkCapitalComponent>();

            var budget = ServiceLocator.Current.GetInstance<IBudget>();
            budget.EquipmentComponent = equipmentComponent;
            budget.ConstructionComponent = constructionComponent;
            budget.OtherExpensesComponent = otherExpensesComponent;
            budget.WorkCapitalComponent = workCapitalComponent;
            budget.InvestmentElement = element;

            equipmentComponent.Budget = constructionComponent.Budget = otherExpensesComponent.Budget = workCapitalComponent.Budget = budget;

            element.Budget = budget;

            var period = ServiceLocator.Current.GetInstance<IInvestmentElementPeriod>();
            period.InvestmentElement = element;

            element.Period = period;

            return element;
        }
    }
}
