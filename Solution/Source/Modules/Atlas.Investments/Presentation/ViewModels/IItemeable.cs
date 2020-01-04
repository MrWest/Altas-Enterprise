using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IItemeable<Titem> : IItemeable
        where Titem:class,IPresenter
    {
        IList<Titem> Items { get; } 
    }

    public interface IItemeable
    {
        bool HasItems { get; }
        object SelectedItem { get; }
    }
}
