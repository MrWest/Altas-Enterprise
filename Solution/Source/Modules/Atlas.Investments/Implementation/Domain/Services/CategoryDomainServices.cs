using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="ICategoryDomainServices" /> used to enforce the business rules in Category domain
    ///     entities.
    /// </summary>
    public class CategoryDomainServices : CodedNomenclatorDomainServicesBase<ICategory>, ICategoryDomainServices
    {
        /// <summary>
        ///     Creates a new instance of an Category.
        /// </summary>
        /// <returns>A new instance of type <see cref="ICategory" />.</returns>
        public override ICategory Create()
        {
            ICategory category = base.Create();
            category.Name = Resources.NewCategory_Name;
            category.Description = Resources.NewCategory_Description;

            return category;
        }
    }
}