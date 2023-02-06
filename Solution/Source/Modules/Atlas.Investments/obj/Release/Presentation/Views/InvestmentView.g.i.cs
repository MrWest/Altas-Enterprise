﻿#pragma checksum "..\..\..\..\Presentation\Views\InvestmentView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "645A6E10FB909A746C12E77D399DD6C0CF3CD1D5"
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
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
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
    /// InvestmentView
    /// </summary>
    public partial class InvestmentView : CompanyName.Atlas.UIControls.AtlasOptionalContent, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\Presentation\Views\InvestmentView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.Investments.Presentation.Views.InvestmentElementTreeView InvestmentElementTreeView;
        
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
            System.Uri resourceLocater = new System.Uri("/Atlas.Investments;component/presentation/views/investmentview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Presentation\Views\InvestmentView.xaml"
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
            
            #line 14 "..\..\..\..\Presentation\Views\InvestmentView.xaml"
            ((CompanyName.Atlas.Investments.Presentation.Views.InvestmentView)(target)).DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.InvestmentElementTreeView = ((CompanyName.Atlas.Investments.Presentation.Views.InvestmentElementTreeView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
