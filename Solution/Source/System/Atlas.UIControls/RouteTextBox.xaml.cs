using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Represents a text box used in hierarchical data structures. Has tree parts: the first is the actual
    /// text box, where there can be edited the name of the current node, the second is the route that is
    /// required to travel from the tree's root to the current node and the third is the name of the root
    /// node.
    /// </summary>
    [TemplatePart(Name = MidRouteName, Type = typeof(TextBlock))]
    [TemplatePart(Name = RootName, Type = typeof(TextBlock))]
    public partial class RouteTextBox
    {
        /// <summary>
        /// The name of the template part representing the text of the mid route between the a tree's root and its
        /// selected node (using their names to form it).
        /// </summary>
        public const string MidRouteName = "PART_MidRoute";

        /// <summary>
        /// The name of the template part representing the name of the root node's.
        /// </summary>
        public const string RootName = "PART_Root";

        private TextBlock _midRoute, _rootNode;


        /// <summary>
        /// Initializes a new instance of <see cref="RouteTextBox"/>.
        /// </summary>
        public RouteTextBox()
        {
            DataContextChanged += OnDataContextChanged;

            InitializeComponent();
        }

        /// <summary>
        /// Called when there is applied the template for the current <see cref="RouteTextBox"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _midRoute = (TextBlock)Template.FindName(MidRouteName, this);
            _rootNode = (TextBlock)Template.FindName(RootName, this);
        }


        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var selectedItem = e.NewValue as INavigable;
            if (selectedItem == null || _midRoute == null || _rootNode == null)
                return;

            string midRoute, rootName;
            GetRoutes(selectedItem, out midRoute, out rootName);

            midRoute = midRoute ?? string.Empty;
            _midRoute.Text = midRoute.Any() ? "{0} >".EasyFormat(midRoute) : string.Empty;
            _rootNode.Text = rootName ?? string.Empty;
        }

        private void GetRoutes(INavigable current, out string midRoute, out string rootName)
        {
            var parentList = new List<INavigable>();

            while ((current = current.Parent) != null)
                parentList.Add(current);

            IEnumerable<INavigable> midRouteElements = parentList.Take(parentList.Count - 1);
            midRoute = string.Join(" >", (from p in midRouteElements select p.Name).ToArray());

            var rootNode = parentList.LastOrDefault();
            rootName = rootNode != null ? rootNode.Name : null;
        }
    }
}
