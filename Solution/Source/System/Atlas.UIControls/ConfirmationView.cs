using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using CompanyName.Atlas.Contracts.Presentation.Visuals;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls
{
    public class ConfirmationView: ContentControl
    {
        /// <summary>
        /// Dependency property to contain the command that is executed by the Delete Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty DisableCommandProperty = DependencyProperty.Register("DisableCommand", typeof(ICommand), typeof(ConfirmationView), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand DisableCommand
        {
            get { return (ICommand)GetValue(DisableCommandProperty); }
            set { SetValue(DisableCommandProperty, value); }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Delete Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ConfirmationView), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Dependency property holding the value saying whether a <see cref="InvestmentElementTreeView"/> is collased or not.
        /// </summary>
        public static readonly DependencyProperty IsCollapsedProperty = DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(ConfirmationView), new PropertyMetadata(true));

        /// <summary>
        /// Gets whether the current <see cref=""/> is collapsed or not.
        /// </summary>
        public bool IsCollapsed
        {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }

        /// <summary>
        /// Dependency property holding the value saying whether a <see cref="InvestmentElementTreeView"/> is collased or not.
        /// </summary>
        public static readonly DependencyProperty RelativeMarginProperty = DependencyProperty.Register("RelativeMargin", typeof(Thickness), typeof(ConfirmationView), new PropertyMetadata(new Thickness()));

        /// <summary>
        /// Gets whether the current <see cref=""/> is collapsed or not.
        /// </summary>
        public Thickness RelativeMargin
        {
            get { return (Thickness)GetValue(RelativeMarginProperty); }
            set { SetValue(RelativeMarginProperty, value); }
        }
        /// <summary>
        /// Dependency property holding the value saying whether a <see cref="InvestmentElementTreeView"/> is collased or not.
        /// </summary>
        public static readonly DependencyProperty DeleteButtonMarginProperty = DependencyProperty.Register(" DeleteButtonMargin", typeof(Thickness), typeof(ConfirmationView), new PropertyMetadata(new Thickness()));

        /// <summary>
        /// Gets whether the current <see cref=""/> is collapsed or not.
        /// </summary>
        public Thickness DeleteButtonMargin
        {
            get { return (Thickness)GetValue(DeleteButtonMarginProperty); }
            set { SetValue(DeleteButtonMarginProperty, value); }
        }
        private ThicknessAnimation slideOut;
        private DoubleAnimation fadeOut;

        public TaskCompletionSource<object> ContinueClicked { get; set; }

        public ConfirmationView()
        {

            DefaultStyleKey = typeof(ConfirmationView);
            DisableCommand = new DelegateCommand(ExecuteMethod,CanExecuteMethod);
            MouseLeave += OnMouseLeave;

         //   IsEnabled = false;

        }

        private void OnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            FrameworkElement confirmElement = (FrameworkElement) this.Template.FindName("ConfirmElement",this);
            if((!Equals(confirmElement,null)&& confirmElement.ActualWidth>120) || ActualWidth>120)
            IsCollapsed = true;
        }

        private bool CanExecuteMethod()
        {
            return true;
        }

        private void ExecuteMethod()
        {
         
            IsCollapsed = !IsCollapsed;
            //var mainWindow = (Window)ServiceLocator.Current.GetInstance(typeof(Window));
            //if (IsCollapsed)
            //    mainWindow.Effect = null;
            //else
            //{

            //    BlurEffect blurEffect = new BlurEffect();
            //    blurEffect.Radius = 5;
            //    mainWindow.Effect = blurEffect;
            //}
        }
    }
}
