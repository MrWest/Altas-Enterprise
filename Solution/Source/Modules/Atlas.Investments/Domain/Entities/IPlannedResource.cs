using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Contract representing the specification of the domain entity: "Planned Resource".
    /// </summary>
    public interface IPlannedResource : IPlannedBudgetComponentItem
    {
        /// <summary>
        /// Parent related to this entity
        /// </summary>
        IBudgetComponentItem Component { get; set; }
        /// <summary>
        /// Parent related to this entity
        /// </summary>
        
        string ComponentId { get; set; }
        /// <summary>
        /// Establish the norm for the current planned resource
        /// </summary>
        decimal Norm { get; set; }
   
        /// <summary>
        ///  Gets or sets the <see cref="IWeight"/> for the current <see cref="IPlannedResource"/>.
        /// </summary>
        IWeight Weight { get; set; }

        /// <summary>
        ///  Gets or sets the <see cref="IWeight"/> for the current <see cref="IPlannedResource"/>.
        /// </summary>
        string WeightId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IVolume"/> for the current <see cref="IPlannedResource"/>.
        /// </summary>
        IVolume Volume { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="IVolume"/> for the current <see cref="IPlannedResource"/>.
        /// </summary>
        string VolumeId { get; set; }

        object WageScale { get; set; }
        ///<summary>
        ///  Gets or sets the Waste Coefficient for the current <see cref="IPlannedResource"/>.
        /// </summary>
        decimal WasteCoefficient { get; set; }

        ///<summary>
        ///  Gets or sets the Men Number for the current <see cref="IPlannedResource"/>.
        /// </summary>
        int MenNumber { get; set; }

        ///<summary>
        ///  Gets or sets the Resource Kind for the current <see cref="IPlannedResource"/>.
        /// </summary>
        ResourceKind ResourceKind { get; set; }


        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        object Supplier { get; set; }


        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        object Provider { get; set; }


       


    }
}