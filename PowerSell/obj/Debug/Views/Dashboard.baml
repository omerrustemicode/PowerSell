   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5       	PowerSell   PowerSell.Views.Dashboard   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5        � Title$  	Dashboard��5   	   $	��450��6   $	��800��6(    � WindowState$ 	Maximized=�64    � Icon$ /Views/logos/tufo.png��6L    � WindowStartupLocation$ CenterScreen=�6i    � 
ResizeMode$ NoResize=�6�    � WindowStyle$
 None=�6�   .��5      �  � ColumnDefinitions 5   
   �� 5	      $
��159* �6   �� 5
      �� 5      $	��40* �6   .+�5   
   �� $)��#/Views/Logos/PowerSell_BG_Logo.jpeg��6   $
��Fill=�6=   �� 5   
   -   5         ��Name$ tablesListBox��5      $��0��6H   ���#2C3E50$��
10,10,3,10q�6m   $y�Visible=�6�   ��6)   �� *Tables+��5      �� 5      .R�5      A�  A� Rows$ 15��6"   	 A� Columns$	 8��6,   5      ��5      �� 5      .R�5      �� -   5        $	��180��5      $	��110��6)   $��16(�66   $��0q�6D   ���#3498DB
�������
������ $��0q�5   '   ��5      �� 5      
 �� TextAlignment$
 Center=�6)   .+�5   "   �� ��6,   �� *	TableName+�� 5   "   ��6/   �� *	OrderList+��5   &   �� 5    *   .R�5!   .   �� .+�5"   2   �� ��6<   �� *
ClientName+�� 5%   2    ��Name$ TotalValueTxtb��6<   ��6T   �� *Total+5'   /   5(   +   5*   #   5+       J� Effect 5-      3  -System.Windows.Media.Effects.DropShadowEffect  5.         
BlurRadius$ 0Q�6/      ShadowDepth$ 0Q�6>      Color$ #3498DB��6N   50      51      ��55      �� 56       �� 
TargetType" ��6"   .R�57      �� $��Disabled=�6#   $
��Auto=�6L   .��58      �� 59      5:      5<      �� 5>   
   $��2��6   ���#2C3E50.+�5@      �� -   5         ��Name$ searchTextBox��5@      $	��180��6-   $��30��5A      $��5q�6"   ���#3498DB
�������*   $MahApps.Metro.Controls.TextBoxHelper   	Watermark$  Baraj preku broj na tabeli��5C         ClearTextButton . 5D      �� 5E      -   5        $��To Go��5E      $	��180��6%   $��70��61   $��5q�6=   ���#3498DB
�������6p   �� 5F      -   5        $��Reports��5F      $	��180��6'   $��70��63   $��5q�6?   ���#3498DB
�������6r   �� 5G      -   5        $��Return Product��5G      $	��180��6.   $��70��6:   ���#3498DB
�������6n   �� 5H      -   5        $��Logout��5H      $	��180��6&   $��70��62   $��5q�6>   ���#3498DB
�������6q   5I      5K      5L      