   M S B A M L   `   `   `  ����L  HAtlas.Investments, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:CompanyName.Atlas.Investments.Properties(CompanyName.Atlas.Investments.Properties  5      P LAtlas.UIControls, Version=0.7.12.24305, Culture=neutral, PublicKeyToken=nulleDclr-namespace:CompanyName.Atlas.UIControls;assembly=Atlas.UIControlsCompanyName.Atlas.UIControls 5      s>clr-namespace:CompanyName.Atlas.Investments.Presentation.Views0CompanyName.Atlas.Investments.Presentation.Views  5      m;clr-namespace:CompanyName.Atlas.Investments.Domain.Entities-CompanyName.Atlas.Investments.Domain.Entities  5	      �Jclr-namespace:CompanyName.Atlas.Investments.Presentation.ViewModels.Budget<CompanyName.Atlas.Investments.Presentation.ViewModels.Budget  5
      N JAtlas.Contracts, Version=0.5.3.24302, Culture=neutral, PublicKeyToken=null~Pclr-namespace:CompanyName.Atlas.Contracts.Domain.Common;assembly=Atlas.Contracts)CompanyName.Atlas.Contracts.Domain.Common 5      }Cclr-namespace:CompanyName.Atlas.Investments.Presentation.ViewModels5CompanyName.Atlas.Investments.Presentation.ViewModels  5      �Rclr-namespace:CompanyName.Atlas.Investments.Implementation.Presentation.ViewModelsDCompanyName.Atlas.Investments.Implementation.Presentation.ViewModels  5      �jclr-namespace:CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;assembly=Atlas.ContractsCCompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common 5      �Iclr-namespace:CompanyName.Atlas.Investments.Presentation.Views.Converters;CompanyName.Atlas.Investments.Presentation.Views.Converters  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      �Yclr-namespace:CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.BudgetKCompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget  5      �Tclr-namespace:CompanyName.Atlas.Contracts.Presentation.Data;assembly=Atlas.Contracts-CompanyName.Atlas.Contracts.Presentation.Data 5      {Oclr-namespace:CompanyName.Atlas.UIControls.Converters;assembly=Atlas.UIControls'CompanyName.Atlas.UIControls.Converters 5      V    PCompanyName.Atlas.Investments.Presentation.Views.BudgetComponentResourceDataGrid   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      M
properties6clr-namespace:CompanyName.Atlas.Investments.Properties     5      [
uiControlsDclr-namespace:CompanyName.Atlas.UIControls;assembly=Atlas.UIControls     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Pviews>clr-namespace:CompanyName.Atlas.Investments.Presentation.Views     5      Pentities;clr-namespace:CompanyName.Atlas.Investments.Domain.Entities     5	      ]budgetJclr-namespace:CompanyName.Atlas.Investments.Presentation.ViewModels.Budget     5
      ccommonPclr-namespace:CompanyName.Atlas.Contracts.Domain.Common;assembly=Atlas.Contracts     5      Z
viewModelsCclr-namespace:CompanyName.Atlas.Investments.Presentation.ViewModels     5      jviewModels1Rclr-namespace:CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels     5      �viewModels11jclr-namespace:CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;assembly=Atlas.Contracts     5      `
convertersIclr-namespace:CompanyName.Atlas.Investments.Presentation.Views.Converters     5      7core&clr-namespace:System;assembly=mscorlib     5      mbudget1Yclr-namespace:CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget     5      edataTclr-namespace:CompanyName.Atlas.Contracts.Presentation.Data;assembly=Atlas.Contracts     5      gconverters1Oclr-namespace:CompanyName.Atlas.UIControls.Converters;assembly=Atlas.UIControls     5      -   5          J� 	AllowDrop  . ��5      �� *FiltredItems+��5      �� 5   
    �� 
TargetType" R�6   .�5      ��  �� Property � �� !  InvestmentElementDataTemplate �� Value# �   6-   �� 5       � �� & "InvestmentElementTreeViewItemStyle# �  63   �� 5        � ��  DataGrid.Static.Background# �  6+   �� 5!       � ��  DataGrid.Static.Foreground# �  6+   �� 5"       � ��$
 Auto=�6K   �� 5#       � ��$
 Auto=�6I   �� 5$       �@��PanningMode$
 Both=�69   �� 5%       �@��IsFlicksEnabled .  67   �� 5&       � ��$ Center=�69   �� 5'       � �� 6,   Z� �� 5(       � ��$ 0q�60   �� 5)       � ��$ 0q�6(   �� 5*       �@�FocusVisualStyle 61   Z� �� 5+       � ��  DataGrid.Static.FontSize# �  6)   �� 5,       � �� ( $InvestmentElement.Default.FontFamily# �  6+   �� 5.       � �� 5/      �� 50       �� 
TargetType" R�6&    � 	Resources 52       �� Source�� 53      $7 1/Atlas.UIControls;component/Themes/Paginator.xaml?�61   %    .R�56      ��  ��Name$ Bd��6!   #��z��6-   #��z��6Y    J� SnapsToDevicePixels . ._�57      ]� 	 ]�Name$	 	dockPanel��6(   $
��Left=�6<   #���  6W   .+�58   "   � 
 � RowDefinitions
 59   &   �� 5:   *   $
