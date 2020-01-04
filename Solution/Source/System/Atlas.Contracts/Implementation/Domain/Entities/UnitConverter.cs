using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class UnitConverter : EntityBase, IUnitConverter
    {
        /// <summary>
        /// Entity to be converted
        /// </summary>
        public IConvertibleEntity ConversionForEntity { get; set; }

        /// <summary>
        /// Entity to convert to
        /// </summary>
        public string ConversionUnit { get; set; }
        public decimal Factor { get; set; }
        public string ConversionForEntityId { get; set; }
    }
}
