   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType �� BasedOn �� 
ResourceId �� TypeInTargetAssembly&    System.Windows.Controls.DataGrid �� Property �� ValueG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader  Name J� IsHitTestVisible   ColumnHeaderGripperStyle	 �� Padding  RowHeaderGripperStyle
 F�Name J� RenderTransformOrigin �� Angle  ColumnFloatingHeaderStyle ��Name " ColumnHeaderDropSeparatorStyle  DataGridDropSeparator J� SnapsToDevicePixels)  #System.Windows.Controls.DataGridRow ��Name?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions0   *CompanyName.Atlas.UIControls.AtlasDataGrid    HeadersVisibilityA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter#    RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType �� Triggers P� 
SourceName P� Property P� Value �� 
TargetName % !DataGridCell.MouseOver.Background?  9System.Windows.Controls.Primitives.DataGridCellsPresenter*	  $System.Windows.Controls.DataGridCell  DataGridCellBackground  DataGridCellForeground  �� Triggers % !DataGridCell.MouseOver.Foreground $	  DataGridCell.Selected.Background $
  DataGridCell.Selected.Foreground * &DataGridColumnHeader.Static.Background * &DataGridColumnHeader.Static.Foreground  Default.FontFamily  DataGrid.Static.FontSize! �Name" �� Stretch# J� Opacity$ n�Name ! DataGridHeaderBackgroundHover ! DataGridHeaderForegroundHover # DataGridHeaderBackgroundPressed # DataGridHeaderForegroundPressed:
  4System.Windows.Controls.Primitives.DataGridRowHeader  DataGridHeaderBackground  DataGridHeaderForeground  DataGridGridLines $  DataGridHeaderBackgroundSelected $  DataGridHeaderForegroundSelected ) %DataGridTextColumnEditingElementStyle  DataGrid.Static.Foreground % !DataGridTextColumn.SelectionBrush  DataGrid.Static.Background + 'DataGridRowBottomLine.Static.Background% ��Name&  Name7  1System.Windows.Controls.DataGridHeadersVisibility'   Column!(    HeadersVisibilityConverter) ��Name* �� CanContentScroll+ ��Name, �� Orientation- �� ScrollableHeight. � Maximum/ �� ViewportHeight0 �� ViewportSize*1 �� #ComputedVerticalScrollBarVisibility2 � Value3 �� Mode4 �� ScrollableWidth5 �� ViewportWidth,6 �� %ComputedHorizontalScrollBarVisibility " DataGridCommandsColumnTemplate  DataGridAddButtonStyle7 � ToolTip  DataGridDeleteButtonStyle8    DeleteCommand9 �� Triggers: �� Value; �� Binding *  &DataGridDeleteButton.Static.Background *! &DataGridDeleteButton.Static.Foreground< ��Name= � FocusVisualStyle -" )DataGridDeleteButton.MouseOver.Foreground +# 'DataGridDeleteButton.Pressed.Foreground ,$ (DataGridDeleteButton.Disabled.Foreground  % DataGridAddButton.Foreground & Buttom.MouseOver.Foreground ' Buttom.Pressed.Foreground�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      7controls*clr-namespace:CompanyName.Atlas.UIControls  5      8d2http://schemas.microsoft.com/expression/blend/2008  5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      %d;  (��       5      $" DataGridSelectAllButtonStyle��"   )7��'  r     &	  F    &	 \    &	 �    &	 =    '      '  I    '	  �    '  �    7   7   '
  �    7  7  &	 p    '   �    7  7  &	 :%    &	 [,    7 $ &	 �/    '  '3    7  �� 6   "  ��5	      8   .�5
   
   ��  � ��
 ���� 6'   �� 5   
    � ��$ 0q�6,   5      �� 5      "   6<   .�5   
   ��  � �� 5      �� 5      "  6"   .R�5      � .+�5        $ PART_FillerColumnHeader��6H    .  �� 5      5      5      5      5      �� 5      "  n�6-   .�5   
   ��  � ��$ 8��6"   �� 5   
    � ��
 ���� 6'   �� 5   
    �@�Cursor$ SizeWE��6#   �� 5   
    � �� 5       �� 5!      " n�6"   .R�5"      �� #	 z��6   #��z��5#      5$      5&      5'      �� 5)      "  n�6*   .�5*   
   ��  � ��$ 8��6#   �� 5+   
    � ��
 ���� 6'   �� 5,   
    �@�Cursor$ SizeNS��6#   �� 5-   
    � �� 5.      �� 5/      " n�6"   .R�50      �� #	 z��6   #��z��51      ._�56      F� $
 path1��6   V����m݀����4�l �8T笪*@    Ѱ�� �x  D2��j@7C`�C@Ѱ�� �� �ȫSU�@XX� 	
�������$��0��6�   $ 0.5,0.5&�6�   $��Center=�6�   $��Center=�6�   $��0,3,0,0q�6  z�57      V� 58   "   .~�59   &   �� �� 5:   &   �� 5;   &   $	 -90Q�66   T� 5<   &   5=   #   5?      5@      5B      5D      5E      �� 5G      "  ��6.   .�5H   
   ��  �@J�Opacity$	 0.6Q�6$   �� 5I   
    � ��$ 0q�6,   �� 5J   
    � �� 5K      �� 5L      " ��6"   .R�5M      �� #��z��6   #��z��5N      #	 z��5O      #��z��5P      ._�5Q      � .+�5R      �� $ PART_VisualBrushCanvas��6%   #��z��6E   5S      5T      5U      5W      5X      �� 5Z      "  ��63   .�5[   
   ��  � ��# �  6'   �� 5\   
    � �� 5]      �� 5^      " ��6"   .R�5_      �� #��z��6   #��z��5`      #��z��5a       . 5b      5c      5e      5f      �� 5h      "   6)   .�5i   
   ��  � ��
 ���� 6'   �� 5j   
    � ��$ 35��6&   �� 5k   
    �@J�SnapsToDevicePixels . 60   �� 5l   
    �@=�ErrorTemplate 65   Z� �� 5m   
    � �� 5n      �� 5o      "  6"   .R�5p      �� $ 
