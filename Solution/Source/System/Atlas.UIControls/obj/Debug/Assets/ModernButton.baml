   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source/    )CompanyName.Atlas.UIControls.ModernButton �� 
TargetType �� Property �� Value   ModernButtonText �� 
TargetType �Name � ColumnDefinitions    EllipseDiameter	 A�Name  ModernButtonBorder
    EllipseStrokeThickness F�Name    IconData    	IconWidth    
IconHeight �� Stretch � DataContext * &NullOrEmptyStringToVisibilityConverter �� 	Converter �� ConverterParameter  ToLowerConverter �� Triggers P� Property P� Value  ModernButtonTextHover �� 
TargetName  ModernButtonBorderHover  ModernButtonTextPressed  ModernButtonBorderPressed % !ModernButtonIconBackgroundPressed %	 !ModernButtonIconForegroundPressed 
 ModernButtonTextDisabled  ModernButtonBorderDisabled 
 Accent�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
uiControls*clr-namespace:CompanyName.Atlas.UIControls  5        5      �� 5   
   $ Converters.xaml?�6   %<  '         7  7  �� 5	      "   6   .�5
   
   ��  �@�FocusVisualStyle 6-   Z� �� 5   
    � ��$ Center=�67   �� 5   
    � ��$ Center=�65   �� 5   
    � ��# �   6'   �� 5   
    � ��$ 1q�6$   �� 5   
    � �� 5      �� 5      "   6"   .R�5      � $
 grid��6   
������ #��z��6B    5      �� 5      $
��Auto �6/   �� 5      .+�5      � #��z 6   #��z 5      .+�5      A� $	 ellipse��6&   #���  5   &   #��z
 5   &   $��Stretch=�5   &   F� 5      $
 icon��6#   #��z 5   #   #��z 5    #   #��z 5!   #   #��z��5"   #   $ Uniform=�5#   #   $��Center=�65   $��Center=�5$   #   5%      � 5&      $��1��6   .+�5'      �� # z��6(   $��CharacterEllipsis=�5*   (   #��z��5+   (   $��4,-1,0,0q�5,   (   #��z��5-   (   y�5(   (   �� 8   $ inverse����5)   (   �� 8  5-   (   5.      5/       50      P� 51       �@J�IsMouseOver . . �52      �� # �  6;    � ���� 53      $ ellipse��6%   # �  6L    � ��54      P� 55       � �� . . �56      �� # �  6;    � ���� 57      $ ellipse��6%   # �  6L    � ���� 58      $ ellipse��6%   # �  6J    � ���� 59      $
 icon��6%   # � 	 6G    � ��5:      P� 5;       � {� .  . �5<      �� # � 
 6;    � ���� 5=      $ ellipse��6%   # �  6L    � ��5>      P� 5?       �@J�	IsFocused . . �5@      �� $ ellipse��6%   # �  6L    � ��5A      5C      5E      5F      5H      