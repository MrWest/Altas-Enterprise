using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting
{
    public class GenericComparer : IEqualityComparer<object>
    {
        private string[] properties;

        public GenericComparer(string[] properties)
        {
            this.properties = properties;
        }
        public bool Equals(object x, object y)
        {
            //Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            if (properties.Length == 0)
                return false;


            return
                properties.All(
                    p =>
                        (Object.ReferenceEquals(x.GetType().GetProperty(p).GetValue(x), null) &&
                         Object.ReferenceEquals(y.GetType().GetProperty(p).GetValue(y), null)) ||
                         Object.ReferenceEquals(x.GetType().GetProperty(p).GetValue(x), y.GetType().GetProperty(p).GetValue(y)) ||
                         (x.GetType().GetProperty(p).GetValue(x).ToString() == y.GetType().GetProperty(p).GetValue(y).ToString())
                );
           
        }

        public int GetHashCode(object obj)
        {
            //Check whether the object is null 
            if (Object.ReferenceEquals(obj, null)) return 0;

            ////Get hash code for the Name field if it is not null. 
            //int hashProductName = obj.ToString() == null ? 0 : obj.Name.GetHashCode();

            ////Get hash code for the Code field. 
            //int hashProductCode = obj.Code.GetHashCode();

            //Calculate the hash code for the product. 
            return 1; // hashProductName ^ hashProductCode;
        }
    }
}