DGR_Border��6   #��z��5q      #��z��5r      #��z��5s       . $
��Left=�5t   "   ._�5u         5v      �� 5w   "   $
��Auto �63   �� 5x   "   $��* �63    5{      �� 5|   "   $��* �60   $��70��6;   �� 5}   "   $
��Auto �60   .+�5�      � $��1��6#    5�   "   �� 5�   &   $��* �64   �� 5�   &   $
��Auto �64   .+�5�   "     $��1��69   $
 None=�6F   ��6_   �� *PlannedResources+6�   5�        5�      $��1��5�   7   $��1��6G   #y�z 6T    5�   7   �� *AreRowDetailsFrozen+# Z # Z  �� "   5�      5�      5�       5�      P� 5�      $ 
DGR_Border��6F    �@J�IsMouseOver . . �5�      �� $ 
DGR_Border��6%   # �  6S    � ��5�      5�      5�      5�      �� 5�      "   64   .�5�   
   ��  � �� 5�      �� 5�      "  6"   .R�5�      �� 5�      5�      5�      �� 5�      "  	 6*   .�5�   
   ��  � ��# �  6'   �� 5�   
    � ��# �  6'   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��$ Center=�65   �� 5�   
    �@�FocusVisualStyle 6-   Z� �� 5�   
    � �� 5�      �� 5�      " 	 6"   .R�5�      �� #��z��6   #��z��5�      #��z��5�      #	 z��5�       . ._�5�      �� # z 6/   $
��10,0q�6k   #��z��5�      5�      5�      5�        5�   
   P� 5�       �@J�IsMouseOver . . �5�      ��  � ��# �  6/   �� 5�       � ��# �  6/   5�      P� 5�       �@	 
IsSelected . . �5�      ��  � ��# � 	 6/   �� 5�       � ��# � 
 6/   5�      5�      �� 5�      "   62   .�5�   
   ��  � ��# �  6'   �� 5�   
    � ��# �  6'   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��$ 35��6#   �� 5�   
    � ��# �  6'   �� 5�   
    � ��# �  6%   �� 5�   
    � ��$
 Left=�67   �� 5�   
    � ��$ Center=�65   �� 5�   
    � �� 5�      �� 5�      "  6"   .R�5�      � .+�5�      �� #��z��6!   #��z��5�      #��z��5�      #	 z��5�      ._�5�      � $! containerGrid��6#   $��5q�6:    5�   "   �� 5�   &   $��* �67   �� 5�   &   $
