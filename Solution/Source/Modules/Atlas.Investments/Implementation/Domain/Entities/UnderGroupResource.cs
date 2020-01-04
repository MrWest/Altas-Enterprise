using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class UnderGroupResource:PlannedResource, IUnderGroupResource
    {
        public IUnderGroup UnderGroup { get; set; }
        public string UnderGroupId { get; set; }
    }
}