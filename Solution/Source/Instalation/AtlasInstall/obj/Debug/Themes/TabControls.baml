   M S B A M L   `   `   `  ����G  CAtlasInstall, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CompanyName.Atlas.UIControls.Converters'CompanyName.Atlas.UIControls.Converters  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 !  PlanningExecutionTabItemStyle  �� 
TargetType �� Property . *PlanningExecutionTabItem.Static.Foreground �� Value 1 -PlanningExecutionTabControl.Static.Background �� 
TargetType � 	Resources  Key �� TargetProperty �� 
TargetName9   3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value	 ��Name . *PlanningExecutionTabItem.Static.Background
 J� OpacityMask ��Name J� SnapsToDevicePixels � FocusVisualStyle �� RelativeSource �� AncestorType F�Name J� Opacity �� Triggers P� Property P� Value N� ExitActions ��Name  HideSelectionMarker N� EnterActions  ShowSelectionMarker 0 ,PlanningExecutionTabItem.Selected.Foreground P� 
SourceName 1 -PlanningExecutionTabItem.MouseOver.Foreground #	 ResourcesActivitiesTabItemStyle 2
 .ResourcesActivitiesTabControlHeader.Background 0 ,ResourcesActivitiesTabItem.Static.Foreground 3 /ResourcesActivitiesTabItem.MouseOver.Foreground 2 .ResourcesActivitiesTabItem.Selected.Foreground & "ResourcesActivitiesTabControlStyle �Name-  'System.Windows.Input.KeyboardNavigation   TabNavigation � ColumnDefinitions ��Name � RowDefinitions ��Name ��Name  K� IsItemsHost!   TabIndex" K� ZIndex#   DirectionalNavigation  ConceptViewTabControlStyle  ExportTabItemStyle  ItemText  ItemTextHover  VerticalTabControlStyle  Default.FontFamily  Default.FontSize  ScrollBarThumbBackground  ScrollBarThumbBorder$ ��Name  ScrollBarThumbForeground ! ScrollBarThumbBackgroundHover% �� 
TargetName ! ScrollBarThumbForegroundHover $  ScrollBarThumbBackgroundDragging $  ScrollBarThumbForegroundDragging  ButtonBackground  ButtonBorder  
ButtonText   ButtonTextHover ! ButtonTextPressed " ScrollBarButton& J� IsMouseOver' �� LineUpCommand( �� Stretch) �� Mode* �� LineDownCommand+ �� Triggers, �� LineLeftCommand- �� BasedOn. �� Binding/ �� Path0 �� Value1 �� LineRightCommand2 ��Name3 �� CanContentScroll4 ��Name5 �� Orientation6 � Minimum7 �� ScrollableWidth8 � Maximum9 �� ViewportWidth: �� ViewportSize,; �� %ComputedHorizontalScrollBarVisibility< � Cursor4  .System.Windows.Automation.AutomationProperties=   AutomationId> � Value? �� Mode@ �� Columns!A �� ColumnHeaderContainerStyleB �� ColumnHeaderTemplate#C �� ColumnHeaderTemplateSelectorD �� ColumnHeaderStringFormatE �� AllowsColumnReorderF �� ColumnHeaderContextMenuG �� ColumnHeaderToolTipH �� ScrollableHeightI �� ViewportHeight*J �� #ComputedVerticalScrollBarVisibility 0# ,PlanningExecutionTabControlHeader.Background�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      D
converters5clr-namespace:CompanyName.Atlas.UIControls.Converters  5      %�:  &	        7  7  &		 �    &	 t
    7 	 &	 �    7 	 &	 0    7  7  &	 �    7 " 7 " 7 " 7 " �� 5      "  ��62   .�5   
   ��  � ��# �  6'   �� 5	   
    �@�FocusVisualStyle 6-   Z� �� 5
   
    � ��$ 0q�6#   �� 5   
    � ��$ Stretch=�67   �� 5   
    � ��$ Stretch=�65   �� 5   
    � �� 6(   Z� �� 5   
    � ��# �  6'   �� 5   
    � �� 5      �� 5      " ��6"    5      ���� 5      ShowSelectionMarker .��5      U� $ (UIElement.Opacity)�6<   $
 path��6l   .\�5   "      $ 0��67   $ 0Q�6C      5   "   $ 0:0:0.1��67   $ 1Q�6I   5      5      �� 5      HideSelectionMarker .��5      U� $ (UIElement.Opacity)�6<   $
 path��6l   .\�5   "      $ 0��67   $ 1Q�6C      5   "   $ 0:0:0.05��67   $ 0Q�6J   5      5       5!      .R�5#      � .+�5$      �� $	 
mainBorder��6!   $��35��65   #��z��6A   #���  6u   $��0q�6�   

 �  ��._�5%      �� $ contentPresenter��6/   $��Header��6I   #��z��6�   ��. # z 69   6`   Z� ��6|   �� *HorizontalContentAlignment+ �� " ����6u  �� *VerticalContentAlignment+ �� " ��5&      F� 5'      $
 path��6   #��z��6-   $��Bottom=�6Q   $��5��6l   $��10��6w   "����    @KL �@KL     р�� @KL 	$y�Visible=�6�   $ 0Q�6�   
 6�   Z� 5(       5*      P� 5+       �@��
IsSelected .  5,      �� 5-   "   $) #HideSelectionMarker_BeginStoryboard��62   8��  6_    5/      �� 50   "   8�� 62   52      P� 53       �@��
IsSelected . . �54      �� # �  6;    � ��55      P� 56      $ 
mainBorder��6"    �@J�IsMouseOver . . �57      �� # �  6;    � ��58      5:      5<      5=      �� 5>      "  ��64   .�5?   
   ��  �@�FocusVisualStyle 6-   Z� �� 5@   
    � ��# � 
 6'   �� 5A   
    � ��# �  6'   �� 5B   
    � �� 6(   Z� �� 5D   
    � ��$ 0q�6#   �� 5E   
    � ��$ Stretch=�67   �� 5F   
    � ��$ Stretch=�65   �� 5G   
    � �� 5H      �� 5I      " ��6"   .R�5J      �� $	 
mainBorder��6   #��z��61   $��1,1,1,0q�6]   #��z��6w   $��0q�6�   $��35��6�   ._�5K      �� $ contentPresenter��6+   $��Header��6E   #��z��6�   ��. # z 65   6\   Z� ��6x   �� *HorizontalContentAlignment+ �� " ����6q  �� *VerticalContentAlignment+ �� " ��5L       5M      P� 5N      $ 
mainBorder��6F    �@J�IsMouseOver . . �5O      �� # �  6;    � ��5P      P� 5Q       �@��
IsSelected . . �5R      �� # �  6;    � ��5S      5U      5W      5X      �� 5[      "  ��67   .�5\   
   ��  �@�FocusVisualStyle 6-   Z� �� 5]   
    � ��$ Center=�67   �� 5^   
    � ��$ Center=�65   �� 5_   
    � ��# � ��6'   �� 5`   
    � ��$ 0q�6,   �� 5a   
    � ��# � 
 6'   �� 5b   
    � ��$ 0q�6$   �� 5c   
    � ��8   6/   �� 5e   
    � �� 5f      �� 5g      " ��6"   .R�5h      � $ templateRoot��6   }�.  . $ Local=�6`    5i      �� 5j      $ ColumnDefinition0��6/   �� 5k      $ ColumnDefinition1��6/   $��0 �6J    5m      �� 5n      $ RowDefinition0��6,   $
��Auto �6D   �� 5o      $ RowDefinition1��6,   $��* �6D   .+�5q      �� $ headerPanel��6#   
������ $��0��6Q     . $��0��6t   $! 1��6�   $" 1��6�   �� 5r      $	 contentPanel��6!   #��z��67   #��z��6c   #��z��6�   $��0��6�   $# 	Contained=�6�   $��1��6  $! 2��6  $ Local=�63  ._�5s      �� $ PART_SelectedContentHost��6/   $��SelectedContent��6Q   #��z��6q   # z 6�   5t      5u      5v      5x      5y      �� 5{      "  ��6/   .�5|   
   ��  �@�FocusVisualStyle 6-   Z� �� 5}   
    � ��$ Center=�67   �� 5~   
    � ��$ Center=�65   �� 5   
    � ��# � ��6'   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��# � 
 6'   �� 5�   
    � ��$ 0q�6$   �� 5�   
    � ��8   6/   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      � $ templateRoot��6   }�.  . $ Local=�6`    5�      �� 5�      $ ColumnDefinition0��6/   �� 5�      $ ColumnDefinition1��6/   $��0 �6J    5�      �� 5�      $ RowDefinition0��6,   $
