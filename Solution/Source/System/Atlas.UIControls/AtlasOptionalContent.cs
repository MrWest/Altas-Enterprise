using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasOptionalContent: PrismUserControlBase, IView
    {
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty ElementsTreeViewProperties = DependencyProperty.Register("ElementsTreeView", typeof(TreeView), typeof(AtlasOptionalContent));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ExportableViewModelsProperty = DependencyProperty.Register(" ExportableViewModels", typeof(IList<NamedCrudViewModel>), typeof(AtlasOptionalContent), new PropertyMetadata(null));

        ///// <summary>
        ///// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        ///// </summary>
        //public static readonly DependencyProperty ExportCommandProperty = DependencyProperty.Register("ExportCommand", typeof(ICommand), typeof(AtlasOptionalContent), new PropertyMetadata(null));

        public AtlasOptionalContent()
        {
            DefaultStyleKey = typeof(AtlasOptionalContent);
        }
        ///// <summary>
        ///// Gets or sets the optional navigation content for the current <see cref="AtlasWindow"/>.
        ///// </summary>
        public TreeView ElementsTreeView
        {
            get { return (TreeView)GetValue(ElementsTreeViewProperties); }
            set { SetValue(ElementsTreeViewProperties, value); }
        }

        ///// <summary>
        ///// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        ///// </summary>
        //public ICommand ExportCommand
        //{
        //    get { return (ICommand)GetValue(ExportCommandProperty); }
        //    set
        //    {
        //        if (Equals(value, null))
        //            return;
        //        SetValue(ExportCommandProperty, value);
        //    }
        //}
        ///<summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public IList<NamedCrudViewModel> ExportableViewModels
        {
            get { return (IList<NamedCrudViewModel>)GetValue(ExportableViewModelsProperty); }
            set
            {
                if (Equals(value, null))
                    return;
                SetValue(ExportableViewModelsProperty, value);
            }
        }
    }
}