��Auto �68   6H   �� 5;   *   $��* �68   6E   �� 5<   *   $��35 �68   6F   .+�5>   &   ]� $	��Top=�60    4 0AddRootInvestmentElementButton.Static.Background#���  6;   .+�5@   *   0  *CompanyName.Atlas.UIControls.AtlasDataGrid    Name$ AtlasDataGrid��6C   &   System.Windows.Controls.DataGrid   CanUserSortColumns .     CanUserReorderColumns .   � 	Resources 5A   .    �� MergedDictionaries�� 5B   2    5C   6   �� 5D   :   $B </Atlas.UIControls;component/Assets/HierarchicalDataGrid.xaml?�6M   �� 5E   :   $7 1/Atlas.UIControls;component/Themes/Paginator.xaml?�6M   %    5G   3      Columns 5J   .   0  *System.Windows.Controls.DataGridTextColumn  5K   2   
CodeColumn��8   2CompanyName.Atlas.Investments.Properties.Resources   Code,  &System.Windows.Controls.DataGridColumn   Header# Z 6s      Width5  /System.Windows.Controls.DataGridLengthConverter$	 180 6�    ) %DataGridTextColumnEditingElementStyle1  +System.Windows.Controls.DataGridBoundColumn   EditingElementStyle# �  5L   -      Binding 5K   Y   �� *Code+5L   -     5M   2   
NameColumn��   Name# Z 6s   $ 4* 6�   # �  5N   -    5M   Z   �� *Name+5N   -     5O   2   
NormColumn��   Norm# Z 6r   $
 Auto 6�   # �  5P   -    5O   Y   �� *Norm+5P   -     5Q   2   MeasurementUnitColumn��   	U_Slash_M# Z 6�   $
 Auto 6�   # �  5R   -    5Q   d   �� *MeasurementUnit+5R   -     5S   2   QuantityColumn��   Quantity# Z 6z   $
 Auto 6�   # �  5T   -    5S   ]   �� *
Quantity+5T   -     5U   2   CurrencyColumn��   Currency# Z 6{   $
 Auto 6�   # �  5V   -    5U   ^   �� *
Currency+5V   -     5W   2   UnitaryCostComlumn��   UnitaryCost# Z 6�   $
 Auto 6�   # �  5X   -    5W   a   �� *UnitaryCost+5X   -     5Y   2   CostComlumn��   
IsReadOnly .    Cost# Z 6�   $
 Auto 6�   # �  5Z   -    5Y   Z   �� *Cost+ �� Mode$ OneWay=�5Z   -   5^   +   5`   '   �� 5a   &    ��Name$ _tv_scrollviewer_��6@   $��1��63   ��.  |�.  #��z��6�   # z 6�   $��Hidden=�6�   $��Hidden=�6  #��z��63  .��5b   *   �� 5c   '   � 5d   &   $��2��6+   .+�5f   *   ��   ��Name$  	AddButton��61   $
��Left=�6D     DataGridAddButtonStyle#���  5g   -   ��5h   -   �� *AddButtonCommand+! �� RelativeSource! �� *FindAncestor+" �� AncestorType$+" %views:BudgetComponentResourceDataGridL�.��5i   .   ]� .+�5j   2   � # �Name$
# grid��67   $��20��6E   $��20��6Q   $
��Left=�6]   .+�5k   6   A� ��6>   �� *
Foreground+! �� *FindAncestor+$" ButtonL���6�   Z� �� 5l   6   $ ��Name$$ lineVer��6;   $��4��6L   	% �� X1$% 10��6`   	& �� X2$& 10��6h   	' �� Y1$' 3��6p   	( �� Y2$( 17��6w   #���  6   �� 5m   6   $$ lineHor��6;   $' 10��6{   $( 10��6�   $% 3��6�   $& 17��6�   $��4��6�   ��6L   �� *Stroke+) �� ElementName$) lineVer��6�   5n   3   �� 5o   2   * ��Name$* AddTextBlock��6<   $��Center=�6�   $��18(�6�   $��10,0,0,0q�6�   ��6R   �� *AddButtonText+! �� *FindAncestor+$+" %views:BudgetComponentResourceDataGridL�6�   5p   /   5q   +   ,  &CompanyName.Atlas.UIControls.Paginator  5r   *   $��Center=�6?   $	��400��6\   +   PaginationItems+ 6h   �� *Items+! �� *FindAncestor+$+" %views:BudgetComponentResourceDataGridL�,   
DoPaginate, 6�   �� *Change+6  ]� 5s   *   $��Right=�66   $��Center=�6R   $
��15,0q�6m   .+�5t   .   ��  *	 &DataGridColumnHeader.Static.Foreground#��� 	 6�   $��SemiBold"�6�   $��0,0,15,0q�6  ��68   �� *	TotalText+! �� *FindAncestor+$+" %views:BudgetComponentResourceDataGridL�6&  �� 5u   .   $��15,0,0,0q�6�   ��68   �� *Total+! �� *FindAncestor+$+" %views:BudgetComponentResourceDataGridL�$ OneWay=�6�   �� 5v   .   $��MP��68   $��15,0,0,0q�6B   6V   5w   +   5x   '   5y   #   5z      5{      - �� Triggers- 5}      P� 5      . P� Property. � {�/ P� Value/ .  . �5�   "   �� 0 �� 
TargetName$0 Bd��6?   # � ��6O    � ��5�      5�      5�      5�       5�      e	   _CompanyName.Atlas.Investments.Presentation.Views.Converters.BudgetComponentItemTemplateSelector '
 #budgetComponentItemTemplateSelector  activityTemplate1 �� DataType`
   ZCompanyName.Atlas.Investments.Presentation.ViewModels.Budget.IBudgetComponentItemPresenterN  HCompanyName.Atlas.UIControls.Converters.SoftBooleanToVisibilityConverter  Key * &DataGridColumnHeader.Static.Background2 ��Name3 � FocusVisualStyle 6 2AddRootInvestmentElementButton.Disabled.Background 6 2AddRootInvestmentElementButton.Disabled.Foreground 5 1InvestmentElementTreeViewCollapserButtonBaseStyle4 �� BasedOn  DataGrid.Dark.Background $  DataGrid.Activity.Row.Background5 � ToolTip6 � ColumnDefinitions 7 3InvestmentElementTreeViewCollapserToggleButtonStyle7 d� 	IsChecked $  SoftBooleanToVisibilityConverter8 �� 	Converter9 F�Name: �� Stretch; ��Name< �� Path= �� Padding> ��Nameb  \CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common.MeasurementUnitViewModel?   MeasurementUnits@ �� SelectedItemA �� SelectedValue