��Auto �6D   �� 5�      $ RowDefinition1��6,   $��* �6D   .+�5�      � $��0��6   $��0��6/   .+�5�      �� $ headerPanel��6'   
������   . $! 1��6i   $" 1��6�   5�      �� 5�      $	 contentPanel��6%   #��z��6;   #��z��6g   #��z��6�   $��0��6�   $# 	Contained=�6�   $��1��6
  $! 2��6  $ Local=�67  ._�5�      �� $ PART_SelectedContentHost��6/   $��SelectedContent��6Q   #��z��6q   # z 6�   5�      5�      5�      5�      5�      �� 5�      "  ��6'   .�5�   
   ��  � ��
 ���� 6'   �� 5�   
    � ��# �  6'   �� 5�   
    �@�FocusVisualStyle 6-   Z� �� 5�   
    � ��$ 0q�6#   �� 5�   
    � ��$ Stretch=�67   �� 5�   
    � ��$ Stretch=�65   �� 5�   
    � �� 6(   Z� �� 5�   
    � �� 5�      �� 5�      " ��6"    5�      ���� 5�      ShowSelectionMarker .��5�      U� $ (UIElement.Opacity)�6<   $
 path��6l   .\�5�   "      $ 0��67   $ 0Q�6C      5�   "   $ 0:0:0.1��67   $ 1Q�6I   5�      5�      �� 5�      HideSelectionMarker .��5�      U� $ (UIElement.Opacity)�6<   $
 path��6l   .\�5�   "      $ 0��67   $ 1Q�6C      5�   "   $ 0:0:0.05��67   $ 0Q�6J   5�      5�      5�      .R�5�      � .+�5�      �� $	 
mainBorder��6!   $��35��65   #��z��6A   #���  6u   $��0q�6�   

 �  ��._�5�      �� $ contentPresenter��6/   $��Header��6I   #��z��6�   ��. # z 69   6`   Z� ��6|   �� *HorizontalContentAlignment+ �� " ����6u  �� *VerticalContentAlignment+ �� " ��5�      F� 5�      $
 path��6   #��z��6-   $��Bottom=�6Q   $��5��6l   $��10��6w   "����    @KL �@KL     р�� @KL 	$y�Visible=�6�   $ 0Q�6�   
 6�   Z� 5�       5�      P� 5�       �@��
IsSelected .  5�      �� 5�   "   $) #HideSelectionMarker_BeginStoryboard��62   8��  6_    5�      �� 5�   "   8�� 62   5�      P� 5�       �@��
IsSelected . . �5�      �� # �  6;    � ��5�      P� 5�      $ 
mainBorder��6"    �@J�IsMouseOver . . �5�      �� # �  6;    � ��5�      5�      5�      5�      �� 5�      "  ��6)   .�5�   
   ��  � ��$ Center=�67   �� 5�   
    �@�FocusVisualStyle 6-   Z� �� 5�   
    � ��$ Center=�65   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��
 ���� 6'   �� 5�   
    � ��# �  6'   �� 5�   
    � ��$ 0q�6$   �� 5�   
    � ��# �   6/   �� 5�   
    � ��# �  6'   �� 5�   
    � ��# �  6%   �� 5�   
    � �� 5�      �� 5�      " ��6"    5�      ���� 5�      ScrollBarThumb "  n�67   .�5�      ��  �@�OverridesDefaultStyle . 6F   �� 5�       � �� .  6:   �� 5�       � ��# �  6;   �� 5�       � ��# �  6<   �� 5�       � ��$ 1q�6@   �� 5�       � �� 5�   "   �� 5�   &   " n�66   .R�5�   *   �� #��z��61   #��z��5      #��z��5     ._�5  .   �� $
