using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract of the crud view model used to manage the planned resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned resources managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the planned resources.</typeparam>
    public interface IPlannedResourceViewModel<TComponent> :
        IBudgetComponentItemViewModel<IPlannedResource,IPlannedResourcePresenter<TComponent>>//, IPlannedResourceViewModel 
        where TComponent : class ,IBudgetComponentItem
        //where TPresenter : class, IBudgetComponentResourcePresenter<IPlannedResource, TComponentItem>
    {
        ///// <summary>
        /////     Gets the budget component containing the items managed in the current
        /////     <see cref="IBudgetComponentItemViewModel{TItem, TPresenter, TComponent}" />.
        ///// </summary>
        IBudgetComponentItemPresenter<TComponent> Component { get; set; }
        void AddFromScratch(string code, string name, string desc, string mu, string cu, decimal norm, decimal price, int kind, string wmu, decimal wv);
        void EditFromScratch(object Id, string name, string desc, string MU, string cu, decimal norm, decimal Price, int kind, string wmu, decimal wv);
        //  void AddFromScratch(string code, string name, string desc, object mu, object cu, decimal norm, decimal price);
        bool ExistPlannedResource(string code);
        IPlannedResourcePresenter<TComponent> GetPlannedResource(string code);
    }



    //public interface IPlannedResourceViewModel : IBudgetComponentItemViewModel<IBudgetComponentItem>
    //{
    //    /// <summary>
    //    ///     Gets the budget component containing the items managed in the current
    //    ///     <see cref="IBudgetComponentItemViewModel{TItem, TPresenter, TComponent}" />.
    //    /// </summary>
    //    IBudgetComponentItem Component { get; set; }
    //}
}
