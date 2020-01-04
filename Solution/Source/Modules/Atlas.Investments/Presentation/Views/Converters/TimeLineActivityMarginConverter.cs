using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
    /// <summary>
    /// Convert a color according to INavigable.Deep
    /// </summary>
    public class TimeLineActivityMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null && values[4] != null)
            {
                var ActualWidth = (double) values[0];
                var ColumnWidth = (double) values[1];
                var Starts = (DateTime) values[2];
                var Start = (DateTime) values[3];
                var TreeNode = (ITreeNode)values[4];

                int startDatesDelta = Starts.GetMonthDelta(Start);

                double leftMargin = (ActualWidth - ((ColumnWidth*startDatesDelta)+TreeNode.TimeLineThickness.Left)) +250;
                return leftMargin;// new Thickness( leftMargin,0,0,0);
            }

            return  new Thickness(0);
        }

       
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