$ Rect��68   #���  6F   #��z��6x   5  +    5  *   P� 5  .    �@J�IsMouseOver . . �5  2   �� # �  6O    � ���� 5  2   $
% Rect��69   # �  6[    � ��5	  /   P� 5
  .    �@n�
IsDragging . . �5  2   �� # �  6O    � ���� 5  2   $
% Rect��69   # �  6[    � ��5  /   5  '   5     5     �� 5     ScrollBarButton "  ��5     .�5     ��  �@�OverridesDefaultStyle . 5     �� 5      � ��# �  6;   �� 5      � ��# �  6<   �� 5      � ��$ 1q�6@   �� 5      � ��# �  6;   �� 5      � |� .  5     �� 5      � �� .  5     �� 5       � ��$ Center=�6K   �� 5!      � ��$ Center=�6I   �� 5"      � �� 5#  "   �� 5$  &   " ��66   .R�5%  *   �� 
������ 
������ #��z��5'     #��z��5(     #��z��5)     ._�5*  .   �� #��z��6?   #��z��5+      #��z��5,      ��. # z 5.      5/  +    50  *   P� 51  .    �@J�IsMouseOver . . �54  2   �� # �   6O    � ��55  /   P� 56  .    � �� . . �59  2   �� # � ! 6O    � ��5:  /   5G  '   5I     5J     �� 5L     ScrollBarPageButton "  ��5M     .�5N     ��  �@�OverridesDefaultStyle . 5O     �� 5P      � ��
 ���� 5Q     �� 5R      � |� .  5S     �� 5T      � �� .  5U     �� 5V      � �� 5W  "   �� 5X  &   " ��66   .R�5Y  *   �� #��z��64   #��z��5Z     #��z��5[     5\  '   5^     5_     �� 5a     '��       "  ��6;   .�5b     ��  � ��
 ���� 6;   �� 5c      �@��IsPressAndHoldEnabled .  6M   �� 5d      �@��IsFlicksEnabled .  6G   �� 5e      �@�Value$ 0Q�66   �� 5f      �@�LargeChange$ 35Q�6<   �� 5g      �@�SmallChange$ 35Q�6<   �� 5h      � �� 5i  "   �� 5j  &   " ��66   .R�5k  *   � $��Bg��6/   
