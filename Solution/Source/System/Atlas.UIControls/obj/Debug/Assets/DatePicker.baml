   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source(   "System.Windows.Controls.DatePicker �� 
TargetType �� Property �� Value   	InputText&   System.Windows.Controls.Calendar �� BasedOn �� 
TargetType �� CornerRadius5  /System.Windows.Controls.Primitives.CalendarItem  PopupBackground   CalendarItemStyle	 ��Name
 �� Padding �Name � ColumnDefinitions/   )CompanyName.Atlas.UIControls.ModernButton  Name   EllipseStrokeThickness   	IconWidth   
IconHeight   IconData �� ElementName �� Path:  4System.Windows.Controls.Primitives.DatePickerTextBox  Name �Name � HorizontalOffset � VerticalOffset � 	StaysOpen � AllowsTransparency � PlacementTarget �� Triggers P� Property P� Value �� 
TargetName  InputTextDisabled ]� 
Conditions  �� Property! �� Value  InputTextHover'  !System.Windows.VisualStateManager"   VisualStateGroups%  System.Windows.VisualStateGroup#   Name$   Transitions%  System.Windows.VisualTransition%   GeneratedDuration&   States   System.Windows.VisualState'   Name(   
Storyboard) �� 
TargetName* �� TargetProperty	+ W� To, g� Duration- J� Opacity. ��Name/ J� IsHitTestVisible0 ��Name�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
uiControls*clr-namespace:CompanyName.Atlas.UIControls  5        5      �� 5   
   $ Calendar.xaml?�6   %U  '         7 '      �� 5	      "   6   .�5
   
   ��  � ��
 ���� 6'   �� 5   
    � ��# �   6'   �� 5   
    � ��
 ���� 6(   �� 5   
    � ��$ 1q�6,   �� 5   
    �@  IsTodayHighlighted . 6/   �� 5   
    �@  SelectedDateFormat$ Short=�6/   �� 5   
    � ��$ 2,2,2,1q�6$   �� 5   
    � ��$ Center=�6.   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ Stretch=�67   �� 5   
    � |� . 6&   �� 5   
    � ��$	 100��6"   �� 5   
    �@  CalendarStyle 5      �� 5      "  6   8   .�5      ��  � �� 5      �� 5      "  6.   .R�5   "   � $��	PART_Root��6'   $��4q�68   $��Center=�6C   .+�5   &   �� ���	#0C000000���	#0F000000$��1q�6\   $	 2.5��6p   $��-1q�6�   �� 5    &   ���	#0A000000���	#0C000000$��1q�6\   $
 2.75��6p   $��-2q�6�     5"   &   $��PART_CalendarItem��63   #���  5#   3   #��z 5$   3   $��0q�5%   3   $��1q�5&   3   �� 5(   &   
������ $��1q�6G   5)   #   5*      5,      5-      5/      �� 51   
    � �� 52      �� 53      "   6"   .R�54      �� $	 
LayoutRoot��6   #��z��61   #��z��55      #��z��56      #
 z��57      ._�58      � $ 	PART_Root��6   #��z��59      #��z��5:       5;      �� 5<   "   $��* �63   �� 5=   "   $
