using CompanyName.Atlas.Contracts.Presentation.Reporting;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting
{
    public interface IInvestmentChildCustomReportSettingsPresenter: IChildCustomReportSettingsPresenter<IInvestmentChildCustomReportSettings, IInvestmentChildCustomReportSettings>, IInvestmentCustomReportSettingsPresenter
    {

        
    }
    public interface IInvestmentChildMainCustomReportSettingsPresenter : IChildCustomReportSettingsPresenter<IInvestmentChildCustomReportSettings, IInvestmentMainCustomReportSettings>, IInvestmentCustomReportSettingsPresenter
    {

        
    }
    public interface IInvestmentMainCustomReportSettingsPresenter : ICustomReportSettingsPresenter<IInvestmentMainCustomReportSettings>, IInvestmentCustomReportSettingsPresenter
    {
        
    }
    public interface IInvestmentCustomReportSettingsPresenter 
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
        bool ShowSubExpeseConcepts { get; set; }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        bool ShowCategories { get; set; }

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

        string Name { get; set; }
    }

    public interface IInvestmentChildCustomReportSettingsViewModel : IChildCustomReportSettingsViewModel<IInvestmentChildCustomReportSettings, IInvestmentChildCustomReportSettingsPresenter, IInvestmentChildCustomReportSettings>
    {

    }
    public interface IInvestmentChildMainCustomReportSettingsViewModel : IChildCustomReportSettingsViewModel<IInvestmentChildCustomReportSettings, IInvestmentChildMainCustomReportSettingsPresenter, IInvestmentMainCustomReportSettings>
    {

    }
    public interface IInvestmentMainCustomReportSettingsViewModel : ICustomReportSettingsViewModel<IInvestmentMainCustomReportSettings, IInvestmentMainCustomReportSettingsPresenter>
    {

    }
}