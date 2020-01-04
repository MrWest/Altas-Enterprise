﻿#pragma checksum "..\..\..\..\..\Presentation\Views\Arrangement\ExecutionView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B434044B4A1C6FA1755833D20BA6535AFD154C83"
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


namespace CompanyName.Atlas.Investments.Presentation.Views.Arrangement {
    
    
    /// <summary>
    /// ExecutionView
    /// </summary>
    public partial class ExecutionView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\..\..\Presentation\Views\Arrangement\ExecutionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.Investments.Presentation.Views.SubSpecialityHolderDataGrid ExecutedSubSpecialityHolderDataGrid;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\Presentation\Views\Arrangement\ExecutionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel ExecutionDockPanel;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\..\Presentation\Views\Arrangement\ExecutionView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.UIControls.AtlasDataGrid AtlasDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/Atlas.Investments;component/presentation/views/arrangement/executionview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Presentation\Views\Arrangement\ExecutionView.xaml"
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
            this.ExecutedSubSpecialityHolderDataGrid = ((CompanyName.Atlas.Investments.Presentation.Views.SubSpecialityHolderDataGrid)(target));
            return;
            case 2:
            this.ExecutionDockPanel = ((System.Windows.Controls.DockPanel)(target));
            
            #line 57 "..\..\..\..\..\Presentation\Views\Arrangement\ExecutionView.xaml"
            this.ExecutionDockPanel.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.ExecutionDockPanel_OnDataContextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AtlasDataGrid = ((CompanyName.Atlas.UIControls.AtlasDataGrid)(target));
            
            #line 85 "..\..\..\..\..\Presentation\Views\Arrangement\ExecutionView.xaml"
            this.AtlasDataGrid.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.AtlasDataGrid_OnDataContextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

