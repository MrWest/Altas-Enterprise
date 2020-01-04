﻿#pragma checksum "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4009B3AF3C719503C23591CC56DD7030631296F8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CompanyName.Atlas.Investments.Presentation.Views;
using CompanyName.Atlas.Investments.Presentation.Views.Arrangement;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
using CompanyName.Atlas.UIControls.Views;
using Infralution.Localization.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CompanyName.Atlas.Investments.Presentation.Views.Arrangement {
    
    
    /// <summary>
    /// BudgetView
    /// </summary>
    public partial class BudgetView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.UIControls.AtlasTabControl AtlasTabControl;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.Investments.Presentation.Views.Arrangement.BudgetItemView Equipment;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.Investments.Presentation.Views.Arrangement.BudgetItemView Construction;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.Investments.Presentation.Views.Arrangement.BudgetItemView OtheExpenses;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.Investments.Presentation.Views.Arrangement.WorkCapitalItemView WorkCapital;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Atlas.Investments;component/presentation/views/arrangement/budgetview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.AtlasTabControl = ((CompanyName.Atlas.UIControls.AtlasTabControl)(target));
            
            #line 21 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
            this.AtlasTabControl.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.AtlasTabControl_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 25 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
            ((CompanyName.Atlas.UIControls.Views.MeasurementUnitEditor)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UIElement_OnIsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 28 "..\..\..\..\..\Presentation\Views\Arrangement\BudgetView.xaml"
            ((CompanyName.Atlas.UIControls.Views.CurrencyEditor)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UIElement_OnIsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Equipment = ((CompanyName.Atlas.Investments.Presentation.Views.Arrangement.BudgetItemView)(target));
            return;
            case 5:
            this.Construction = ((CompanyName.Atlas.Investments.Presentation.Views.Arrangement.BudgetItemView)(target));
            return;
            case 6:
            this.OtheExpenses = ((CompanyName.Atlas.Investments.Presentation.Views.Arrangement.BudgetItemView)(target));
            return;
            case 7:
            this.WorkCapital = ((CompanyName.Atlas.Investments.Presentation.Views.Arrangement.WorkCapitalItemView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

