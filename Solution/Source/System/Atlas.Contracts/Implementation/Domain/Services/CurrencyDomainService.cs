using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="ICurrencyDomainService" /> used to enforce the business rules in Currency domain
    ///     entities.
    /// </summary>
    public class CurrencyDomainService: ConvertibleEntityDomainService<ICurrency>,ICurrencyDomainService
    {
        /// <summary>
        ///     Creates a new instance of an WageScale.
        /// </summary>
        /// <returns>A new instance of type <see cref="IConvertibleEntity" />.</returns>
        public override ICurrency Create()
        {
            ICurrency currency = base.Create();
            currency.Name = Resources.NewCurrency;
            currency.Description = Resources.NewCurrencyDescription;
            currency.Letters = "$$";

            return currency;
        }
    }
}
