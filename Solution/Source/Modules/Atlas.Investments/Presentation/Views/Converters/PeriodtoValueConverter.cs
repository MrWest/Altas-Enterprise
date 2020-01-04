using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
    /// <summary>
    /// Converts to decimal from a given period and a list of cash movement values
    /// </summary>
    public class PeriodtoValueConverter
     : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            decimal rslt = 0;

            IList<ICashMovement> OverTimeValues = (IList<ICashMovement>)value;

            if (OverTimeValues!=null)
            {


               

                IPeriod period = (IPeriod)parameter;

                //if (OverTimeValues.All(x => x.CashMovementCategory != null))
                //{
                //    if (OverTimeValues.All(x => x.CashMovementCategory.GetType().Implements<ICashEntry>()) || OverTimeValues.All(x => x.CashMovementCategory.GetType().Implements<ICashOutgoing>()))
                //    {
                        foreach (ICashMovement overTimeValue in OverTimeValues)
                        {
                            
                            if ((overTimeValue.Date.CompareTo(period.Starts)) >= 0 &&
                                (overTimeValue.Date.CompareTo(period.Ends)) <= 0)
                                rslt += overTimeValue.Amount;


                        }
                    //}
                    //else
                    //{
                    //    foreach (ICashMovement overTimeValue in OverTimeValues)
                    //    {


                    //        if ((overTimeValue.Date.CompareTo(period.Starts)) >= 0 &&
                    //            (overTimeValue.Date.CompareTo(period.Ends)) <= 0)
                    //            if (overTimeValue.CashMovementCategory.GetType().Implements<ICashEntry>())
                    //                rslt += overTimeValue.Amount;
                    //            else
                    //                rslt -= overTimeValue.Amount;


                    //    }
                    //}

                //}

               
              

            }
            //if (value.GetType() == typeof(ArrayList))
            //{

            //    if (parameter.GetType() == typeof(ArrayList))
            //    {
            //        decimal auxrslt = 0;
            //        ArrayList OverTimeValues = (ArrayList)value;

            //        Period period = (Period)(((ArrayList)parameter)[0]);
            //        ObservableCollection<Period> periods = (ObservableCollection<Period>)(((ArrayList)parameter)[1]);

            //        ObservableCollection<OverTimeValue> entriesValues = (ObservableCollection<OverTimeValue>)OverTimeValues[0];
            //        ObservableCollection<OverTimeValue> outgoingValues = (ObservableCollection<OverTimeValue>)OverTimeValues[1];

            //        auxrslt = GetRemainingCash(entriesValues, outgoingValues, period, periods);

            //        rslt = auxrslt;
            //    }
            //    if (parameter.GetType() == typeof(Period))
            //    {
            //        decimal auxrslt = 0;
            //        ArrayList OverTimeValues = (ArrayList)value;

            //        Period period = (Period)parameter;

            //        ObservableCollection<OverTimeValue> entriesValues = (ObservableCollection<OverTimeValue>)OverTimeValues[0];
            //        foreach (OverTimeValue overTimeValue in entriesValues)
            //        {
            //            if ((overTimeValue.Date.CompareTo(period.Starts)) >= 0 &&
            //                (overTimeValue.Date.CompareTo(period.Ends)) <= 0)
            //                auxrslt += overTimeValue.Value;
            //        }

            //        ObservableCollection<OverTimeValue> outgoingValues = (ObservableCollection<OverTimeValue>)OverTimeValues[1];
            //        foreach (OverTimeValue overTimeValue in outgoingValues)
            //        {
            //            if ((overTimeValue.Date.CompareTo(period.Starts)) >= 0 &&
            //                (overTimeValue.Date.CompareTo(period.Ends)) <= 0)
            //                auxrslt -= overTimeValue.Value;
            //        }

            //        rslt = auxrslt;
            //    }

            //}
            return rslt;
            //bool inverse = (parameter as string) == "inverse";

            //var bold = value as bool?;
            //if (bold.HasValue && bold.Value)
            //{
            //    return inverse ? FontWeights.Normal : FontWeights.Bold;
            //}
            //return inverse ? FontWeights.Bold : FontWeights.Normal;
        }

        //private decimal GetRemainingCash(ObservableCollection<OverTimeValue> entriesValues, ObservableCollection<OverTimeValue> outgoingValues, Period period, ObservableCollection<Period> periods)
        //{
        //    int index = periods.IndexOf(period);
        //    decimal auxrslt = 0;

        //    foreach (OverTimeValue overTimeValue in entriesValues)
        //    {
        //        if ((overTimeValue.Date.CompareTo(period.Starts)) >= 0 &&
        //            (overTimeValue.Date.CompareTo(period.Ends)) <= 0)
        //            auxrslt += overTimeValue.Value;
        //    }


        //    foreach (OverTimeValue overTimeValue in outgoingValues)
        //    {
        //        if ((overTimeValue.Date.CompareTo(period.Starts)) >= 0 &&
        //            (overTimeValue.Date.CompareTo(period.Ends)) <= 0)
        //            auxrslt -= overTimeValue.Value;
        //    }

        //    if (index > 0)
        //    {
        //        auxrslt += GetRemainingCash(entriesValues, outgoingValues, periods[index - 1], periods);
        //    }
        //    return auxrslt;
        //}
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                var cashmovement = new CashMovement() { Amount = System.Convert.ToDecimal(value), Date = ((IPeriod)parameter).Starts };

                IList<ICashMovement> list = new List<ICashMovement>();
                list.Add(cashmovement);
                return list;
            }
            
            return new List<ICashMovement>();
            //  throw new NotSupportedException();
        }
    }
}
