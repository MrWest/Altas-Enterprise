   M S B A M L   `   `   `  ���� G  CAtlasInstall, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CompanyName.Atlas.UIControls.Converters'CompanyName.Atlas.UIControls.Converters  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 !  PlanningExecutionTabItemStyle  �� 
TargetType �� Property . *PlanningExecutionTabItem.Static.Foreground �� Value 1 -PlanningExecutionTabControl.Static.Background �� 
TargetType � 	Resources  Key �� TargetProperty �� 
TargetName9   3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value	 ��Name . *PlanningExecutionTabItem.Static.Background
 J� OpacityMask ��Name J� SnapsToDevicePixels � FocusVisualStyle �� RelativeSource �� AncestorType F�Name J� Opacity �� Triggers P� Property P� Value N� ExitActions ��Name  HideSelectionMarker N� EnterActions  ShowSelectionMarker 0 ,PlanningExecutionTabItem.Selected.Foreground P� 
SourceName 1 -PlanningExecutionTabItem.MouseOver.Foreground #	 ResourcesActivitiesTabItemStyle 2
 .ResourcesActivitiesTabControlHeader.Background 0 ,ResourcesActivitiesTabItem.Static.Foreground 3 /ResourcesActivitiesTabItem.MouseOver.Foreground 2 .ResourcesActivitiesTabItem.Selected.Foreground & "ResourcesActivitiesTabControlStyle �Name-  'System.Windows.Input.KeyboardNavigation   TabNavigation � ColumnDefinitions ��Name � RowDefinitions ��Name ��Name  K� IsItemsHost!   TabIndex" K� ZIndex#   DirectionalNavigation  ConceptViewTabControlStyle  ExportTabItemStyle  ItemText  ItemTextHover  VerticalTabControlStyle  Default.FontFamily  Default.FontSize  ScrollBarThumbBackground  ScrollBarThumbBorder$ ��Name  ScrollBarThumbForeground ! ScrollBarThumbBackgroundHover% �� 
TargetName ! ScrollBarThumbForegroundHover $  ScrollBarThumbBackgroundDragging $  ScrollBarThumbForegroundDragging  ButtonBackground  ButtonBorder  
ButtonText   ButtonTextHover ! ButtonTextPressed " ScrollBarButton& J� IsMouseOver' �� LineUpCommand( �� Stretch) �� Mode* �� LineDownCommand+ �� Triggers, �� LineLeftCommand- �� BasedOn. �� Binding/ �� Path0 �� Value1 �� LineRightCommand2 ��Name3 �� CanContentScroll4 ��Name5 �� Orientation6 � Minimum7 �� ScrollableWidth8 � Maximum9 �� ViewportWidth: �� ViewportSize,; �� %ComputedHorizontalScrollBarVisibility< � Cursor4  .System.Windows.Automation.AutomationProperties=   AutomationId> � Value? �� Mode@ �� Columns!A �� ColumnHeaderContainerStyleB �� ColumnHeaderTemplate#C �� ColumnHeaderTemplateSelectorD �� ColumnHeaderStringFormatE �� AllowsColumnReorderF �� ColumnHeaderContextMenuG �� ColumnHeaderToolTipH �� ScrollableHeightI �� ViewportHeight*J �� #ComputedVerticalScrollBarVisibility 0# ,PlanningExecutionTabControlHeader.Background�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     D
converters5clr-namespace:CompanyName.Atlas.UIControls.Converters  %�$  &	        7  7  &		 9    &	 �    7 	 &	 2	    7 	 &	 �    7  7  &	     7 " 7 " 7 " 7 " �� "  ��.���  � ��# �  ��  �@�FocusVisualStyle Z� ��  � ��$ 0q���  � ��$ Stretch=���  � ��$ Stretch=���  � �� Z� ��  � ��# �  ��  � �� �� " �� ���� ShowSelectionMarker .��U� $ (UIElement.Opacity)�$
 path��.\�   $ 0��$ 0Q�   $ 0:0:0.1��$ 1Q��� HideSelectionMarker .��U� $ (UIElement.Opacity)�$
 path��.\�   $ 0��$ 1Q�   $ 0:0:0.05��$ 0Q�.R�� .+��� $	 
mainBorder��$��35��#��z��#���  $��0q�

 �  ��._��� $ contentPresenter��$��Header��#��z����. # z  Z� ���� *HorizontalContentAlignment+ �� " ������ *VerticalContentAlignment+ �� " ��F� $
 path��#��z��$��Bottom=�$��5��$��10��"����    @KL �@KL     р�� @KL 	$y�Visible=�$ 0Q�
 Z�  P�  �@��
IsSelected .  �� $) #HideSelectionMarker_BeginStoryboard��8��   �� 8�� P�  �@��
IsSelected . . ��� # �   � ��P� $ 
mainBorder�� �@J�IsMouseOver . . ��� # �   � ���� "  ��.���  �@�FocusVisualStyle Z� ��  � ��# � 
 ��  � ��# �  ��  � �� Z� ��  � ��$ 0q���  � ��$ Stretch=���  � ��$ Stretch=���  � �� �� " ��.R��� $	 
mainBorder��#��z��$��1,1,1,0q�#��z��$��0q�$��35��._��� $ contentPresenter��$��Header��#��z����. # z  Z� ���� *HorizontalContentAlignment+ �� " ������ *VerticalContentAlignment+ �� " �� P� $ 
mainBorder�� �@J�IsMouseOver . . ��� # �   � ��P�  �@��
IsSelected . . ��� # �   � ���� "  ��.���  �@�FocusVisualStyle Z� ��  � ��$ Center=���  � ��$ Center=���  � ��# � ����  � ��$ 0q���  � ��# � 
 ��  � ��$ 0q���  � ��8   ��  � �� �� " ��.R�� $ templateRoot��}�.  . $ Local=� �� $ ColumnDefinition0���� $ ColumnDefinition1��$��0 � �� $ RowDefinition0��$
��Auto ��� $ RowDefinition1��$��* �.+��� $ headerPanel��
������ $��0��  . $��0��$! 1��$" 1���� $	 contentPanel��#��z��#��z��#��z��$��0��$# 	Contained=�$��1��$! 2��$ Local=�._��� $ PART_SelectedContentHost��$��SelectedContent��#��z��# z �� "  ��.���  �@�FocusVisualStyle Z� ��  � ��$ Center=���  � ��$ Center=���  � ��# � ����  � ��$ 0q���  � ��# � 
 ��  � ��$ 0q���  � ��8   ��  � �� �� " ��.R�� $ templateRoot��}�.  . $ Local=� �� $ ColumnDefinition0���� $ ColumnDefinition1��$��0 � �� $ RowDefinition0��$
��Auto ��� $ RowDefinition1��$��* �.+�� $��0��$��0��.+��� $ headerPanel��
������   . $! 1��$" 1���� $	 contentPanel��#��z��#��z��#��z��$��0��$# 	Contained=�$��1��$! 2��$ Local=�._��� $ PART_SelectedContentHost��$��SelectedContent��#��z��# z �� "  ��.���  � ��
 ���� ��  � ��# �  ��  �@�FocusVisualStyle Z� ��  � ��$ 0q���  � ��$ Stretch=���  � ��$ Stretch=���  � �� Z� ��  � �� �� " �� ���� ShowSelectionMarker .��U� $ (UIElement.Opacity)�$
 path��.\�   $ 0��$ 0Q�   $ 0:0:0.1��$ 1Q��� HideSelectionMarker .��U� $ (UIElement.Opacity)�$
 path��.\�   $ 0��$ 1Q�   $ 0:0:0.05��$ 0Q�.R�� .+��� $	 
mainBorder��$��35��#��z��#���  $��0q�

 �  ��._��� $ contentPresenter��$��Header��#��z����. # z  Z� ���� *HorizontalContentAlignment+ �� " ������ *VerticalContentAlignment+ �� " ��F� $
 path��#��z��$��Bottom=�$��5��$��10��"����    @KL �@KL     р�� @KL 	$y�Visible=�$ 0Q�
 Z�  P�  �@��
IsSelected .  �� $) #HideSelectionMarker_BeginStoryboard��8��   �� 8�� P�  �@��
IsSelected . . ��� # �   � ��P� $ 
mainBorder�� �@J�IsMouseOver . . ��� # �   � ���� "  ��.���  � ��$ Center=���  �@�FocusVisualStyle Z� ��  � ��$ Center=���  � ��$ 0q���  � ��
 ���� ��  � ��# �  ��  � ��$ 0q���  � ��# �   ��  � ��# �  ��  � ��# �  ��  � �� �� " �� ���� ScrollBarThumb "  n�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � ��# �  ��  � ��# �  ��  � ��$ 1q���  � �� �� " n�.R��� #��z��#��z��#��z��._��� $
$ Rect��#���  #��z�� P�  �@J�IsMouseOver . . ��� # �   � ���� $
% Rect��# �   � ��P�  �@n�
IsDragging . . ��� # �   � ���� $
% Rect��# �   � ���� ScrollBarButton "  ��.���  �@�OverridesDefaultStyle . ��  � ��# �  ��  � ��# �  ��  � ��$ 1q���  � ��# �  ��  � |� .  ��  � �� .  ��  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� 
������ 
������ #��z��#��z��#��z��._��� #��z��#��z��#��z����. # z  P�  �@J�IsMouseOver . . ��� # �    � ��P�  � �� . . ��� # � !  � ���� ScrollBarPageButton "  ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  � �� �� " ��.R��� #��z��#��z��#��z���� '��       "  ��.���  � ��
 ���� ��  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  �@�Value$ 0Q���  �@�LargeChange$ 35Q���  �@�SmallChange$ 35Q���  � �� �� " ��.R�� $��Bg��
������  .  �� #��� q��� $��0.00001* ��� #��� q�.+��� 8��  #{�z& #��Z' $��0,0,0,0q�.��F� $��15��$��7��$
( Fill=�"����     	= ��g5     ���j  	= 	���� *
Foreground+ �� $) FindAncestor=�$ RepeatButtonL��� 8�� $��2��#{�z& #��Z* $��0,0,0,0q�.��F� $��15��$��7��$
( Fill=�"����        ��g5  	= ���j     	���� *
Foreground+ �� $) FindAncestor=�$ RepeatButtonL�+ P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 0����  � ��$ 7����  � ��$ 7����  � �� �� " ��.R�� $��Bg��#��z�� .  �� #��� ���� $��0.00001* ��� #��� ��.+��� #��Z, $��	-8,0,0,-5q����� "  ��8-  + �� . ��  �� $ 	ScrollBarL�$/ Value�0 0.a���  � y�$ 	Collapsed=�.��F� $��4��$��7��$
( Fill=�"����    �g5 � 	= ��j � 	=     	���� *
Foreground+ �� $) FindAncestor=�$ RepeatButtonL��� $��2��8�� #��Z1 $��
0,0,-10,-5q�.��F� $��4��$��7��$
( Fill=�"����        �    ��j � 	= �g5 	���� *
Foreground+ �� $) FindAncestor=�$ RepeatButtonL��� '��       "  ��.���  �@�FocusVisualStyle Z� ��  � ��$
 Auto=���  � �� �� " ��.R�� 
������ .+��� $'	 !BorderPART_ScrollContentPresenter��._��� $!2 PART_ScrollContentPresenter��#��z��#��z��#��z��#3 z���� $4 PART_HorizontalScrollBar��$5 
Horizontal=�$	��Top=�$	6 0.0Q�#8 z7 #: z9 #y�z; $< Arrow��$= HorizontalScrollBar��> ��  �� *TemplatedParent+$/ HorizontalOffset�$? OneWay=��� '��       "  ��(��       *'%GridView.GridViewScrollViewerStyleKey+).���  � |� .  ��  �@�FocusVisualStyle Z� ��  � �� �� " ��.R�� 
������  �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+�]� #��z��.+��� $	��Top=�$��Hidden=�$��Hidden=�|�.  .���� $��2,0,2,0q�# z @ �� *TemplatedParent.View.Columns+ �� *TemplatedParent+A �� *1/TemplatedParent.View.ColumnHeaderContainerStyle+ �� *TemplatedParent+B �� *+)TemplatedParent.View.ColumnHeaderTemplate+ �� *TemplatedParent+C �� *31TemplatedParent.View.ColumnHeaderTemplateSelector+ �� *TemplatedParent+D �� */-TemplatedParent.View.ColumnHeaderStringFormat+ �� *TemplatedParent+E �� **(TemplatedParent.View.AllowsColumnReorder+ �� *TemplatedParent+F �� *.,TemplatedParent.View.ColumnHeaderContextMenu+ �� *TemplatedParent+G �� **(TemplatedParent.View.ColumnHeaderToolTip+ �� *TemplatedParent+�� $!2 PART_ScrollContentPresenter��$# Local=�#��z��#��z��#3 z��# z �� $4 PART_VerticalScrollBar��$��1��$	6 0.0Q�#8 zH #: zI #y�zJ $< Arrow��$= VerticalScrollBar��> �� *VerticalOffset+$? OneWay=� �� *TemplatedParent+�� $4 PART_HorizontalScrollBar��$5 
Horizontal=�$��1��$	6 0.0Q�#8 z7 #: z9 #y�z; $< Arrow��$= HorizontalScrollBar��> �� *HorizontalOffset+$? OneWay=� �� *TemplatedParent+.R�� $ templateRoot��}�.  . $ Local=� �� $ ColumnDefinition0��$
��Auto ��� $ ColumnDefinition1��$��* �.+��� $��0q�$��0��$" 1��$��35��#��� # ._�]� .+��� $
��Left=���. .���� $ headerPanel��
������   . $! 1��$" 1��$
��Left=��� $	 contentPanel��#��z��#��z��$# 	Contained=�$��1��$! 2��$ Local=�._��� $ PART_SelectedContentHost��$��SelectedContent��#��z��# z 