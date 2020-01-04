using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    /// <summary>
    /// Represents the specification of a criteria to filter the 
    /// </summary>
    public class NomenclatorByNameSpecification<T> : Specification<T> where T : INomenclator
    {
        public NomenclatorByNameSpecification(string name)
            : base(x => x.Name == name)
        {
        }
    }
}
