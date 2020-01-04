using System;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views.Controls
{
    public class BudgetComponentCombobox: ComboBox
    {

        public BudgetComponentCombobox()
        {
            DefaultStyleKey = typeof(ComboBox);
            SelectionChanged+=OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (FiltrableObject != null)
                FiltrableObject.SecondView = SelectedItem;
        }
        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="PlanningExecutionView"/>.
        /// </summary>
        public static readonly DependencyProperty FiltrableObjectProperty = DependencyProperty.Register("FiltrableObject", typeof(IFiltrable), typeof(BudgetComponentCombobox), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        public IFiltrable FiltrableObject
        {
            get { return (IFiltrable)GetValue(FiltrableObjectProperty); }
            set { SetValue(FiltrableObjectProperty, value); }
        }

    }
}