﻿#pragma checksum "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CF93F585198F37E1CF2F2D4DEEAD81135F48E49D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting;
using CompanyName.Atlas.Investments.Presentation.Views;
using CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI;
using CompanyName.Atlas.UIControls;
using CompanyName.Atlas.UIControls.Properties;
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


namespace CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI {
    
    
    /// <summary>
    /// ReportsGenerator
    /// </summary>
    public partial class ReportsGenerator : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MyGrid;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.FlowDocumentPageViewer MyDocumentPageViewer;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.FlowDocument FlowDocument;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Grid;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CompanyName.Atlas.Investments.Presentation.Views.CustomReportTreeViewContainer CustomReportTreeViewContainer;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid InfoGrid;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock InfoTextBlock;
        
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
            System.Uri resourceLocater = new System.Uri("/Atlas.Investments;component/presentation/views/reportviewsui/reportsgenerator.xa" +
                    "ml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
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
            
            #line 16 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
            ((CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI.ReportsGenerator)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ReportsGenerator_OnLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MyGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.MyDocumentPageViewer = ((System.Windows.Controls.FlowDocumentPageViewer)(target));
            
            #line 51 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
            this.MyDocumentPageViewer.IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UIElement_OnIsVisibleChanged);
            
            #line default
            #line hidden
            
            #line 51 "..\..\..\..\..\Presentation\Views\ReportViewsUI\ReportsGenerator.xaml"
            this.MyDocumentPageViewer.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.FrameworkElement_OnDataContextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.FlowDocument = ((System.Windows.Documents.FlowDocument)(target));
            return;
            case 5:
            this.Grid = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.CustomReportTreeViewContainer = ((CompanyName.Atlas.Investments.Presentation.Views.CustomReportTreeViewContainer)(target));
            return;
            case 7:
            this.InfoGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.InfoTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

