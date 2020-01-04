using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for SubjectMainView.xaml
    /// </summary>
    public partial class SubjectMainView : UserControl
    {
        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="PlanningExecutionView"/>.
        /// </summary>
        public static readonly DependencyProperty ViewProperty = DependencyProperty.Register("View", typeof(ConceptView), typeof(SubjectMainView), new PropertyMetadata(ConceptView.Definition));

        public SubjectMainView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        public ConceptView View
        {
            get { return (ConceptView)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var value = (bool)e.NewValue;
            if (value)
            {
                AtlasTabControl.FilterCommand?.Execute("");

            }

        }
    }
}
