using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI
{
    /// <summary>
    /// Interaction logic for GraphicModelator.xaml
    /// </summary>
    public partial class GraphicModelator : UserControl
    {
        protected readonly ResourceDictionary ColorResources = new ResourceDictionary
        {
            Source = new Uri("/Atlas.UIControls;component/Assets/GaussChart.xaml", UriKind.RelativeOrAbsolute)
        };
        public GraphicModelator()
        {
            InitializeComponent();

            var collection = (ObservableCollection<ResourceDictionary>)ColorResources["Gradients"];

            if (collection != null)
            {
              

                Equip.Fill = ((LinearGradientBrush)((ResourceDictionary)collection[0])["Brush1"]);
                Const.Fill = ((LinearGradientBrush)((ResourceDictionary)collection[1])["Brush2"]);
                Other.Fill = ((LinearGradientBrush)((ResourceDictionary)collection[2])["Brush3"]);
                Work.Fill = ((LinearGradientBrush)((ResourceDictionary)collection[3])["Brush4"]);
            }
        }

        private void GraphicModelator_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var shit = DataContext;
        }

        private void GraphicModelator_OnLoaded(object sender, RoutedEventArgs e)
        {
            var shit = DataContext;
        }

        private void GraphicModelator_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var shit = DataContext;
        }
    }
}
