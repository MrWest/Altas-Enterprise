using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Features;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentComponentPresenter" /> being the presenter view model used to
    ///     decorated and impersonate instances of <see cref="IInvestmentComponent" /> in the UI.
    /// </summary>
    public class InvestmentComponentPresenter<TInvestmentElement> :
        InvestmentElementPresenterBase<IInvestmentComponent, IInvestmentComponentManagerApplicationServices>,
        IInvestmentComponentPresenter<TInvestmentElement>
        where TInvestmentElement:class ,IInvestmentElement
    {
        
        private IInvestmentElementPresenter<TInvestmentElement> _investmentElement;
      //  private ITreeNode _parent;

        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentComponentPresenter" /> given an investment component
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent" /> to decorated and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentComponent" /> is null.</exception>
        //public InvestmentComponentPresenter(IInvestmentComponent investmentComponent)
        //    : base(investmentComponent)
        //{
        //}


        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedInvestmentComponent; }
        }


        ///// <summary>
        /////     Gets or sets the depth the current <see cref="InvestmentComponentPresenter" /> is with respect the
        /////     root of the tree formed by the root <see cref="IInvestmentPresenter" /> containing directly
        /////     or indirectly the current one. Note that the returned value is 0-based, where a value of one denotes a child of the
        /////     root <see cref="IInvestmentPresenter" />.
        ///// </summary>
        //public override int Depth
        //{
        //    get
        //    {
        //        //var parent = Parent.Value as IInvestmentElementPresenter;

        //        return (Parent == null ? 0 : ((IInvestmentElementPresenter)Parent).Depth) + 1;
        //    }
        //}

        /// <summary>
        ///     Gets or sets the parent investment element of the investment component presenters in the current crud view model.
        /// </summary>
        public IInvestmentElementPresenter<TInvestmentElement> InvestmentElement
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
        ///     Gets or sets the parent (if any) of the current  <see cref="ITreeNode{T}" />.
        /// </summary>
        public override INavigable Parent
        {
            get
            {
                //if (_parent == null)
                //    throw new InvalidOperationException(
                //        Resources.InitializeInvestmentComponentPresenterParentBeforeUsingIt);
                return InvestmentElement ;
            }
          
        }

        /// <summary>
        /// Creates a new instance of the application services that this class will use.
        /// </summary>
        /// <returns>An instance of <see cref="IInvestmentComponentManagerApplicationServices"/>.</returns>
        protected override IInvestmentComponentManagerApplicationServices CreateServices()
        {
            IInvestmentComponentManagerApplicationServices services = base.CreateServices();

            var parentInvestmentElement = InvestmentElement.Object;
            services.InvestmentElement = parentInvestmentElement;
            

            return services;
        }
       
        protected override int GetPercent()
        {
            if (Parent == null)
                return 0;
            int fatherdays = InvestmentElement.Period.Days;
            int mydays = Period.Days;
            return fatherdays == 0 ? 0 : mydays * 100 / fatherdays;
        }

        protected override int GetStartPercent()
        {
            int fatherdays = InvestmentElement.Period.Days;

            var period = ServiceLocator.Current.GetInstance<IPeriod>();
            period.Starts = InvestmentElement.Period.Starts;
            period.Ends = Period.Starts;
            int mydays =  period.Days;
            return fatherdays == 0 ? 0 : mydays * 100 / fatherdays;
        }

        //public void DoPaste(IInvestmentComponentPresenter<TInvestmentElement> investmentComponent)
        //{
        //    if(investmentComponent==null)
        //        throw new NullReferenceException("investmentComponent");
        //    var newService = CreateServices();
        //    newService.InvestmentElement = Object;
        //    newService.Paste(investmentComponent.Object);
        //}

        public override string Kind
        {
            get { return "InvestmentComponent"; }
        }
    }
}