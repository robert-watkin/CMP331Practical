   M S B A M L   `   `   `  ����J  FCMP331Practical, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CMP331Practical.ViewsCMP331Practical.Views  5   	   &     CMP331Practical.Views.NewInvoice   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   X TXceed.Wpf.Toolkit, Version=4.1.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CMP331Practical.Views     5   	     � Title$  
NewInvoice��5	   	    � 
ResizeMode$ NoResize=�6   $	��320��62   $	��550��6?   .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��0.4* �6   �� 5      $
��0.3* �6   �� 5      $
��0.5* �6   �� 5      $
��0.3* �6   �� 5      $
��0.4* �6    � RowDefinitions 5   
   �� 5      $
��0.6* �6   �� 5      $
��0.4* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      .+�5   
   �� $��New Invoice��6   $��Center=�6&   $	��Top=�6C   $��1��6[   $��3��6k   $��26(�6   $��Sergoe UI Semibold)�6�   $��0,15,0,0q�6�   �� 5   
   $��	Property:��6   $��Right=�6$   $��Center=�6@   $��
-50,0,10,0q�6[   $��1��6o   $��2��6   �� 5   
   -   5        $��cmbProperty��5      $
��Left=�6&   $��Center=�6A   $��2��6\   $��2��6l   $��2��6y   $	��200��6�   $��10,0,0,0q�6�   �� 5"   
   $��	Due Date:��6   $��Right=�6$   $��Center=�6@   $��
-50,0,10,0q�6[   $��1��6o   $��3��6   &   Xceed.Wpf.Toolkit.DateTimePicker  5#   
   -   5        $��
dtpDueDate��5#      $
��Left=�60   $��Center=�6K   $��2��6f   $��3��6v   $��2��6�   $	��200��6�   $��10,0,0,0q�6�   �� 5&   
   $��Paid:��6   $��Right=�6    $��Center=�6<   $��-150,0,10,0q�6W   $��1��6l   $��4��6|   �� 5'   
   -   5        $��chkPaid��5'      $
��Left=�6#   $��Center=�6>   $��2��6Y   $��4��6i   $��2��6v   $	��200��6�   $��10,0,0,0q�6�   �� 5*   
   -   5        $
��Save��5*      $��Center=�6    $��2��6=   $��8��6M   $��Center=�6Z   $��60��6u   $��Consolas)�6�   $��18(�6�   $
��Bold"�6�   �� 5-   
   -   5        $��	Dashboard��5-      $��Center=�6%   $��0��6B   $��4��6O   $��Center=�6_   $��16(�6z   $��90��6�   6�   5/      50      