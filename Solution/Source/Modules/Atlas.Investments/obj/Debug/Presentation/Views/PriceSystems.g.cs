﻿#pragma checksum "..\..\..\..\Presentation\Views\PriceSystems.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9643650B08773C8F692CC0DEE7B5E61CA27FEA00"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Presentation.Views;
using CompanyName.Atlas.Investments.Presentation.Views.Converters;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
using CompanyName.Atlas.UIControls.Views;
using Infralution.Localization.Wpf;
using Microsoft.Practices.Prism.Mvvm;
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


namespace CompanyName.Atlas.Investments.Presentation.Views {
    
    
    /// <summary>
    /// PriceSystems
    /// </summary>
    public partial class PriceSystems : CompanyName.Atlas.Contracts.Presentation.Prism.PrismUserControlBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 92 "..\..\..\..\Presentation\Views\PriceSystems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.UIControls.AtlasTabControl AtlasTabControl;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\Presentation\Views\PriceSystems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PsComboBox;
        
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
            System.Uri resourceLocater = new System.Uri("/Atlas.Investments;component/presentation/views/pricesystems.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Presentation\Views\PriceSystems.xaml"
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
            return;
            case 2:
            
            #line 97 "..\..\..\..\Presentation\Views\PriceSystems.xaml"
            ((CompanyName.Atlas.UIControls.Views.MeasurementUnitEditor)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UIElement_OnIsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 100 "..\..\..\..\Presentation\Views\PriceSystems.xaml"
            ((CompanyName.Atlas.UIControls.Views.CurrencyEditor)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UIElement_OnIsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PsComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            
            #line 320 "..\..\..\..\Presentation\Views\PriceSystems.xaml"
            ((System.Windows.Controls.DockPanel)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UIElement_OnIsVisibleChanged);
            
            #line default
            #line hidden
            
            #line 320 "..\..\..\..\Presentation\Views\PriceSystems.xaml"
            ((System.Windows.Controls.DockPanel)(target)).DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.FrameworkElement_OnDataContextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

