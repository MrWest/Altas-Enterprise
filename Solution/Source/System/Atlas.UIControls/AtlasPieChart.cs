using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Presentation.Reporting;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasPieChart:ContentControl
    {
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ChartNameProperty = DependencyProperty.Register("ChartName", typeof(string), typeof(AtlasPieChart), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty SeriesSourceProperty = DependencyProperty.Register("SeriesSource", typeof(ObservableCollection<ISeriesData>), typeof(AtlasPieChart), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty SeriesTitleProperty = DependencyProperty.Register("SeriesTitle", typeof(string), typeof(AtlasPieChart), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(string), typeof(AtlasPieChart), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedItemProperty =
         DependencyProperty.Register("SelectedItem",
         typeof(object), typeof(AtlasPieChart), new PropertyMetadata(null));

        public static readonly DependencyProperty ChartLegendVisibilityProperty =
        DependencyProperty.Register("ChartLegendVisibility",
        typeof(Visibility), typeof(AtlasPieChart), new PropertyMetadata(Visibility.Visible));

        public AtlasPieChart()
        {
            DefaultStyleKey = typeof (AtlasPieChart);
            Loaded+=OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var crap = SeriesSource;
            var items = ItemsSource;
        }

        public Visibility ChartLegendVisibility
        {
            get { return (Visibility)GetValue(ChartLegendVisibilityProperty); }
            set { SetValue(ChartLegendVisibilityProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ObservableCollection<ISeriesData> SeriesSource
        {
            get { return (ObservableCollection<ISeriesData>)GetValue(SeriesSourceProperty); }
            set { SetValue(SeriesSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public string ChartName
        {
            get { return (string)GetValue(ChartNameProperty); }
            set { SetValue(ChartNameProperty, value); }
        }
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public string SeriesTitle
        {
            get { return (string)GetValue(SeriesTitleProperty); }
            set { SetValue(SeriesTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public string ItemsSource
        {
            get { return (string)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
    }
}
