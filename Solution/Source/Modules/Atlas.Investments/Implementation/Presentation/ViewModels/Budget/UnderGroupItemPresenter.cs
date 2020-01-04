using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public abstract class UnderGroupItemPresenter<TItem,TService>: BudgetComponentItemPresenterBase<TItem,TService>, IUnderGroupItemPresenter<TItem>
        where TItem : class, IUnderGroupItem
        where TService:class, IUnderGroupItemManagerApplicationServices<TItem>

    {
        private IPlannedResourceViewModel<TItem> _plannedResources;


        public IUnderGroupPresenter UnderGroup { get; set; }

        public override INavigable Parent
        {
            get { return UnderGroup; }
        }

       

        public override Thickness DeepThickness
        {
            get { return new Thickness(24, 0, 0, 0); }
        }
       
       

        /// <summary>
        /// Gets or sets the Cost for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        //public override decimal Cost
        //{
        //    get
        //    {

        //        return Math.Round(UnitaryCost * Quantity, 2);
        //    }
        //}


        public override void NotifyUp()
        {
            UnderGroup.DoNotify();
        }




    }
}