B � TagC �� Triggers  
ShadowText[  UCompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common.CurrencyViewModelD   
CurrenciesE �� TriggersF �� ValueG �� Binding % !DataGridCell.MouseOver.Background % !DataGridCell.MouseOver.Foreground $  DataGridCell.Selected.Background $  DataGridCell.Selected.Foreground  resourceTemplate3  -CompanyName.Atlas.UIControls.AtlasEasyTextBoxH  Namex   rCompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.PlannedResourceForNomenclatorViewModelI   NomenclatorProviderJ   NomenclatorSourceK   MaxItemsNumberL   SelectedObjectM �� CornerRadiusN   DescriptionO �� TextWrapping ) %DataGridRowDetailLine.Real.ForegroundP S� 
MethodNameQ S� 
ObjectType  System.EnumR S� MethodParametersS M� Type@   :CompanyName.Atlas.Investments.Domain.Entities.ResourceKindT   Type 
 myEnumU �� SourceV   ExpenseConcepts2  ,CompanyName.Atlas.UIControls.LeveredComboBoxb   \CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.ExpenseConceptViewModelW   ExpenseConceptsX   SubLevelY �� BindsDirectlyToSourceZ   Category[ �� DisplayMemberPath\   VCompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.CategoryViewModel\   
Categories] �� Text^   Supply_   UnitaryWeight` � DataContexta   UnitaryVolumeb   MenLaborc   	WageScale]   WCompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.WageScaleViewModeld   
WageScalese   	MenNumberf   	Equipment  System.Windows.Visibilityg   Visible   BooleanToVisibilityConverterh �� ItemsSource 4 0AddRootInvestmentElementButton.Static.Foreground  CommandButtonBaseStyle R  NInvestmentElementAddCommandAddRootInvestmentElementButton.MouseOver.Foreground M! IInvestmentElementCommandAddRootInvestmentElementButton.Pressed.Foreground U" QInvestmentElementDeleteCommandAddRootInvestmentElementButton.MouseOver.Foregroundi �� Orientation # AddCommandButtonStylej   AddNewPlannedResource $ DeleteCommandButtonStylek ��Name +% 'DataGridRowBottomLine.Static.Backgroundl ��Namem ]� 
Conditionsn �� 
SourceNameo �� Propertyp �� Value��%a  &	
       &	      7  7  7  7��7��&	 e    7  7  7  7��7��7  7��7��7��&	 �L    &	  �L    &	 �L    7  7  7 
 7  	  5�   
   69   �� 5�   
   "1 
 60    5�      ��  5�      $ SoftBooleanToVisibilityConverter 6j   �� 5�      51InvestmentElementTreeViewCollapserButtonBaseStyle " ��6R   .�5�      ��  �@�FocusVisualStyle 69   Z� �� 5�       � ��# �  63   �� 5�       � ��# � 	 63   �� 5�       � ��$ 0q�68   �� 5�       � ��$ Stretch=�6C   �� 5�       � ��$ Center=�6A   �� 5�       � �� 5�      �� 5�      " ��6.   .R�5�   "   �� $ border��6)   #��z��69   #��z��6e   #��z��6�    . ._�5�   &   �� $2 contentPresenter��67   #��z��6m   #��z��6�   ��. # z 6�   #��z��6*  3 6Q   Z� 6*  5�   #   - 5�   "   P� 5�   &   . �@��IsDefaulted/ . . �5�   *   �� $0 border��6H   # � ��6\    � ��5�   '   P� 5�   &   . �@J�IsMouseOver/ . . �5�   *   �� $0 border��6G   # �  6[    � ��5�   '   P� 5�   &   . � ��/ . . �5�   *   �� $0 border��6G   # �  6[    � ��5�   '   P� 5�   &   . � {�/ .  . �5�   *   �� $0 border��6G   # �  6[    � ���� 5�   *   $0 contentPresenter��6S   # �  6q    � ��5�   '   5�      5�      �� 5�       � ��$ 35��6/   �� 5�       � ��$ 0q�60   5�      �� 5�      73InvestmentElementTreeViewCollapserToggleButtonStyle 84   " d�5�      .�5�      ��  � ��
 ���� 6I   �� 5�       � �� 5�      �� 5�      " d�6.   .R�5�   "   �� $ border��6)   #��z��69   #��z��6e   #��z��6�    . ._�5�   &   �� $2 contentPresenter��67   #��z��6m   #��z��6�   ��. # z 6�   #��z��6*  3 6Q   Z� 6*  5�   #   - 5�   "   P� 5�   &   . �@J�IsMouseOver/ . . �5�   *   �� $0 contentPresenter��6S   # �  6q    � ��5�   '   P� 5�   &   . � ��/ . . �5�   *   �� $0 contentPresenter��6S   # �  6q    � ��5�   '   P� 5�   &   . � {�/ .  . �5�   *   �� $0 border��6G   # �  6[    � ���� 5�   *   $0 contentPresenter��6S   # �  6q    � ��5�   '   5�      5�      5�      5�      .R�5�      � $# ActivityRow��6   #���  6(   
 5�      �� 5�      $
��Auto �6$   64   .+�5�      � $��Center=�6   $��35��65   $��0,0,0,0q�6Z   5 6A   �� *Name+6 5�      �� 5�      $��5 �6+   67   �� 5�      ��6+   �� *ActualWidth+$) 
CodeColumn��6b   �� 5�      ��6+   �� *ActualWidth+$) 
NameColumn��6b   �� 5�      ��6+   �� *ActualWidth+$) 
NormColumn��6c   �� 5�      ��6+   �� *ActualWidth+$) MeasurementUnitColumn��6o   �� 5�      ��6+   �� *ActualWidth+$) QuantityColumn��6g   �� 5�      ��6+   �� *ActualWidth+$) CurrencyColumn��6h   �� 5�      ��6+   �� *ActualWidth+$) UnitaryCostComlumn��6k   �� 5�      ��6+   �� *ActualWidth+$) CostComlumn��6d   .+�5�      ]� $��1��6    .+�5�      �� 
������ ��67   �� *DeepThickness+6[   ]� 5�      $��8��6$   $��0,0,5,0q�61   .+�5�      d� 8�� 7 6x   �� *
IsExpanded+y�5�   !   �� *
HasItems+88  .��5�   "   �� ._�5�   &   F� $
9 icon��6+   $��7��69   $��7��6C   $: Uniform=�6N   $	��5,0q�5�   #   #���  5�   #   ��5�   #   �� *
IconData+5�   #   5�      5�      �� 5�      $; 
textBlock0��6"   $��0,0,10,0q�69   $��0q�6�   
������ 
������ $��Center=�5�      $
��Left=�67   #���  6R   #���  5�      ��5�   L   �� *Code+5 6c   �� *Code+$ OneWay=���5�   `   �� $) 	textBlock��$< 
Foreground�5      �� 5     $* 	textBlock��6    $��2��64   $��2��6D   $= 3,0,0,0q�6X   
������ $��Center=�5     $
��Left=�68   #���  5     #���  6R   #���  5     ��5  j   �� *Name+5     �� 5     $> 
textBlock1��61   $��4��6!   $��Center=�6E   .+�5     �� #��Z? 6#   $��-2,0,5,0q�6t   
������ {�.  $��Center=�5     @ 5  �   �� *MeasurementUnit+��5  %   �� ! �� $" TreeViewItemL�$< 
Foreground���6�   �� $) ActivityRow��$< 
Background�A 5  #   �� *MeasurementUnit+B 6M   �� *MeasurementUnit+��5     �� 5  "   " ��6(   84  .�5  &   ��  � ��
 ���� 6Z   C 5  &   P� 5  *   . �@J�	IsFocused/ . . �5  .   ��  � ��# �  6L   6s   5  +   5  #   5$     5%     �� 5(     $; 
