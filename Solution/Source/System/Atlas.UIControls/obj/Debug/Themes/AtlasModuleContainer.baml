   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      a5clr-namespace:CompanyName.Atlas.UIControls.Properties'CompanyName.Atlas.UIControls.Properties  5      a5clr-namespace:CompanyName.Atlas.UIControls.Properties'CompanyName.Atlas.UIControls.Properties  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source7    1CompanyName.Atlas.UIControls.AtlasModuleContainer �� 
TargetType �� Property �� Value&   System.Windows.Media.TextOptions   Window.Default.Background �� RelativeSource �� AncestorType �� Path �� 
TargetType	 � 	Resources  Key
 �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value �� 
TargetName   EasingFunction.  (System.Windows.Media.Animation.CubicEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode  ItemText ��Name �� ContentStringFormat �� ContentStringFormat J� SnapsToDevicePixels �� Triggers P� Value P� Property �� 
TargetName  ItemTextHover  ItemTextSelected  ItemTextDisabled J� Opacity �Name ��Name    Modules$ �� IsSynchronizedWithCurrentItem �� SelectedItem �� DataType ��Name  ModuleButtonStyle  � ToolTip  ModuleDescriptionConverter! �� 	Converter  ModuleIconStyle 	 ModuleIconConverter 
 ModuleNameConverter" �� Triggers# �� Triggers$ ��Name7   1CompanyName.Atlas.UIControls.Properties.Resources%   No_Installed_Modules& J� Effect3  -System.Windows.Media.Effects.DropShadowEffect' N� EnterActions 
 FadeIn  ExpandStoryboard( N� ExitActions  FadeOut  CollapseStoryboard�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
uiControls*clr-namespace:CompanyName.Atlas.UIControls  5      D
properties5clr-namespace:CompanyName.Atlas.UIControls.Properties  5      C	converter5clr-namespace:CompanyName.Atlas.UIControls.Properties  5        5      �� 5   
   $ ../Assets/Button.xaml?�6   �� 5   
   $ ../Assets/Icons.xaml?�6   �� 5	   
   $ ../Assets/Converters.xaml?�6   %�  '         7  7  7 	 7 
 7  7  7  7  �� 5      "   6   .�5   
   ��  �@�UseLayoutRounding . 6.   �� 5   
    �@J�SnapsToDevicePixels . 60   �� 5   
    �@ TextFormattingMode$ Display=�6;   �� 5   
    � ��# �   6'   �� 5   
    �@  SelectedModule 6,   ��  �� $ uiControls:AtlasWindowL�$ SelectedModule��� 5   
    � �� 5      �� 5      "   6"   	 5      ���� 5      
FadeIn .��5      U� $
 (UIElement.Opacity)�6<   .\�5    "     $ 0��67   $	 .65Q�6C     5!   "   $ 0:0:0.4��67   $ 1Q�6I   5"      5#      �� 5$      FadeOut .��5%      U� $
 (UIElement.Opacity)�6<   .\�5&   "     $ 0��67   $ 1Q�6C     5'   "   $ 0:0:0.4��67   $	 .65Q�6I   5(      5)      �� 5*      ExpandStoryboard .��5+      U� $
 (FrameworkElement.Width)�6<   $ GridModuleListView��6q   .\�5,   "     $ 0��67   $ 0Q�6C     5-   "   $ 0:0:0.15��67   $	 200Q�6J    5.   &     5/   *   $ EaseOut=�64   51   #   52      53      �� 54      CollapseStoryboard .��55      U� $
 (FrameworkElement.Width)�6<   $ GridModuleListView��6q   .\�56   "     $ 0��67   $	 200Q�6C     57   "   $ 0:0:0.15��67   $ 0Q�6J    58   &     59   *   $ EaseOut=�64   5;   #   5<      5=      �� 5>      '��       " ��6    .�5?      ��  �@�FocusVisualStyle 6A   Z� �� 5@       � ��
 ���� 6;   �� 5A       � ��
 ���� 5B      �� 5C       � ��$ 0q�5D      �� 5E       � ��$ 0q�5F      �� 5G       � ��# �  6;   �� 5H       � ��$ 0q�5I      �� 5J       � ��$ Stretch=�5K      �� 5L       � ��$ Center=�5M      �� 5N       � �� 5O   "   �� 5P   &   " ��66   .R�5Q   *   � #��z��6/   .+�5[   .   �� $ contentPresenter��6?   #��z��5\   +   #��z��5]   '   $y�	Collapsed=�5^   '   #��z��5_   '   # z 5`   '   #��z��5a   '   #��z��5b   '   # z 5c   '   5d   +    5e   *   P� 5f   .    6[   Z�  �@��ColumnCollection. �5g   2   �� $ contentPresenter��69    � y�$ Visible=�6m   5h   /   P� 5i   .    �@J�IsMouseOver . . �5l   2   �� # �  6O    � ��5m   /   P� 5n   .    �@��
IsSelected . . �5q   2   �� # �  6O    � ���� 5r   2    � ��
 ���� 6e   5s   /   P� 5t   .    � {� .  . �5u   2   �� # �  6O    � ��5v   /   5x   '   5z      5{      5|      .R�5~      � $	��400��6   .+�5�      � $
 0.01Q�6   ���#0000005�      � 5�      $ GridModuleListView��6   $��0��6;   $��Right=�6E   $��Disabled=�6a   $��Hidden=�6�   .+�5�      �� $��RightBackgroundContent��6(   $	 0.5Q�6F   ���#0000005�      �� 5�      $ ModuleListView��6'   $��Center=�6?   $��	-2,35,0,0q�6]   $��Center=�6p   #��z 5�   '   
������ 
������ $
 TrueY�5�   [   $��Hidden=�5�   '   $��Hidden=�6Y    5�   O   ��  �� $ uiControls:AtlasWindowL�$ SelectedModule���5�   "   �� 5�   &   (��       63   *ListViewItem+)$ ListViewItem��DataTemplate��.R�5�   *   �� $ ModuleButton��61   $��Center=�6H   $	��199��6l   #���  6x   $
��Left=�6�   $��	0,-1,0,-2q�5�   0     5�   �   �� *Ref+8!   ��5�   1   ��  �� $% uiControls:AtlasModuleContainerL�$ Select���6�   ��  �� $ ListViewItemL�.��5�   .   �� $	��100��6:   $��Center=�6H   $��4,0,0,0q�6e   .+�5�   2   F� 8�� $��	0,10,0,10q�6a   
�������$��Center=�5�   x   
�������$	��1.5��6�   ��64   �� *Ref+8!  6�   �� 5�   2   $��18(�6�   $	��0,5q�6�   $��Center=�6�   
���������6<   �� *Ref+8!  6�   5�   /   5�   +   " 5�   *   5�   '   ��5�   "   �� 5�   &   " ��6,   .�5�   *   ��  � ��
 ���� 6]   # 5�   *   P� 5�   .    �@��HasItems .  . �5�   2   ��  � �� 5�   6   �� 5�   :   " ��6J   .R�5�   >   �� $$ 	TextBlock��6I   #��Z% 6\   
�������$y�Visible=�6�   $��SemiBold"�6�   $��Bottom=�6�   $
��0,35q�6  $��Center=�6  & 5�   B     5�   F   6Z   5�   ?   5�   ;   5�   3   5�   /   5�   '   5�      5�      5�       5�      P� 5�       �@J�IsMouseOver . ' 5�      �� 5�   "   8�� 6Y   �� 5�   "   8�� 6c   ( 5�      �� 5�   "   8�� 6Z   �� 5�   "   8�� 6e   5�      5�      5�      5�      5�      