   M S B A M L   `   `   `  ����J  FCMP331Practical, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CMP331Practical.ViewsCMP331Practical.Views  5   	   +    %CMP331Practical.Views.ReportingScreen   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CMP331Practical.Views     5   	     � Title$  ReportingScreen��5   	    � 
ResizeMode$ NoResize=�6!   $	��450��67   $	��650��6D    � 	Resources 5	         markPaid �� 
TargetType �� Triggers �� Binding �� Value �� Property �� Value��%�   &	        �� 5
   
   " ��6!    5      �� 5       6   �� *IsPaid+ True69   .a�5      ��  � y�$ Hidden=�63   5      5      5      .��5      � 	 � ColumnDefinitions	 5   
   �� 5      �� 5      
 � RowDefinitions
 5   
   �� 5      $
��0.1* �6   �� 5      .+�5   
   �� -   5        $��New Invoice��5      $��20,0,0,0q�6'   $
��Left=�69   $��0��6T   $��0��6a   $��Center=�6q   $��16(�6�   $��90��6�   6�   �� 5   
   $��	Reporting��6   $��Center=�6$   $��24(�6A   $��Sergoe UI Semibold)�6O   $��0��6o   $��0��6   $��2��6�   �� 5   
   -   5        $��	Dashboard��5      $��0,0,20,0q�6%   $��Right=�67   $��0��6S   $��1��6`   $��Center=�6p   $��16(�6�   $��90��6�   6�   �� 5"   
   $��1q�6   
���   �$��1��69   $��0��6F   $��5q�6V    �� Padding$ 0q�6a   ._�5#      �� $
��Left=�6   $	��305��66   $
��Auto=�6B   .��5$      ��  �� Orientation$ Vertical=�6   .+�5%      �� $��Segoe UI Bold)�6   $��14(�67   $��Invoices��6E   �� 5&      -   5        $��Invoices��5&   #   ��5'      �� 5(      .R�5)   "   �� $��0,1,0,0q�6)   
���   �._�5*   &   �� $ Vertical=�61   $��	5,10,5,10q�6H   .+�5+   *   �� ��64   �� *	DueDate+ �� StringFormat$ Due Date: {0}���� 5,   *   $
��Wrap=�64   ��6H   �� *	AmountDue+$ Amount Due: £{0}���� 5-   *   $
��Wrap=�64   ��6H   �� *IsPaid+$ 	Paid: {0}���� 5.   *   -   5        $��Mark as Paid��5.   1   #��[  6H   $��5q�6}   
 � Tag 6j   �� *Id+6�   �� 5/   *   -   5        $��Delete��5/   1   $��5,0,5,0q�6U    6B   �� *Id+6f   50   '   51   #   52      54      55      56      57      �� 59   
   $��1q�6   
���   �$��1��69   $��1��6F   $��5q�6V   $ 0q�6a   ._�5:      �� $ Vertical=�6   .+�5;      �� $��Segoe UI Bold)�6   $��14(�63   $
��Info��6A   �� 5>      $��Segoe UI Semibold)�6   $��Address:��67   �� 5?      -   5        $��
lblAddress��5?   .   $��Segoe UI)�6   $��example��6@   �� 5A      $��Segoe UI Bold)�6   $��Required Maintainance:��63   �� 5B      -   5        $��lblMaintainance��5B   .   $��Segoe UI)�6   6.   �� 5D      $��Segoe UI Bold)�6   $��Letting Agent:��63   �� 5E      -   5        $��lblLettingAgent��5E   .   $��Segoe UI)�6   6.   �� 5G      $��Segoe UI Bold)�6   $��Maintainance Staff:��63   �� 5H      -	   5        $��lblMaintainanceStaff��5H   .   $��Segoe UI)�6   6.   5I      5J      5K      5L      