   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ThemeDictionary  �� Source  DownArrowGeometry  ComboBoxToggleButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels F�Name J� RenderTransformOrigin	 �� Triggers
 �� Value �� Binding �� RelativeSource �� AncestorType �� 
TargetName �� Angle2    ,CompanyName.Atlas.UIControls.LeveredComboBox $  DataGridCell.Selected.Background $  DataGridCell.Selected.Foreground  
ShadowText �� Path �� Padding ��Name �Name � AllowsTransparency � 	StaysOpen �� Mode � PlacementTarget%  System.Windows.Markup.Reference  ItemTextSelected ��Name �� SelectedItem �� SelectedValue � 	Resources � ColumnDefinitions ��Name �� ElementName P� 
SourceName  P� Property! P� Value  ItemBackgroundSelected" �� Triggers# �Name$ d�Name% d� 	IsChecked& J� IsHitTestVisible'    SubLevel#( �� SelectionBoxItemStringFormat) �� ContentStringFormat* �� MaxDropDownHeight+ ��Name, �� ClearTypeHint- ��Name. ��Name/ ��Name-  'System.Windows.Input.KeyboardNavigation0   DirectionalNavigation1 ]� 
Conditions2 �� Value3 �� Property   DataGridEditingComboBoxStyle4 �� BasedOn�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
uiControls*clr-namespace:CompanyName.Atlas.UIControls  5      %I  &	        &	 ;     &	 h     '   L    7  &	 Y    7���� 5      $"  ../Themes/ModernUI.Dark.xaml?�61   6Y   �5      M 0 0 L 3.5 4 L 7 0 Z�6@   �� 5	      " d�6)   .�5
   
   ��  �@�OverridesDefaultStyle . 62   �� 5   
    � �� .  6&   �� 5   
    � |� .  6&   �� 5   
    �@��	ClickMode$ Press=�6&   �� 5   
    � ��$ 0,0,1,0q�6#   �� 5   
    �@d�	IsChecked$
 TrueY�6&   65   �� 5   
    � ��$ Right=�60   6@   �� 5   
    � �� 5      �� 5      " d�6"   .R�5      �� $ templateRoot2��6    . #��z��5      #��z��5      $��Right=�6G   ._�5      �� $ splitBorder��6!    . $��Right=�5   !   $��1q�6=   
������ ._�5      F� $ path1��6#   #���  64   #��z��6_   $��0��6�   $ 0.5,0.5&�6�   $��Center=�6�   $��Center=�6�   #��z��6�   5#      5$      5%      	 5'      �� 5(      $

 True��5)   &    5(   &   �� *IsDropDownOpen+ �� *FindAncestor+" ��.a�5*      �� $ path1��6%    � z� 5+   "   �� 5,   &   $	 180Q�66   5.      5/      51      53      54      �� 57      "   68   .�58   
   ��  �@�FocusVisualStyle 6-   Z� �� 59   
    � ��# �  6'   �� 5:   
    � ��# �  6'   �� 5;   
    � ��$ Hidden=�6G   �� 5<   
    � ��$ Hidden=�6E   �� 5=   
    � �� . 6:   �� 5>   
    �@��PanningMode$
 Both=�65   �� 5A   
    � ��$	 0,4q�6$   �� 5B   
    � ��$ 25��6&   �� 5C   
    � ��$ 0q�6,   �� 5H   
    � ��# �  6(   �� 5J   	    � �� 5K      �� 5L      " ��6   .�5M      ��  � �� 6C   �� $  HorizontalContentAlignment� �� " ���� 5N       � ��$ Center=�6A   �� 5O       � ��$ 5,3,8,3q�60   �� 5P       � ��$ Bottom=�6:   �� 5Q       � ��$ 0q�68   �� 5R       � �� 63   ��  �� $  uiControls:LeveredComboBoxL�$ 
Foreground��� 5S       � �� 63   ��  �� $  uiControls:LeveredComboBoxL�$ 
Background��� 5T       � �� 5U      �� 5V      " ��6.   .R�5W   "   �� $��Bd��6)   #��z��5X      #��z��5Y      #��z��5Z      # z��5[       . ��5\   8   ��  �� $  uiControls:LeveredComboBoxL�$ ActualWidth�._�5]   &   � .+�5^   *   �� $ ContentPresenter��6;   #��z��6W   #��z��5_   +   # z 5`   +   � 5a   *   $ 
PART_Popup��60    . $��2��6^   $��Right=�5c   /    .  ��5b   /   ��  �� $ ComboBoxItemL�$ IsMouseOver�$ OneWay=� 5c   S     *Bd+.��5f   .   �� #���  66   #��z��6i   $��1,0,0,0q�6�   ._�5g   2   �� $ ListView��6:   ��6L   �� *Items+ 6j   ��  �� $  uiControls:LeveredComboBoxL�$ SubLevel� 6�   ��  �� $  uiControls:LeveredComboBoxL�$ SubLevel� 5h   6   ���� 5i   :   '��       " ��6@   .�5j   >   ��  � �� 6[   �� *
Background+$ OneWay=� �� $ ComboBoxItemL��� 5k   >    � �� 6[   �� *
Foreground+$ OneWay=� �� $ ComboBoxItemL��� 5l   >    � ��$ 3q�6X   6d   �� 5m   >    � �� 5n   B   �� 5o   F   " ��6V   .R�5p   J   �� $ Border��6Q   $ 7,3,8,3q�6b   ��6t   ��  �� $ ComboBoxItemL�$ ActualHeight�._�5q   R   �  5r   R   �� 5s   V   $��* �6g   �� 5t   V   $��3* �6g   .+�5v   V   �� $ 
TextBlock1��6`   $��0,0,3,0q�6�   ��6t   �� *Code+��6�   ��  �� $  uiControls:LeveredComboBoxL�$ 
Foreground�6  �� 5w   V   $��1��6`   $��5,0,0,0q�6�   ��6p   �� *Name+��6�   �� $ 
TextBlock1��$ 
Foreground�6�   5x   S   5y   O   	 5z   N   P� 5|   R   $ Border��6Z     �@J�IsMouseOver! . . �5}   V   �� $ Border��6]   # �  6�    � ���� 5~   V   $ 
TextBlock1��6]   # �  6�    � ��5�   S   5�   K   5�   ?   " 5�   >   P� 5�   B     �@J�IsMouseOver! . . �5�   F   ��  � ��# �  6c   �� 5�   F    � ��# �  6c   5�   C   5�   ;   5�   7   5�   3   5�   /   5�   +   5�   '   5�   #   	 5�   "   P� 5�   &     �@��IsHighlighted! . . �5�   *   �� $ Bd��61   # �  6W    � ���� 5�   *   # �  6G    � ��5�   '   P� 5�   &     � {�! .  5�   '   5�      5�      5�      5�      �� 5�   
    � �� 5�      �� 5�      "   6"   .R�5�      � .+�5�      �� $��
