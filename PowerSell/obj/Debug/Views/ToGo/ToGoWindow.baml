   M S B A M L   `   `   `  ����D  @PowerSell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:PowerSell.LocalizationPowerSell.Localization  5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   %    PowerSell.Views.ToGo.ToGoWindow   5      .local$clr-namespace:PowerSell.Localization  5   	   Cmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Client Panel��5	   	   $	��800��6   $
��1200��6+    � WindowState$ 	Maximized=�68    � 	Resources 5
      0   *PowerSell.Localization.LocalizationManager   LocalizationManager��%   &	          5   
   6$   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridOrdersNew��5       ! MahApps.Styles.DataGrid.Azure#��[ 6.      
IsReadOnly .    SelectionMode$ Single=�6w   $��0,0,6,0q�6�   ��6�   �� *YourOrdersCollection+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� *	ServiceId+  5      $ Service Name��6%   	 6;   �� *ServiceName+  5      $ Price��6%   	 64   �� *ServicePrice+  5      $ Quantity��6%   	 67   �� *
Quantity+  5      $ Total��6%   	 64   �� *Total+5      �� 5!   
   -   5        
 ��Name$
 TotalToOrder��5!      $�� ��6&   
��� � �$
��auto��6D   $��30��6Q   $��20(�6]   $��Normal$�6k   �� 5#   
    �� Orientation$ 
Horizontal=�6   ���#3498DB$��93��6D    J� Opacity$	 0.7Q�6P    � Cursor$
 Hand��6^   $��2��6l   $��0,410,4,281q�6�   .+�5%      �� -   5        $
��Edit��5%      $��2q�69   ���#3498DB$	��100��6Y   �� 5&      -   5        $��Delete��5&      $��2q�6H   ���#3498DB$	��100��6h   �� 5'      -   5        $��Message��5'      $��2q�6C   ���#3498DB$	��100��6d    J� RenderTransformOrigin$ 0.5,0.5&�6p   �� 5(      -   5        $
 	NameLabel��5(      $�� ��6'   $y�Hidden=�62   �� 5)      -   5        $
 
PhoneLabel��5)      $�� ��6(   $y�Hidden=�63   �� 5*      -   5        $
 
EmailLabel��5*      $�� ��6(   $y�Hidden=�63   �� 5+      -	   5        $
 MessageLabel��5+      $�� ��6*   $y�Visible=�65   �� 5,      -
   5        $
 TransportLabel��5,      $�� ��6,   $y�Hidden=�67   5-      �� 50   
   $��1��6   $��5��6'   ���#34495E$��Stretch=�6F   $
��Left=�6b   �� 53   
   $��1��6   ���#2C3E50$��2��6:   $��Right=�6N   $	��598��6j   .+�55      � .+�56      �� -   5        $	��120��56   d   $��30��6p   $��5q�6|   ���#3498DB
���������6   ��  �� Source# [   �� Path$
 Bill�6�   57      �� 5:      $
��Auto��6   $��1��6%   
�������$��0,5,0,5q�6C   � 5<      -   5         �Name$ CategoryServiceWrap��5<      .+�5>      �� ��6   �� *!YourServiceCategoriesCollection+��5?      �� 5@      .R�5A      � 5B      ��5D      �� 5E      .R�5G      �� $��80��5H      $��30��5I      $��5q�5J      ���#3498DB
���������5G   %   �� *CategoryName+��5M      �� $+ %DataContext.YourCommandForButtonClick� �� RelativeSource ��  �� AncestorType" ���5N      �� *
CategoryId+5O      5Q      5R      � 5T      -   5        $ SubcategoryServiceWrap��5T      $��10q�68    � Orientation$ 
Horizontal=�6D   � 5V      -   5        $ ServiceWrap��5V      $��10q�6-   $ 
Horizontal=�69   5X      5Y      5Z      