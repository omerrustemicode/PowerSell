   M S B A M L   `   `   `  ����D  @PowerSell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PowerSell.Views.ClientViewPowerSell.Views.ClientView  5   	   .    (PowerSell.Views.ClientView.ReturnProduct   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:PowerSell.Views.ClientView     5   	     � Title$  ReturnProduct��5   	   $	��450��6   $	��800��6,   .��5	      � .+�5
   
   �� -   5        $��txtOrderListId��5
      $	��200��6(   $��25��64   $��	10,10,0,0q�6@   $	��Top=�6S   $
��Left=�6k   �� 5   
   -   5        $��btnSearchOrder��5      $��Search Order��6'   $	��150��6>   $��30��6J   $��
220,10,0,0q�6V   $	��Top=�6j   $
��Left=�6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGridProducts��5         AutoGenerateColumns .  $	��300��6G   $	��780��6T   $��
10,50,10,0q�6`      
IsReadOnly .    SelectionMode$ Single=�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Service ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	ServiceId+6W     5      $ Service Name��6%   $ * 6[    6;   �� *ServiceName+6[     5      $ Client Name��6%   $ * 6Y    6:   �� *
ClientName+6Y     5      $ Service Price��6%   $ * 6]    6<   �� *ServicePrice+6]     5      $ Quantity��6%   $ * 6T    67   �� *
Quantity+6T   5      �� 5   
   -   5        $��txtQuantityToReturn��5      $	��200��6-   $��25��69   $��
10,370,0,0q�6E   $	��Top=�6Y   $
��Left=�6q   �� 5   
   -   5        $��btnReturnProduct��5      $��Return Product��6)   $	��150��6B   $��30��6N   $��220,370,0,0q�6Z   $	��Top=�6o   $
��Left=�6�   6�   5      5      