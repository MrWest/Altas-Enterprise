using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    ///     Implementation of the contract <see cref="ICategory" /> for the domain entity: "Category".
    /// </summary>
    public class Category : CodedNomenclatorBase, ICategory
    {
        public IEntity OwnerEntity { get; set; }
        public string OwnerEntityId { get; set; }
    }
}