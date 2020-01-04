using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{

    /// <summary>
    /// Implements a currency kind entity
    /// </summary>
    public class Currency: ConvertibleEntity,ICurrency
    {
        public Currency()
        {
            IsPrincipal = false;
        }

        public bool IsPrincipal { get; set; }
    }
}
