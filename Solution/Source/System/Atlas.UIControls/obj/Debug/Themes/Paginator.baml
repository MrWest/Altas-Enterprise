   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      a5clr-namespace:CompanyName.Atlas.UIControls.Properties'CompanyName.Atlas.UIControls.Properties  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   BaseButtonStyle  �� 
TargetType �� Property �� Value / +AtlasTimelineScrollButton.Static.Foreground �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property	 P� Value 2 .AtlasTimelineScrollButton.MouseOver.Foreground 0 ,AtlasTimelineScrollButton.Pressed.Foreground 1 -AtlasTimelineScrollButton.Disabled.Foreground " AtlasTimelineScrollButtonStyle
 �� BasedOn  AtlasTextBoxStyle " AtlasTextBox.Static.Foreground �� Triggers % !AtlasTextBox.MouseOver.Foreground,    &CompanyName.Atlas.UIControls.Paginator 	 FilterBox.Background 
 FilterBox.Foreground7   1CompanyName.Atlas.UIControls.Properties.Resources   Previous � ToolTip �� RelativeSource �� AncestorType �� Stretch J� RenderTransformOrigin d� 	IsChecked ��Name �� Padding �� Mode �� 
TargetName   Next �� Angle ! FilterBox.Disabled.Foreground�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
uiControls*clr-namespace:CompanyName.Atlas.UIControls  5      D
properties5clr-namespace:CompanyName.Atlas.UIControls.Properties  5      %(  &	        &	 g    7   &	 �    '   �    �� 5      "  ��6$   .�5   
   ��  �@�FocusVisualStyle 6-   Z� �� 5	   
    � ��
 ���� 6'   �� 5
   
    � ��# �  6'   �� 5   
    � ��$ 0q�6,   �� 5   
    � ��$ Center=�67   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$
 10,2q�6#   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ border��6   #��z��6-   #��z��5   !   #��z��5   !    . ._�5      �� $ contentPresenter��6+   |�.  #��z��5   /   #��z��5   /   ��. # z 5   /   #��z��5   /   5       5      P� 5       �@J�IsMouseOver	 . . �5      �� # �  5    )    � ��5!      P� 5"       � ��	 . . �5#      �� # �  5$   )    � ��5%      P� 5&       � {�	 .  . �5'      �� # �  5(   )    � ��5)      5+      5-      5.      �� 50      8
   "  ��51      .�52   
   ��  �@��IsPressAndHoldEnabled . 69   53      �� 55      "  ��6&   .�56   
   ��  � ��
 ���� 6'   �� 57   
    � ��# �  6'   �� 58   
    � ��$	 5,0q�6$   �� 59   
    � ��$ 25��6%   �� 5:   
    � ��$ 1q�6,   �� 5;   
    � ��$ Center=�65   6F   �� 5<   
    � ��$ 30��6&   �� 5=   
    � ��
 ���� 6(    5>   
   P� 5?       �@J�IsMouseOver	 . . �5@      ��  � ��# �  6/   �� 5A       � ��
 ���� 60   �� 5B       � ��$ 0q�64   5C      P� 5D       �@J�IsMouseOver	 .  . �5E      ��  � ��# �  6/   �� 5F       � ��
 ���� 60   �� 5G       � ��$ 0q�64   5H      P� 5I       �@J�	IsFocused	 . . �5J      ��  � ��# �  6/   �� 5K       � ��
 ���� 60   �� 5L       � ��$ 0q�64   5M      P� 5N       �@J�	IsFocused	 .  . �5O      ��  � ��# �  6/   �� 5P       � ��
 ���� 60   �� 5Q       � ��$ 0q�64   5R      5T      �� 5V      "    62   .�5W   
   ��  � ��# � 	 6'   �� 5X   
    � ��# � 
 6'   �� 5Y   
    � ��$ 1q�6,   �� 5Z   
    �@�FocusVisualStyle 6-   Z� �� 5[   
    �@��PanningMode$ VerticalFirst=�65   �� 5]   
    � �� 5^      �� 5_      "   6"   .R�5a      ]� $��Center=�6$   $��Center=�6A   .+�5b      �� #���  5c   !   $
��10,2q�6Z   $
��Left=�6i   # Z 6�   ��5b   '   �� *MovePreviousCommand+ �� *FindAncestor+$ uiControls:PaginatorL�.��5d   "   � $	��Top=�6'   .+�5e   &   A� $��20��5g      $	��Top=�6)   $��20��6A   $��2��6L   ��5f      �� *
Foreground+ �� *FindAncestor+" ��5g   L   F� 5h   &   Q���pD� �H�b@Q۔� ���%�"@��KǜgP"@T�� �� Vw� �|O� ���X��܂`@�� 	$��Center=�5k      $��9.766��6:   $��6.454,5.116,7.331,5.118q�6I   $
 Fill=�6j   $��Center=�5l      $��6.215��68   $ 0.5,0.5&�6F   ��5i      �� *
Foreground+ �� *FindAncestor+" ��5l   F   5m   #   5n      �� 5o      ._�5p      ]� $��20��6(   $��Center=�64   .+�5q      �� $��25��6&   #���  64   ��5r   &   �� *
ActualPage+ �� *FindAncestor+$ uiControls:PaginatorL�6�   �� 5s      $��/��6(   $��25��61   $	��2,0q�6?   $��SemiBold"�6L   $��18(�6b   #���  6p   $��Center=�6�   6�   d� 5t       6+   �� *OnePageStyle+ �� *FindAncestor+$ uiControls:PaginatorL���5u   #   �� 5v   %   "  d�6+    5w   )   P� 5x   -    �@d�	IsChecked$	 FalseY�6J   6Z   5z   &   ��5|   !   �� 5}   #   " d�63   .R�5   *   �� $ TotalPageTextBlock��6O   $��25��64   $	 5,0q�6@   $��SemiBold"�6k   #���  6�   $��Center=�6�   ��5�   &   �� *
TotalPages+ �� *FindAncestor+$ uiControls:PaginatorL�$ OneWay=�6�    5�   )   P� 5�   .    �@d�	IsChecked$	 FalseY�6K   . �5�   2   �� $ TotalPageTextBlock��69    � ��$ Regular"�6o   6�   5�   /   5�   '   5�      5�      5�      �� 5�      #���  5�      $
��10,2q�6Y   # Z 6h   ��5�   '   �� *MoveNextCommand+ �� *FindAncestor+$ uiControls:PaginatorL�.��5�      � $	��Top=�6#   $��Right=�6;   .+�5�   "   A� $��20��5�      $	��Top=�6)   $��20��6A   $��2��6L   ��5�      �� *
Foreground+ �� *FindAncestor+" ��5�   L   F� 5�   "   Q���pD� �H�b@Q۔� ���%�"@��KǜgP"@T�� �� Vw� �|O� ���X��܂`@�� 	$��Center=�5�      $��9.766��6:   $��7.642,5.117,6.142,5.117q�6I   $
 Fill=�6j   $��Center=�5�      $��6.216��68   $ 0.5,0.5&�6F   ��5�      �� *
Foreground+ �� *FindAncestor+" ��z�5�   &   V� 5�   *   .~�5�   .   �� $	 180Q�6>   5�   +   5�   #   5�      5�      5�       5�      P� 5�       � {�	 .  . �5�      �� # �  6;    � ��5�      5�      5�      5�      5�      