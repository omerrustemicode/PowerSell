   M S B A M L   `   `   `  ����  	PowerSell%    PowerSell.Views.Reports.Reports   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Reports��5   	   $	��450��6   $	��800��6&   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5
   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5      �� -   5        $��Calculate Sales��5      $	��120��6/   $��30��6;   $��5q�6G   6R   �� 5      -   5        $��
Close Case��5      $	��120��6*   $��30��66   $��5q�6B   6M   �� 5      -   5         ��Name$ TotalSalesLabel��5      $��Bottom=�6-   $
��Left=�6H   $��5q�6c   
���   �$��20(�6�   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ OrdersDataGrid��5      $��1��6+      AutoGenerateColumns . $��10q�6S   5      5      