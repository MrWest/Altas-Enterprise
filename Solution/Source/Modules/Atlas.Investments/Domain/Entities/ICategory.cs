using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    ///     Contract of the domain entity: "Category" in abbreviation. Used to describe investment elements.
    /// </summary>
    public interface ICategory : ICodedNomenclator, IOwnable
    {
    }
}