������  .  5n  .   �� 5o  2   #��� q�6@   �� 5p  2   $��0.00001* �6@   �� 5q  2   #��� q�6@   .+�5s  .   �� 8��  #{�z& 5t  '   #��Z' 5u  '   $��0,0,0,0q�5v  '   .��5w  2   F� $��15��67   $��7��6B   $
( Fill=�6M   "����     	= ��g5     ���j  	= 	��6\   �� *
Foreground+ �� $) FindAncestor=�$ RepeatButtonL�6�   5x  /   �� 5�  .   8�� $��2��5�  '   #{�z& 5�  '   #��Z* 5�  '   $��0,0,0,0q�5�  '   .��5�  2   F� $��15��67   $��7��6B   $
( Fill=�6M   "����        ��g5  	= ���j     	��6\   �� *
Foreground+ �� $) FindAncestor=�$ RepeatButtonL�6�   5�  /   5�  +   5�  '   5�     + 5�     P� 5�  "    �@��Orientation$ 
Horizontal=�6A   . �5�  &   ��  � ��$
 Auto��6>   �� 5�  &    � ��$ 0��6A   �� 5�  &    � ��$ 7��6?   �� 5�  &    � ��$ 7��6B   �� 5�  &    � �� 5�  *   �� 5�  .   " ��6>   .R�5�  2   � $��Bg��67   #��z��5�  #    .  5�  6   �� 5�  :   #��� ��6K   �� 5�  :   $��0.00001* �6K   �� 5�  :   #��� ��6K   .+�5�  6   �� #��Z, 5�  /   $��	-8,0,0,-5q�5�  /   ��5�  :   �� 5�  >   "  ��6D   8-  + 5�  B   �� 5�  F   . 6R   ��  �� $ 	ScrollBarL�$/ Value�0 06�   .a�5�  J   ��  � y�$ 	Collapsed=�6g   5�  G   5�  ?   .��5�  :   F� $��4��6?   $��7��6I   $
( Fill=�6T   "����    �g5 � 	= ��j � 	=     	��6c   �� *
Foreground+ �� $) FindAncestor=�$ RepeatButtonL�6�   5�  7   �� 5�  6   $��2��5�  /   8�� #��Z1 5�  /   $��
0,0,-10,-5q�5�  /   .��5�  :   F� $��4��6?   $��7��6I   $
( Fill=�6T   "����        �    ��j � 	= �g5 	��6c   �� *
Foreground+ �� $) FindAncestor=�$ RepeatButtonL�6�   5�  7   5�  3   5�  /   5�  '   5�  #   5�     �� 5�     '��       "  ��6>   .�5�     ��  �@�FocusVisualStyle 6A   Z� �� 5�      � ��$
 Auto=�6N   �� 5�      � �� 5�  "   �� 5�  &   " ��66   .R�5�  *   � 
