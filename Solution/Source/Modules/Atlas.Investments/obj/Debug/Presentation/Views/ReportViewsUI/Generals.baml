   M S B A M L   `   `   `  ����L  HAtlas.Investments, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:CompanyName.Atlas.Investments.Properties(CompanyName.Atlas.Investments.Properties  5      P LAtlas.UIControls, Version=0.7.12.24305, Culture=neutral, PublicKeyToken=nulleDclr-namespace:CompanyName.Atlas.UIControls;assembly=Atlas.UIControlsCompanyName.Atlas.UIControls 5      �Rclr-namespace:CompanyName.Atlas.Investments.Implementation.Presentation.ViewModelsDCompanyName.Atlas.Investments.Implementation.Presentation.ViewModels  5      �Iclr-namespace:CompanyName.Atlas.Investments.Presentation.Views.Converters;CompanyName.Atlas.Investments.Presentation.Views.Converters  5	      M    GCompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI.Generals   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      M
properties6clr-namespace:CompanyName.Atlas.Investments.Properties     5      [
uiControlsDclr-namespace:CompanyName.Atlas.UIControls;assembly=Atlas.UIControls     5      i
viewModelsRclr-namespace:CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels     5      `
convertersIclr-namespace:CompanyName.Atlas.Investments.Presentation.Views.Converters     5	         DataGrid.Static.Background#���   5      $��1q�6I     Default.FontSize#���  5        Default.FontFamily#���  6<     DataGrid.Static.Foreground#���  6n     � 	Resources  5      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value + 'DataGridRowBottomLine.Static.Background  ColumnFloatingHeaderStyle " ColumnHeaderDropSeparatorStyle  DataGrid.Static.FontSize �� 
TargetType * &DataGridColumnHeader.Static.Background �� Padding ��Name � RowDefinitions � ColumnDefinitionsG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter	  Name7  1System.Windows.Controls.DataGridHeadersVisibility
   Column �� ConverterParameter!   HeadersVisibilityConverter �� 	Converter �� RelativeSource �� AncestorType ��Name �� CanContentScroll ��Name �� Orientation �� ScrollableHeight � Maximum �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility � Value �� Mode �� ScrollableWidth �� ViewportWidth, �� %ComputedHorizontalScrollBarVisibility J� SnapsToDevicePixels �� Triggers  P� Property! P� Value 	 DataGridCellRighted*  $System.Windows.Controls.DataGridCell 
 DataGridCellBackground  DataGridCellForeground" �� Path# �� Binding$ �� Value $  DataGridCell.Selected.Background $  DataGridCell.Selected.Foreground  DataGridColumnHeaderLeft=  7System.Windows.Controls.Primitives.DataGridColumnHeader . *PlanningExecutionTabItem.Static.Foreground , (DataGridColumnHeader.DarkDark.Foreground% �Name& F�Name' �� Stretch( J� Opacity) J� RenderTransformOrigin* n�Name  ColumnHeaderGripperStyle+ �� Triggers, �� 
TargetName- �� Angle # DataGridHeaderBackgroundPressed # DataGridHeaderForegroundPressed  DataGridColumnHeaderRighted)  #System.Windows.Controls.DataGridRow. ��Name?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid?  9System.Windows.Controls.Primitives.DataGridCellsPresenter/   
ItemsPanelA	  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter0   DetailsVisibility$1   SelectiveScrollingOrientation;
  5System.Windows.Controls.SelectiveScrollingOrientation2 
  Vertical#3   RowDetailsScrollingConverter % !DataGridCell.MouseOver.Background  RowLabelStyle 0 ,PlanningExecutionTabControlHeader.Background  RowLabelStyle2 6 2InvestmentComponentPropertiesRow.Static.Foreground  TextBlockStyle  InvestmentDataTemplate4 �� DataType^   XCompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.InvestmentPresenter5 � 	Resourcesb   \CompanyName.Atlas.Investments.Presentation.Views.Converters.DateToToShortDateStringConverter  Key8   2CompanyName.Atlas.Investments.Properties.Resources6   InvestmentSheet7   InvestmentName_Colon8   Code9   AuthorOrEmitter_Colon:   Entity_Colon;   Objective_Colon<   Scope_Colon=   
