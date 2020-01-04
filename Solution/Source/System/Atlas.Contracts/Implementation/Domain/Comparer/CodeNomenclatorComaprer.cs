using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Comparer
{
    public class CodeNomenclatorComaprer<TCodedNomenclator> : IEqualityComparer<TCodedNomenclator>
            where TCodedNomenclator: class , ICodedNomenclator
    {
        public bool Equals(TCodedNomenclator x, TCodedNomenclator y)
        {
            //Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal. 
            return x.Code == y.Code && x.Name == y.Name && x.Description == y.Description;
        }

        public int GetHashCode(TCodedNomenclator codedNomenclator)
        {
            //Check whether the object is null 
            if (Object.ReferenceEquals(codedNomenclator, null)) return 0;

            //Get hash code for the Name field if it is not null. 
            int hashProductName = codedNomenclator.Name == null ? 0 : codedNomenclator.Name.GetHashCode();

            //Get hash code for the Code field. 
            int hashProductCode = codedNomenclator.Code.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;

        }
    }
}