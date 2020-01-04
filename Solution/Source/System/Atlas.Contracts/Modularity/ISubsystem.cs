using System;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Contracts.Modularity
{
    /// <summary>
    /// Describes the interface of an Atlas subsystem.
    /// </summary>
    public interface ISubsystem : IModule
    {
        /// <summary>
        /// Gets the name of the current subsystem.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the version of the current subsystem.
        /// </summary>
        Version Version { get; }
    }
}
