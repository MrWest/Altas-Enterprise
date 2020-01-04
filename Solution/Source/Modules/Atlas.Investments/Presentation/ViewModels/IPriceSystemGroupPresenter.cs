using System;
using System.Collections.Generic;
using System.Linq;
using System.Notifications;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IPriceSystemGroupPresenter<TGroup> : IPresenter<TGroup>, IPriceSystemGroupPresenter
    where TGroup : class,IPriceSystemGroup
    {
       
    }

    public interface IPriceSystemGroupPresenter : INavigable//,INotifiyer, TreeViewCommander
    {
        string Code { get; set; }
        //bool HasItems { get; }
        //Thickness DeepThickness { get; }

        //String ItemKind { get; }

        //string IconData { get; }

        //bool IsExpanded { get; set; }
    }
}
