   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType �� BasedOn �� Property "  AtlasTextBox.Static.Foreground �� Value # AtlasTextBox.Static.BorderBrush 
 Accent �� Orientation ��Name ��Name J� Opacity �� CornerRadius	 J� IsHitTestVisible
 �� ElementName �� Path �� Triggers �� Value �� Binding N� EnterActions ��Name g� Duration �� 
TargetName �� TargetProperty	 W� To N� ExitActions �� BeginStoryboardName �� 
TargetType �� Padding J� SnapsToDevicePixels ��Name �� ClearTypeHint P� Property P� Value  InputTextDisabled �� 
TargetName ]� 
Conditions  �� Property! �� Value & "AtlasTextBox.MouseOver.BorderBrush % !AtlasTextBox.MouseOver.Foreground3    -CompanyName.Atlas.UIControls.AtlasPasswordBox" � OverridesDefaultStyle#    Password  DataGridTextStyle  DataGridEditingTextStyle�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
uiControls*clr-namespace:CompanyName.Atlas.UIControls  5      %  '��       '�� Z	    7��'�� �	    7��'   �	    &	     &	 N    7���� 5      "  ��6)    6K   Z� .�5   
   ��  � ��# �   6'   �� 5   
    � ��# �  6(   �� 5	   
    � ��$ 1q�6,   �� 5
   
    � ��$ 2,1,1,1q�6$   �� 5   
    �@J�	AllowDrop . 6&   �� 5   
    �@�FocusVisualStyle 6-   Z� �� 5   
    �@��PanningMode$ VerticalFirst=�65   �� 5   
    �@��IsFlicksEnabled .  63   �� 5   
    �@��SelectionBrush# �  6+   �� 5   
    �@=�ErrorTemplate 5      �� 5      .R�5      �� $ 
Horizontal=�6!   .+�5      �� $��1q�6!   ���	#FFdc000c$	��Top=�6M   ._�5      � .+�5   "   �� $ adorner��6<   $��-1q�6M   5      5      �� 5      $ errorBorder��6!   ���	#FFdc000c$��8,0,0,0q�6M   $ 0Q�5   !   $ 0��6-   	 .  $��24��5   !   ._�5      �� 
�������$��8,2,8,3q�5   ;   $
��Wrap=�6L   $��Center=�6`   ��5   (   �� $
 adorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�5   `   5      5        5!      �� 5"      $
 True��6&    5#      �� 5$   "   $
 adorner��6*   $&  AdornedElement.IsKeyboardFocused�6@    5&      �� 5'   "   $ fadeInStoryboard��62   .��5(   &   �� .��5)   *   W� $ 00:00:00.15D�6:   $ errorBorder��5*   :   $ Opacity�5+   :   $ 1X�5,   :   5-   '   5.   #    50      �� 51   "   $ fadeInStoryboard��61   �� 52   "   $ fadeOutStoryBoard��62   .��53   &   �� .��54   *   W� $ 00:00:00D�6:   $ errorBorder��55   :   $ Opacity�56   :   $ 0X�57   :   58   '   59   #   5;      5=      5?      �� 5@   
    � �� 5A      �� 5B      " ��6"   .R�5C      �� $ Bd��6   #��z��5D      #��z��5E      #��z��5F      # z��5G       . ._�5I      �� $ PART_ContentHost��6'   $ Enabled=�6A   # z 5J   '   5K       5L      P� 5M       � {� .  . �5N      �� # �  6;    � ��5O      P� 5P       �@��
IsReadOnly . . �5Q      �� # �  6;    � ��5R      P� 5S       �@J�	IsFocused . . �5T      �� $ Bd��6%   # �  6L    � ��5U      ]� 5V       5W      �� 5X   "     �@��
IsReadOnly! .  6B   �� 5Y   "     � {�! . 6A   �� 5Z   "     �@J�IsMouseOver! . 6C   ./�5]      �� # �  6<    � ���� 5^      # �  6;    � ��5_      5a      5c      5d      �� 5e      8   "  ��6<   5f      �� 5g      8   "  ��6<   5h      �� 5j      "    6:   .�5k   
   ��  � ��# �   6'   �� 5l   
    � ��
 ���� 6'   �� 5m   
    � ��
 ���� 6(   �� 5n   
    � ��$ 0q�6,   �� 5p   
    � �� 5q      �� 5r      "   6"   .R�5s      �� " . #��z��6;   #��z��6e   #��z��6�   #��z��5t      #��z# 6J   6n   5u      5w      5x      �� 5z      "  ��6&   .�5{   
   ��  � ��$	 2,0q�6#   5|      �� 5~      "  ��6-   8   .�5   
   ��  � ��$ 0q�6,   �� 5�   
    � ��$ 0q�6$   5�      5�      