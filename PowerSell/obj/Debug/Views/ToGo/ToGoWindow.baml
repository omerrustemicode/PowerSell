   M S B A M L   `   `   `  ����D  @PowerSell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:PowerSell.LocalizationPowerSell.Localization  5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   %    PowerSell.Views.ToGo.ToGoWindow   5      .local$clr-namespace:PowerSell.Localization  5   	   Cmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Client Panel��5	   	   $	��800��6   $
��1200��6+    � WindowState$ 	Maximized=�68    � 	Resources 5
      0   *PowerSell.Localization.LocalizationManager   LocalizationManager��%   &	          5   
   6$   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridOrdersNew��5       ! MahApps.Styles.DataGrid.Azure#��[ 6.      
IsReadOnly .    SelectionMode$ Single=�6w   $��	0,0,6,443q�6�   ��6�   �� *YourOrdersCollection+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� *	ServiceId+  5      $ Service Name��6%   	 6;   �� *ServiceName+  5      $ Price��6%   	 64   �� *ServicePrice+  5      $ Quantity��6%   	 67   �� *
Quantity+  5      $ Total��6%   	 64   �� *Total+5        5!   
   -   5        $ dataGridOrders��5!      #��[ 6+    . $ Single=�6t   
�������$��-11,444,10,10q�6�    5"        5#      $ Order ID��6%   	 67   �� *	OrderId+  5$      $ Service Name��6%   	 6;   �� *ServiceName+  5%      $ Quantity��6%   	 67   �� *
Quantity+  5&      $ Service Price��6%   	 6<   �� *ServicePrice+5)      �� 5-   
   
 �� Orientation$
 
Horizontal=�6   ���#3498DB$��93��6D    J� Opacity$	 0.7Q�6P    � Cursor$
 Hand��6^   .+�5.      �� -   5        $
��Edit��5.      $��2q�69   ���#3498DB$	��100��6Y   �� 5/      -   5        $��Delete��5/      $��2q�6H   ���#3498DB$	��100��6h   �� 50      -   5        $��Message��50      $��2q�6C   ���#3498DB$	��100��6d    J� RenderTransformOrigin$ 0.5,0.5&�6p   �� 51      -   5         ��Name$ 	NameLabel��51      $�� ��6'   $y�Hidden=�62   �� 52      -   5        $ 
PhoneLabel��52      $�� ��6(   $y�Hidden=�63   �� 53      -   5        $ 
EmailLabel��53      $�� ��6(   $y�Hidden=�63   �� 54      -	   5        $ MessageLabel��54      $�� ��6*   $y�Visible=�65   �� 55      -
   5        $ TransportLabel��55      $�� ��6,   $y�Hidden=�67   56      �� 59   
   $��1��6   $��5��6'   ���#34495E$��Stretch=�6F   $
��Left=�6b   �� 5<   
   $��1��6   ���#2C3E50$��2��6:   $��Right=�6N   $	��598��6j   .+�5>      � .+�5?      �� -   5        $	��120��5?   d   $��30��6p   $��5q�6|   ���#3498DB
���������6   ��  �� Source# [   �� Path$
 Bill�6�   5@      �� 5C      $
��Auto��6   $��1��6%   
�������$��0,5,0,5q�6C   � 5E      -   5         �Name$ CategoryServiceWrap��5E      .+�5G      �� ��6   �� *!YourServiceCategoriesCollection+��5H      �� 5I      .R�5J      � 5K      ��5M      �� 5N      .R�5P      �� $��80��5Q      $��30��5R      $��5q�5S      ���#3498DB
���������5P   %   �� *CategoryName+��5V      �� $+ %DataContext.YourCommandForButtonClick� �� RelativeSource ��  �� AncestorType" ���5W      �� *
CategoryId+5X      5Z      5[      � 5]      -   5        $ SubcategoryServiceWrap��5]      $��10q�68    � Orientation$ 
Horizontal=�6D   � 5_      -   5        $ ServiceWrap��5_      $��10q�6-   $ 
Horizontal=�69   5a      5b      5c      