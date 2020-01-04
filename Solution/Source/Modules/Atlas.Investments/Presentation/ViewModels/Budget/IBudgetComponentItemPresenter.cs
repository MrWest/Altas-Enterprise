using System;
using System.Collections.Generic;
using System.Notifications;
using System.Windows;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract of the crud view models handling budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to the items belong.</typeparam>
    //public interface IBudgetComponentItemPresenter<TItem, TComponent> : IPresenter<TItem>, IBudgetComponentItemPresenter<TItem>
    //    where TItem : class ,IBudgetComponentItem

        
    //{

     
    //    /// <summary>
    //    /// Gets or sets the budget component to which belong the underlying item.
    //    /// </summary>
    //  //  TComponent Component { get; set; }
    //    /// <summary>
    //    /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
    //    /// presenter.
    //    /// </summary>
    //  //  IPlannedResourceViewModel<TItem> PlannedResources { get; }

    //    /// <summary>
    //    /// Gets or sets an identifier to differentiate the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/> 
    //    /// with respect to others.
    //    /// </summary>
      
      

    //    /// <summary>
    //    /// Gets  the total cost of the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
    //    /// </summary>
    //    // decimal Cost { get; }
    //    //void Notify();

    //    /// <summary>
    //    /// Notify changes to inferior levels
    //    /// </summary>
    //    void NotifyDown();

      

    //}

    public interface IBudgetComponentItemPresenter<TItem> : IPresenter<TItem>, IBudgetComponentItemPresenter
        where TItem :class, IBudgetComponentItem

    {
        IPlannedResourceViewModel<TItem> PlannedResources { get; }
        /// <summary>
        //    /// Gets or sets the time interval (<see cref="IPeriod"/> for  the current <see cref="IBudgetComponentItem"/>.
        //    /// </summary>
        IPeriodPresenter Period { get; set; }

        void NotifyUp();
        void NotifyDown();

        bool ExistPlannedResource(string code);
        IPlannedResourcePresenter<TItem> GetPlannedResource(string code);
        void AddFromScratch(string code, string name, string desc, string muId, string cuId, decimal norm, decimal price, int kind, string muwId, decimal wv);

        
    }

    /// <summary>
    /// Base contract of the crud view models handling budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to the items belong.</typeparam>
    public interface IBudgetComponentItemPresenter : IPresenter,  INavigable, IBudgetComponentItemChangesSpreadder
    {
        //IEntity BudgetComponentItemParent { get; set; }

        /// <summary>
        /// As the name says
        /// </summary>
        decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets an identifier to differentiate the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Gets or sets an identifier to differentiate the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        decimal UnitaryCost { get; set; }

        /// <summary>
        /// Gets or sets the measurement unit id
        /// </summary>
        IMeasurementUnitPresenter MeasurementUnit { get; set; }

        ///// <summary>
        ///// Gets or sets the currency id
        ///// </summary>
        //ICurrencyPresenter Currency { get; set; }

        /// <summary>
        /// Get or sets the Category id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        ICategoryPresenter Category { get; set; }

        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        ISubExpenseConceptPresenter SubExpenseConcept { get; set; }


        //void AdquireProperties(IBudgetComponentItemPresenter toAdquire);

        bool isUnitaryPriceCalculated { get; set; }
        decimal CalculatedUnitaryPrice { get; set; }


    }
  
}
