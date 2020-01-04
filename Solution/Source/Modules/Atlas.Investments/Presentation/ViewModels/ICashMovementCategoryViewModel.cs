using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    /// CRUD operations over <see cref="ICashMovementCategory"/>
    /// </summary>
    public interface ICashMovementCategoryViewModel<TItem>: ICrudViewModel<TItem,ICashMovementCategoryPresenter<TItem>>
        where TItem:class ,ICashMovementCategory
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        ICashMovementCategoryPresenter<TItem> SuperiorCategory { get; set; }

        //void DeleteLiquity(TItem liquity);
       
    }
}