������ .+�5�  .   �� $'	 !BorderPART_ScrollContentPresenter��65   ._�5�  2   �� $!2 PART_ScrollContentPresenter��6I   #��z��5�  5   #��z��5�  5   #��z��5�  5   #3 z��5�  5   5�  /   �� 5�  .   $4 PART_HorizontalScrollBar��69   $5 
Horizontal=�5�  )   $	��Top=�5�  )   $	6 0.0Q�5�  )   #8 z7 5�  )   #: z9 5�  )   #y�z; 5�  (   $< Arrow��5�  )   $= HorizontalScrollBar��5�  )   > 5�  )   ��  �� *TemplatedParent+$/ HorizontalOffset�$? OneWay=�5�  )   5  +   5  '   5
     5     �� 5     '��       "  ��6Y   (��       6    *'%GridView.GridViewScrollViewerStyleKey+).�5     ��  � |� .  6:   �� 5      �@�FocusVisualStyle 6A   Z� �� 5      � �� 5  "   �� 5  &   " ��66   .R�5  *   � 
������  5  .   �� 5  2   $��* �6C   �� 5  2   $
��Auto �6C    5  .   �� 5  2   $��* �6@   �� 5  2   $
��Auto �6@   .+�5  .   ]� #��z��68   .+�5  2   �� $	��Top=�6?   $��Hidden=�5  !   $��Hidden=�5   !   |�.  .��5"  6   �� $��2,0,2,0q�6Q   # z 5+  "   @ 5#  "   �� *TemplatedParent.View.Columns+ �� *TemplatedParent+A 5$  "   �� *1/TemplatedParent.View.ColumnHeaderContainerStyle+ �� *TemplatedParent+B 5%  "   �� *+)TemplatedParent.View.ColumnHeaderTemplate+ �� *TemplatedParent+C 5&  "   �� *31TemplatedParent.View.ColumnHeaderTemplateSelector+ �� *TemplatedParent+D 5'  "   �� */-TemplatedParent.View.ColumnHeaderStringFormat+ �� *TemplatedParent+E 5(  "   �� **(TemplatedParent.View.AllowsColumnReorder+ �� *TemplatedParent+F 5)  "   �� *.,TemplatedParent.View.ColumnHeaderContextMenu+ �� *TemplatedParent+G 5*  "   �� **(TemplatedParent.View.ColumnHeaderToolTip+ �� *TemplatedParent+5+  "   5,  3   �� 5-  2   $!2 PART_ScrollContentPresenter��6I   $# Local=�5.  !   #��z��5/  !   #��z��50  !   #3 z��51  !   # z 52  !   53  /   �� 55  .   $4 PART_VerticalScrollBar��68   $��1��56      $	6 0.0Q�57      #8 zH 58      #: zI 59      #y�zJ 5;      $< Arrow��5<      $= VerticalScrollBar��5=      > 5:      �� *VerticalOffset+$? OneWay=� �� *TemplatedParent+5=      �� 5>  .   $4 PART_HorizontalScrollBar��69   $5 
Horizontal=�5?      $��1��5@      $	6 0.0Q�5A      #8 z7 5B      #: z9 5C      #y�z; 5E      $< Arrow��5F      $= HorizontalScrollBar��5G      > 5D      �� *HorizontalOffset+$? OneWay=� �� *TemplatedParent+5G      5H  +   5I  '   5K     5L     5N     .R�5O     � $ templateRoot��6   }�.  . $ Local=�6`    5P     �� 5Q     $ ColumnDefinition0��6/   $
��Auto �6K   �� 5R     $ ColumnDefinition1��6/   $��* �6J   .+�5X     �� $��0q�6!   $��0��6,   $" 1��6<   $��35��6M   #��� # 5Y  )   ._�5Z     ]� .+�5]  "   �� $
��Left=�61   ��. .��5_  &   �� $ headerPanel��6/   
������   . $! 1��6q   $" 1��6�   $
��Left=�6�   5a  '   5b  #   5c     5d     �� 5f     $	 contentPanel��6!   #��z��67   #��z��6k   $# 	Contained=�6�   $��1��6�   $! 2��6�   $ Local=�6�   ._�5g     �� $ PART_SelectedContentHost��6/   $��SelectedContent��6Q   #��z��6q   # z 6�   5h     5i     5j     5l     5m     5r     