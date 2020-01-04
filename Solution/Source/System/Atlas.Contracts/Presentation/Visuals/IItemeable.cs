using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Visuals
{
    public interface IItemeable<Titem> : IItemeable
        where Titem:class,IPresenter
    {
        IList<Titem> Items { get; } 
    }

    public interface IItemeable
    {
        bool HasItems { get; }
        object SelectedItem { get; set; }
    }
}
