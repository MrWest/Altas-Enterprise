using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
    public class ComboBoxItemIdConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var main = ServiceLocator.Current.GetInstance(parameter as Type, null);

            (main as ICrudViewModel).Load();
            var test = (main as ICrudViewModel).Items.GetEnumerator();//Cast<IEntity>().FirstOrDefault(x => x.Id == value);

            int index = -1;
            while (test.MoveNext() && value != null)
            {
                IEntity iEntity = test.Current as IEntity;
                
                index++;

                if (iEntity.Id.ToString() == value.ToString())
                    break;
            };


            return index;
           

          //  return value == null ? null : (value as IEntity).Id;
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var main = ServiceLocator.Current.GetInstance(parameter as Type, null);

            (main as ICrudViewModel).Load();




            int index = System.Convert.ToInt32(value);

            IEntity test = null;
            if (index >= 0)
                test = (main as ICrudViewModel).Items[index] as IEntity;



            return test == null ? null : test.Id;
          
           
        }
    }
}
