using System;
using System.Collections.Generic;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using System.Windows;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    public interface INavigable :INomenclator, ICurrenciablePresenter
    {
        INavigable Parent { get; }
        ICrudViewModel Items { get; }
        ICommand DeleteMySelfCommand { get; }
        bool IsExpanded { get; set; }
        bool FakeIsExpanded { get; set; }
        NavigableState State { get; set; }

       bool AnyIsExpanded { get; }
       bool IsSelected { get; set; }

        string IconData {get;}
        int Depth { get; }

        bool HasItems { get; }

        String Kind { get; }
        String NewText { get; }
        String DeleteText { get; }
        Thickness DeepThickness { get; }

        void Filter(ref IEnumerable<INavigable> list, string filter );
        void DoNotify();
        void ExpandAll();
        void CollapseAll();
        void SetToPrint();
        INavigable GetSelected();
        void ResetFakeIsExpanded();
    }
}