��Auto �67   .+�5�   "   �� $��0��63   $	��7,0q�6C   #��z��5�   
   # z 5�   
   #��z��5�   
   F� 5�   "   $
 	SortArrow��6'   $��1��5�   
   $��Center=�5�   
   $��Center=�6'   $��8��5�   
   $��6��6   $��5,0,5,0q�6    $
" Fill=�5�   
   $	# 0.5Q�6   #��z��6'   $ 0.5,0.4&�5�   
   $y�Hidden=�5�   
   "����        �@B     � � @B 	5�   
   5�      5�      n� 5�      $$ PART_LeftHeaderGripper��6    $
��Left=�5�      8��  5�      n� 5�      $$ PART_RightHeaderGripper��6    $��Right=�5�      8�� 5�      5       5     P� 5      �@ SortDirection$ 	AscendingX�6;   . �5     �� $ 	SortArrow��6%    � y�$ Visible=�6R   �� 5     $ 	SortArrow��6%    � z� 5  "   �� 5  &   $	 180Q�66   5     5	     P� 5
      �@ SortDirection$ 
DescendingX�6;   . �5     �� $ 	SortArrow��6%    � y�$ Visible=�6R   5     5     5       5  
   P� 5      �@J�IsMouseOver . . �5     ��  � ��# �  6/   �� 5      � ��# �  6/   5     P� 5      � �� . . �5     ��  � ��# �  6/   �� 5      � ��# �  6/   5     5     �� 5     "  
 6/   .�5   
   ��  � ��# �  6'   �� 5!  
    � ��# �  6'   �� 5"  
    � ��# �  6(   �� 5#  
    � ��$ 0,0,0,1q�6,   �� 5$  
    � ��$ 16��6"   �� 5%  
    � �� 5&     �� 5'     " 
 6"   .R�5(     � .+�5)     �� #��z��6!   #��z��5*     #��z��5+     #	 z��5,     ._�5.     �� # z 6/   $��Center=�6k   5/     n� 50     $$ PART_TopHeaderGripper��6    $	��Top=�51     8��  52     n� 53     $$ PART_BottomHeaderGripper��6    $��Bottom=�54     8�� 55     56     57     59       5;  
   P� 5<      �@J�IsMouseOver . . �5=     ��  � ��# �  6/   �� 5>      � ��# �  6/   5?     P� 5@      � �� . . �5A     ��  � ��# �  6/   �� 5B      � ��# �  6/   5C     P� 5D      �@
 IsRowSelected . . �5E     ��  � ��# �  6/   �� 5F      � ��# �  6/   5G     5I     �� 5K     "  ��6:   .�5L  
   ��  � ��$ 0q�6,   �� 5M  
    � ��$ 0q�6$   �� 5N  
    � ��
 ���� 6'   �� 5O  
    � �� 6(   Z� �� 5P  
    � ��# �  6'   �� 5Q  
    �@��SelectionBrush# �  6+   �� 5R  
    � ��$ Center=�65   5S     �� 5U     "    6&   .�5V  
   ��  � ��# �  6'   �� 5W  
    � ��# �  6'   �� 5X  
    � ��$ 0q�6,   �� 5Y  
     �@  RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5Z  
     �@  HorizontalGridLinesBrush# �  65   �� 5[  
    �@  VerticalGridLinesBrush# �  63   �� 5\  
    �@  DragIndicatorStyle8   6/   �� 5]  
   " �@  DropLocationIndicatorStyle8  67   �� 5^  
    �@  GridLinesVisibility$ 
