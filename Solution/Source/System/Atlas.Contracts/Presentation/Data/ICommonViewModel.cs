using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    public interface ICommonViewModel<TEntity, TPresenter> : ICrudViewModel<TEntity, TPresenter>//, ICommonProvider
        where TEntity: class , IEntity
        where TPresenter:class ,IPresenter<TEntity>
    {
        ICommand DeleteMySelfCommand { get; }
        IEnumerable<IPresenter> Presenters { get; }
    }


}
