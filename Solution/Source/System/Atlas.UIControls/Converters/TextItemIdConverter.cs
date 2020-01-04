using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    /// converts a combobox selected index value from an string Id
    /// </summary>
    public class TextItemIdConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var main = ServiceLocator.Current.GetInstance(parameter as Type, null);

            (main as ICrudViewModel).Load();
            var test = (main as ICrudViewModel).Items.GetEnumerator();//Cast<IEntity>().FirstOrDefault(x => x.Id == value);

          
            while (test.MoveNext() && value != null)
            {
                IEntity iEntity = test.Current as IEntity;

                if (iEntity.Id.ToString() == value.ToString())
                   return iEntity.ToString();
            };


            return null;


            //  return value == null ? null : (value as IEntity).Id;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //var main = ServiceLocator.Current.GetInstance(parameter as Type, null);

            //(main as ICrudViewModel).Load();

            //int index = System.Convert.ToInt32(value);

            //IEntity test = null;
            //if (index >= 0)
            //    test = (main as ICrudViewModel).Items[index] as IEntity;



            //return test == null ? null : test.Id;

            return null;
        }
    }
}
