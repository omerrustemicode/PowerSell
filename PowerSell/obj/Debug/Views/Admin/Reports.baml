   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      D @PowerSell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PowerSell.Views.AdminPowerSell.Views.Admin 5      #   PowerSell.Views.Admin.Reports   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      5local#clr-namespace:PowerSell.Views.Admin     5      .��5
      �� $
��Auto=�6   $��Visible=�66   .��5   
   � ���	#FFF0F0F0  � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   $��20q�6?   .+�5      �� $��Select Date Range:��6   $��Center=�65   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpStartDate��5      
 �� Now   SelectedDate# Z 62   $
��10,0q�6]   �� 5      $��to��6   $��Center=�6%   $
��10,0q�6@     5      -   5        $ 	dpEndDate��5      # Z 60   $��0,0,10,0q�6[   �� 5      -   5        $��Generate Report��5      $
��10,0q�6S   5      �� 5      -   5         ��Name$ lvOrders��5      $��1��6)   $��20q�66   
�������
�������$��1q�6o    �� View 5      �� 5      .P�5       �� $��Order ID��6)    �� DisplayMemberBinding 6;   �� *
OrdersId+�� 5!      $��Service Name��6)    6?   �� *ServiceName+�� 5"      $��Quantity��6)    6;   �� *
Quantity+�� 5#      $��Service Price��6)    6@   �� *ServicePrice+5%      5'      �� 5*      $��2��6   $ 
Horizontal=�6&   $��20q�6?   
�������.+�5+      �� $��Best-Selling Product:��6   $��Center=�68   �� 5,      -   5         ��Name$ txtBestSellingProduct��5,      $
��10,0q�6;   5-      �� 50      $��3��6   $ 
Horizontal=�6&   $��20q�6?   
�������.+�51      �� $��Total:��6   $��Center=�6)   �� 52      -   5        $ txtTotal��52      $
��10,0q�6.   53      54      55      56      