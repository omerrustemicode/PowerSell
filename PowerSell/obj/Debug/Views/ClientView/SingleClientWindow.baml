   M S B A M L   `   `   `  ����D  @PowerSell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:PowerSell.LocalizationPowerSell.Localization  5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   3    -PowerSell.Views.ClientView.SingleClientWindow   5      .local$clr-namespace:PowerSell.Localization  5   	   Cmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Client Panel��5	   	   $	��800��6   $
��1200��6+    � WindowState$ 	Maximized=�68    � 	Resources 5
      0   *PowerSell.Localization.LocalizationManager   LocalizationManager��%   &	          5   
   6$   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $��7* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6%   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridOrdersNew��5       ! MahApps.Styles.DataGrid.Azure#��[ 62      
IsReadOnly .    SelectionMode$ Single=�6{   ��6�   �� *YourOrdersCollection+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6)   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 65   �� *	ServiceId+  5!      $	 Service Name��6)   
 6?   �� *ServiceName+  5"      $	 Price��6)   
 68   �� *ServicePrice+  5#      $	 Quantity��6)   
 6;   �� *
Quantity+  5$      $	 Total��6)   
 68   �� *Total+5&      �� 5'      -   5         ��Name$ TotalToOrder��5'      $�� ��6*   
��� � �$
��auto��6H   $��20(�6U   $��Normal$�6c   �� 5)       �� Orientation$ 
Horizontal=�6   ���#3498DB$��93��6H    J� Opacity$	 0.7Q�6T    � Cursor$
 Hand��6b   .+�5*      �� -   5        $��2q�5*   �   ���#3498DB$	��100��6�   ��6   ��  �� Source# [   �� Path$ DeleteButton�6�   �� 5+      -   5         ��Name$ 	AddButton��5+      $��2q�6�   ���#3498DB$	��100��6�   ��6,   �� # [  $ 	AddButton�6�   �� 5,      -   5        $��2q�5,   �   ���#3498DB$	��100��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   ��6   �� # [  $ Message�6�   �� 5/      -   5        $ ClientIdLabel��5/      $�� ��6/   $y�Hidden=�6:   �� 50      -   5        $ 	NameLabel��50      $�� ��6+   $y�Hidden=�66   �� 51      -   5        $ 
PhoneLabel��51      $�� ��6,   $y�Hidden=�67   �� 52      -	   5        $ 
EmailLabel��52      $�� ��6,   $y�Hidden=�67   �� 53      -
   5        $ MessageLabel��53      $�� ��6.   $y�Hidden=�69   �� 54      -   5        $ TransportLabel��54      $�� ��60   $y�Hidden=�6;   55        58      -   5        $ dataGridOrders��58      #��[ 6/    . $ Single=�6x   
�������$��
0,10,10,10q�6�    59        5:      $	 Order ID��6)   
 6;   �� *	OrderId+  5;      $	 Service Name��6)   
 6?   �� *ServiceName+  5<      $	 Quantity��6)   
 6;   �� *
Quantity+  5=      $	 Price��6)   
 68   �� *ServicePrice+5@      �� 5A      -   5        $ TotalOrders��5A      $�� ��6)   
��� � �$
��auto��6G   $��20(�6T   $��Normal$�6b   5B      �� 5E   
   $��0��6   $��3��6$   $��0��65   $��5��6E   ���#34495E$��Stretch=�6d   $��Right=�6�   �� 5H   
   $��1��6   ���#2C3E50$��3��6:   $��Stretch=�6K   .+�5J      � .+�5K      �� -   5        $��80��5K   i   $��5q�6u   ���#3498DB
���������6   �� # [  $ 	Transport�6�   �� 5L      -   5        $��80��5L   d   $��5q�6p   ���#3498DB
���������6   �� # [  $
 Bill�6�   �� 5M      -   5        $��80��5M   d   $��5q�6p   ���#3498DB
���������6   �� # [  $
 Paid�6�   �� 5N      -   5        $��80��5N   e   $��5q�6q   ���#3498DB
���������6   �� # [  $ Ready�6�   �� 5O      -   5        $��80��5O   l   ���#3498DB
�������$��58��6�   ��6   �� # [  $ PrintService�6�   �� 5P      -   5        $ PrinterSettingsButton��5P      $��Printer Settings��68   $	��120��6S   $��30��6_   $��Right=�5Q      $	��Top=�6-   $��	0,10,10,0q�6E   5R      5S      �� 5V      $
��Auto��6   $��1��6%   
�������$��0,5,0,5q�6C   � 5X      -   5         �Name$ CategoryServiceWrap��5X      .+�5Z      �� ��6   �� *!YourServiceCategoriesCollection+��5[      �� 5\      .R�5]      � 5^      ��5`      �� 5a      .R�5c      �� $��80��5d      $��30��5e      $��5q�5f      ���#3498DB
���������5c   %   �� *CategoryName+��5i      �� $+ %DataContext.YourCommandForButtonClick� �� RelativeSource ��  �� AncestorType" ���5j      �� *
CategoryId+5k      5m      5n      � 5p      -   5        $ SubcategoryServiceWrap��5p      $��10q�68    � Orientation$ 
Horizontal=�6D   � 5r      -   5        $ ServiceWrap��5r      $��10q�6-   $ 
Horizontal=�69   5t      5u      5v      