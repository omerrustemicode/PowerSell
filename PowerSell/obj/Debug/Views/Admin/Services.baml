   M S B A M L   `   `   `  ����D  @PowerSell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PowerSell.ServicesPowerSell.Services  5      $    PowerSell.Views.Admin.Services   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      2local clr-namespace:PowerSell.Services     5      ���#282C34.��5      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� ���#3A3F4B �� CornerRadius$ 10��6&   $��0,0,0,10q�68    �� Padding$ 10q�6J   ._�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5         ��Name$ txtServiceName��5      $	��150��62   $��30��6>   $��5q�6J   
������� � ToolTip$ Enter Service Name��5   -   �� 5      -   5        $ txtQuantity��5      $	��100��6/   $��30��6;   $��5q�6G   
�������$ Enter Quantity��5   -   �� 5      -   5        $ txtServicePrice��5      $	��120��63   $��30��6?   $��5q�6K   
�������$ Enter Price��5   -   �� 5      -   5         ��Name$ cmbCategory��5      $	��160��60   $��30��6<   $��5q�6H    �� DisplayMemberPath$ CategoryName��5       �� SelectedValuePath$ 
CategoryId��6<   $ Select a Category��5       �� 5"      -   5        $��Add Service��5"      ���#0078D4
�������$��10,0,0,0q�5$      $	��8,5q�6+   5%      5&      �� 5)   
   ���#3A3F4B$ 10��6&   $��0,0,0,10q�68   $ 10q�6J   $��1��6W   ._�5*      �� $ 
Horizontal=�6   $��Center=�62   .+�5+      �� -   5        $��Edit Service��5+      ���#F9A825
�������$��5q�5,   A   $	��8,5q�6L   �� 5-      -   5        $��Delete Service��5-      ���#D32F2F
�������$��5q�5.   A   $	��8,5q�6L   5/      50      �� 53   
   $��2��6   ���#1E1E1E$ 10��63   $ 10q�6E   ._�54      &   System.Windows.Controls.DataGrid  -   5        	  Name$	 dataGridServices��54      
   AutoGenerateColumns
 .  
������ 
�������
�������$��1q�57   *      RowBackground �#333   AlternatingRowBackground �#444   Columns 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Service ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *	ServiceId+6[     5;      $ Service Name��6)   $	 200 6_    6?   �� *ServiceName+6_     5<      $ Quantity��6)   $	 100 6X    6;   �� *
Quantity+6X     5=      $ Price��6)   $	 120 6Y    68   �� *ServicePrice+6Y     5>      $ Category��6)   $	 150 6\    6;   �� *CategoryName+6\   5@      5A      5C      5D      