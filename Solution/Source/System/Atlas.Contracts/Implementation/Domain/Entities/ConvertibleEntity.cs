using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class ConvertibleEntity: NomenclatorBase, IConvertibleEntity
    {
        public ConvertibleEntity()
        {
           
            Convertions = new List<IUnitConverter>();
        }


        public IEntity OwnerEntity { get; set; }
        public IList<IUnitConverter> Convertions { get; set;} 
        public String Letters { get; set; }
        public string OwnerEntityId { get; set; }
    }
}
