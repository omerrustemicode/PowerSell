   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls   	PowerSell#   PowerSell.Views.Account.Login   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro       � Title$  Login��$	��450��$	��800�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.��� .+��� $)��#/Views/Logos/PowerSell_BG_Logo.jpeg��$
��Fill=�� -    �Name$ LoadingGrid�� J� IsHitTestVisible .  $y�Visible=�
������ $	��800�� � RowDefinitions �� $��* � � ColumnDefinitions �� $��* �.+��� $��Center=�$��Center=�.+�-   'MahApps.Metro.Controls.MetroProgressBar  -     Name$ ProgressBar��$	��700��$��80��
���  ��	 � IsIndeterminate	 . $��4q�
 � Maximum$	
 100Q� � Minimum$ 0Q� � Value$ 0Q��� -    ��Name$ UtcPlusOneTimeTextBlock��$��Center=�$	��Top=�$��10q�$��50(�$
��Bold"�� -   $ 	LoginGrid��$y�	Collapsed=� �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* � �� $��* ��� $��* �.+��� $��1��$��2��$��Center=�$��Center=�.+�G� -    G�Name$ PasswordBox��*   $MahApps.Metro.Controls.TextBoxHelper   	Watermark$	 PIN��   ClearTextButton . $	��350��$��60��$��0 0 10 0q�$��40(��� -   $��Логирај�� '  #MahApps.Styles.Button.Square.Accent#��[  $	��300��$��50��$��
10 10 10 0q�$��16(�
�������
����  