textBlock3��6   $��5��63   $��1,0,0,0q�6D   $��0q�6o   
������ 
������ $��Center=�5)     $
��Left=�68   #���  5*     #���  5+     $��Right=�6�   ��5(  U   �� *
Quantity+��5+  `   �� $) 	textBlock��$< 
Foreground�6�   �� 5-     $> 
textBlock4��6!   $��6��66   $��Center=�6G   .+�5/     �� #��ZD 6#   $��-4,0,5,0q�6g   {�.  $��Center=�51  r   @ 5/  y   �� *
Currency+��50  %   �� ! �� $" TreeViewItemL�$< 
Foreground���6�   �� $) ActivityRow��$< 
Background�A 51  $   �� *
Currency+B 6G   �� *
Currency+��52     �� 53  "   " ��6(   84  .�54  &   ��  � ��
 ���� 6Z   C 55  &   P� 56  *   . �@J�	IsFocused/ . . �57  .   ��  � ��# �  6L   6s   58  +   5:  #   5B     5C     �� 5E     $; 
textBlock5��6   $��7��63   $��1,0,0,0q�6D   $��0q�6U   $��Center=�6a   $��0q�6�   
������ 
������ $��Center=�5F     $
��Left=�69   #���  5G     #���  5H     $��Right=�6�   ��5E  �   �� *UnitaryCost+��5H  `   �� $) 	textBlock��$< 
Foreground�6�   �� 5I     $; 
textBlock6��6   $��8��63   $��0,0,0,0q�6D   $��0q�6U   $��Center=�6a   $��0q�6�   
������ 
������ $��Center=�5J     $
��Left=�67   #���  5K     #���  5L     $��Right=�6�   ��5I  �   �� *Cost+$ OneWay=���5L  `   �� $) 	textBlock��$< 
Foreground�6�   5M     5P     E 5Q     �� 5R     $
F True��6�   G 6   �� *IsMouseOver+! �� *FindAncestor+$" BorderL�.a�5T     �� $0 ActivityRow��6   # �  6L    � ���� 5V     $0 	textBlock��6   # �  6J    � ��5X     �� 5Y     $
F True��6�   G 6   �� *
IsSelected+! �� *FindAncestor+$" TreeViewItemL�.a�5[     �� $0 ActivityRow��6   # �  6L    � ���� 5]     $0 	textBlock��6   # �  6J    � ��5_     �� 5`     $
F True��6�   G 6   �� *IsMouseOver+! �� *FindAncestor+$" TreeViewItemL�.a�5a     �� $0 	textBlock��6   # �  6J    � ��5c     5e     �� 5h  
   "1 
 60    5i     ��  5j     $ SoftBooleanToVisibilityConverter 6j   �� 5l     51InvestmentElementTreeViewCollapserButtonBaseStyle " ��6R   .�5m     ��  �@�FocusVisualStyle 69   Z� �� 5n      � ��# �  63   �� 5o      � ��# � 	 63   �� 5p      � ��$ 0q�68   �� 5q      � ��$ Stretch=�6C   �� 5r      � ��$ Center=�6A   �� 5s      � �� 5t     �� 5u     " ��6.   .R�5v  "   �� $ border��6)   #��z��69   #��z��6e   #��z��6�    . ._�5w  &   �� $2 contentPresenter��67   #��z��6m   #��z��6�   ��. # z 6�   #��z��6*  3 6Q   Z� 6*  5x  #   - 5y  "   P� 5z  &   . �@��IsDefaulted/ . . �5{  *   �� $0 border��6H   # � ��6\    � ��5|  '   P� 5}  &   . �@J�IsMouseOver/ . . �5~  *   �� $0 border��6G   # �  6[    � ��5  '   P� 5�  &   . � ��/ . . �5�  *   �� $0 border��6G   # �  6[    � ��5�  '   P� 5�  &   . � {�/ .  . �5�  *   �� $0 border��6G   # �  6[    � ���� 5�  *   $0 contentPresenter��6S   # �  6q    � ��5�  '   5�     5�     �� 5�      � ��$ 35��6/   �� 5�      � ��$ 0q�60   5�     �� 5�     73InvestmentElementTreeViewCollapserToggleButtonStyle 84   " d�5�     .�5�     ��  � ��
 ���� 6I   �� 5�      � �� 5�     �� 5�     " d�6.   .R�5�  "   �� $ border��6)   #��z��69   #��z��6e   #��z��6�    . ._�5�  &   �� $2 contentPresenter��67   #��z��6m   #��z��6�   ��. # z 6�   #��z��6*  3 6Q   Z� 6*  5�  #   - 5�  "   P� 5�  &   . �@J�IsMouseOver/ . . �5�  *   �� $0 contentPresenter��6S   # �  6q    � ��5�  '   P� 5�  &   . � ��/ . . �5�  *   �� $0 contentPresenter��6S   # �  6q    � ��5�  '   P� 5�  &   . � {�/ .  . �5�  *   �� $0 border��6G   # �  6[    � ���� 5�  *   $0 contentPresenter��6S   # �  6q    � ��5�  '   5�     5�     5�     5�     .R�5�     � 
 5�     �� 5�     $
