﻿using System.Windows;
using System.Windows.Controls;
#if NETFX_CORE

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

#else

#endif

namespace De.TorstenMandelkow.MetroChart.ChartControls
{
    public class ChartTitle : ContentControl
    {        
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ChartTitle),
            new PropertyMetadata(null));
        public static readonly DependencyProperty SubTitleProperty =
            DependencyProperty.Register("SubTitle", typeof(string), typeof(ChartTitle),
            new PropertyMetadata(null));
     
        static ChartTitle()
        {
#if NETFX_CORE
            //do nothing
#elif SILVERLIGHT
            //do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartTitle), new FrameworkPropertyMetadata(typeof(ChartTitle))); 
#endif
        }

        public ChartTitle()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(ChartTitle);
#elif SILVERLIGHT
            this.DefaultStyleKey = typeof(ChartTitle);
#else
            //do nothing
#endif
        }

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public string SubTitle
        {
            get
            {
                return (string)GetValue(SubTitleProperty);
            }
            set
            {
                SetValue(SubTitleProperty, value);
            }
        }
    }
}
