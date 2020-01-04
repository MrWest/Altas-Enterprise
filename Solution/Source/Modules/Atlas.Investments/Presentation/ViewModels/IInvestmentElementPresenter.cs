using System;
using System.Collections.Generic;
using System.ComponentModel;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using CompanyName.Atlas.UIControls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Investments.Application.Budget;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract describing the interface of an investment element presenter view model, decorating and impersonating
    ///     instances of <see cref="IInvestmentElement" /> in the UI. Note that this presenter may decorate an independent
    ///     investment element or a one that is child of another one.
    /// </summary>
    public interface IInvestmentElementPresenter<TInvestmentElement> : IPresenter<TInvestmentElement>, IInvestmentElementPresenter
    where TInvestmentElement : class , IInvestmentElement
    {


        /// <summary>
        ///     Gets or sets the crud view model handling the investment element presenters being children of the current one.
        /// </summary>
        IInvestmentComponentViewModel<TInvestmentElement> Elements { get; }


    }

    public interface IInvestmentElementPresenter: ITreeNode, IPeriodCalculator, IFiltrable, IBudgetSummary, ICopyPasteable, IBudgetComponentItemChangesSpreadder
    {
        /// <summary>
        ///     Gets or sets the budget of the <see cref="IInvestmentElement" /> contained in the current
        ///     <see cref="IInvestmentElementPresenter" />.
        /// </summary>
        IBudgetPresenter Budget { get; set; }

       

        /// <summary>
        /// Gets or sets the time interval (<see cref="IPeriod"/> for  the current <see cref="IInvestmentElement"/>.
        /// </summary>
        IPeriodPresenter Period { get; set; }


        /// <summary>
        ///     Gets or sets the code of the underlying investment element.
        /// </summary>
        string Code { get; set; }

        /// <summary>
        ///     Gets or sets the location of the underlying investment element.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        ///     Gets or sets the constructor of the underlying investment element.
        /// </summary>
        string Constructor { get; set; }

        /// <summary>
        ///     Gets or sets the objective of the underlying investment element.
        /// </summary>
        string Objective { get; set; }

        /// <summary>
        ///     Gets or sets the scope of the underlying investment element.
        /// </summary>
        string Scope { get; set; }

        /// <summary>
        /// Gets a list of <see cref="IMeasurementUnitPresenter"/> to use for presentation purposses.
        /// </summary>
        IEnumerable<IMeasurementUnitPresenter> MeasurementUnits { get; }

        /// <summary>
        /// Gets a list of <see cref="ICurrencyPresenter"/> to use for presentation purposses.
        /// </summary>
        IEnumerable<ICurrencyPresenter> Currencies { get; }

        /// <summary>
        /// Gets a list of <see cref="ICategoryPresenter"/> to use for presentation purposses.
        /// </summary>
        IEnumerable<ICategoryPresenter> Categories { get; }

        /// <summary>
        /// Gets a list of <see cref="IExpenseConceptPresenter"/> to use for presentation purposses.
        /// </summary>
        IEnumerable<IExpenseConceptPresenter> ExpenseConcepts { get; }

        /// <summary>
        /// Presentation for metrochart utilities
        /// </summary>
        IMetroChartPresenter MetroChartPresenter { get; }
        /// <summary>
        /// Presentation of budget information
        /// </summary>
        IBudgetSummaryPresenter BudgetSummaryPresenter { get; }

        IList<IInvestmentElementPresenter> MyElements { get; }
        decimal GoDeepOnBudgetComponent(Type budgetComponent, ICurrency currency, IPeriod period);
        int InDeep { get; }
    }
}