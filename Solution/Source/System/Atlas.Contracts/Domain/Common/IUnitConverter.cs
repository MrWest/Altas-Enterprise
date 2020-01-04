

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IUnitConverter: IEntity
    {
        IConvertibleEntity ConversionForEntity { get; set; }
        string ConversionForEntityId { get; set; }
        string ConversionUnit { get; set; }
        decimal Factor { get; set; }
    }
}
