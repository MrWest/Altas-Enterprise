﻿using System.Windows;
using System.Windows.Controls;
using De.TorstenMandelkow.MetroChart.Core;

#if NETFX_CORE

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#else

#endif

namespace De.TorstenMandelkow.MetroChart.ChartControls
{
    public class PlotterArea : ContentControl
    {
        public static readonly DependencyProperty DataPointItemTemplateProperty =
            DependencyProperty.Register("DataPointItemTemplate",
            typeof(DataTemplate),
            typeof(PlotterArea),
            new PropertyMetadata(null));

        public static readonly DependencyProperty DataPointItemsPanelProperty =
            DependencyProperty.Register("DataPointItemsPanel",
            typeof(ItemsPanelTemplate),
            typeof(PlotterArea),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ChartLegendItemStyleProperty =
            DependencyProperty.Register("ChartLegendItemStyle",
            typeof(Style),
            typeof(PlotterArea),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ParentChartProperty =
            DependencyProperty.Register("ParentChart",
            typeof(ChartBase),
            typeof(PlotterArea),
            new PropertyMetadata(null));

        static PlotterArea()
        {
#if NETFX_CORE
            //do nothing
#elif SILVERLIGHT
            //do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlotterArea), new FrameworkPropertyMetadata(typeof(PlotterArea))); 
#endif
        }

        public PlotterArea()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(PlotterArea);
#elif SILVERLIGHT
            this.DefaultStyleKey = typeof(PlotterArea);
#else
            //do nothing
#endif

        }
        
        public Style ChartLegendItemStyle
        {
            get { return (Style)GetValue(ChartLegendItemStyleProperty); }
            set { SetValue(ChartLegendItemStyleProperty, value); }
        }

        public DataTemplate DataPointItemTemplate
        {
            get { return (DataTemplate)GetValue(DataPointItemTemplateProperty); }
            set { SetValue(DataPointItemTemplateProperty, value); }
        }

        public ItemsPanelTemplate DataPointItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(DataPointItemsPanelProperty); }
            set { SetValue(DataPointItemsPanelProperty, value); }
        }

        public ChartBase ParentChart
        {
            get { return (ChartBase)GetValue(ParentChartProperty); }
            set { SetValue(ParentChartProperty, value); }
        }
    }
}
