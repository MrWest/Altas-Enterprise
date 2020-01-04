using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IInvestmentCustomReportSettings 
    {
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowInvestmentElements { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowBudgetComponents { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
         bool ShowSubSpecialities { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowActivities { get; set; }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowResources { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowEquipment { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowConstruction { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowOthers { get; set; }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowWorkCapital { get; set; }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowMU { get; set; }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowQuantity { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowCurrency { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowUC { get; set; }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowCost { get; set; }

         bool ShowSubExpeseConcepts { get; set; }
         bool  ShowCategories { get; set; }

    }

    public interface IInvestmentMainCustomReportSettings : IMainCustomReportSettings, IInvestmentCustomReportSettings
    {

    }

    public interface IInvestmentChildCustomReportSettings : IChildCustomReportSettings, IInvestmentCustomReportSettings
    {

    }
}