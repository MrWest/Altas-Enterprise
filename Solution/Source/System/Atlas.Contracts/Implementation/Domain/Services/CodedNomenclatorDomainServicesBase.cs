using System;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    /// <summary>
    ///     Base class of the domain services handling coded nomenclators.
    /// </summary>
    /// <typeparam name="TNomenclator">The type of the coded nomenclators to handle in the current services provider.</typeparam>
    public abstract class CodedNomenclatorDomainServicesBase<TNomenclator> : DomainServicesBase<TNomenclator>
        where TNomenclator : class, ICodedNomenclator
    {
        /// <summary>
        ///     Creates a new instance of an OACE.
        /// </summary>
        /// <returns>A new instance of type <see cref="IOace" />.</returns>
        public override TNomenclator Create()
        {
            TNomenclator nomenclator = base.Create();
            nomenclator.Code = Guid.NewGuid().ToString().Length>8? Guid.NewGuid().ToString().Substring(0,8): Guid.NewGuid().ToString();

            return nomenclator;
        }
    }
}