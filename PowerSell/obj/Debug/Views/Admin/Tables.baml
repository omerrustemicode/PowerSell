   M S B A M L   `   `   `  ����D  @PowerSell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PowerSell.Views.AdminPowerSell.Views.Admin  5      "    PowerSell.Views.Admin.Tables   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      5local#clr-namespace:PowerSell.Views.Admin     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5      �� -   5         ��Name$ txtTableName��5      $	��200��6,   $��0,0,10,0q�68   �� 5      -   5        $��	Add Table��5      6)   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridTables��5      $��1��6+   $��10q�68      AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�6r      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Table ID��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 6S   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 67   �� *	TableId+6S     5      $ 
Table Name��6%   $		 200 6W   
 69   �� *	TableName+6W   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Actions��6)   $		 120 6:      CellTemplate 5       �� 5!      .R�5#      �� -   5        $
��🗑��5#   %   
������ 
���  ��$��0q�6z   $��5,0,0,0q�5$       � Cursor$
 Hand��6   $��14(�6"   62   5%      5'      5)      5*      5+      