Osde_Colon>   
Oace_Colon?   Constructor_Colon@   Location_ColonA   PlannedPeriodB �� OrientationC � DataContextD   RelatedPrograms_ColonE   ClassifiedByF   InvestmentType_Colon # DataGrid.Generic.Row.BackgroundG   ByNatureH   ByImpactI   Capacity_ColonJ   InducedDoings_ColonK   Phase # InvestmentComponentDataTemplate  Window.Default.Backgroundc   ]CompanyName.Atlas.Investments.Presentation.Views.Converters.InvestmentElementTemplateSelector %  !InvestmentElementTemplateSelector ! DataGridCell %" !DataGridCell.MouseOver.Foreground��%�F  '        7  7  &		 s
    &	 /    7  7  &	 �    7  7  '  ,    &	 P!    &	 �"    &	 �#    &	 Q%    7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  &	 b8    7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  &	  LA    &	! _A    �� 5   
   "  6*   .�5      ��  � ��# �   6+   �� 5       � ��# �  6+   �� 5       � ��$ 0q�60   �� 5        �@ RowDetailsVisibilityMode$ VisibleWhenSelected=�69   �� 5        �@ HorizontalGridLinesBrush# �  69   �� 5       �@ VerticalGridLinesBrush# �  67   �� 5       �@ DragIndicatorStyle8   63   �� 5      " �@ DropLocationIndicatorStyle8  6;   �� 5       �@ GridLinesVisibility$
 None=�64   �� 5       � �� . 6>   �� 5       �@��PanningMode$
 Both=�69   �� 5       �@��IsFlicksEnabled .  67   �� 5       �@ AutoGenerateColumns .  64   �� 5        � ��# �  6+   �� 5!       � ��# �  6)   �� 5"       � �� 5#      �� 5$      "  6&   .R�5%      � $��0,0,0,0q�6   .+�5&      ]� $��0,0,0,0q�6(   .+�5'   "   �� $	��Top=�6)   #���  5(   	   $��35��5)   	   $��0q�6   �� 5*   "   $	��Top=�6)   #��z��5+   	   $��0q�63   $ 0q�6>   5,      �� 5-      $ DG_ScrollViewer��6=   |�.  $
��Auto��6V   $��0q�6d   ��5.   "   �� 5/   &   " ��66   .R�50   *   � $��0,0,0,0q�6/    51   .   �� 52   2   $
��Auto �6@   �� 53   2   $��* �6@   �� 54   2   $
��Auto �6@    57   .   �� 58   2   $
��Auto �6C   �� 59   2   $��* �6C   �� 5:   2   $
��Auto �6C   .+�5=   .     $!	 PART_ColumnHeadersPresenter��5>      $��1��5=   M   $��2��6]   $
��Left=�5>   1   $��0q�5?   �   $��0q�6�   y�6   �� *HeadersVisibility+# Z
 # Z  �� "  6�   �� 5A   .   $! PART_ScrollContentPresenter��6E   $��1��6j   $��2��6w   $��0��5B   A   $��0q�6Q   # z��5C   A   �� 5E   .   $ PART_VerticalScrollBar��6U   $��1��68   $��2��6E   $ Vertical=�5F      # z 5G      # z 5H      #y�z 5J       5I      �� *VerticalOffset+$ OneWay=� �� *TemplatedParent+5J      � 5L   .   $��2��63   $��1��6@    5M   2   �� 5N   6   ��6G   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "  �� 5O   6   $��* �6G   .+�5Q   2   �� $ PART_HorizontalScrollBar��5R      $��1��5Q   <   $ 
Horizontal=�5S      # z 5T      # z 5U      #y�z 5W       5V      �� *HorizontalOffset+$ OneWay=� �� *TemplatedParent+5W      5X   /   5Y   +   5Z   '   .��5\   "   �� # z 61   $��0q�6m   5]      5^      5_      5a      �� 5b       �@ CanUserAddRows .  6/   �� 5c       �@ CanUserResizeRows .  62    5d      P� 5e        �@��
IsGrouping! . . �5f      ��  � �� .  6F   5g      5j      �� 5k   
   "  6,   .�5l      ��  � ��# � 
 6+   �� 5m       � ��# �  6+   �� 5n       � ��$ 0q�60   �� 5o       � ��$ Center=�69   �� 5p       � ��$ Right=�6;   �� 5q       � ��$ 35��6*   �� 5r       � ��$ 35��6*   �� 5s       �@�FocusVisualStyle 61   Z� �� 5t       � �� 6+   ��  �� $ DataGridColumnHeaderL�$" 
