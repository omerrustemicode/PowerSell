   M S B A M L   `   `   `  ����  	PowerSell&     PowerSell.Views.Admin.Categories   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   $��0��6:   .+�5      �� -   5         ��Name$ txtCategoryName��5      $��0,0,10,0q�6/   �� 5      -   5        $��Add Category��5      6,   5      �� 5   
   $ 
Horizontal=�6   $��10q�6.   $��1��6:   .+�5      �� -   5        $ txtSubcategoryName��5      $��0,0,10,0q�62   $��Enter subcategory name��6D   �� 5      -   5         ��Name$ cmbParentCategory��5      $	��150��62   �� 5      -   5        $��Add Subcategory��5      6/   5      R� 5   
   -   5         R�Name$ treeCategories��5      $��2��6+   $��10q�68   ��5      �� 5       �� ItemsSource 6+   �� *Subcategories+.R�5      �� ��6    �� *CategoryName+5      5       5!      5"      