﻿#pragma checksum "..\..\..\..\..\Presentation\Views\ReportViewsUI\DetailedTimeline.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2B3DC1BE940CBE47609D369FC5A6BC6AA8F54E20"
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
using CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI;
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


namespace CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI {
    
    
    /// <summary>
    /// DetailedTimeline
    /// </summary>
    public partial class DetailedTimeline : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\..\Presentation\Views\ReportViewsUI\DetailedTimeline.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer MyGrid;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\..\Presentation\Views\ReportViewsUI\DetailedTimeline.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.FlowDocumentScrollViewer MyDocumentPageViewer;
        
        #line default
        #line hidden
        
        
        #line 327 "..\..\..\..\..\Presentation\Views\ReportViewsUI\DetailedTimeline.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.FlowDocument FlowDocument;
        
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
            System.Uri resourceLocater = new System.Uri("/Atlas.Investments;component/presentation/views/reportviewsui/detailedtimeline.xa" +
                    "ml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Presentation\Views\ReportViewsUI\DetailedTimeline.xaml"
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
            this.MyGrid = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.MyDocumentPageViewer = ((System.Windows.Controls.FlowDocumentScrollViewer)(target));
            
            #line 14 "..\..\..\..\..\Presentation\Views\ReportViewsUI\DetailedTimeline.xaml"
            this.MyDocumentPageViewer.IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UIElement_OnIsVisibleChanged);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\..\..\Presentation\Views\ReportViewsUI\DetailedTimeline.xaml"
            this.MyDocumentPageViewer.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.FrameworkElement_OnDataContextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FlowDocument = ((System.Windows.Documents.FlowDocument)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

