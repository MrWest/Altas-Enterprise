using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls.Converters
{
    public class EasyTextBoxItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            return   (container as FrameworkElement).FindResource(
                                "CodeTemplate") as DataTemplate;
            return (container as FrameworkElement).FindResource(
                                "NameTemplate") as DataTemplate;
        }

    }
}