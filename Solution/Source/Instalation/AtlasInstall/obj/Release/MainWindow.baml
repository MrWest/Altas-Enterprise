   M S B A M L   `   `   `  ���� G  CAtlasInstall, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AtlasInstallAtlasInstall  7 clr-namespace:AtlasInstall.LogicAtlasInstall.Logic      AtlasInstall.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35KShell?http://schemas.microsoft.com/winfx/2006/xaml/presentation/shell  &localclr-namespace:AtlasInstall  ,logic clr-namespace:AtlasInstall.Logic    � Title$  ATLAS ENTERPRISE��$	��350��$	��525�� � 	Resources    SlideIn �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value   EasingFunction.  (System.Windows.Media.Animation.CubicEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode<  6System.Windows.Media.Animation.EasingThicknessKeyFrame p� KeyTime p� Value	   EasingFunction  SlideOut  FadeOut 
 FadeIn  Window.Default.BorderBrush
 �� Color  Window.Default.Foreground  Window.Default.Background " SystemButton.Static.Foreground % !SystemButton.MouseOver.Background #	 SystemButton.Pressed.Background #
 SystemButton.Pressed.Foreground * &CloseSystemButton.MouseOver.Foreground  SystemButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� ContentStringFormat �� ContentStringFormat �� Triggers P� Property P� Value ( $AtlasContextButton.Static.Foreground  CloseSystemButtonStyle �� BasedOn  SystemButtonBase J� SnapsToDevicePixels��%  &	        &	 �     &	 �    &	 S    &	 �    &	 �    &	 �    &	     &	     &		 ,    &	
 A    &	 V    &	 k    &	 �    7  &	 �    �� .��U� $  (FrameworkElement.Opacity)�.\�  $ 0��$ 0Q�  $ 0:0:0.3��$ 1Q�   $ EaseOut=�r� $ (FrameworkElement.Margin)�.	�  $ 0��$ 
-30,0,30,0q�  $ 0:0:0.3��$ 0q�	   $ EaseOut=��� .��U� $  (FrameworkElement.Opacity)�.\�  $ 0��$ 1Q�  $ 0:0:0.1��$ 0Q�   $ EaseIn=�r� $ (FrameworkElement.Margin)�.	�  $ 0��$ 0q�  $ 0:0:0.1��$ 30,0,0,0q�	   $ EaseIn=��� .��U� $  (FrameworkElement.Opacity)�.\�  $ 0��$ 1Q�  $ 0:0:0.1��$ 0Q�   $ EaseIn=��� .��U� $  (FrameworkElement.Opacity)�.\�  $ 0��$ 0Q�  $ 0:0:0.1��$ 1Q�   $ EaseIn=��� $
 #252526���� $
 #252526���� $
 White���� $
 	#FF5A708C���� $
 
WhiteSmoke���� $
 	#FF5A708C���� $
 	#FFEAEAEA���� $
 	#FFF55757���� " ��.���  � ��
 ���� ��  � �� Z� ��  � ��$ 0q���  � �� �#252526��  � ��$ 22����  � ��$ 28����  � �� �� " ��.R��� $ border��#��z��._��� #��z��# z  P�  �@J�IsMouseOver . . ���  � ��# �  ��  � ��
 ���� P�  � �� . . ���  � ��# � 	 ��  � ��# � 
 �� " ��8    P�  �@J�IsMouseOver . . ���  � ��
 ���� ��  � ��# �  P�  � �� . . ���  � ��
 ���� ��  � ��# � 
 �� " ��.���  � ��
 ���� ��  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R��� $��Chrome��#��z��#��z��#��z�� . ._��� #��z��#��z��#��z����. # z ���� " �.���  � ��$	 700����  � ��$	 440����  � ��$	 700����  � ��$	 440����  �@�
ResizeMode$ CanResizeWithGrip=���  �@�UseLayoutRounding . ��  �@J�SnapsToDevicePixels . �� &   System.Windows.Media.TextOptions �@ TextFormattingMode$ Display=���  �@�WindowStyle$
 None=���  � ��# �  �� *  $Microsoft.Windows.Shell.WindowChrome �@ WindowChrome      CornerRadius$ 0��   GlassFrameThickness$ 1q�   UseAeroCaptionButtons .  ��  � �� �� " �.R�� #��[  � RowDefinitions �� $��35 ��� $��* ��� $
��Auto � � ColumnDefinitions �� $��* ��� $��* ��� $��* �.+���  ��Name$ systemButtons��|�.  $��3��$��Right=� �� Orientation$ 
Horizontal=�   IsHitTestVisibleInChrome . $��Center=�$��0,0,5,0q�.+��� $��-1��#  System.Windows.SystemCommands   MinimizeWindowCommand#��Z |�.    � ToolTip$  minimize��#��[ ! � FocusVisualStyle! Z� ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� ����    ��[ � z ��[ 	$��8��$��7��$��Center=�$��Center=�$	��0.8������ *
Foreground+" �� RelativeSource" �� # �� Mode$# FindAncestor=�$ �� AncestorType$$ ButtonL��� %   CloseWindowCommand#��Z% |�.  $  Close��#��[ ! Z� ��� $��13��$��12��$z�1,0,0,1,0,1W�.+�F� +����        � z ��j � z     �    ��j 	$��8��$��7��$��Center=�$��Center=�$	��0.8������ *
Foreground+" �� $# FindAncestor=�$$ ButtonL��� $��0��$��2�����#0059b3& �� Padding$
& 5,35q�._��� $��Stretch=�$��Center=�.+��� $��Atlas-Enterprise.jpeg���� $��1��$��1��$��2��$& 24,10,24,10q�._�� .+��� ���� " ��.���  � y�$ 	Collapsed=� �� %   AtlasInstall.Logic.InstallState'   Welcome( �� Value#( Z' ) �� Binding) �� " �� $$ local:MainWindowL�* �� Path$* InstallState�.a���  � y�$ Visible=�+ N� EnterActions+ �� #��[  �� #��[ , N� ExitActions, �� #��[ �� #��[ ._�� .+��� $6��0Welcome to ATLAS ENTERPRISE Installation Wizard.������ " ��# [B��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�	( False.a���  � y�$ Visible=��� $C��=You are about to uninstall ATLAS ENTERPRISE from your system.������ " ��# [B��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�( True.a���  � y�$ Visible=��� ���� " ��.���  � y�$ 	Collapsed=� �� -   	Agreement#( Z- ) �� " �� $$ local:MainWindowL�$* InstallState�.a���  � y�$ Visible=�+ �� #��[  �� #��[ , �� #��[ �� #��[ ._��  �� $
��Auto ��� $��* ��� $
��Auto �.+��� $)��#ATLAS ENTERPRISE License Agreement:��$��2,2,2,8q��� . ��Name$. 
licenseRTb��$��1��
�������|�.  "/ �� VerticalScrollBarVisibility$
/ Auto=�$
��4,10q�]� $��2��$��Right=�$��5,12,5,5q�.+��� $��I Agree��$��14(����#2525260 d� 	IsChecked0 �� " �� $$ local:MainWindowL�$* 	Agreement��� ���� " ��.���  � y�$ 	Collapsed=� �� 1   Miscellaneuos#( Z1 ) �� " �� $$ local:MainWindowL�$* InstallState�.a���  � y�$ Visible=�+ �� #��[  �� #��[ , �� #��[ �� #��[ ._��  �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��Aditional Settings: ��$��2q��� $��1��$
��2,35q����� " ��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�	( False.a���  � y�$ Visible=�.+��� $��Install Atlas for:��$��0,0,0,10q�	� $��Current User��$��0,0,0,6q�0 �� " �� $$ local:MainWindowL�$* CurrentUser�	� $��	All Users��$��0,0,0,6q�0 �� " �� $$ local:MainWindowL�$* AllUsers��� $��2��$
��2,35q����� " ��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�	( False.a���  � y�$ Visible=�.+��� $��Aditional shortcuts:��$��0,0,0,10q��� $��Desktop��$��0,0,0,6q����#2525260 �� " �� $$ local:MainWindowL�$* Desktop��� $��
Start Menu��$��0,0,0,6q����#2525260 �� " �� $$ local:MainWindowL�$* 	StartMenu��� $��2��$
��2,35q����� " ��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�( True.a���  � y�$ Visible=�.+��� $��Remove Data Options:��$��0,0,0,10q��� $"��Remove all application data.��$��0,0,0,6q����#2525260 �� " �� $$ local:MainWindowL�$* Desktop��� $��Keep Activation data.�����#2525260 �� " �� $$ local:MainWindowL�$* 	StartMenu��� ���� " ��.���  � y�$ 	Collapsed=� �� 2   Install#( Z2 ) �� " �� $$ local:MainWindowL�$* InstallState�.a���  � y�$ Visible=�+ �� #��[  �� #��[ , �� #��[ �� #��[ ._��  �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��Installation Progress: ��$��2q����� " ��# [B��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�	( False.a���  � y�$ Visible=��� $��Uninstallation Progress: ������ " ��# [B��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�( True.a���  � y�$ Visible=�� $��1��$
��2,35q� �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��17��3 � Value3 �� " �� $$ local:MainWindowL�$* Progress��� $��Progress Details: ��$��1��$��2,24,2,0q��� 4 ��Name$4 progressDetails��
������ $��2��$��Light"�$��Stretch=�$��0q�$��2,0,2,10q�|�.  $
/ Auto=����� " �� $$ local:MainWindowL�$* Details��� ���� " ��.���  � y�$ 	Collapsed=� �� 5   Finish#( Z5 ) �� " �� $$ local:MainWindowL�$* InstallState�.a���  � y�$ Visible=�+ �� #��[  �� #��[ , �� #��[ �� #��[ ._�� .+��� $G��AATLAS ENTERPRISE has been succcessfully installed on your system.������ " ��# [B��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�	( False.a���  � y�$ Visible=��� $K��EATLAS ENTERPRISE has been succcessfully uninstalled from your system.������ " ��# [B��.���  � y�$ 	Collapsed=� �� ) �� " �� $$ local:MainWindowL�$* IsInstalled�( True.a���  � y�$ Visible=�� $��2��$��3�� �� $��* ��� $��* ��� $��* �.+��� ���#252526�� ���#252526$��1��$��2��._��  �� $��* ��� $��* �.+��� $��0��$��35��$��0,5,5,5q�-	  'System.Windows.Input.NavigationCommands6 	  PreviousPage#��Z6 ���� " ��# [ .���  � �� �#0059b3��  � ��
 �������  � ��
 ����� P�  �@J�IsMouseOver . . ���  � ��$ 2q�P�  � {� .  . ���  � y�$ 	Collapsed=�+ �� #��[ , �� #��[ ���� 
�������$
��Back���� $��1��$��35��$��5q�7 	  NextPage#��Z7 ���� " ��# [ .���  � �� �#0059b3��  � ��
 �������  � ��
 ����� P�  �@J�IsMouseOver . . ���  � ��$ 2q�P�  � {� .  . ���  � ��
 �������  � ��$ 0q����� ���� " �� $$ ButtonL�$* 
Foreground����� " �� $$ local:MainWindowL�$* 
ButtonText