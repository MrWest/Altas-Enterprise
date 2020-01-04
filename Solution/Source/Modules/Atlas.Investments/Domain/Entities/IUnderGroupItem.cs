using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IUnderGroupItem: IBudgetComponentItem
    {
        IUnderGroup UnderGroup { get; set; }
        string UnderGroupId { get; set; }
    }
}