Horizontal=�60   �� 5_  
    � �� . 6:   �� 5`  
    �@��PanningMode$
 Both=�65   �� 5a  
    �@��IsFlicksEnabled .  63   �� 5b  
    �@��SelectedIndex$ -1��6*   �� 5c  
    �@  AutoGenerateColumns .  60   �� 5d  
    � ��# �  6'   �� 5e  
    � ��# �  6%   �� 5f  
    � �� 5g     �� 5h     "   6"   .R�5i     � $��16,0,0,0q�6   .+�5j     ]� $��	-16,0,0,0q�6$   .+�5k     �� $	��Top=�6%   #���  5l  	   $��35��5m  	   $��0q�6   �� 5n     $	��Top=�6%   #��z��5o  	   $��0q�63   $	 0q�6>   5p     �� 5q     $% DG_ScrollViewer��69   |�.  $
��Auto��6R   $��0q�6`   ��5r     �� 5s  "   " ��62   .R�5t  &   � $��0,0,16,0q�6+    5u  *   �� 5v  .   $
��Auto �6<   �� 5w  .   $��* �6<   �� 5x  .   $
��Auto �6<    5{  *   �� 5|  .   $
��Auto �6?   �� 5}  .   $��* �6?   �� 5~  .   $
��Auto �6?   .+�5�  *     $!& PART_ColumnHeadersPresenter��5�     $��1��5�  I   $��2��6Y   $��0q�5�  �   $��0q�6�   y�6   �� *HeadersVisibility+# Z' # Z(  �� "   6�   �� 5�  *   $!) PART_ScrollContentPresenter��6A   $��1��6f   $��2��6s   $��0��5�  A   $��0q�6Q   #* z��5�  A   �� 5�  *   $+ PART_VerticalScrollBar��6Q   $��1��64   $��2��6A   $, Vertical=�5�     #. z- 5�     #0 z/ 5�     #y�z1 5�     2 5�     �� *VerticalOffset+$3 OneWay=� �� *TemplatedParent+5�     � 5�  *   $��2��6/   $��1��6<    5�  .   �� 5�  2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "   �� 5�  2   $��* �6C   .+�5�  .   �� $+ PART_HorizontalScrollBar��5�     $��1��5�  8   $, 
Horizontal=�5�     #. z4 5�     #0 z5 5�     #y�z6 5�     2 5�     �� *HorizontalOffset+$3 OneWay=� �� *TemplatedParent+5�     5�  +   5�  '   5�  #   .��5�     �� # z 6-   $��0q�6i   5�     5�     5�     5�     �� 5�  
    �@  CanUserAddRows .  6+   �� 5�  
    �@  CanUserResizeRows .  6.     5�  
   P� 5�      �@��
IsGrouping . . �5�     ��  � �� .  6B   5�     5�     �� 5�     .R�5�  
   � $! 	container��6   $��40��6"   $
��Auto��60   $��Center=�6=   $��Center=�6Z   $
��Auto��6u   $��0q�6�   $y�Hidden=�5�     .+�5�     ]� .+�5�     �� #���  6   $7 Añadir Nuevo Recurso��5�  1   $����F1 M 51.5487,37.9814L 56.814,41.0534L 50.3383,48.7636L 50.3383,48.8841L 60.0205,46.9565L 60.0205,53.0403L 50.2777,51.414L 50.2777,51.5344L 56.814,59.0037L 51.4882,62.0757L 47.978,52.7994L 47.8569,52.7994L 44.4072,62.0757L 39.2025,59.0037L 45.6782,51.4742L 45.6177,51.3537L 36.1159,53.0403L 36.1159,46.9565L 45.5571,48.8841L 45.5571,48.7636L 39.2025,41.1137L 44.5888,37.9814L 47.9174,47.3179L 48.0385,47.3179L 51.5487,37.9814 Z M 20,17L 43.25,17L 56,29.75L 56,39L 52,36.75L 52,34L 39,34L 39,21L 24,21L 24,55L 41,55L 37.5,59L 20,59L 20,17 Z M 43,22.25L 43,30L 50.75,30L 43,22.25 Z ��5�  1   ��5�  1   �� *
AddCommand+$3 OneWay=���5�  1   �� 5�     �� 5�     #���  6   #��Z8 5�     .��5�     F� $" Uniform=�6   �����4��pej��pej�ltpej;t��;t��шopt��ӈopt���6upej4��pej�(�T�-��hT�-Ө�nT�-�;�H|?�;�(FӜ;���L��n@Q^�h@Q^�(�@Q^�X�@Q^ o��L o(F� oH|?X�T�-(�T�-��˦ ���� ��7� �8�D��8�H	�����n5���X�BP�|�ML��|�MѴ��|�MӰ��|�M�˦X�B�˦�n5�Ȣ�H	��Ȣ�D���s� ��˦ ����2��`��E��Z��EѸP�2����2��+2�Ѽ�+�E�D�[�E�D�[2�Ѽ�+2��HZ�2��0P��EѠ���E���2��HZ�2���5�     �� *
Foreground+ �� *FindAncestor+" ��5�     5�     5�     5�     9 5�  
   �� 5�     $
