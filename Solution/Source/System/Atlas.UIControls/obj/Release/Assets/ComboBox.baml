   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ThemeDictionary  �� Source  DownArrowGeometry  ComboBoxToggleButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels F�Name J� RenderTransformOrigin	 �� Triggers
 �� Value �� Binding �� RelativeSource �� AncestorType �� 
TargetName �� Angle �� Path �� Padding P� Property P� Value  ItemBackgroundSelected  ItemTextSelected $  DataGridCell.Selected.Background $  DataGridCell.Selected.Foreground �Name � ColumnDefinitions d�Name d� 	IsChecked �� Mode J� IsHitTestVisible �� SelectedItem �� SelectionBoxItemTemplate# �� SelectionBoxItemStringFormat �� ContentStringFormat �� Triggers �Name  � AllowsTransparency! �� ElementName" �� MaxDropDownHeight# ��Name$ �� ClearTypeHint% ��Name& ��Name' ��Name-   'System.Windows.Input.KeyboardNavigation(    DirectionalNavigation) P� 
SourceName* ]� 
Conditions+ �� Property, �� Value   DataGridEditingComboBoxStyle- �� BasedOn�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      %�  &	        &	 (     &	 G     '�� �    '�� �    7  &	 D    7���� $"  ../Themes/ModernUI.Dark.xaml?��M 0 0 L 3.5 4 L 7 0 Z��� " d�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  �@��	ClickMode$ Press=���  � ��$ 0,0,1,0q���  �@d�	IsChecked$
 TrueY���  � ��$ Right=���  � �� �� " d�.R��� $ templateRoot2�� . #��z��#��z��$��Right=�._��� $ splitBorder�� . $��Right=�$��1q�
������ ._�F� $ path1��#���  #��z��$��0��$ 0.5,0.5&�$��Center=�$��Center=�#��z��	 �� $

 True�� �� *IsDropDownOpen+ �� *FindAncestor+" ��.a��� $ path1�� � z� �� $	 180Q��� " ��.���  � �� �� $  HorizontalContentAlignment� �� " ����  � ��$ Center=���  � ��$ 7,2,8,2q���  � ��$ Bottom=���  � ��$ 0q���  � �� ��  �� $ ComboBoxL�$ 
Foreground���  � ��$ 25����  � �� �� " ��.R��� $��Bd��#��z��#��z��#��z��# z��#��z��#��z�� . ._��� #��z��#��z��# z 	 P�  �@��IsHighlighted . . ��� $ Bd��# �   � ���� # �   � ��P�  � {� .  �� " ��.���  �@�FocusVisualStyle Z� ��  � ��# �  ��  � ��# �  ��  � ��$ Hidden=���  � ��$ Hidden=���  � �� . ��  �@��PanningMode$
 Both=���  � ��# �  ��  � ��$ 25����  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R�� .+��� $��
MainBorder��$��Center=�#��z��#��z��$��0q�#��z��#��z��#��z��._�� $ templateRoot�� .  �� $��* ��� $��13 �.+�d� $ toggleButton��#��z��
������ $��1��8��   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+��  .  #��z��#��z #��z #��z��# z #��z��#��z��# z �� #��z��$��Center=�#��z��#��z������ $	 Tag� �� $ ComboBoxL����� " ��.���  � y�$ 	Collapsed=� ��  �� $ SelectedItem� �� $ ComboBoxL�
 Z� .a���  � y�$ Visible=�� $ 
PART_Popup��  . $��2��$��0q�$��Bottom=����� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+���� *ActualWidth+$ OneWay=�$! 
MainBorder��.��� $ shadow��#��z" $��0q����� *ActualWidth+$! 
MainBorder��.+��� $ dropDownBorder��$��1,0,1,1q�#��z��#��z������ *ActualWidth+$! templateRoot������ " �� ��  �� $! 
PART_Popup��$ IsOpen�
 True._��� $# DropDownScrollViewer��#��z��#��z��.��� $
 grid��$$ Enabled=�.+��� $% canvas��$��0��$��0��.+��� $& 
opaqueRect������ *
Background+$! dropDownBorder������ *ActualHeight+$! dropDownBorder������ *ActualWidth+$! dropDownBorder���� $' ItemsPresenter��$( 	Contained=�# z }�. ���� *ActualWidth+$! templateRoot��	 P� $) 
PART_Popup�� �@�HasDropShadow . P� $) 
PART_Popup�� � �� . P�  �@��HasItems .  . ��� $ dropDownBorder�� � ��$ 95��]� * �� + �@��
IsGrouping, . �� "+ �@%�IsVirtualizingWhenGrouping, .  ./���  � �� .  P� $) DropDownScrollViewer�� � �� .  . ��� $ 
opaqueRect�� �� *VerticalOffset+$! DropDownScrollViewer�� �@��Top�� $ 
opaqueRect�� �� *HorizontalOffset+$! DropDownScrollViewer�� �@��Left�� " ��8-   .��� % �@��IsSynchronizedWithCurrentItem$ FalseY���  � ��$ 0q���  � ��$	 1,1q