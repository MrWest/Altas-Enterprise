   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.39206, Culture=neutral, PublicKeyToken=null{Oclr-namespace:CompanyName.Atlas.UIControls.Converters;assembly=Atlas.UIControls'CompanyName.Atlas.UIControls.Converters  5      {Oclr-namespace:CompanyName.Atlas.UIControls.Properties;assembly=Atlas.UIControls'CompanyName.Atlas.UIControls.Properties  5      qJclr-namespace:CompanyName.Atlas.UIControls.Views;assembly=Atlas.UIControls"CompanyName.Atlas.UIControls.Views  5      eDclr-namespace:CompanyName.Atlas.UIControls;assembly=Atlas.UIControlsCompanyName.Atlas.UIControls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089K    ECompanyName.Atlas.UIControls.Converters.BooleanToConceptViewConverter !  BooleanToConceptViewConverter   BooleanToVisibilityConverter . *BudgetComponentItemViewSwitcherButtonStyle  �� 
TargetType �� Property �� Value / +BudgetComponentItemButton.Static.Background / +BudgetComponentItemButton.Static.Foreground �� 
TargetType �� Padding �� Triggers P� Property P� Value 2 .BudgetComponentItemButton.MouseOver.Foreground 1 -BudgetComponentItemButton.Selected.Foreground $  PlanningExecutionTabControlStyle . *PlanningExecutionTabItem.Static.Foreground 1	 -PlanningExecutionTabControl.Static.Background !
 PlanningExecutionTabItemStyle  Default.FontFamily  Default.FontSize �Name	 J� SnapsToDevicePixels-  'System.Windows.Input.KeyboardNavigation
   TabNavigation � ColumnDefinitions ��Name � RowDefinitions ��Name K� ZIndex 0 ,PlanningExecutionTabControlHeader.Background �� Orientation7   1CompanyName.Atlas.UIControls.Properties.Resources   
Definition � ToolTip d� 	IsChecked �� RelativeSource �� AncestorType �� 	Converter.   (CompanyName.Atlas.UIControls.ConceptView   
Definition �� ConverterParameter �� Stretch   Example   Example   RelatedConcepts   RelatedConcept	 �� Y1	 �� Y2  FilterBoxSeparator.Stroke,   &CompanyName.Atlas.UIControls.FilterBox    FilterCommand!   Filtred_Search" ��Name# K� IsItemsHost$   TabIndex% ��Name&   DirectionalNavigation' ��Name  ButtonlessTabControlStyle( � 	Resources  Key) �� TargetProperty* �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame+ O� KeyTime, O� Value . *PlanningExecutionTabItem.Static.Background- J� OpacityMask. � FocusVisualStyle/ F�Name0 J� Opacity1 �� Triggers2 N� ExitActions3 ��Name  HideSelectionMarker4 N� EnterActions  ShowSelectionMarker 0 ,PlanningExecutionTabItem.Selected.Foreground5 P� 
SourceName 1 -PlanningExecutionTabItem.MouseOver.Foreground # ResourcesActivitiesTabItemStyle 2 .ResourcesActivitiesTabControlHeader.Background 0 ,ResourcesActivitiesTabItem.Static.Foreground 3 /ResourcesActivitiesTabItem.MouseOver.Foreground 2 .ResourcesActivitiesTabItem.Selected.Foreground & "ResourcesActivitiesTabControlStyle�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      _converters1Oclr-namespace:CompanyName.Atlas.UIControls.Converters;assembly=Atlas.UIControls  5      ^
propertiesOclr-namespace:CompanyName.Atlas.UIControls.Properties;assembly=Atlas.UIControls  5      TviewsJclr-namespace:CompanyName.Atlas.UIControls.Views;assembly=Atlas.UIControls  5      S
uiControlsDclr-namespace:CompanyName.Atlas.UIControls;assembly=Atlas.UIControls  5      %�'  &	        &	      &	 &     &	     7  7   7  7  7   7  7  7   7  &	 �    &	
 �    7  7  &	 S    &	 �"    7     5      60   �� 5	      6#   �� 5
      "  	�6?   .�5   
   ��  � ��$ 60��6#   �� 5   
    � ��$ 24��6"   �� 5   
    � ��$ Center=�6.   �� 5   
    � ��$ Center=�60   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ Center=�67   �� 5   
    � ��# �  6'   �� 5   
    � ��# �  6'   �� 5   
    � ��$ 4q�6$   �� 5   
    � �� 5      �� 5      " 	�6"   .R�5      �� #��z��6   #��z��6G   # z��6i   ._�5      �� #��z��6+   #��z��5   3   5      5      5       5    
   P� 5!       �@J�IsMouseOver . . �5"      ��  � ��# �  6/   5#      P� 5$       �@d�	IsChecked$
 TrueY�6+   . �5%      ��  � ��# �  6/   5&      5(      �� 5+      "  ��65   .�5,   
   ��  � ��$ Center=�67   �� 5-   
    �@�FocusVisualStyle 6-   Z� �� 5.   
    � ��$ Center=�65   �� 5/   
    � ��$ 0q�6,   �� 50   
    � ��# �  6'   �� 51   
    � ��# � 	 6'   �� 52   
    � ��$ 0q�6$   �� 53   
    � ��# � 
 6/   �� 54   
    � ��# �  6'   �� 55   
    � ��# �  6%   �� 57   
    � �� 58      �� 59      " ��6"   .R�5:      � $ templateRoot��6   }�. 	 . $
 Local=�6`    5;      �� 5<      $ ColumnDefinition0��6/   �� 5=      $ ColumnDefinition1��6/   $��0 �6J    5?      �� 5@      $ RowDefinition0��6,   $
��Auto �6D   �� 5A      $ RowDefinition1��6,   $��* �6D   .+�5C      �� $��0q�6!   $��0��6,   $ 1��69   $��35��6J   #���  5D   )   ._�5E      ]� .+�5F   "   ]� $��Right=�6,   $��0,0,10,0q�6C   .+�5G   &   �� $ 
Horizontal=�61   $��Center=�6J   $	��Top=�6e   $��Center=�5H   9   .+�5I   *   	� 8��  # Z 5J   >    5K   >   �� *View+ �� *FindAncestor+$ views:SubjectMainViewL�8  # Z .��5L   .   � .+�5M   2   F� $ Uniform=�5N   ?   ������`BT�2Ѭ�T8JE�,�k�I\,�kl����T쒘�D4T��Ѭ���+S��T�2Ӕ��Ha+���`BT�2��"(�R��HX� �_�d2���e�`�!�x���"(�R��$�苄�ܓ���^�4L��$�苄���!@�e���!@x}ќ�n@x}ќ�1@�����^@�����^@�(�@�(@�(Q@�(c�=yX@@�@�e�ʵ�@�e@�e���!@�e��5M   7   �� *
Foreground+ �� *FindAncestor+$ RadioButtonL�5O   ?   F� 5P   2   $��Center=�67   "����    @KL �@KL     р�� @KL 	#��� 	 5Q   ?   $��0,44,0,0q�5R   ?   y�5S   ?   �� *	IsChecked+ �� *FindAncestor+$ RadioButtonL�8  5T   /   5U   +   	� 5V   *   8�� $��3q�6z   # Z 5W   >    5X   >   �� *View+ �� *FindAncestor+$ views:SubjectMainViewL�8  # Z .��5Y   .   � .+�5Z   2   F� $ Uniform=�5[   ?   $��0,1,0,1q�6Q   ����p0��fffff�@@�x�UX=c���,���0�B�T���\������Զ1Q0��fffff�@@� 6n���� �����`F���������������@��р�@��р����@��0GO�@�� u� 6n u� 6n�����5Z   7   �� *
Foreground+ �� *FindAncestor+$ RadioButtonL�5\   ?   F� 5]   2   $��Center=�67   "����    @KL �@KL     р�� @KL 	#��� 	 5^   ?   $��0,44,0,0q�5_   ?   y�5`   ?   �� *	IsChecked+ �� *FindAncestor+$ RadioButtonL�8  5a   /   5b   +   	� 5c   *   8�� # Z 5d   >   $��0,1,0,0q�6x    5e   >   �� *View+ �� *FindAncestor+$ views:SubjectMainViewL�8  # Z .��5f   .   � .+�5g   2   F� $ Uniform=�5h   ?   �����@�e@x}�@�e�2
�`F��2
����2
@�q��7@�q��7����7����7���2
�d��2
���!�2
���!@x}Ѡm@x}Ӡm@x}t$xN���;�?�� KX@x}�lu@x}�lu@x}�@�e@x}�� t���-tJ�h�� ?�@�� ?�р�O ?�р�O l���� l���� l�@��p���0b�� t��@�q0b�� �p��@�� l�@�� l�р�7 l�р�7 ?��� � ?�Ә�� ?�(}{tJ�@�q t��@�q0b��5g   7   �� *
Foreground+ �� *FindAncestor+$ RadioButtonL�5i   ?   F� 5j   2   $��Center=�67   "����    @KL �@KL     р�� @KL 	#��� 	 5k   ?   $��0,44,0,0q�5l   ?   y�5m   ?   �� *	IsChecked+ �� *FindAncestor+$ RadioButtonL�8  5n   /   5o   +   5p   '   5s   #   �� 5u   "   $ 0��6'   $ 20��6.   $��Center=�66   $
��10,0q�6Q   #���  5v   /   $��1��5w   /   $��Right=�6C     5y   "   $��Right=�67   $	��150��6N   $	��200��6]   $
  Find��5{   ?   # Z! 6T   �� 5}   "   $" headerPanel��6+   # . $��0q�6S   $$ 1��5~   3   $ 1��6S   5      5�      �� 5�      $% contentPanel��6!   #��z��67   #��z��6k   $��0��6�   $& 	Contained=�6�   $��1��6�   $$ 2��6�   $
 Local=�6  ._�5�      �� $' PART_SelectedContentHost��6/   $��SelectedContent��6Q   #��z��6q   #	 z	 6�   5�      5�      5�      5�      5�      �� 5�      "  ��6.   .�5�   
   ��  � ��$ Center=�67   �� 5�   
    �@�FocusVisualStyle 6-   Z� �� 5�   
    � ��$ Center=�65   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��# �  6'   �� 5�   
    � ��# � 	 6'   �� 5�   
    � ��$ 0q�6$   �� 5�   
    � ��# � 
 6/   �� 5�   
    � ��# �  6'   �� 5�   
    � ��# �  6%   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      � $ templateRoot��6   }�. 	 . $
 Local=�6`    5�      �� 5�      $ ColumnDefinition0��6/   �� 5�      $ ColumnDefinition1��6/   $��0 �6J    5�      �� 5�      $ RowDefinition0��6,   $