: True��6�   ; 6   �� *IsMouseOver+ �� *FindAncestor+"  .a�5�     �� $ 	container��6?    � y�$ Visible=�6/   6?   5�     5�     �� 5�     "  ��6.   .�5�  
   ��  �@�FocusVisualStyle 6-   Z� �� 5�  
    � ��# �   6'   �� 5�  
    � ��# � ! 6'   �� 5�  
    � ��$ Center=�67   �� 5�  
    � ��$ Center=�65   �� 5�  
    � ��$ 20��6"   �� 5�  
    � ��$ 20��6#   �� 5�  
    � ��$ 0q�6,   �� 5�  
    � �� 5�     �� 5�     " ��6"   .R�5�     �� $ border��6   #��z��6-   #��z��6Y   #��z��6�    . ._�5�     �� $< contentPresenter��6+   #��z��6a   #��z��6�   ��. # z 6�   #��z��6  = 6E   Z� 6  5�      5�     P� 5�      �@J�IsMouseOver . . �5�     �� # � " 6;    � ��5�     P� 5�      � �� . . �5�     �� # � # 6;    � ��5�     P� 5�      � {� .  . �5�     �� 8    � ��6;   5�     5�     5�     5�     �� 5�     "  ��6+   .�5�  
   ��  � ��# �  6'   �� 5�  
    � ��# � % 6'   �� 5�  
    � ��$
 Left=�67   �� 5�  
    � ��$ Center=�65   �� 5�  
    � ��$ 1q�6$   �� 5�  
    � �� 5�     �� 5�     " ��6"   .R�5�     �� $ border��6   #��z��6-   #��z��6a    . ._�5�     �� $< contentPresenter��6+   #��z��6a   #��z��6�   ��. # z 6�   #��z��6  = 6E   Z� 6  5�      5�     P� 5�      �@J�IsMouseOver . . �5�     �� # � & 6;    � ���� 5�     $ border��6;    � ��
 ���� 6O   5�     P� 5�      � �� . . �5�     �� # � ' 6;    � ��5�     P� 5�      � {� .  . �5�     �� # � $ 6;    � ��5      5     5     �� 5  
    � ��$ 35��6#   �� 5  
    � ��$ 0q�6,   5     �� 5	     "   64   8   .�5
  
   ��  �@  EnableRowVirtualization .  64   �� 5  
    � �� 5     �� 5     "   6"   .R�5     ]� .+�5+     � $��0,0,0,0q�6   .+�5,     ]� $��0,0,0,0q�6(   .+�5-  "   �� $	��Top=�6)   #���  5.  
   $��35��5/  
   $��0q�6   �� 50  "   $	��Top=�6)   #��z��51  
   $��0q�64   $	 0q�6?   52     �� 53     $% DG_ScrollViewer��6=   |�.  $
��Auto��6V   $��0q�6d   ��54  "   �� 55  &   " ��66   .R�56  *   � $��0,0,0,0q�6/    57  .   �� 58  2   $
��Auto �6@   �� 59  2   $��* �6@   �� 5:  2   $
��Auto �6@   �� 5;  2   $
��Auto �6@    5>  .   �� 5?  2   $
��Auto �6C   �� 5@  2   $��* �6C   �� 5A  2   $
��Auto �6C   .+�5D  .     $!& PART_ColumnHeadersPresenter��5E     $��1��5D  M   $��2��6]   $��0q�5F  �   $��0q�6�   y�6   �� *HeadersVisibility+# Z' # Z(  �� "   6�   �� 5H  .   $!) PART_ScrollContentPresenter��6E   $��1��6j   $��2��5I  E   $��0��6Y   $��0q�6i   #* z��5J  E   �� 5L  .   $+ PART_VerticalScrollBar��6U   $��1��68   $��2��6E   $, Vertical=�5M     #. z- 5N     #0 z/ 5O     #y�z1 5Q     2 5P     �� *VerticalOffset+$3 OneWay=� �� *TemplatedParent+5Q     � 5S  .   $��2��63   $��1��6@    5T  2   �� 5U  6   ��6G   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "   �� 5V  6   $��* �6G   .+�5X  2   �� $+ PART_HorizontalScrollBar��5Y     $��1��5X  <   $, 
Horizontal=�5Z     #. z4 5[     #0 z5 5\     #y�z6 5^     2 5]     �� *HorizontalOffset+$3 OneWay=� �� *TemplatedParent+5^     5_  /   5`  +   5a  '   .��5c  "   �� # z 61   $��0q�6m   5d     5e     5f     5g     5i     5j     5l     