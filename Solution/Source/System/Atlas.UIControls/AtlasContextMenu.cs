using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Effects;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Contracts.Presentation.Visuals.Structures;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasContextMenu : Popup
    {
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ImportCommandProperty = DependencyProperty.Register("ImportCommand", typeof(ICommand), typeof(AtlasContextMenu), new PropertyMetadata(null));
        
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ExportCommandProperty = DependencyProperty.Register("ExportCommand", typeof(ICommand), typeof(AtlasContextMenu), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty PrintCommandProperty = DependencyProperty.Register("PrintCommand", typeof(ICommand), typeof(AtlasContextMenu), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty PrinttableProperty = DependencyProperty.Register("Printtable", typeof(object), typeof(AtlasContextMenu), new PropertyMetadata(null));

        public AtlasContextMenu()
        {
            DefaultStyleKey = typeof(AtlasContextMenu);
            ExportCommand = new DelegateCommand(Export, CanExport);
            ImportCommand = new DelegateCommand(Import, CanImport);
            PrintCommand = new DelegateCommand(Print, CanPrint);
        }

        private bool CanPrint()
        {
            return true;
            //return !Equals(Printtable, null) && !Equals(Printtable as UserControl, null) &&
            //   !Equals((Printtable as UserControl).Content, null) &&
            //    !Equals(((Printtable as UserControl).Content as DocumentViewerBase), null);
        }

        private void Print()
        {
            if(!Equals(Printtable, null) && !Equals(Printtable as UserControl, null) &&
               !Equals((Printtable as IPrinttableContainer), null) )
            (Printtable as IPrinttableContainer).Print();
        }

        private bool CanImport()//string arg
        {
            return DataContext != null && ((Control)DataContext).DataContext!=null;
        }

        private void Import()//string path
        {
            //var dataContext =  ServiceLocator.Current.GetInstance<IDatabaseContext>();
            //dataContext = new Db4ODatabaseContext(path);

            var mainWindow = (Window)ServiceLocator.Current.GetInstance(typeof(Window));
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            mainWindow.Effect = blurEffect;
            // var crap = mainWindow.Name;
            var response = new InteractionStructure() { Text = "Import Content", Title = "Import" };
            var ImportDialog = new Views.AtlasImportPlatform() {Owner = mainWindow, ExportableViewModels = ((AtlasOptionalContent)DataContext).ExportableViewModels };
            ImportDialog.ShowDialog();
            // Display the Message Box to the user with information gathered
            // var response = MessageBox.Show(text, title, MessageBoxButton.YesNo, icon);

            // remove blur effect
            mainWindow.Effect = null;
        }

        private bool CanExport()//string arg
        {
            return DataContext != null && ((Control)DataContext).DataContext!=null;
        }

        private void Export()//string path
        {
            //var dataContext =  ServiceLocator.Current.GetInstance<IDatabaseContext>();
            //dataContext = new Db4ODatabaseContext(path);

            if (!Equals(Printtable, null) && !Equals(Printtable as UserControl, null) &&
                !Equals((Printtable as IExporttableContainer), null))
                (Printtable as IExporttableContainer).Export();

            else
            {
                var mainWindow = (Window)ServiceLocator.Current.GetInstance(typeof(Window));
                BlurEffect blurEffect = new BlurEffect();
                blurEffect.Radius = 5;
                mainWindow.Effect = blurEffect;
                // var crap = mainWindow.Name;
                var response = new InteractionStructure() { Text = "Export Content", Title = "Export" };
                var exportDialog = new Views.AtlasExportPlatform() { Owner = mainWindow, ExportableViewModels = ((AtlasOptionalContent)DataContext).ExportableViewModels };
                //   exportDialog.ExportebleViewModels = ((AtlasOptionalContent) DataContext).ExportableViewModels;
                exportDialog.ShowDialog();
                // Display the Message Box to the user with information gathered
                // var response = MessageBox.Show(text, title, MessageBoxButton.YesNo, icon);

                // remove blur effect
                mainWindow.Effect = null;
            }

           
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand ExportCommand
        {
            get { return (ICommand)GetValue(ExportCommandProperty); }
            set
            {
                if (Equals(value, null))
                    return;
                SetValue(ExportCommandProperty, value);
            }
        }
        ///<summary>
         /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand ImportCommand
        {
            get { return (ICommand)GetValue(ImportCommandProperty); }
            set
            {
                if (Equals(value, null))
                    return;
                SetValue(ImportCommandProperty, value);
            }
        }
        ///<summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand PrintCommand
        {
            get { return (ICommand)GetValue(PrintCommandProperty); }
            set
            {
                if (Equals(value, null))
                    return;
                SetValue(PrintCommandProperty, value);
            }
        }
        
        ///<summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public object Printtable
        {
            get { return (object)GetValue(PrinttableProperty); }
            set { SetValue(PrinttableProperty, value); }
        }

    }
}
