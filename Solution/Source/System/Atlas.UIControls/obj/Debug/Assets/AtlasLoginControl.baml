   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      a5clr-namespace:CompanyName.Atlas.UIControls.Properties'CompanyName.Atlas.UIControls.Properties  5      a5clr-namespace:CompanyName.Atlas.UIControls.Converters'CompanyName.Atlas.UIControls.Converters  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0894    .CompanyName.Atlas.UIControls.AtlasLoginControl  �� 
TargetType �� Property �� Value&   System.Windows.Media.TextOptions2   ,CompanyName.Atlas.UIControls.LoginInfoHolder G�Name G� 
CaretBrush   Window.Default.Background �� 
TargetType � 	ResourcesH   BCompanyName.Atlas.UIControls.Converters.LoginViewStateToVisibility  Key � RowDefinitions � ColumnDefinitions	 ��Name
 �� CornerRadius �� Padding J� Opacity �� ElementName �� Path ��Name �� 
CaretBrush  LoginViewStateToVisibility �� 	Converter6   0CompanyName.Atlas.UIControls.AtlasLoginViewState   UserText �� ConverterParameter �� RelativeSource �� AncestorType ��Name7   1CompanyName.Atlas.UIControls.Properties.Resources   User   	UserLabel   Password   	PassLabel   PassText   UnsuccessfullText  Atlas.Fail.Foreground�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
uiControls*clr-namespace:CompanyName.Atlas.UIControls  5      D
properties5clr-namespace:CompanyName.Atlas.UIControls.Properties  5      D
converters5clr-namespace:CompanyName.Atlas.UIControls.Converters  5      %Z	  '         7  7  7  7  �� 5      "    6   .�5
   
   ��  �@�FocusVisualStyle 6-   Z� �� 5   
    �@�UseLayoutRounding . 6.   �� 5   
    �@J�SnapsToDevicePixels . 60   �� 5   
    �@ TextFormattingMode$ Display=�6;   �� 5   
    � ��$ 0��6%   �� 5   
    �@ PasswordBox 5      G� 5      $ PasswordBox��6   $	��100��63   |�. $��0q�6T   
�������$��Center=�6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   
 �����
������ 
�������6C  5      �� 5   
    � ��# �   6'   �� 5   
    � �� 5      �� 5      "   6"    5      ��  5      LoginViewStateToVisibility 6@   5       .R�5!      � |�.   5"      �� 5#      $
��Auto �6,   6<   �� 5$      $��* �6,   69   .+�5&      �  5'      �� 5(   !   $��* �62   6>   �� 5)   "   $
��Auto �63   6B   �� 5*   "   $��* �63   6?   .+�5,      �� $	 GreatBorder��67   $��1��6&   |�.  $
 12��6_   $ 1q�6r   $��0q�6~   
������ ._�5-      �� $	 Border��65   $��1q�6!   |�.  ���#555555$
 12��6n   $	��110��6�   $��25��6�   $	 2,2q�6�   ._�5/      � .+�50   *   �� ���#000000$
 12��6~   $	 0.2Q�6�   ��61   �� $ Border��$ ActualHeight�6�   �� 52   "   $ UserTextBox��6*   
������ $��1��6X   $��Center=�6e   
 �����$��Center=�53   '   $��Center=�6D   
�������$	��100��6r   $��0q�6�   y�52   �   �� *	LoginView+8   # Z ��53   �   ��  �� $" uiControls:AtlasLoginControlL�$ UserName�.��6  55   #   �� 56   "   $ UserTextBlock��6.   #��Z 6E   
�������$��0,0,0,0q�6�   $��Center=�6�   $��Center=�6�   y�57   )   �� *	LoginView+8  # Z 59   #   �� 5;   "   $ Password��6,   
�������#��Z 6R   $��0,0,0,0q�6�   $��Center=�5=   )   $��Center=�6F   y�5<   )   �� *	LoginView+8  # Z 5?   #   G� 5A   "   $ PasswordBox��6.   $	��100��6C   $��2��6S   $��0q�6`   ��. 
�������$��Center=�5C   B   $��Center=�6d   $��Center=�6�   $��Center=�6�   
 �����
������ 
�������y�5B   *   �� *	LoginView+8  # Z 5C     5S      5U      5V      5W      �� 5X      $��1��6$   #��z 61   $
��Wrap=�6\   $��0,5,0,0q�6p   #���  6�   6�   5Z      5_      5a      5b      5c      