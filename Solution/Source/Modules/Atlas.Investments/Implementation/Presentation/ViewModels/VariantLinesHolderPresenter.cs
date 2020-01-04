using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    //public class VariantLinesHolderPresenter : BudgetComponentItemPresenterBase<IVariantLinesHolder,IBudgetComponent, IVariantLinesHolderManagerApplicationServices>, IVariantLinesHolderPresenter
    //{
    //    private IPlannedActivityViewModel<IVariantLinesHolder> _plannedActivities;
    //    private IPlannedResourceViewModel<IVariantLinesHolder> _plannedResource;

    //    //public VariantLinesHolderPresenter(IVariantLinesHolder variantLinesHolder)
    //    //{
    //    //    Object = variantLinesHolder;
    //    //}
    //    /// <summary>
    //    ///     Gets or sets the crud view model handling the investment components presenters being children of the current investment element presenter.
    //    /// </summary>
    //    public IPlannedActivityViewModel<IVariantLinesHolder> PlannedActivities
    //    {
    //        get
    //        {

    //            if (_plannedActivities == null)
    //            {
    //                _plannedActivities = ServiceLocator.Current.GetInstance<IPlannedActivityViewModel<IVariantLinesHolder>>();
    //                _plannedActivities.Component = this;
    //                _plannedActivities.Load();
    //            }

    //            return _plannedActivities;
    //        }
    //    }

    //    public IPlannedResourceViewModel<IVariantLinesHolder> PlannedResources
    //    {
    //        get
    //        {

    //            if (_plannedResource == null)
    //            {
    //                _plannedResource = ServiceLocator.Current.GetInstance<IPlannedResourceViewModel<IVariantLinesHolder>>();
    //                _plannedResource.Component = this;
    //                _plannedResource.Load();
    //            }

    //            return _plannedResource;
    //        }
    //    }


    //    public IList<IPlannedActivityPresenter<IVariantLinesHolder>> GetActivities(String name)
    //    {
    //        var list = new List<IPlannedActivityPresenter<IVariantLinesHolder>>();
            
    //        var rslt =
    //           PlannedActivities.Items.Cast<IPlannedActivityPresenter<IVariantLinesHolder>>()
    //               .FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
    //        if (rslt != null)
    //            list.Add(rslt);
    //        return list.Count > 0 ? list : null;
    //    }

    //    public IList<IPlannedResourcePresenter<IVariantLinesHolder>> GetResources(String name)
    //    {
    //        var list = new List<IPlannedResourcePresenter<IVariantLinesHolder>>();
    //        var rslt =
    //            PlannedResources.Items.Cast<IPlannedResourcePresenter<IVariantLinesHolder>>()
    //                .FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
    //        if(rslt!=null)
    //        list.Add(rslt);
    //        return list.Count>0?list:null;
    //    }

    //    decimal IBudgetComponentPresenter<IVariantLinesHolder>.ExecutedCost { get{return 0;} }

    //    public override void Notify()
    //    {


    //        GetOrInitialize(ref _plannedResources, x => x.Component = this);
    //        OnPropertyChanged(() => HasItems);
    //        OnPropertyChanged(() => Quantity);
    //        OnPropertyChanged(() => UnitaryCost);
    //        OnPropertyChanged(() => Cost);



    //        //  Object.PlannedResources.ForEach(x=> x.Component = Object);

    //    }

    //    void IBudgetComponentPresenter<IVariantLinesHolder>.NotifyUp()
    //    {
    //        NotifyUp();
    //    }

    //    /// <summary>
    //    /// Notify changes to superior levels
    //    /// </summary>
    //    protected override void NotifyUp()
    //    {
    //       // Component.Notify();
    //    }

    //    public override void NotifyDown()
    //    {
    //        OnPropertyChanged(() => Quantity);
    //        OnPropertyChanged(() => UnitaryCost);
    //        OnPropertyChanged(() => Cost);
    //        foreach (IPlannedResourcePresenter<IVariantLinesHolder> plannedResource in PlannedResources)
    //        {

    //            plannedResource.NotifyDown();

    //        }
    //    }
    //    //public IEnumerable<IPlannedActivityPresenter<IVariantLinesHolder>> ActivityList { get; }

    //    //public IEnumerable<IPlannedResourcePresenter<IVariantLinesHolder>> ResourceList { get{return 0;} }

    //    decimal IBudgetSummary.PlannedCost { get{return 0;} }
    //    decimal IBudgetComponentPresenter<IVariantLinesHolder>.PlannedCost { get{return 0;} }
    //    decimal IBudgetSummary.ExecutedCost { get{return 0;} }
    //    public decimal ExecutionPercent { get{return 0;} }
    //    public decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period)
    //    {
    //        return 0;
    //    }
    //}
}
