   M S B A M L   `   `   `  ����  	PowerSell0    *PowerSell.Views.ClientView.PrinterSettings   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      $	��400��5      $	��500��6      WindowBackgroundBrush#���   5        � 	Resources  5        CustomBackgroundBrush �� 
StartPoint �� EndPoint � Color � Offset��%�   &	       �� 5	   
   $	 0,0&�6<   $	 1,1&�6M   .��5
      � $ 	#FF6B7280��6   $	 0.0Q�6-   � 5      $ 	#FF1E3A8A��6   $	 1.0Q�6-   5      5      .��5      � $��20q�6   .+�5   
   �� #��[ 6    �� CornerRadius$ 10��6E    J� Opacity$	 0.9Q�6W    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6#   	   ShadowDepth$	 5Q�63   
   Color$

 Gray��6C   5      � 5   
   $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��20 �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    �� 5!      $��20 �6    �� 5#      $
��Auto �6    .+�5'      �� $��0��6   $��Printer Settings��6%   $��24(�5(      $
��Bold"�6&   
�������$��Center=�5)   +   $��0,0,0,10q�5*      �� 5-      $��1��6   
�������$ 5��65    �� Padding$ 10q�6F   $
 0.95Q�6S   ._�5.      �� .+�5/      �� $��Print Order Printer:��6   $��16(�50      ���	#FF1E3A8A6*   �� 51      -   5         ��Name$ PrintOrderComboBox��51      $	��300��6;   $��0,5,0,0q�52        MetroComboBox#���  53      54      55      56      �� 59      $��2��6   $��1��6%   ���#E5E7EB60   �� 5<      $��3��6   
�������$ 5��65   $ 10q�6F   $
 0.95Q�6S   $��0,20,0,0q�6b   ._�5=      �� .+�5>      �� $��Print Fiscal Printer:��6   $��16(�5?      ���	#FF1E3A8A6*   �� 5@      -   5        $ PrintFiscComboBox��5@      $	��300��6:   $��0,5,0,0q�5A      #���  5B      5C      5D      5E      �� 5H      $��4��6   $��1��6%   ���#E5E7EB60   �� 5K      -   5         ��Name$ 
SaveButton��5K   "   $��6��6   $��Save Settings��66   $	��150��5L      $��40��6!   $��Right=�5M        AccentSquareButtonStyle#���  5O       5P        5Q      $ 5Q�6'   $	 2Q�66   $
 	#FF1E3A8A��6F   5S      5T      5U      5V      