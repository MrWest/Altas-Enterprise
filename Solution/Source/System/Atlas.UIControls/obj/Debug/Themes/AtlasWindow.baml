   M S B A M L   `   `   `  ����P  LAtlas.UIControls, Version=0.7.12.24304, Culture=neutral, PublicKeyToken=nullK*clr-namespace:CompanyName.Atlas.UIControlsCompanyName.Atlas.UIControls  5      a5clr-namespace:CompanyName.Atlas.UIControls.Properties'CompanyName.Atlas.UIControls.Properties  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _Infralution.Localization.Wpf, Version=3.3.0.0, Culture=neutral, PublicKeyToken=547ccae517a004b5R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089b ^Microsoft.Windows.Shell, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35  �� MergedDictionaries �� Source.    (CompanyName.Atlas.UIControls.AtlasWindow �� 
TargetType �� Property   Default.FontFamily �� Value  Default.FontSize  Default.Foreground&   System.Windows.Media.TextOptions  Window.Default.BorderBrush*  $Microsoft.Windows.Shell.WindowChrome   CornerRadius   GlassFrameThickness   UseAeroCaptionButtons �� 
TargetType	 � 	Resources  Key
 �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value   EasingFunction.  (System.Windows.Media.Animation.CubicEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode ��Name � UseLayoutRounding J� SnapsToDevicePixels �Name4   .CompanyName.Atlas.UIControls.AtlasMediaElement  Name r� Stretch r� Source �� RelativeSource �� AncestorType �� Path   	ViewState   IsSomeModuleLoaded �� Triggers :� RoutedEvent 
 Appear1   +CompanyName.Atlas.UIControls.AtlasFrontPage    LoginCommand   LoginCommand0   *CompanyName.Atlas.UIControls.AtlasMainPage   	StatusBar     AtlasModuleView! �� Orientation" ��Name#   IsHitTestVisibleInChrome$    MinimizeButtonToolTip% � ToolTip  SystemButtonStyle,	  &Microsoft.Windows.Shell.SystemCommands& 	  MinimizeWindowCommand' �� Source( � FocusVisualStyle) �� Mode* ��Name+    RestoreButtonToolTip, 	  RestoreWindowCommand-    MaximizeButtonToolTip. 	  MaximizeWindowCommand/    CloseButtonToolTip  CloseSystemButtonStyle0 	  CloseWindowCommand2
   ,CompanyName.Atlas.UIControls.AtlasBackButton  AtlasBackButtonStyle1    BackButtonCommand2 �� Triggers3 P� Property4 P� Value5 �� 
TargetName�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
UIControls*clr-namespace:CompanyName.Atlas.UIControls  5      KShell?http://schemas.microsoft.com/winfx/2006/xaml/presentation/shell  5      D
properties5clr-namespace:CompanyName.Atlas.UIControls.Properties  5        5      �� 5   
   $ ../Assets/Button.xaml?�6   �� 5	   
   $ ../Assets/Converters.xaml?�6   �� 5
   
   $ ../Assets/Menu.xaml?�6   �� 5   
   $# ../Themes/AtlasFrontPage.xaml?�6   �� 5   
   $ FirstLevelNavItem.xaml?�6   �� 5   
   $ SecondLevelNavItem.xaml?�6   %�  '         7  7  7  7  7  �� 5      "   6   .�5   
   ��  � ��# �   6'   �� 5   
    � ��# �  6%   �� 5   
    � ��# �  6'   �� 5   
    � ��$	 800��6"   �� 5   
    � ��$	 640��6#   �� 5   
    � ��$	 800��6%   �� 5   
    � ��$	 640��6&   �� 5   
    �@�
ResizeMode$ CanResizeWithGrip=�6'   �� 5   
    �@�UseLayoutRounding . 6.   �� 5   
    �@J�SnapsToDevicePixels . 60   �� 5   
    �@ TextFormattingMode$ Display=�6;   �� 5   
    �@�WindowStyle$
 None=�6(   �� 5    
    � ��# �  6(   �� 5"   
    �@ WindowChrome 5#        5$      $ 0��6%   $ 1q�66    .  6N   5&      �� 5(   
    � �� 5)      �� 5*      "   6"   	 5+      ���� 5,      
Appear .��5-      U� $ 
 (FrameworkElement.Opacity)�6<   .\�5.   "     $ 0��67   $ 0Q�6C     50   "   $ 0:0:4.25��67   $ 1Q�6J    51   &     52   *   $ EaseOut=�64   54   #   55      57      58      .R�5:      �� $ border��6   #��z��6-   |�.  #��z��5;      $��1q�6I    .  . ._�5<      �� |�.  .u�5>      � $ 
LayoutRoot��6#   |�.  .+�5?   "   ]� |�.  .+�5B   &   � |�.  .+�5E   *     $ AtlasMediaElement��6Z   |�.  $ UniformToFill=�6�    6v   ��  �� $ UIControls:AtlasWindowL�$ LoginMediaPath� 5F   G   ��  �� $ UIControls:AtlasWindowL�$ 	ViewState� 6�   ��  �� $ UIControls:AtlasWindowL�$ IsSomeModuleLoaded���5G   .   �� 5H   2   "  68    5I   6   :� 5J   :   $ Loaded��6G   [�5K   >   �� 5L   B   8��  6y   5N   ;   5P   3   5R   +     5T   *   |�.  # z 6W   5Y   +     5[   *   |�.   6V   ��  �� $ UIControls:AtlasWindowL�$ AtlasModuleView.StatusBar�5`   +   �� 5b   *   #��z  69   |�.  6{   5f   '   5i   '   �� 5m   "   $! 
Horizontal=�6-   |�.  $	��Top=�6Y   $��Right=�6q   $��35��6�   $	��Top=�6�   .+�5z   &   �� $" systemButtons��61   |�.  $��Right=�6[   $! 
Horizontal=�6r   # . $��Center=�6�   $��0,0,5,0q�6�   .+�5}   *   �� $��-1��61   |�.  #% z$ 5~   1   8�� ��5}   ?   �� #' Z& ( 5~   �   Z� ��5   .   � 5�   2   $��13��67   $��12��6B   $z�1,0,0,1,0,1W�6N   .+�5�   6   F� ����    ��[ � z ��[ 	$��8��6N   $��7��6X   $��Center=�6c   $��Center=�6~   $��2��5�   �   ��6:   �� *
Foreground+ �� $) FindAncestor=�$ ButtonL�6�   5�   3   5�   +   � 5�   *   $	��1,0q�6/   .+�5�   .   �� $* Restore��6C   $��-1��65   #% z+ 5�   5   8�� $y�	Collapsed=�5�   5   |�.  ��5�   T   �� #' Z, ( 5�   L   Z� ��5�   2   � 5�   6   $��8��6;   $��12��6E    . .+�5�   :   F� X���Ѐ�     � z     � z ��[ �    ��- р�[ ��- �    �� р�[ �� р�[  z �     z 	$��8��6u   $��8��6   $��Center=�6�   $��Center=�6�   $��1��5�   �   ��6?   �� *
Foreground+ �� $) FindAncestor=�$ ButtonL�6�   5�   7   5�   /   �� 5�   .   $* Maximize��65   $��-1��6G   |�.  #% z- 5�   5   8�� ��5�   5   �� #' Z. ( 5�   �   Z� ��5�   2   � 5�   6   $��13��6;   $��12��6F   .+�5�   :   F� +����    @B �@T� @B �@T�  z �     z 	$��9��6\   $��8��6f   $��Center=�6q   $��Center=�6�   $��2��5�   �   ��6?   �� *
Foreground+ �� $) FindAncestor=�$ ButtonL�6�   5�   7   5�   /   5�   +   �� 5�   *   |�.  #% z/ 5�   1   8�� ��5�   1   �� #' Z0 ( 5�   a   Z� ��5�   .   � 5�   2   $��13��67   $��12��6B   $z�1,0,0,1,0,1W�6N   .+�5�   6   F� +����        � z ��j � z     �    ��j 	$��8��6X   $��7��6b   $��Center=�6m   $��Center=�6�   $	��1.5��5�   �   ��6;   �� *
Foreground+ �� $) FindAncestor=�$ ButtonL�6�   5�   3   5�   +   5�   '   5�   #   �� 5�   "   |�.  $! 
Horizontal=�6A   $	��Top=�6Z   $
��Left=�6r   # . $	��Top=�6�   .+�5�   &   
  #���  6A   $
��Left=�6p   $	��Top=�6�   #��z1 5�   A   6q   5�   #   5�      5�      5�      2 5�      P� 5�      3 �@�WindowState$4 	Maximized=�69   . �5�      �� $5 Maximize��6%    � y�$ 	Collapsed=�6Q   �� 5�      $5 Restore��6%    � y�$ Visible=�6P   �� 5�      $5 
LayoutRoot��6%    � ��$ 7q�6O   5�      P� 5�      3 �@�WindowState$4 Normal=�69   . �5�      �� $5 Maximize��6%    � y�$ Visible=�6Q   �� 5�      $5 Restore��6%    � y�$ 	Collapsed=�6P   5�      5�      5�      5�      5�      