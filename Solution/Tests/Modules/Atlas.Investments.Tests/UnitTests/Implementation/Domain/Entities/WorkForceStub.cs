using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class WorkForceStub : IWorkForce
    {
        public object Id { get; set; }

        public string FullName { get; private set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public IWageScale WageScale { get; set; }
    }
}