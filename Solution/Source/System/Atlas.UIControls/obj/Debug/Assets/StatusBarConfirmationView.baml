   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      a5clr-namespace:CompanyName.Atlas.UIControls.Properties'CompanyName.Atlas.UIControls.Properties  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   BaseButtonStyle  �� 
TargetType �� Property �� Value  ButtonBackground  ButtonBorder  
ButtonText �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value  ButtonTextDisabled  ButtonBackgroundHover 
 Accent  ButtonTextHover  ButtonBackgroundPressed 	 ButtonBorderPressed 
 ButtonTextPressed �� 
TargetName  ExpandStoryboard	 �� TargetProperty9   3System.Windows.Media.Animation.EasingDoubleKeyFrame
 O� KeyTime O� Value    EasingFunction.  (System.Windows.Media.Animation.CubicEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode  CollapseStoryboard<   6CompanyName.Atlas.UIControls.StatusBarConfirmationView ��Name  StatusBar.Background �� Padding �� RelativeSource �� AncestorType �� Path �� Triggers �� Binding �� Value N� EnterActions N� ExitActions7   1CompanyName.Atlas.UIControls.Properties.Resources   WantToSpreadChanges  StatusBar.Foreground  Default.FontSize
   Yes � ToolTip �� 	IsDefault	   No �� IsCancel�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
uiControls*clr-namespace:CompanyName.Atlas.UIControls  5      D
properties5clr-namespace:CompanyName.Atlas.UIControls.Properties  5      %o  &	        &	     &	 g    '  O    7  7  7   7   �� 5�      "  ��5�      .�5�   
   ��  �@�FocusVisualStyle 6-   Z� �� 5�   
    � ��# �  5�      �� 5�   
    � ��# �  5�      �� 5�   
    � ��$ 1q�5�      �� 5�   
    � ��# �  5�      �� 5�   
    � ��$ Center=�5�      �� 5�   
    � ��$ Center=�5�      �� 5�   
    � ��$ 	16,3,16,5q�5�      �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� $��Chrome��6   #��z��5�   )   #��z��5�   )   #��z��5�   )    . ._�5�      �� $��	Presenter��6+   #��z��6<   #��z��5�   +   #��z��5�   +   ��. # z 5�   +   5�       5�      P� 5�       � {� .  . �5�      �� # �  6;    � ��5�      P� 5�       �@J�IsMouseOver . . �5�      �� # �  6;    � ���� 5�      # �  6<    � ���� 5�      # �  6;    � ��5�      P� 5�       � �� . . �5�      �� # �  6;    � ���� 5�      # � 	 6<    � ���� 5�      # � 
 6;    � ��5�      P� 5�       �@J�	IsFocused . . �5�      �� $ Chrome��6%   # �  6P    � ��5�      5�      5�      5�      �� 5�      .��5�   
   U� $	 (FrameworkElement.Width)�6(   .\�5�         $
 0��6#   $ 0Q�6/      5�      $
 0:0:0.15��6#   $	 360Q�66    5�        5�      $ EaseOut=�6    5�      5�      5�      �� 5�      .��5�   
   U� $	 (FrameworkElement.Width)�6(   .\�5�         $
 0��6#   $	 360Q�6/      5�      $
 0:0:0.15��6#   $ 0Q�66    5�        5�      $ EaseOut=�6    5�      5�      5�      �� 5�      "   6   .�5�   
   ��  � ��$ Right=�60   6@   �� 5�   
    � �� 5�      �� 5�      "  6"   .R�5�      � .+�5�      �� $ ConfirmElement��6!   $��0��69   $��35��6C   #���  6P    5�       ��  �� $* $uiControls:StatusBarConfirmationViewL�$ Padding���5�      �� 5�   "   "  ��6(    5�   &   �� 5�   *    67   ��  �� $* $uiControls:StatusBarConfirmationViewL�$ IsCollapsed�	 False6�    5�   .   �� 5�   2   8��  6s    5�   .   �� 5�   2   8�� 6u   5�   +   5�   #   ._�5�      � .+�5�   "   ]� $��Right=�6,   .+�5�   &   ]� $
��Left=�61   .+�5�   *   �� #��Z 64   #���  6o   #���  6�   $��Center=�6�   $��Center=�6�   $��	10,0,18,0q�6	  6  5�   '   ]� 5�   $   $��Right=�6/   ��5�   *   �� 5�   .   "  ]�64    5�   2   �� 5�   6    6C   ��  �� $* $uiControls:StatusBarConfirmationViewL�$ IsCollapsed�	 False6�   .a�5�   :   ��  � �� 6S   ��  �� $* $uiControls:StatusBarConfirmationViewL�$ RelativeMargin�6�   5�   ;   5�   /   .+�5�   *   �� $��Right=�61   # Z 5�   5   8�� $��SemiBold"�5�   Z    . #��Z 5�   5   $��54��6c   #���  6o   #���  6�   $
��10,4q�5   1   ��5�   5   ��  �� $* $uiControls:StatusBarConfirmationViewL�$ Command���5�   5   ��  �� $* $uiControls:StatusBarConfirmationViewL�$ DataContext�5
  '   �� 5  &   $��Right=�6-   8�� # Z 5  5   $��SemiBold"�6b   #��Z 5  5   $��54��6b   $��10,4 q�6m    . #���  6�   #���  6�   ��5  5   ��  �� $* $uiControls:StatusBarConfirmationViewL�$ DisableCommand�5  '   5  '   5  #   5     5     5     5     5     5     5      