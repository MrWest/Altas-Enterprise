using System.ComponentModel.Composition;

namespace Common.Tests.Stubs
{
    /// <summary>
    /// Represents a dependencys stub.
    /// </summary>
    [Export(typeof(IDependencyStub))]
    public class DependencyStub : IDependencyStub
    {
    }
}