FontWeight��� 5u       � �� 5v      �� 5w      "  6&   .R�5x      �� #��z��6!   #��z��5y      #��z��5z      $ 6,0,12,0q�5{       . ._�5}      �� # z 61   $	��6,0q�6m   #��z��5~      #��z��6Y   5      5�      5�       5�      �� 5�      # 6   ��  �� $ DataGridRowL�$" Item.Background�$ True6�   .a�5�      ��  � ��$
 Bold"�63   5�      P� 5�        �@ 
IsSelected! . . �5�      ��  � ��# �  63   �� 5�       � ��# �  63   5�      5�      �� 5�   
   "  61   .�5�      ��  � ��# �  6+   �� 5�       � |� .  6*   �� 5�       � ��# �  6+   �� 5�       � ��$ 0q�60   �� 5�       � ��$ 35��6'   �� 5�       � ��# �  6+   �� 5�       � ��# �  6)   �� 5�       � ��$	 0,0q�6'   �� 5�       � ��$
 Left=�6;   �� 5�       � ��$ Center=�69   �� 5�       � �� 5�      �� 5�      "  6&   .R�5�      � .+�5�      �� #��z��6%   #��z��5�      #��z��5�      # z��5�      ._�5�   "   � $% containerGrid��6'   #��z��6>    5�   &   �� 5�   *   $��* �6;   �� 5�   *   $
��Auto �6;   .+�5�   &   �� $��0��67   $
��12,0q�6G   #��z��5�   
   # z 5�   
   #��z��5�   
   ��5�   *   �� 5�   .   " ��64   .�5�   2   ��  � �� 5�   6   �� 5�   :   .R�5�   >   �� #��z��6H   $
��Bold"�6i   6}   5�   ;   5�   3   5�   /   5�   '   F� 5�   &   $& 	SortArrow��6+   $��1��5�   
   $��Center=�5�   
   $��Center=�6'   $��8��5�   
   $��6��6   $��5,0,5,0q�6    $
' Fill=�5�   
   $	( 0.5Q�6   #��z��6'   $) 0.5,0.4&�5�   
   $y�Hidden=�5�   
   "����        �@B     � � @B 	5�   
   5�   #   5�      n� 5�      $* PART_LeftHeaderGripper��6$   $
��Left=�5�      8��  5�      n� 5�      $* PART_RightHeaderGripper��6$   $��Right=�5�      8�� 5�      5�      + 5�      P� 5�        �@ SortDirection$! 	AscendingX�6?   . �5�   "   �� $, 	SortArrow��6)    � y�$ Visible=�6V   �� 5�   "   $, 	SortArrow��6)    � z� 5�   &   �� 5�   *   $	- 180Q�6:   5�   #   5�      P� 5�        �@ SortDirection$! 
DescendingX�6?   . �5�   "   �� $, 	SortArrow��6)    � y�$ Visible=�6V   5�      5�      5�       5�      P� 5�        � ��! . . �5�      ��  � ��# �  63   �� 5�       � ��# �  63   5�      5�      �� 5�   
   "  64   .�5�      ��  � ��# �  6+   �� 5�       � |� .  6*   �� 5�       � ��# �  6+   �� 5�       � ��$ 0q�60   �� 5�       � ��$ 35��6'   �� 5�       � ��# �  6+   �� 5�       � ��# �  6)   �� 5�       � ��$	 0,0q�6'   �� 5�       � ��$ Right=�6;   �� 5�       � ��$ Center=�69   �� 5�       � �� 5�      �� 5�      "  6&   .R�5�      � .+�5�      �� #��z��6%   #��z��5      #��z��5     # z��5     ._�5  "   � $% containerGrid��6'   #��z��6>    5  &   �� 5  *   $��* �6;   �� 5  *   $