��Auto �6$   64   �� 5�     $��* �6$   61   .+�5�     � $��Center=�6   $��35��65   $��0,0,0,0q�6Z   5 6A   �� *Name+6 5�     �� 5�     $��5 �6+   67   �� 5�     ��6+   �� *ActualWidth+$) 
CodeColumn��6b   �� 5�     ��6+   �� *ActualWidth+$) 
NameColumn��6b   �� 5�     ��6+   �� *ActualWidth+$) 
NormColumn��6c   �� 5�     ��6+   �� *ActualWidth+$) MeasurementUnitColumn��6o   �� 5�     ��6+   �� *ActualWidth+$) QuantityColumn��6g   �� 5�     ��6+   �� *ActualWidth+$) CurrencyColumn��6h   �� 5�     ��6+   �� *ActualWidth+$) UnitaryCostComlumn��6k   �� 5�     ��6,   �� *ActualWidth+$) CostComlumn��6e   .+�5�     ]� $��1��6    .+�5�     �� 
������ ��67   �� *DeepThickness+6[   ]� 5�     $��8��6$   $��0,0,5,0q�61   .+�5�     d� 8�� 7 6x   �� *
IsExpanded+y�5�  !   �� *
HasItems+88  .��5�  "   �� ._�5�  "   F� $
9 icon��6'   $��7��65   $��7��6?   $: Uniform=�6J   $	��5,0q�5�  #   #���  5�  #   ��5�  #   �� *
IconData+5�  #   5�     5�     �� 5�     $; 
textBlock0��6"   $��0,0,10,0q�69   $��0q�6�   
������ 
������ $��Center=�5�     $
��Left=�67   #���  6R   #���  5�     ��5�  L   �� *Code+5 6b   �� *Code+$ OneWay=���5�  `   �� $) 	textBlock��$< 
Foreground�5�       5�     $H 	textBlock��62   $��2��6F   $��3,0,0,0q�6V   #J ZI 6j   $K 5��6�   $��0q�5�  2   
������ 
������ $��Center=�5�     #���  5�     #���  6R   #���  5�     ��5�  �   �� *Name+L 6�   �� *BudgetComponent+$ TwoWay=�5�     �� 5�     $; 
textBlock2��6   $��3��63   $��3,0,0,0q�6D   $��0q�6k   
������ 
������ $��Center=�5�     $��Right=�68   #���  5�     #���  5�     ��5�  U   �� *Norm+��5�  `   �� $) 	textBlock��$< 
Foreground��� 5�     $> 
textBlock1��61   $��4��6!   $��Center=�6E   .+�5�     �� #��Z? 6#   $��-2,0,5,0q�6t   
������ $��Center=�5�  o   @ 5�  �   �� *MeasurementUnit+��5�  &   �� ! �� $" TreeViewItemL�$< 
Foreground���6�   �� ! �� $" TreeViewItemL�$< 
Background�A 5�  #   �� *MeasurementUnit+B 6O   �� *MeasurementUnit+��5�     �� 5�  "   " ��6(   84  .�5�  &   ��  � ��
 ���� 6Z   C 5�  &   P� 5�  *   . �@J�	IsFocused/ . . �5�  .   ��  � ��# �  6L   6s   5�  +   5�  #   5�     5�     �� 5     $; 
