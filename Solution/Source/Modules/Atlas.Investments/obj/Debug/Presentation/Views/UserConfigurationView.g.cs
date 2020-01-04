﻿#pragma checksum "..\..\..\..\Presentation\Views\UserConfigurationView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F0507B2B7E66500E1AEDFCB431B53BFB53CD56FD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.Views;
using CompanyName.Atlas.Investments.Presentation.Views.Arrangement;
using CompanyName.Atlas.Investments.Presentation.Views.Converters;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
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


namespace CompanyName.Atlas.Investments.Presentation.Views {
    
    
    /// <summary>
    /// UserConfigurationView
    /// </summary>
    public partial class UserConfigurationView : CompanyName.Atlas.Contracts.Presentation.Prism.PrismUserControlBase, System.Windows.Markup.IComponentConnector {
        
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
            System.Uri resourceLocater = new System.Uri("/Atlas.Investments;component/presentation/views/userconfigurationview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Presentation\Views\UserConfigurationView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 24 "..\..\..\..\Presentation\Views\UserConfigurationView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.FilterCommand_CanExecute);
            
            #line default
            #line hidden
            
            #line 24 "..\..\..\..\Presentation\Views\UserConfigurationView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.FilterCommand_Executed);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
