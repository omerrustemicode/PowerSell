   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    	PowerSell   PowerSell.Views.Dashboard   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	     � Title$  	Dashboard��5   	    � WindowState$ 	Maximized=�5   	    � Icon$% /Views/logos/PowerSell_Logo.png��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ 	CanResize=�5	   	    � WindowStyle$ SingleBorderWindow=�5
   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   $	��200��6,   .+�5   
   �� $)��#/Views/Logos/PowerSell_BG_Logo.jpeg��6   $
��Fill=�6=   $��3��6L   �� 5   
   -   5         ��Name$ tablesListBox��5      $��0��5   
   ���#2C3E50$��10q�5   
   $
��Auto=�5   
   $
��Auto=�5   
   ��5   
   �� *Tables+��5      �� 5      .R�5      ��  K� IsItemsHost . 6   5      ��5       �� 5!      .R�5"      �� -   5        $	��100��5"      $��80��6,   $��5q�5#      ���#3498DB
�������
������ $��0q�5(      ��5)      �� 5*      	 �� TextAlignment$	 Center=�6)   .+�5+   "   �� $��16(�6G   $
��Bold"�6U   ��6,   �� *	TableName+6U   �� 5,   "   ��6/   �� *	OrderList+��5-   &   �� 5.   *   .R�5/   .   �� .+�50   2   �� $��14(�6X   ��6<   �� *
ClientName+6X   �� 51   2   
 ��Name$
 TotalValueTxtb��6<   $��12(�6�   ��6T   �� *Total+ �� StringFormat$ Total: {0:C}��6�   52   /   53   +   55   #   56       J� Effect 58      3  -System.Windows.Media.Effects.DropShadowEffect  59         
BlurRadius$ 0Q�6/      ShadowDepth$ 0Q�6>      Color$ #3498DB��6N   5;      5<      ��5>      �� 5?       �� 
TargetType" ��6   .�5@      ��  �� Property �@��Left �� Value 64   �� *	XPosition+�� 5A       �@��Top 63   �� *	YPosition+-   5        5E      5G      �� 5I   
   $��1��6   $��5��5J      ���#34495E$��Stretch=�5L      $��Center=�5M      �� 5O   
   $��2��6   ���#2C3E50$	��200��5Q      $��10q�5R      $��Stretch=�5S      .+�5T      �� -   5         ��Name$ searchTextBox��5T      $	��150��5U      $��30��5V      $��5q�5W      ���#3498DB
�������*   $MahApps.Metro.Controls.TextBoxHelper   	Watermark$  Baraj preku broj na tabeli��5Z         ClearTextButton . 5\      �� 5]      -   5        $��To Go��5]      $	��150��5^      $��50��5_      $��5q�5`      ���#3498DB
�������5c      �� 5d      -   5        $��Reports��5d      $	��150��5e      $��50��5f      $��5q�5g      ���#3498DB
�������5j      �� 5k      -   5        $��Return Product��5k      $	��150��5l      $��50��5m      $��5q�5n      ���#3498DB
�������5q      �� 5r      -   5         ��Name$ LockUnlockButton��5r      $��Lock Tables��5s      $	��150��5t      $��50��5u      $��5q�5v      ���#3498DB
�������5y      �� 5z      -	   5        $��Save Table Position��5z      $	��150��5{      $��50��5|      $��5q�5}      ���#3498DB
�������5�      �� 5�      -
   5        $��Refresh��5�      $	��150��5�      $��50��5�      $��5q�5�      ���#3498DB
�������5�      �� 5�      -   5        $��Logout��5�      $	��150��5�      $��50��5�      $��5q�5�      ���#3498DB
�������5�      5�      5�      5�      