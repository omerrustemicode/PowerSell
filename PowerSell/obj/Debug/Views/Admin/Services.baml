   M S B A M L   `   `   `  ����D  @PowerSell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PowerSell.ServicesPowerSell.Services  5      $    PowerSell.Views.Admin.Services   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      2local clr-namespace:PowerSell.Services     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   $��0��6:   .+�5      �� -   5         ��Name$ txtServiceName��5      $��0,0,10,0q�6.   &    PowerSell.Services.TextBoxHelper   Placeholder$ Enter Service Name��6A   �� 5      -   5        $ txtQuantity��5      $��0,0,10,0q�6+   $ Enter Quantity��6=   �� 5      -   5        $ txtServicePrice��5      $��0,0,10,0q�6/   $ Enter Price��6A   �� 5      -   5         ��Name$ cmbCategory��5      $	��150��6,    �� DisplayMemberPath$ CategoryName��68    �� SelectedValuePath$ 
CategoryId��6Y   �� 5      -   5        $��Add Service��5      6+   5      �� 5   
   $ 
Horizontal=�6   $��10q�6.   $��1��6:   .+�5      �� -   5        $��Edit Service��5      6,   �� 5      -   5        $��Delete Service��5      $��10,0,0,0q�6J   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridServices��5      $��2��6-      AutoGenerateColumns .  	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn
   Header$
 
Service ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	ServiceId+  5!      $
 Service Name��6%    6;   �� *ServiceName+  5"      $
 Quantity��6%    67   �� *
Quantity+  5#      $
 Price��6%    64   �� *ServicePrice+  5$      $
 Category��6%    67   �� *CategoryName+5'      5)      5*      