MainBorder��6#   $��Center=�65   $��1q�6S   #��z��6h   $��0q�6�   # z��6�   ._�5�   "   � $# templateRoot��6'    .  5�      �� 5�   "   $��* �63   �� 5�   "   $��13 �63   .+�5�      d� $$ toggleButton��6,   #��z��6B   
������ $��1��5�   '   8��  % 5�   '   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+5�   '   � 5�   &   .+�5�   &   �� $ placeholderText��60   $��Center=�6I   $��4,0,0,0q�6d   $��Light"�6u   $��Italic$�6�   $y�	Collapsed=�6  ��6�   ��  �� $  uiControls:LeveredComboBoxL�$ Placeholder�6  �� 5�   &   $ PART_NameText��68   & .  $��4,0,0,0q�5�   +   #��z' 5�   +   #) z( 5�   +   #��z��5�   +   #��z��5�   +   # z 5�   +   6i   5�   '   5�      5�      � 5�      $ 
PART_Popup��6     . $��2��6N   $��0q�6b   $��Bottom=�5�       ��5�       �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+��5�      �� *ActualWidth+$ OneWay=�$ 
MainBorder��.��5�   "   � $# shadow��6'   #��z* 67   $��0q�6g   ��5�   #   �� *ActualWidth+$ 
MainBorder��.+�5�   &   �� $ dropDownBorder��6-   $��1,0,1,1q�6E   #��z��5�   )   ��6T   �� *ActualWidth+$ templateRoot����5�   *   �� 5�   .   " ��64   " 5�   2   �� 5�   6    6B   �� $ 
PART_Popup��$ IsOpen�
 True6y   .a�5�   6   ��  � ��# �  6T   6�   5�   7   5�   /   ._�5�   *   �� $+ DropDownScrollViewer��67   #��z��5�   3   #��z��5�   3   .��5�   .   � $
# grid��63   $, Enabled=�6A   .+�5�   2   �� $- canvas��69   $��0��6J   $��0��6V   .+�5�   6   �� $. 
opaqueRect��6@   ��6T   �� *
Background+$ dropDownBorder����5�   <   �� *ActualHeight+$ dropDownBorder����5�   <   �� *ActualWidth+$ dropDownBorder��5�   3   �� 5�   2   $/ ItemsPresenter��6A   $0 	Contained=�6Y   # z 5�   =   }�. ��5�   �   �� *ActualWidth+$ templateRoot��5�   y   5�   /   5�   +   5�   '   5�   #   5�      5�      	 5�      ]� 5�      1 5�      �� 5�   "   2 6@   Z� 3 �@  SubLevel�� 5�   "   3 �@��Text$2  ��6<   ./�5�      �� $ placeholderText��6%    � y�$ Visible=�6X   5      P� 5       �@J�	IsFocused! . . �5     �� $ 
MainBorder��6%   # �  6T    � ��5     P� 5       �@��
IsSelected! . . �5     �� # �  6<    � ���� 5     $ 
MainBorder��6%   # �  6T    � ���� 5     $ dropDownBorder��6%   # �  6X    � ��5     P� 5	     $ 
PART_Popup��6;     �@�HasDropShadow! . 5     P� 5     $ 
PART_Popup��64     � ��! . . �5     �� $ 
MainBorder��6<   # �  6T    � ��6�   5     P� 5       �@��HasItems! .  . �5     �� $ dropDownBorder��67    � ��$ 95��6S   5     ]� 5     1 5     �� 5  "   3 �@��
IsGrouping2 . 6B   �� 5  "   "3 �@%�IsVirtualizingWhenGrouping2 .  6d   ./�5     ��  � �� .  6N   5     P� 5     $ DropDownScrollViewer��6K     � ��! .  . �5     �� $ 
opaqueRect��6;    6S   �� *VerticalOffset+$ DropDownScrollViewer�� �@��Top�� 5     $ 
opaqueRect��6<    6T   �� *HorizontalOffset+$ DropDownScrollViewer�� �@��Left5     5      5"     5$     �� 5%     " ��61   84   .�5&  
   �� % �@��IsSynchronizedWithCurrentItem$ FalseY�6:   �� 5'  
    � ��$ 0q�6,   �� 5(  
    � ��$	 1,1q�6$   5)     5+     