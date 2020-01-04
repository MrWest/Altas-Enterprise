using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasTabItem: TabItem
    {
        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="PlanningExecutionView"/>.
        /// </summary>
        public static readonly DependencyProperty ViewProperty = DependencyProperty.Register("View", typeof(object), typeof(AtlasTabItem), new PropertyMetadata(null));

        public AtlasTabItem()
        {
            DefaultStyleKey = typeof(TabItem);
        }

        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        public object View
        {
            get { return GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }
    }
}