   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   Atlas.UIControls/   )CompanyName.Atlas.UIControls.RouteTextBox   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9System&clr-namespace:System;assembly=mscorlib     ���� *Name+.������   �� 
TargetType"  ��.���  �� Property � �� �� Value Z� ��  � �� "  RouteTextBox.Static.Foreground# �   �� -  'System.Windows.Input.KeyboardNavigation �@ TabNavigation$
 None=���  � ��$
 Left=���  �@�FocusVisualStyle Z� ��  �@J�	AllowDrop . ��  �@��PanningMode$ VerticalFirst=���  �@��IsFlicksEnabled .  ��  � ��$
 Auto����  � ��$
 Auto����  � ��$
 Left=���  � ��$	 540����  � ��  RouteTextBox.FontFamily# �  ��  � ��   RouteTextBox.Static.FontSize# �  ��  � �� Z� ��  � ��$
 Bold"���  � ��$	 0,2q���  � ��$
 Nave����  � �� ��  �� 
TargetType" ��.R���  �� Orientation$ 
Horizontal=�.+���  ��Name$ PART_ContentHost��|�.  $��Hidden=�$��Hidden=�$
��Auto��$
��Auto��$��Bottom=�#��z��#��z��#��z��F�  F�Name$
 path��[�����Q�V�@9���� � @yZ;��2�΄@�_�q�l/K@�7V�d|�v�,@b/<���3b�@k�	$	��8.5��$��3,0,3,6q� �� Stretch$
 Fill=�$��Bottom=�$��4.813��#��z����  ��Name$ PART_MidRoute��$��NoWrap=�  MidRoute.Static.FontFamily#���    MidRoute.Static.FontSize#���  $��Bottom=�$��3,0,0,3q�  MidRoute.Static.Foreground#���  �� $ 	PART_Root��$��NoWrap=�  RootNode.Static.FontFamily#���  #���  $��Bottom=�$��3,0,0,3q�  RootNode.Static.Foreground#���  	 �� Triggers	 ]� 
 ]� 
Conditions
 ��  �� 
SourceName$ PART_MidRoute�� �� Empty �� Value# Z  �� Property � ���� $ 	PART_Root��# Z  � ��./���  �� 
TargetName$
 path�� � y�$ 	Collapsed=�P�  P� 
SourceName$ PART_MidRoute�� P� Value# Z  P� Property � ��. ��� $ PART_MidRoute�� � y�$ 	Collapsed=��� $ 	PART_Root�� � ��$ 0,0,0,3q� �� Triggers ]� 
 �� + �@��#IsInactiveSelectionHighlightEnabled . ��  �@��IsSelectionActive .  ./���  �@��SelectionBrush# � /�.��