��Auto �63   .+�5?        $ PART_Button��66   $��0��6K   $��1��6X   $��16��5@   4   $��20��5A   4   $��0,-3,0,0q�5B   4   $��-3,0,0,0q�5C   4   |�.  $��Center=�5E   4   $
��Left=�5F   4   $ 0Q�5G   4   $��Right=�5H   6   $ 16Q�5I   4   $ 14Q�6C   � �������(����(�T�Kӄ��P�`H��TnP��zx�P��@�؏��f;q��2h���k%�X���Ty��	����ǘ���@@Q���ǘ���@@������ܽ����ܽ�,��ѐ�4,��ѐ�4ܽ�Qܽ��ǘ���@@Q,���ǘ���@@�,��ܽ����ܽ����,���,��,���,�����ܽ����ܽ�ܽ��ܽ���G�p@fܽ��ܽ�ܽ��D�H�e�D�� _�@ _� _�@ _T81Ӥ _P\ ��l���}���������l���(�P\ ��(71ь�� _�ܽ�� _�x��(71�x���[ �g�|�$C�|��|N
|�Ӹ)|�(�(�[ (�((71�(�(� _�$"u� _�$"uH�e�D�H�e�z@�8�pYG�8�pYG��єz@��єz@�8�L'�T81����T81$C�(x;$C�|H�$C���u�$C�������XɌL'�XɌ��i�XɌӨ XɌN
���N
��u�N
|H�N
(x;� T81�i�T81�L'�T81�j�T81�\ŇT81�}�w;�}|H��}��u��}���\Ň�Ȍj��ȌѸ���Ȍ�pQ��Ȍ�����������u����|H�����w;pQ�T81���T81�j�T81��5J   4   �� $ PART_TextBox��$ 
Foreground�5K   4     5M      $ PART_TextBox��60   #��z��5N   0   #��z��5O   0   #���@��5P   0   $��0q�5Q   0   $��Stretch=�5R   0   $��Center=�5S   0   #��z��5T   0   #|�z|�5U   0   � 5V      $ 
PART_Popup��6$   $��Bottom=�5X   $   $ -4��5Y   $   $ -5��5Z   $    .   .  5W   $   �� $ 
LayoutRoot��5\   $   5]      5^       5`      P� 5a       � {� .  . �5b      �� $ PART_TextBox��6%   # �  6U    � ��5c      P� 5g       �@J�IsKeyboardFocusWithin . 5i      ]� 5j       5k      �� 5l   "     � {�! . 6A   �� 5m   "     �@J�IsMouseOver! . 6C   ./�5q      �� # �  6;    � ��5r      5u      5w      5x      �� 5z      "  6   .�5{   
   ��  � ��# �   6'   �� 5|   
    � ��
 ���� 6'   �� 5}   
    � ��$ 0q�6,   �� 5~   
    � ��$ 1,0,0,0q�6$   �� 5   
    �@��PanningMode$ VerticalFirst=�65   �� 5�   
    �@��IsFlicksEnabled .  63   �� 5�   
    �@�FocusVisualStyle 6-   Z� �� 5�   
    � �� 5�      �� 5�      "  6"   .R�5�      � " 5�        5�      $# WatermarkStates��6/   $ 5�   "     5�   &   $% 0D�67   .& 5�   "     $' Unwatermarked��6.     5�   "   $' Watermarked��6.   .( 5�   &   �� .��5�   *   W� $) ContentElement��6:   $* Opacity�6a   $+ 0X�6�   $, 0D�6�   W� 5�   *   $) PART_Watermark��6:   $* Opacity�6a   $+ 1X�6�   $, 0D�6�   5�   '   5�   #   5�      .+�5�      �� $	 Border��6!   #��z��5�   !   #��z��5�   !   #��z��5�   !   #
 z��5�   !   ._�5�      � $ WatermarkContent��6#   #��z��5�   #   #��z��5�   #   .+�5�   "   �� $	 ContentElement��6)   $��0q�6A   $- 1Q�6U   �� 5�   "   $	 watermark_decorator��6)   $��0q�6F   ._�5�   &   �� $. PART_Watermark��65   $- 0Q�5�   5   |�.  / .  $	��3,0q�5�   5   5�   #   �� 5�   "   $0 PART_ContentHost��6/   $��0q�5�   /   #��z��5�   /   #��z��5�   /   5�      5�      5�       5�      P� 5�       � {� .  . �5�      �� $ PART_Watermark��6%   # �  6W    � ��5�      P� 5�       �@��
IsReadOnly . . �5�      �� $ PART_Watermark��6%   # �  6W    � ��5�      5�      5�      5�      5�      