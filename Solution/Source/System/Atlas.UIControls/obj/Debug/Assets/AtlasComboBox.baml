   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      a5clr-namespace:CompanyName.Atlas.UIControls.Converters'CompanyName.Atlas.UIControls.Converters  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0890    *CompanyName.Atlas.UIControls.AtlasComboBox  �� 
TargetType �� Property �� Value $   DataGridCell.Selected.Background $  DataGridCell.Selected.Foreground �� 
TargetType �Name J� SnapsToDevicePixels � ColumnDefinitions d�Name  ComboBoxToggleButton d� 	IsChecked	 �� Mode
 �� RelativeSource J� IsHitTestVisible �� SelectedItem �� SelectionBoxItemTemplate# �� SelectionBoxItemStringFormat �� ContentStringFormat �� Path �� AncestorType �� Triggers �� Binding �� Value ��Name �Name � AllowsTransparency �� ElementName �� MaxDropDownHeight ��Name ��Name �� ClearTypeHint ��Name ��Name ��Name-  'System.Windows.Input.KeyboardNavigation    DirectionalNavigation! �� Triggers" ]� 
Conditions# �� Value$ �� Property% �� 
TargetName& P� 
SourceName' P� Property( P� Value�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      4local*clr-namespace:CompanyName.Atlas.UIControls  5      D
converters5clr-namespace:CompanyName.Atlas.UIControls.Converters  5      %�  '         �� 5      "    61   .�5   
   ��  �@�FocusVisualStyle 6-   Z� �� 5   
    � ��# �   6'   �� 5   
    � ��# �  6'   �� 5	   
    � ��$ Hidden=�6G   �� 5
   
    � ��$ Hidden=�6E   �� 5   
    � �� . 6:   �� 5   
    �@��PanningMode$
 Both=�65   �� 5   
    � ��# �  6)   6f   �� 5   
    � ��$ 25��6&   �� 5   
    � ��$ 1q�6,   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ Center=�6.   �� 5   
    � �� 5      �� 5      "   6"   .R�5      � .+�5      �� $��
MainBorder��6!   $��Center=�63   #��z��6P   #��z��6�   $��0q�6�   #��z��6�   #��z��5      #��z��6?   ._�5      � $ templateRoot��6#    .  5   "   �� 5    &   $��* �67   �� 5!   &   $��13 �67   .+�5$   "   d� $ toggleButton��6/   #��z��6E   
������ $��1��5&   '   #���  5(   '    5'   '   �� *IsDropDownOpen+$	 TwoWay=�
 �� *TemplatedParent+5(   '   � 5+   "   .+�5,   '   ��  .  $��4,0,0,0q�5-   *   #��z 5.   +   #��z 5/   +   #��z��50   +   # z 51   +   #��z��52   +   #��z��53   +   # z 54   +   �� 56   &   $��4,0,0,0q�6�   $��Center=�6�   #��z��6�   #��z��6�   ��60   �� $	 Tag�
 �� $ ComboBoxL���57   *   �� 58   .   "  ��64   .�59   2   ��  � y�$ 	Collapsed=�6O   6c    5:   2   �� 5;   6    6B   �� $ SelectedItem�
 �� $ ComboBoxL� 6�   Z� .a�5<   :   ��  � y�$ Visible=�6W   6i   5=   7   5?   /   5A   '   �� 5B   &   $ placeholderText��60   $��Center=�6I   $��Light"�6d   $��Italic$�6w   $��4,0,0,0q�6�   $y�	Collapsed=�6   ��6�   �� 
 �� $ local:AtlasComboBoxL�$ Placeholder�6   5D   #   5E       5F      � 5G      $ 
PART_Popup��6     . $��2��6N   $��0q�6b   $��Bottom=�5J       ��5H       �� *IsDropDownOpen+$	 TwoWay=�
 �� *TemplatedParent+��5I      �� *ActualWidth+$	 OneWay=�$ 
MainBorder��.��5K      � $ shadow��6#   #��z 63   $��0q�6c   ��5L   #   �� *ActualWidth+$ 
MainBorder��.+�5M   "   �� $ dropDownBorder��6)   $��1,0,1,1q�6A   #��z��6\   #��z��5N   )   ��6T   �� *ActualWidth+$ templateRoot����5O   &   �� 5P   *   "  ��60    5Q   .   �� 5R   2    6>   �� $ 
PART_Popup��$ IsOpen� True6u   5T   3   5V   +   ._�5X   &   �� $ DropDownScrollViewer��63   #��z��5Y   3   #��z��5Z   3   .��5[   *   � $
 grid��6/   $ Enabled=�6=   .+�5\   .   �� $ canvas��65   $��0��6F   $��0��6R   .+�5]   2   �� $ 
opaqueRect��6<   ��6P   �� *
Background+$ dropDownBorder����5^   <   �� *ActualHeight+$ dropDownBorder����5_   <   �� *ActualWidth+$ dropDownBorder��5`   /   �� 5a   .   $ ItemsPresenter��6=   $  	Contained=�6U   # z 5b   =   }�. ��5a   �   �� *ActualWidth+$ templateRoot��5b   y   5c   +   5e   '   5f   #   5g      5h      5j      ! 5k      ]� 5m      " 5n      �� 5p   "   # 6D   Z� $ �@��SelectedItem�� 5q   "   $ �@��Text$#  ��6<   ./�5s      �� $% placeholderText��6%    � y�$ Visible=�6X   5t      P� 5}      $& 
PART_Popup��6;   ' �@�HasDropShadow( . 5�      P� 5�      $& 
PART_Popup��64   ' � ��( . 5�      P� 5�      ' �@��HasItems( .  . �5�      �� $% dropDownBorder��67    � ��$ 95��6S   5�      ]� 5�      " 5�      �� 5�   "   $ �@��
IsGrouping# . 6B   �� 5�   "   "$ �@%�IsVirtualizingWhenGrouping# .  6d   ./�5�      ��  � �� .  6N   5�      P� 5�      $& DropDownScrollViewer��6K   ' � ��( .  . �5�      �� $% 
opaqueRect��6;    6S   �� *VerticalOffset+$ DropDownScrollViewer�� �@��Top�� 5�      $% 
opaqueRect��6<    6T   �� *HorizontalOffset+$ DropDownScrollViewer�� �@��Left5�      5�      5�      5�      5�      