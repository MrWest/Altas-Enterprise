using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace CompanyName.Atlas.Contracts.Presentation.Visuals.Structures
{
    public class InteractionStructure : DependencyObject
    {
        public string Text { get; set; }
        public string Title { get; set; }

        public IConfirmation Confirmation { get; set; }
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty YesNoCommandProperty = DependencyProperty.Register("YesNoCommand", typeof(ICommand), typeof(InteractionStructure), new PropertyMetadata(null));

        public InteractionStructure()
        {
            YesNoCommand = new DelegateCommand<String>(YesNo, CanYesNo);
        }

        private bool CanYesNo(string arg)
        {
            return true;
        }

        private void YesNo(string obj)
        {
            if(Confirmation==null)
            Confirmation = new Confirmation();

            Confirmation.Confirmed = obj == "Yes";
            
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand YesNoCommand
        {
            get { return (ICommand)GetValue(YesNoCommandProperty); }
            set { SetValue(YesNoCommandProperty, value); }
        }
    }
}
