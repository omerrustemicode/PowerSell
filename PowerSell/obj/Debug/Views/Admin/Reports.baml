   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       	PowerSell#   PowerSell.Views.Admin.Reports   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      .��5	      �� $
��Auto=�6   $��Visible=�66   .��5
   
   � ���#282C34$��20q�6$     � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   ���#3A3F4B �� CornerRadius$ 8��67    �� Padding$ 10q�6H   $��0,0,0,10q�6U   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Select Date Range:��6   
�������$��Center=�6L   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpStartDate��5   !   
 �� Now   SelectedDate# Z 66   $
��10,0q�6a   �� 5      $��to��6   
�������$��Center=�6<   $
��10,0q�6W     5      -   5        $ 	dpEndDate��5   !   # Z 64   $��0,0,10,0q�6_   �� 5      $��	Category:��6   
�������$��Center=�6C   $
��10,0q�6^   �� 5      -   5         ��Name$ 
cbCategory��5      $	��150��63   $
��10,0q�6?   �� 5      $��Product:��6   
�������$��Center=�6B   $
��10,0q�6]   �� 5      -   5        $ 	cbProduct��5      $	��150��62   $
��10,0q�6>   �� 5      -   5        $��Generate Report��5      ���#0078D4
�������$	��6,3q�6   5       5!      �� 5$      -   5         ��Name$ lvOrders��5$      $��1��6)   ���#1E1E1E
�������$��1q�6^   	 �� View	 5%      �� 5&      .P�5'      �� $��Order ID��6)   
 �� Width$	
 100��6e    �� DisplayMemberBinding 6;   �� *
OrdersId+6e   �� 5(      $��Service Name��6)   $	
 200��6l    6?   �� *ServiceName+6l   �� 5)      $��Quantity��6)   $	
 100��6e    6;   �� *
Quantity+6e   �� 5*      $��Service Price��6)   $	
 120��6n    6@   �� *ServicePrice+6n   5+      5-      �� 50      $��2��6   ���#3A3F4B$ 8��67   $ 10q�6H   $��10q�6U   ._�51      �� $ 
Horizontal=�6   .+�52      �� $��Best-Selling Product:��6   
�������$��Center=�6O   �� 53      -   5         ��Name$ txtBestSellingProduct��53       
�������$
��Bold"�6R   $
��10,0q�6d   �� 55      $��Total:��6   
�������$��Center=�6@   $��20,0,0,0q�6[   �� 56      -   5        $ txtTotal��56       
�������$
��Bold"�6E   $
��10,0q�6W   �� 58      -	   5        $��Export to PDF��58      ���#4CAF50
�������$	��6,3q�6x   $
��20,0q�6�   �� 59      -
   5        $��Print Report��59      ���#D32F2F
�������$	��6,3q�6s   $
��10,0q�6�   5:      5;      5<      5=      5>      