textBlock3��6   $��5��63   $��1,0,0,0q�6E   $��0q�6p   
������ 
������ $��Center=�5     $��Right=�68   #���  5     #���  5     $��Right=�6�   ��5  V   �� *
Quantity+��5  `   �� $) 	textBlock��$< 
Foreground�6�   �� 5	     $> 
textBlock4��6!   $��6��66   $��Center=�6G   .+�5     �� #��ZD 6#   $��-4,0,5,0q�6g   $��Center=�5  b   @ 5  y   �� *
Currency+��5  &   �� ! �� $" TreeViewItemL�$< 
Foreground���6�   �� ! �� $" TreeViewItemL�$< 
Background�A 5  $   �� *
Currency+B 6I   �� *
Currency+��5     �� 5  "   " ��6(   84  .�5  &   ��  � ��
 ���� 6Z   C 5  &   P� 5  *   . �@J�	IsFocused/ . . �5  .   ��  � ��# �  6L   6s   5  +   5  #   5     5     �� 5!     $; 
textBlock5��6   $��7��63   $��1,0,0,0q�6F   $��0q�6u   
������ 
������ $��Center=�5"     $��Right=�69   #���  5#     #���  5$     $��Right=�6�   ��5!  X   �� *UnitaryCost+��5$  `   �� $) 	textBlock��$< 
Foreground�6�   �� 5%     $; 
textBlock6��6   $��8��63   $��0,0,0,0q�6D   $��0q�6x   
������ 
������ $��Center=�5&     $��Right=�67   #���  5'     #���  5(     $��Right=�6�   ��5%  U   �� *Cost+$ OneWay=���5(  `   �� $) 	textBlock��$< 
Foreground�6�   5)     �� 5*     $��
MainBorder��6'   $��1��6   $y�	Collapsed=�69   $M 5��6R   #���  6c   ._�5+     �� .+�5-     � 6 5.     �� 5/  "   $��* �63   6?   �� 50  "   $
��Auto �63   6B   .+�53     ]� $��7,0,8,5q�6(   .+�54  "   �� $��12(�6.   #��ZN 6<   $��SemiBold"�6o   $��Center=�6�   $��0,2,13,2q�6�   6�   �� 55  "   $��DetailsDescription��6:   $��12(�6,   $��2��6T   $��Stretch=�6h   $	��2,3q�6�   $��20,0,0,0q�6�   $
O Wrap=�6�   
������ #���  6  #���  6E  $��1q�6�  ��6�   �� $< Description�$ TwoWay=�6�  56     ]� 57     $��1��6(   $��10,0,5,5q�68    58  "   ��S� 5:  &   
myEnum $P 	GetValues��6H   "Q  6`   R 5;  *   M� 5<  .   "S  65   5>  '   5?  #   .+�5@  "   �� $��12(�6-   $	��5,0q�6;   #��ZT 6H   $��Center=�6u   $��SemiBold"�6�   6�   �� 5A  "   $��ResourceKindComboBox��69   $��12(�6+   $	��110��6W   #���  6�   #���  5B  %   #���  6e   $��1q�6�   @ 5A  f   �� *ResourceKind+��6�   �� 8U  5B  �   5C     5D     ]� 5E     .+�5F     � 6 5G  "   �� 5H  &   $
��Auto �67   6F   �� 5I  &   $
��Auto �67   6F   �� 5J  &   $
��Auto �67   6F   �� 5K  &   $��* �67   6C   .+�5N  "   ]� $��0��6,   $��Center=�6<   $��8,0,5,5q�6Y   .+�5R  &   �� $��12(�61   $	= 0,1q�6?   $��0,0,11,0q�6M   $��SemiBold"�6_   #��ZV 6v   $��Right=�6�   $��Center=�6�   6�     5U  &   $��SpentConcept��6^   $��12(�6B   $	��0,2q�6P   $	��140��6s   #��ZW 6�   #���  6  #���  5V  N   #���  6�   $��0��5W  +   X 5U  �   �� *SubExpenseConcept+Y .  $ TwoWay=�A 5V  %   �� *ExpenseConcept+��5X  *   �� 5Y  .   .R�5Z  2   �� ��6>   �� *Name+6V   5[  /   5^  '   5_  #   ]� 5`  "   $��1��6,   $��Center=�6<   $��0,0,5,5q�6Y   .+�5c  &   �� $��12(�60   $	= 5,1q�6>   #��ZZ 6L   $��Center=�6|   $��SemiBold"�6�   $��Center=�6�   6�   �� 5d  &   $��Category��6=   $��12(�6/   $	��0,2q�6M   $	��140��6[   $[ Category.Name��6h   #���  6�   #���  5e  %   #���  6e   #��Z\ 5f  *   ] 5d  �   �� *Category.Name+$ OneWay=�@ 5f  l   �� *
Category+A 5g  1   �� *
Category+B 6T   �� *
Category+��5h  *   �� 5i  .   .R�5j  2   �� ��6>   �� *Name+6V   5k  /   5n  '   5o  #   ]� 5p  "   $��2��6,   $��Right=�6<   $��Center=�6S   $��0,0,5,5q�6q   .+�5q  &   ]� $	 SupplyDockPanel��60   ��5r  *   �� 5s  .   " ]�64   .�5t  2   ��  � y�$ 	Collapsed=�6O   6c   C 5u  2   �� 5v  6   #F Z^ 6�   G 6B   �� $) ResourceKindComboBox��$< SelectedItem�.a�5w  :   ��  � y�$ Visible=�6W   6i   5x  7   5z  /   .+�5|  *   ]� .+�5}  .   �� $��12(�68   $	= 5,1q�6F   #��Z_ 6U   $��SemiBold"�6�   $��Center=�6�   $��Center=�6�   6�   ]� 5~  .   $��90��69   $	��0,2q�6H   ` 6V   �� *Weight+.+�5�  2   �� $��12(�6:   $	��5,0q�6a   $��35��6n   $��22��6|   
������ #���  6�   #���  6�   $��1q�6)  $��Center=�6>  ��6H   �� *Amount+6b  �� 5�  2   $��12(�6;   #��Z? 6I   #���  6  #���  5�  3   #���  6s   $��1q�6�   @ 5�  �   �� *MeasurementUnit+A 6�   �� *MeasurementUnit+B 6�   �� *MeasurementUnit+��5�  6   �� 5�  :   " ��6@   84  .�5�  >   ��  � ��
 ���� 6r   C 5�  >   P� 5�  B   . �@J�	IsFocused/ . . �5�  F   ��  � ��# �  6d   6�   5�  C   5�  ;   5�  3   5�  /   5�  +   ]� 5�  *   .+�5�  .   �� $��12(�68   $	= 5,1q�6F   #��Za 6U   $��SemiBold"�6�   $��Center=�6�   $��Center=�6�   6�   ]� 5�  .   $��90��68   $	��0,2q�6G   ` 6T   �� *Volume+.+�5�  2   �� $��12(�6:   $	��5,0q�6`   $��22��6m   $��35��6z   
������ #���  6�   #���  6�   $��1q�6)  $��Center=�6=  ��6H   �� *Amount+6a  �� 5�  2   $��12(�6;   #��Z? 6I   #���  6�   #���  5�  ;   #���  6{   $��1q�6�   @ 5�  �   �� *MeasurementUnit+A 5�  4   �� *MeasurementUnit+B 6a   �� *MeasurementUnit+��5�  6   �� 5�  :   " ��6@   84  .�5�  >   ��  � ��
 ���� 6r   C 5�  >   P� 5�  B   . �@J�	IsFocused/ . . �5�  F   ��  � ��# �  6d   6�   5�  C   5�  ;   5�  3   5�  /   5�  +   5�  '   ]� 5�  &   $	 ManLaborDockPanel��60   ��5�  *   �� 5�  .   " ]�64   .�5�  2   ��  � y�$ 	Collapsed=�6O   6c   C 5�  2   �� 5�  6   #F Zb 6�   G 6B   �� $) ResourceKindComboBox��$< SelectedItem�.a�5�  :   ��  � y�$ Visible=�6W   6i   5�  7   5�  /   .+�5�  *   ]� $	��0,2q�65   .+�5�  .   �� $��12(�68   $	= 5,1q�6F   #��Zc 6T   $��SemiBold"�6�   $��Center=�6�   $��Center=�6�   6�   �� 5�  .   $��12(�67   $	��110��6E   #��Zd 6T   #���  6�   #���  5�  %   #���  6e   @ 5�  �   �� *	WageScale+5�  �   5�  +   ]� 5�  *   $	��0,2q�64   .+�5�  .   �� $��12(�68   $	= 5,1q�6G   #��Ze 6U   $��5,0,0,0q�6�   $��SemiBold"�6�   $��Center=�6�   $��Center=�6�   6�   �� 5�  .   $��12(�66   $��35��6`   $��22��6p   
������ #���  6�   #���  6�   $��1q�6  ��6D   �� *	MenNumber+63  5�  +   5�  (   ]� 5�  &   $	 EquipmentDockPanel��60   ��5�  *   �� 5�  .   " ]�64   .�5�  2   ��  � y�$ 	Collapsed=�6O   6c   C 5�  2   �� 5�  6   #F Zf 6�   G 6B   �� $) ResourceKindComboBox��$< SelectedItem�.a�5�  :   ��  � y�$ Visible=�6W   6i   5�  7   5�  /   .+�5�  *   ]� .+�5�  .   �� $��12(�68   $	= 5,1q�6F   #��Z_ 6U   $��SemiBold"�6�   $��Center=�6�   $��Center=�6�   6�   ]� 5�  .   $��90��68   $	��0,2q�6F   ` 6T   �� *Weight+.+�5�  2   �� $��12(�6:   $	��5,0q�6b   $��22��6o   $��35��6{   
������ #���  6�   #���  6�   $��1q�6*  $��Center=�6?  ��6H   �� *Amount+6c  �� 5�  2   $��12(�6;   #��Z? 6J   #���  6�   #���  5�  5   #���  6u   $��1q�6�   @ 5�  �   �� *MeasurementUnit+A 5�  4   �� *MeasurementUnit+B 6^   �� *MeasurementUnit+��5�  6   �� 5�  :   " ��6@   84  .�5�  >   ��  � ��
 ���� 6r   C 5�  >   P� 5�  B   . �@J�	IsFocused/ . . �5�  F   ��  � ��# �  6d   6�   5�  C   5�  ;   5�  3   5�  /   5�  +   5�  '   5�  #   5�     5�     5�     5	     5
     E 5     �� 5     $
F True��6�   G 6   �� *IsMouseOver+! �� *FindAncestor+$" BorderL�.a�5     �� $0 	textBlock��6   # �  6J    � ��5     �� 5     $
F True��6�   G 6   �� *
IsSelected+! �� *FindAncestor+$" TreeViewItemL�.a�5     �� $0 
MainBorder��6   # Zg 6L    � y��� 5     $0 	textBlock��6   # �  6J    � ��5     �� 5     $
F True��6�   G 6   �� *IsMouseOver+! �� *FindAncestor+$" TreeViewItemL�.a�5     �� $0 	textBlock��6   # �  6J    � ��5     5     �� 5  
   6'   �� 5!  
   h 6I   �� *Items.Items+5$     �� 5&  
   " Q�6;   .�5'     ��  � ��# �  6+   �� 5(      � ��# �  6+   �� 5)      � �� 6;   �� *HorizontalContentAlignment+! �� "" ���� 5*      � �� 69   �� *VerticalContentAlignment+! �� "" ���� 5+      �@Q�
IsExpanded 6+   �� *
IsExpanded+�� 5-      �@�ToolTip 6(   �� *Name+6A   �� 5/      � �� 50     �� 51     " Q�6&    52     ���� 54     CommandButtonBaseStyle " ��6C   .�55  "   ��  � ��
 ���� 6?   �� 56  "    � �� 6@   Z� �� 57  "    � ��$ 20��6:   �� 58  "    � ��$ 20��6;   �� 59  "    � ��# �  6?   �� 5;  "    � �� 5<  &   �� 5=  *   " ��6:   .R�5>  .   �� $ border��65   $��0q�6E   #��z��6Y   #��z��5?  5   #��z��6U   ._�5@  2   F� $
9 path��67   $��12��5A  7   $��12��6B   #��z��5B  7   $: Uniform=�5D  7   ��5C  7   �� *	Content+! �� *FindAncestor+$" ButtonL�5D  7   5E  /   5F  +   5H  #   5I     �� 5K     AddCommandButtonStyle " ��6B   84   C 5L  "   P� 5M  &   . �@J�IsMouseOver/ . . �5N  *   ��  � ��# �   6G   5O  '   P� 5P  &   . � ��/ . . �5Q  *   ��  � ��# � ! 6G   5R  '   5T     �� 5V     DeleteCommandButtonStyle " ��6E   84  C 5W  "   �� 5X  &   G 62   �� *
ItemKind+F Activity6O   .a�5Y  *   ��  � y�$ Hidden=�6G   5Z  '   P� 5[  &   . �@J�IsMouseOver/ . . �5\  *   ��  � ��# � " 6G   5]  '   P� 5^  &   . � ��/ . . �5_  *   ��  � ��# � ! 6G   5`  '   P� 5a  &   . � {�/ .  . �5b  *   ��  � ��
 �����6G   5c  '   5e     5f     .R�5h     � $# Bd��6    #��z��6,   
 5i     �� 5j  "   $
��Auto �60   6@   �� 5k  "   $
��Auto �60   6@   �� 5l  "   $
��Auto �60   6@   .+�5o     ��  . $	��Top=�6@   $��Disabled=�6U   ._�5q  &   � .+�5v  &   �� ._�5y  .   �� $2 PART_Header��6?   8�� $��Header��6�   $��Stretch=�6�   # z 6�   $��Center=�6  $��Disabled=�62  5z  +   �� 5{  &   $> commandsPanel��61   $��35��6I   $
��Auto��6U   $i 
Horizontal=�5|  1   $	��Top=�6K   $��Right=�6c   $��0,0,10,0q�5}  1   y�6C   �� *IsMouseOver+88  $) Bd��$ OneWay=�.+�5  .   �� $��Center=�69   $i 
Horizontal=�6V   .+�5�  2   �� #��� # 69   #5 Zj 5�  5   $����F1 M 51.5487,37.9814L 56.814,41.0534L 50.3383,48.7636L 50.3383,48.8841L 60.0205,46.9565L 60.0205,53.0403L 50.2777,51.414L 50.2777,51.5344L 56.814,59.0037L 51.4882,62.0757L 47.978,52.7994L 47.8569,52.7994L 44.4072,62.0757L 39.2025,59.0037L 45.6782,51.4742L 45.6177,51.3537L 36.1159,53.0403L 36.1159,46.9565L 45.5571,48.8841L 45.5571,48.7636L 39.2025,41.1137L 44.5888,37.9814L 47.9174,47.3179L 48.0385,47.3179L 51.5487,37.9814 Z M 20,17L 43.25,17L 56,29.75L 56,39L 52,36.75L 52,34L 39,34L 39,21L 24,21L 24,55L 41,55L 37.5,59L 20,59L 20,17 Z M 43,22.25L 43,30L 50.75,30L 43,22.25 Z ��5�  5   ��5�  5   �� *Items.AddCommand+5�  3   �� 5�  2   $  DeleteButton��69   #��� $ 6O   $����F1 M 25.3333,23.75L 50.6667,23.75C 51.5411,23.75 51.8541,27.3125 51.8541,27.3125L 24.1458,27.3125C 24.1458,27.3125 24.4589,23.75 25.3333,23.75 Z M 35.625,19.7917L 40.375,19.7917C 40.8122,19.7917 41.9583,20.9378 41.9583,21.375C 41.9583,21.8122 40.8122,22.9584 40.375,22.9584L 35.625,22.9584C 35.1878,22.9584 34.0416,21.8122 34.0416,21.375C 34.0416,20.9378 35.1878,19.7917 35.625,19.7917 Z M 27.7083,28.5L 48.2916,28.5C 49.1661,28.5 49.875,29.2089 49.875,30.0834L 48.2916,53.8334C 48.2916,54.7078 47.5828,55.4167 46.7083,55.4167L 29.2917,55.4167C 28.4172,55.4167 27.7083,54.7078 27.7083,53.8334L 26.125,30.0834C 26.125,29.2089 26.8339,28.5 27.7083,28.5 Z M 30.0833,31.6667L 30.4792,52.25L 33.25,52.25L 32.8542,31.6667L 30.0833,31.6667 Z M 36.4167,31.6667L 36.4167,52.25L 39.5833,52.25L 39.5833,31.6667L 36.4167,31.6667 Z M 43.1458,31.6667L 42.75,52.25L 45.5208,52.25L 45.9167,31.6667L 43.1458,31.6667 Z ��5�  5   ��5�  5   �� *DeleteMySelfCommand+��5�  5   �� 5 5�  5   �� *
DeleteText+5�  3   5�  /   5�  +   5�  '   5�     �� 5�     $k firstbottomLine��6(   $��1��6A   $��0q�6O   $��1��6�   #��� % 6�   $��Bottom=�6�   ��6Z   �� $) AtlasDataGrid��$< ActualWidth�6�   �� 5�     $l 	ItemsHost��6-   $��2��6@   $
��Auto��6M   $
��Left=�6[   $��	0,0,0,0.5q�6v   $y�	Collapsed=�6�   5�     - 5�     P� 5�     . �@Q�
IsSelected/ . . �5�  "   �� $0 Bd��6)   # �  6O    � ���� 5�  "   # �  6@    � ��5�     P� 5�     . �@Q�
IsExpanded/ . . �5�  "   �� $0 	ItemsHost��6O    � y�$ Visible=�6?   6O   5�     ]� 5�     m 5�  "   �� 5�  &   $n Bd��60   o �@J�IsMouseOverp . 6W   �� 5�  &   o �@Q�
IsSelectedp .  6G   /�5�  "   �� 5�  &   $0 Bd��6-   # �  6S    � ���� 5�  &   # �  6C    � ��5�     5�     5�     5�     5�     5�     