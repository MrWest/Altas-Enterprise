using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface ICashMovementViewModel<TCategory> : ICrudViewModel<ICashMovement, ICashMovementPresenter<TCategory>>
   where TCategory:class, ICashMovementCategory
    {
        ICashMovementCategoryPresenter<TCategory> Category { get; set; }
        /// <summary>
        /// Deletes all entities for the current catecory
        /// </summary>
        void DeleteAll();
    }
}
