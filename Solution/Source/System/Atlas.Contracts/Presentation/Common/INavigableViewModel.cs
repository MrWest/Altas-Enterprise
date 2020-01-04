using System.Collections.Generic;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface INavigableViewModel<T,TPresenter>:ICrudViewModel<T,TPresenter>,INavigableViewModel
        where T:class , IEntity
         where TPresenter : class, IPresenter<T>, INavigable
    {
        //ICommand FilterCommand { get; }
        //IEnumerable<INavigable> FiltredItems { get; }
    }
    public interface INavigableViewModel 
      
    {
        ICommand FilterCommand { get; }
        IEnumerable<INavigable> FiltredItems { get; }
    }
}
