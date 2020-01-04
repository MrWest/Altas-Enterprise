using System.Windows.Input;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    public interface IFiltrable
    {
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        ICommand FilterCommand { get;  }

        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        object View { get; set; }
        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        object SecondView { get; set; }
        /// <summary>
        /// Gets or sets the filtering criteria to use when filtering the budget component items by name.
        /// </summary>
        string FilterCriteria { get; set; }
    }
}