��Auto �6;   .+�5	  &   �� $��0��67   $
��12,0q�6G   #��z��5
  
   # z 5  
   #��z��5  
   ��5  *   �� 5  .   " ��64   .�5  2   ��  � �� 5  6   �� 5  :   .R�5  >   �� #��z��6H   $
��Bold"�6i   6}   5  ;   5  3   5  /   5  +   F� 5  &   $& 	SortArrow��6+   $��1��5  
   $��Center=�5  
   $��Center=�6'   $��8��5  
   $��6��6   $��5,0,5,0q�6    $
' Fill=�5  
   $	( 0.5Q�6   #��z��6'   $) 0.5,0.4&�5  
   $y�Hidden=�5   
   "����        �@B     � � @B 	5!  
   5"  #   5#     n� 5$     $* PART_LeftHeaderGripper��6$   $
��Left=�5%     8��  5&     n� 5'     $* PART_RightHeaderGripper��6$   $��Right=�5(     8�� 5)     5*     + 5+     P� 5,       �@ SortDirection$! 	AscendingX�6?   . �5-  "   �� $, 	SortArrow��6)    � y�$ Visible=�6V   �� 5.  "   $, 	SortArrow��6)    � z� 5/  &   �� 50  *   $	- 180Q�6:   52  #   53     P� 54       �@ SortDirection$! 
DescendingX�6?   . �55  "   �� $, 	SortArrow��6)    � y�$ Visible=�6V   56     58     5:      5<     P� 5A       � ��! . . �5B     ��  � ��# �  63   �� 5C      � ��# �  63   5D     5G     �� 5H  
   "  6-   .�5I     ��  � ��
 ���� 6+   �� 5K      �@J�SnapsToDevicePixels . 64   �� 5L      �@=�ErrorTemplate 69   Z� �� 5M      � �� 5N     �� 5O     "  6&   .R�5P     �� $. 
DGR_Border��6!   #��z��5Q     #��z��5R     #��z��5S      . $
��Left=�5T  "   ._�5U        5V  "   �� 5W  &   $
��Auto �67   �� 5X  &   $��* �67    5[  "   �� 5\  &   $��* �64   �� 5]  &   $
��Auto �64   �� 5^  &   $
��Auto �64   .+�5a  "     $��1��69   #��z/ 5b  	   # z 5c  	   #��z��6E   	  5e  "   $��1��5f  7   $��1��6G   #y�z0 6T   1 5e  ;   �� *AreRowDetailsFrozen+# Z2 # Z3  �� "  5g  #   �� 5h  "   $��2��6,   $��2��69   #���  6N   $��1��6�   6�   5j     5k     + 5m     �� 5n     $
$ True��6x   # 6*   ��  �� *Self+$" Item.Background�.a�5o  "   �� $, 
DGR_Border��6)   # �  6W    � ���� 5p  "    � ��$ DemiBold"�6?   5q     5t     5v     5w     �� 5y  
   " ��6&   .�5z     ��  � ��# �  6+   �� 5{      � |� .  6*   �� 5|      � ��# �  6+   �� 5}      � ��$ 12,0,9,0q�6(   �� 5~      � ��$ Center=�69   �� 5      � ��$ 0,0,0,1q�60   �� 5�      � ��# �  6,   �� 5�      � ��$ 35��6'   5�     �� 5�  
   " ��6'   .�5�     ��  � ��# �  6+   �� 5�      � |� .  6*   �� 5�      � ��# �   6+   �� 5�      � ��$ 12,0,9,0q�6(   �� 5�      � ��$ Center=�69   �� 5�      � ��$ 0,0,0,1q�60   �� 5�      � ��# �  6,   �� 5�      � ��$ 35��6'   5�     �� 5�  
   " ��6'   .�5�     ��  �@��Padding$	 5,0q�6(   �� 5�      �@J�	AllowDrop . 6*   �� 5�      �@�FocusVisualStyle 61   Z� �� 5�      �@��PanningMode$ VerticalFirst=�69   �� 5�      �@��IsFlicksEnabled .  67   �� 5�      � ��$ Center=�62   �� 5�      � ��$ 35��6'   5�     �� 5�  
   "4  66   5 5�     ��  5�     $ DateToToShortDateStringConverter 6>   5�     .R�5�     � #���   6    5�     �� 5�     $
��Auto �6'   $	��200��64   �� 5�     $
��Auto �6'   �� 5�      5�     �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $��* �6$   .+�5�     �� $��0��6   $��1��6*   $��18��6:   $��5��6L   #���   6V   5�     �� 5�     $��3��6   $��0��6,   #��Z6 5�     8��  $��16(�5�  @   �� 5�     $��0��6   $��1��6(   #��Z7 5�     8�� 5�     �� 5      $��1��6   $��2��6%   8�� ��5     �� *Name+�� 5     $��0��6   $��2��6(   #��Z8 5     8�� 5     �� 5     $��2��6   $��2��6%   8�� ��5     �� *Code+�� 5	     $��0��6   $��3��6(   #��Z9 5
     8�� 5     �� 5     $��3��6   $��2��6%   8�� ��5     �� *AuthorOrEmitter+�� 5     $��0��6   $��4��6(   #��Z: 5     8�� 5     �� 5     $��4��6   $��2��6%   8�� ��5     �� *Entity+�� 5     $��0��6   $��5��6(   #��Z; 5     8��	 5     �� 5     $��5��6   $��2��6%   8��
 ��5     �� *	Objective+�� 5     $��0��6   $��6��6(   #��Z< 5     8�� 5     �� 5     $��6��6   $��2��6%   8�� ��5     �� *Scope+�� 5!     $��0��6   $��7��6(   #��Z= 5"     8�� 5#     �� 5(     $
��Osde��6:   $��7��6   $��2��6*   8�� ��6F   �� *Osde+5)     �� 5-     $��0��6   $��8��6(   #��Z> 5.     8�� 5/     �� 50     $
��Oace��6   $��8��6%   $��2��62   8�� ��6B   �� *Oace+6�   �� 53     $��0��6   $��9��6(   #��Z? 54     8�� 55     �� 56     $��9��6   $��2��6%   8�� ��57     �� *Constructor+�� 59     $��0��6   $��10��6(   #��Z@ 5:     8�� 5;     �� 5<     $��10��6   $��2��6&   8�� ��5=     �� *
Location+�� 5?     $��0��6   $��11��6(   #��ZA 5@     8�� 5A     �� 5B     $��11��6   $��2��6(   $	 5,0q�68   $��0,0,0,1q�6F   #���  6`   ._�5C     �� $B 
Horizontal=�6!   C 6;   �� *Period+.+�5D     �� 8�� $��0,0,0,0q�6h   ��6    �� *ShortStarts+6�   �� 5E     $��  -  ��6$   $��35��61   $��Center=�6=   #���  6X   6�   �� 5F     8�� $��0,0,0,0q�6f   ��6    �� *	ShortEnds+6�   5G     5H     �� 5L     $��0��6   $��12��6(   #��ZD 5M     8�� 5N     �� 5O     $��12��6   $��2��6&   8�� ��5P     �� *RelatedPrograms+�� 5V     $��0��6   $��13��6(   $��2��66   #��ZE 5W     8�� $
��Auto��5X  ?   $��Stretch=�6M   $��Center=�6i   � 5Y     $��13��6   $��2��6&   #���   66    5Z     �� 5[     $	��170 �6+   69   �� 5\     $��5 �6+   67   �� 5]     $	��170 �6+   69   �� 5^     $��5 �6+   67   �� 5_     $	��170 �6+   6:   �� 5`     $��* �6,   68   .+�5c     �� $��0��6   #��ZF 5d     #���  6X   8�� 5e     �� 5f     $��2��6   #��ZG 5g     #���  6L   8�� 5h     �� 5i     $��4��6   #��ZH 5j     8�� #���  5k  @   5m     � 5p     $��14��6   $��2��6'   #���   67    5q     �� 5r     $	��170 �6,   6:   �� 5s     $��5 �6+   67   �� 5t     $	��170 �6+   69   �� 5u     $��5 �6+   67   �� 5v     $	��170 �6+   69   �� 5w     $��* �6,   68   .+�5{     �� $��0��6   8�� ��60   �� *InvestmentType+6S   �� 5|     $��0,0,0,1q�6   $��1��69   #���  6I   6�   �� 5~     $��2��6   8�� ��6.   �� *Nature+6I   �� 5     $��0,0,0,1q�6   $��3��6:   #���  6J   6�   �� 5�     $��4��6   8��  ��6/   �� *Impact+6J   �� 5�     $��0,0,0,1q�6   $��5��6:   #���  6J   6�   5�     �� 5�     $��0��6   $��15��6(   #��ZI 5�     8��! 5�     �� 5�     $��15��6   $��2��6&   8��" ��5�     �� *
Capacity+�� 5�     $��0��6   $��16��6(   #��ZJ 5�     8��# 5�     �� 5�     $��16��6   $��2��6&   8��$ ��5�     �� *InducedDoings+�� 5�     $��0��6   $��17��6(   #��ZK 5�     8��% 5�     �� 5�     $��Phase��68   $��17��6   $��2��6(   8��& ��6F   �� *Phase+6�   5�     5�     �� 5�  
   .R�5�     � #���   6    5�     �� 5�     $
��Auto �6'   $	��200��64   �� 5�     $
��Auto �6'   �� 5�      5�     �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $
��Auto �6$   �� 5�     $��* �6$   .+�5�     �� $��0��6   $��1��6*   $��9��6:   $��5��6K   #���  6U   5�     �� 5�     $��3��6   $��0��6,   #��Z6 5�     8��  $��16(�5�  @   �� 5�     $��0��6   $��1��6(   #��Z7 5�     8�� 5�     �� 5�     $��1��6   $��2��6%   8�� ��5�     �� *Name+�� 5�     $��0��6   $��2��6(   #��Z8 5�     8�� 5�     �� 5�     $��2��6   $��2��6%   8�� ��5�     �� *Code+�� 5�     $��0��6   $��3��6(   #��Z; 5�     8�� 5�     �� 5�     $��3��6   $��2��6%   8�� ��5�     �� *	Objective+�� 5�     $��0��6   $��4��6(   #��Z< 5�     8�� 5�     �� 5�     $��4��6   $��2��6%   8�� ��5�     �� *Scope+�� 5�     $��0��6   $��5��6(   #��Z= 5�     8��	 5�     ]� 5�     $��5��6   $��2��6)   .+�5�     �� $
��Osde��6   8��
 ��6$   �� *Osde+6g   5�     �� 5�     $��0��6   $��6��6(   #��Z> 5�     8�� 5�     ]� 5�     $��6��6   $��2��6)   .+�5�     �� $
��Oace��6   8�� ��6(   �� *Oace+5�     5�     �� 5�     $��0��6   $��7��6(   #��Z@ 5�     8�� 5�     �� 5�     $��7��6   $��2��6%   8�� ��5�     �� *
Location+�� 5�     $��0��6   $��8��6(   #��Z? 5�     8�� 5�     �� 5�     $��8��6   $��2��6%   8�� ��5�     �� *Constructor+�� 5�     $��0��6   $��9��6(   #��ZA 5�     8�� 5�     �� 5�     $��9��6   $��2��6'   $	 5,0q�67   $��0,0,0,1q�6E   #���  6_   ._�5�     �� $B 
Horizontal=�6!   C 6;   �� *Period+.+�5�     �� 8�� ��6    �� *ShortStarts+6k   �� 5�     $��  -  ��6$   $��35��61   6?   �� 5�     8�� ��6    �� *	ShortEnds+6i   5�     5�     5�     5        5  
   67   �� 5  
   "  6%   .�5     ��  � ��# �  6+   �� 5      � |� .  6*   �� 5      � ��# �  6+   �� 5      � ��$ 0q�60   �� 5      � ��$ Center=�69   �� 5	      � ��$ 35��6*   �� 5
      � ��$ 35��6*   �� 5      �@�FocusVisualStyle 61   Z� �� 5      � �� 5     �� 5     "  6&   .R�5     �� #��z��6!   #��z��5     #��z��5     $ 6,0,0,0q�5      . ._�5     �� # z 61   $	��6,0q�6m   #��z��5     5     5     5      5     P� 5       �@J�IsMouseOver! . . �5     ��  � ��# �  63   �� 5      � ��# � " 63   5     P� 5        �@ 
IsSelected! . . �5!     ��  � ��# �  63   �� 5"      � ��# �  63   5#     �� 5%     # 6   ��  �� $ DataGridRowL�$" Item.Background�$ True6�   .a�5'     ��  � ��$
 Bold"�63   5(     5,     5.     .��55     �� $
��Auto=�6   $
��Auto=�68   .��56  
   � -   5        $% 	PrintGrid��56      57     �� 58     $
��Auto �6    60   �� 59     $
��Auto �6    60   �� 5:     $
��Auto �6    60   �� 5;     $��* �6    6-   .+�5T     �� -   5        L ��Name$L ContentPlace��5T     $��3��64   � 5V     $��1��6   #���  6!   $��0,35,0,0q�6i   .+�5W     � $
��10,5q�6    5Y     �� 5Z     $	��100 �6+   �� 5[     $	��100 �6+   �� 5\     $	��150 �6+    5^     �� 5_     $
��Auto �6(   �� 5`     $
��Auto �6(   .+�5d     �� M   Planned#��ZM 6    #���  6O   6�   �� 5f     $��1��6    N   Executed#��ZN 60   #���  6`   6�   �� 5h     $��2��6    O   	Execution#��ZO 60   #���  6a   6�   �� 5j     $��1��6    #���  6J   ��6-   �� *PlannedCost+6�   �� 5l     $��1��6    $��1��6-   #���  6[   ��6=   �� *ExecutedCost+6�   ]� 5n     $��1��6    $��2��6-   .+�5o     �� #���  6F   ��6$   �� *ExecutionPercent+6�   �� 5p     $�� %��6$   #���  6/   6{   5q     5t     5u     � 5w     $��2��6   $	��0,1q�6P   $
��Left=�6]   C 6!   �� *BudgetSummaryPresenter+ 5x     �� 5y     $��35 �6$   62   �� 5z     $��* �6$   61    5|     �� 5}     $
��Auto �6'   66   �� 5~     $��* �6'   63   .+�5�     �� $ 10,0,0,0q�6   
�������._�5�     �� P   Summary#��ZP 6    $��Center=�6O   6l   5�       5�     -   5        Q  Name$Q SummaryDataGrid��5�     $��1��64   
������ R   AutoGenerateColumnsR .  S   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
S Auto 6�   $
��Auto��6�   T   
IsReadOnlyT . U   CanUserSortColumnsU .  $
��Left=�5�  N   V   CanUserAddRowsV .  W   CanUserResizeRowsW .  X   CanUserReorderColumnsX .  Y   CanUserResizeColumnsY .  ��5�  A   �� *BudgetSummary+  5�     ��%�  '        �� 5�     "  6>   .�5�     ��  � ��# �  6;   �� 5�      � |� .  6:   �� 5�      � ��# �  6;   �� 5�      � ��$ 0q�6@   �� 5�      � ��$ Center=�6I   �� 5�      � ��$ 35��6:   �� 5�      � ��$ 35��6:   �� 5�      �@�FocusVisualStyle 6A   Z� �� 5�      � �� 5�  "   �� 5�  &   "  66   .R�5�  *   �� #��z��61   #��z��5�     #��z��5�     $ 6,0,0,0q�5�      . ._�5�  .   �� # z 6A   $	��6,0q�6}   #��z��5�     5�  +   5�  '   5�      5�     P� 5�  "     �@J�IsMouseOver! . . �5�  &   ��  � ��# �  6C   �� 5�  &    � ��# � " 6C   5�  #   P� 5�  "     �@ 
IsSelected! . . �5�  &   ��  � ��# �  6C   �� 5�  &    � ��# �  6C   5�  #   �� 5�  "   # 6.   ��  �� $ DataGridRowL�$" Item.Background�$ True6�   .a�5�  &   ��  � ��$
 Bold"�6C   5�  #   5�     5�     5�     � 5�     -   5        $��
PeriodGrid��5�  8   $��2��6   $��1��6(    5�     �� 5�     $
��Auto �6(   68   �� 5�     $��* �6(   65   5�     5�     5�     5�     5     