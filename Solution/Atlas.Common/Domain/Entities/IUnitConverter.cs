using System;
using CompanyName.Atlas.Contracts.Domain;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IUnitConverter: IEntity
    {
        IConvertibleEntity ConversionUnit { get; set; }
        decimal Factor { get; set; }
    }
}