��Auto �6D   �� 5�      $ RowDefinition1��6,   $��* �6D   .+�5�      �� $��0q�6!   $��0��6,   $ 1��69   $��35��6J   #���  5�   )   ._�5�      ]� .+�5�   "   �� $ 0��6'   $ 20��6.   $��Center=�66   $
��10,0q�6Q   #���  5�   /   $��1��5�   /   $��Right=�6C     5�   "   $��Right=�67   $	��150��6N   $	��200��6]   $
  Find��5�   ?   # Z! 6T   �� 5�   "   $" headerPanel��6+   # . $��0q�6S   $$ 1��5�   3   $ 1��6S   5�      5�      �� 5�      $% contentPanel��6!   #��z��67   #��z��6k   $��0��6�   $& 	Contained=�6�   $��1��6�   $$ 2��6�   $
 Local=�6  ._�5�      �� $' PART_SelectedContentHost��6/   $��SelectedContent��6Q   #��z��6q   #	 z	 6�   5�      5�      5�      5�      5�      �� 5�      "  ��62   .�5�   
   ��  � ��# �  6'   �� 5�   
    �@�FocusVisualStyle 6-   Z� �� 5�   
    � ��$ 0q�6#   �� 5�   
    � ��$ Stretch=�67   �� 5�   
    � ��$ Stretch=�65   �� 5�   
    � �� 6(   Z� �� 5�   
    � ��# � 	 6'   �� 5�   
    � �� 5�      �� 5�      " ��6"   ( 5�      ���� 5�      ShowSelectionMarker .��5�      U� $) (UIElement.Opacity)�6<   $
* path��6l   .\�5�   "     $+ 0��67   $, 0Q�6C     5�   "   $+ 0:0:0.1��67   $, 1Q�6I   5�      5�      �� 5�      HideSelectionMarker .��5�      U� $) (UIElement.Opacity)�6<   $
* path��6l   .\�5�   "     $+ 0��67   $, 1Q�6C     5�   "   $+ 0:0:0.05��67   $, 0Q�6J   5�      5      5     .R�5     � .+�5     �� $% 
mainBorder��6!   $��35��65   #��z��6A   #���  6u   $��0q�6�   
- �  ��._�5     �� $' contentPresenter��6/   $��Header��6I   #��z��6�   ��. #	 z	 69  . 6`   Z� ��6|   �� *HorizontalContentAlignment+ �� " ����6u  �� *VerticalContentAlignment+ �� " ��5     F� 5     $
/ path��6   #��z��6-   $��Bottom=�6Q   $��5��6l   $��10��6w   "����    @KL �@KL     р�� @KL 	$y�Visible=�6�   $0 0Q�6�   - 6�   Z� 5     1 5
     P� 5      �@��
IsSelected . 2 5     �� 5  "   $)3 #HideSelectionMarker_BeginStoryboard��62   8��  6_   4 5     �� 5  "   8�� 62   5     P� 5      �@��
IsSelected . . �5     �� # �  6;    � ��5     P� 5     $5 
mainBorder��6"    �@J�IsMouseOver . . �5     �� # �  6;    � ��5     5     5     5     �� 5     "  ��64   .�5  
   ��  �@�FocusVisualStyle 6-   Z� �� 5   
    � ��# �  6'   �� 5!  
    � ��# �  6'   �� 5"  
    � �� 6(   Z� �� 5$  
    � ��$ 0q�6#   �� 5%  
    � ��$ Stretch=�67   �� 5&  
    � ��$ Stretch=�65   �� 5'  
    � �� 5(     �� 5)     " ��6"   .R�5*     �� $% 
mainBorder��6   #��z��61   $��1,1,1,0q�6]   #��z��6w   $��0q�6�   $��35��6�   ._�5+     �� $' contentPresenter��6+   $��Header��6E   #��z��6�   ��. #	 z	 65  . 6\   Z� ��6x   �� *HorizontalContentAlignment+ �� " ����6q  �� *VerticalContentAlignment+ �� " ��5,     1 5-     P� 5.     $5 
mainBorder��6F    �@J�IsMouseOver . . �5/     �� # �  6;    � ��50     P� 51      �@��
IsSelected . . �52     �� # �  6;    � ��53     55     57     58     �� 5;     "  ��67   .�5<  
   ��  �@�FocusVisualStyle 6-   Z� �� 5=  
    � ��$ Center=�67   �� 5>  
    � ��$ Center=�65   �� 5?  
    � ��# � ��6'   �� 5@  
    � ��$ 0q�6,   �� 5A  
    � ��# � 	 6'   �� 5B  
    � ��$ 0q�6$   �� 5C  
    � ��8   6/   �� 5E  
    � �� 5F     �� 5G     " ��6"   .R�5H     � $ templateRoot��6   }�. 	 . $
 Local=�6`    5I     �� 5J     $ ColumnDefinition0��6/   �� 5K     $ ColumnDefinition1��6/   $��0 �6J    5M     �� 5N     $ RowDefinition0��6,   $
��Auto �6D   �� 5O     $ RowDefinition1��6,   $��* �6D   .+�5Q     �� $" headerPanel��6#   
������ $��0��6Q   # . $��0��6t   $$ 1��6�   $ 1��6�   �� 5R     $% contentPanel��6!   #��z��67   #��z��6c   #��z��6�   $��0��6�   $& 	Contained=�6�   $��1��6  $$ 2��6  $
 Local=�63  ._�5S     �� $' PART_SelectedContentHost��6/   $��SelectedContent��6Q   #��z��6q   #	 z	 6�   5T     5U     5V     5X     5Y     5Z     