using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
    /// <summary>
    /// selector for row template on resource datagrid xaml
    /// </summary>
    public class BudgetComponentItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate crap = null;
            //if (item != null && item.GetType().Implements<IBudgetComponentItemPresenter>())
            //    {
            //        var plannedItem = item as IBudgetComponentItemPresenter;
            //                switch (plannedItem.Kind)
            //                    {
            //                    case "Activity":
            //                    return (container as FrameworkElement).FindResource(
            //                    "activityTemplate") as DataTemplate;
            //                     case "PlannedActivity":
            //                     return (container as FrameworkElement).FindResource(
            //                            "activityTemplate") as DataTemplate;
            //                     case "ExecutedActivity":
            //                      return (container as FrameworkElement).FindResource(
            //                         "activityTemplate") as DataTemplate;
            //                    case "UnderGroupActivity":
            //                        return (container as FrameworkElement).FindResource(
            //                        "activityTemplate") as DataTemplate;
            //                    case "UnderGroup":
            //                    return (container as FrameworkElement).FindResource(
            //                    "underGroupTemplate") as DataTemplate;
            //                    case "RegularGroup":
            //                    return (container as FrameworkElement).FindResource(
            //                    "regularGroupTemplate") as DataTemplate;
            //                    case "OverGroup":
            //                    return (container as FrameworkElement).FindResource(
            //                    "overGroupTemplate") as DataTemplate;
            //                    default:
            //                    return (container as FrameworkElement).FindResource(
            //                    "resourceTemplate") as DataTemplate;
            //                    }
            //  }
            //if (item != null && item.GetType().Implements<IPriceSystemGroupPresenter>())
            //{
            //    var plannedItem = item as IPriceSystemGroupPresenter;
            //    switch (plannedItem.Kind)
            //    {
            //        case "Activity":
            //            return (container as FrameworkElement).FindResource(
            //            "activityTemplate") as DataTemplate;
            //        case "PlannedActivity":
            //            return (container as FrameworkElement).FindResource(
            //                   "activityTemplate") as DataTemplate;
            //        case "ExecutedActivity":
            //            return (container as FrameworkElement).FindResource(
            //               "activityTemplate") as DataTemplate;
            //        case "UnderGroupActivity":
            //            return (container as FrameworkElement).FindResource(
            //            "activityTemplate") as DataTemplate;
            //        case "UnderGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "underGroupTemplate") as DataTemplate;
            //        case "RegularGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "regularGroupTemplate") as DataTemplate;
            //        case "OverGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "overGroupTemplate") as DataTemplate;
            //        default:
            //            return (container as FrameworkElement).FindResource(
            //            "resourceTemplate") as DataTemplate;
            //    }
            //}

            //if (item != null && !Equals(item as ISubSpecialityHolderPresenter<IPlannedSubSpecialityHolder>,null))
            //{
            //    var plannedItem = item as ISubSpecialityHolderPresenter<IPlannedSubSpecialityHolder>;
            //    switch (plannedItem.Kind)
            //    {
            //        case "Activity":
            //            return (container as FrameworkElement).FindResource(
            //            "activityTemplate") as DataTemplate;
            //        case "PlannedActivity":
            //            return (container as FrameworkElement).FindResource(
            //                   "activityTemplate") as DataTemplate;
            //        case "ExecutedActivity":
            //            return (container as FrameworkElement).FindResource(
            //               "activityTemplate") as DataTemplate;
            //        case "UnderGroupActivity":
            //            return (container as FrameworkElement).FindResource(
            //            "activityTemplate") as DataTemplate;
            //        case "UnderGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "underGroupTemplate") as DataTemplate;
            //        case "RegularGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "regularGroupTemplate") as DataTemplate;
            //        case "OverGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "overGroupTemplate") as DataTemplate;
            //        case "Resource":
            //            return (container as FrameworkElement).FindResource(
            //            "resourceTemplate") as DataTemplate;
            //        default:
            //            return (container as FrameworkElement).FindResource(
            //            "subSpecialityHolderTemplate") as DataTemplate;
            //    }
            //}
            //if (item != null && !Equals(item as ISubSpecialityHolderPresenter<IExecutedSubSpecialityHolder>,null))
            //{
            //    var plannedItem = item as ISubSpecialityHolderPresenter<IExecutedSubSpecialityHolder>;
            //    switch (plannedItem.Kind)
            //    {
            //        case "Activity":
            //            return (container as FrameworkElement).FindResource(
            //            "activityTemplate") as DataTemplate;
            //        case "PlannedActivity":
            //            return (container as FrameworkElement).FindResource(
            //                   "activityTemplate") as DataTemplate;
            //        case "ExecutedActivity":
            //            return (container as FrameworkElement).FindResource(
            //               "activityTemplate") as DataTemplate;
            //        case "UnderGroupActivity":
            //            return (container as FrameworkElement).FindResource(
            //            "activityTemplate") as DataTemplate;
            //        case "UnderGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "underGroupTemplate") as DataTemplate;
            //        case "RegularGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "regularGroupTemplate") as DataTemplate;
            //        case "OverGroup":
            //            return (container as FrameworkElement).FindResource(
            //            "overGroupTemplate") as DataTemplate;
            //        case "Resource":
            //            return (container as FrameworkElement).FindResource(
            //            "resourceTemplate") as DataTemplate;
            //        default:
            //            return (container as FrameworkElement).FindResource(
            //            "subSpecialityHolderTemplate") as DataTemplate;
            //    }
            //}

            if (item != null && !Equals(item as INavigable, null))
            {
                var plannedItem = item as INavigable;
                switch (plannedItem.Kind)
                {
                    case "Activity":
                        return (container as FrameworkElement).FindResource(
                        "activityTemplate") as DataTemplate;
                    case "PlannedActivity":
                        return (container as FrameworkElement).FindResource(
                               "activityTemplate") as DataTemplate;
                    case "ExecutedActivity":
                        return (container as FrameworkElement).FindResource(
                           "activityTemplate") as DataTemplate;
                    case "UnderGroupActivity":
                        return (container as FrameworkElement).FindResource(
                        "activityTemplate") as DataTemplate;
                    case "UnderGroup":
                        return (container as FrameworkElement).FindResource(
                        "underGroupTemplate") as DataTemplate;
                    case "RegularGroup":
                        return (container as FrameworkElement).FindResource(
                        "regularGroupTemplate") as DataTemplate;
                    case "OverGroup":
                        return (container as FrameworkElement).FindResource(
                        "overGroupTemplate") as DataTemplate;
                    case "Resource":
                        return (container as FrameworkElement).FindResource(
                        "resourceTemplate") as DataTemplate;
                    case "Investment":
                        return (container as FrameworkElement).FindResource(
                        "InvestmentTemplate") as DataTemplate;
                    case "InvestmentComponent":
                        return (container as FrameworkElement).FindResource(
                        "InvestmentComponentTemplate") as DataTemplate;
                    default:
                        return (container as FrameworkElement).FindResource(
                        "subSpecialityHolderTemplate") as DataTemplate;
                }
            }
            //item = ((ContentControl)((ContentPresenter)container).TemplatedParent).DataContext;
            //if (item != null && item.GetType().Implements<IBudgetComponentItemPresenter>())
            //{
            //    var plannedItem = item as IBudgetComponentItemPresenter;

            //    switch (plannedItem.ItemKind)
            //    {
            //        case "Activity":
            //            {
            //               crap =  (container as FrameworkElement).FindResource(
            //            "activityTemplate") as DataTemplate;
            //                break;
            //            }
            //        case "For Dummies":
            //        {
            //            crap= (container as FrameworkElement).FindResource(
            //           "resourceTemplate") as DataTemplate;
            //            break;
            //        }

            //        default:
            //        {
            //            crap = (container as FrameworkElement).FindResource(
            //            "resourceTemplate") as DataTemplate;
            //            break;
            //        }

            //    }
            //    ((ContentPresenter) container).DataContext = item;
            //}
            return crap;
                }
            }
        }
