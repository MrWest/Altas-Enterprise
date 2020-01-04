using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentComponentViewModel" /> representing a crud view model handling
    ///     the crud operations in the presentation layer for investment component entities.
    /// </summary>
    public class InvestmentComponentViewModel<TInvestment> :
        InvestmentElementViewModelBase<IInvestmentComponent, IInvestmentComponentPresenter<TInvestment>, IInvestmentComponentManagerApplicationServices>,
        IInvestmentComponentViewModel<TInvestment>
        where TInvestment: class ,IInvestmentElement
    {
        private IInvestmentElementPresenter<TInvestment> _investmentElement;


        /// <summary>
        ///     Gets or sets the parent investment element of the investment component presenters in the current crud view model.
        /// </summary>
        public IInvestmentElementPresenter<TInvestment> InvestmentElement
        {
            get
            {
                if (_investmentElement == null)
                    throw new InvalidOperationException(Resources.InitializeInvestmentComponentViewModelParentBeforeUsingIt);

                return _investmentElement;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _investmentElement = value;
            }
        }

       
        /// <summary>
        ///     Creates a new instance of the application services that this class will use.
        /// </summary>
        /// <returns>An instance of <see cref="IInvestmentComponentManagerApplicationServices" />.</returns>
        protected override IInvestmentComponentManagerApplicationServices CreateServices()
        {
            IInvestmentComponentManagerApplicationServices services = base.CreateServices();
            services.InvestmentElement = InvestmentElement.Object;

            return services;
        }

        /// <summary>
        ///     Creates a new presenter view model decorating the given investment component.
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent" /> to wrap into an presenter view model.</param>
        /// <returns>
        ///     A new instance of <see cref="IInvestmentComponentPresenter" /> decorating
        ///     <paramref name="investmentComponent" />.
        /// </returns>
        protected override IInvestmentComponentPresenter<TInvestment> CreatePresenterFor(IInvestmentComponent investmentComponent)
        {
            IInvestmentComponentPresenter<TInvestment> component = base.CreatePresenterFor(investmentComponent);
            component.InvestmentElement = InvestmentElement;
            
            return component;
        }

        protected override INavigable Parent { get { return InvestmentElement; } }

        
        ////protected override void Filter(string s)
        ////{
        ////    base.Filter(s);
        ////    InvestmentElement?.DoNotify();
        ////}
    }
}