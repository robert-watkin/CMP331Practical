   M S B A M L   `   `   `  ����J  FCMP331Practical, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CMP331Practical.ViewsCMP331Practical.Views  5   	   %    CMP331Practical.Views.Dashboard   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CMP331Practical.Views     5   	     � Title$  	Dashboard��5   	    � 
ResizeMode$ NoResize=�6   $	��600��61   $	��800��6>   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��0.3* �6   �� 5      $
��0.7* �6   �� 5       � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $
��1.5* �6   �� 5      .+�5   
   �� $��	Dashboard��6   $��0,0,0,0q�6$   $��24(�65   $��Segoe UI Semibold)�6C   $��0��6b   $��0��6o   $��2��6   �� 5   
   -   5        $��Sign Out��5      $��Right=�6$   $��
0,10,10,10q�6@   $��80��6T   $��2��6_   $��0��6o   6|   �� 5   
   -   5        $��txtUserName��5      $��Name:��6#   $
��Left=�63   $��10,0,0,0q�6N   $��14(�6`   $��Segoe UI Semibold)�6n   $��1��6�   $��0��6�   $��2��6�   �� 5   
   -   5        $��txtUserRole��5      $��Role:��6#   $
��Left=�63   $��	10,25,0,0q�6N   $��14(�6a   $��Segoe UI Semibold)�6o   $��1��6�   $��0��6�   $��2��6�   �� 5   
   $��1q�6   
���   �$��1��69   $��0��6F   $��2��6V   $��
10,60,10,5q�6j    �� Padding$ 0q�6~   ._�5      �� $
��Auto=�6   .��5      ��  �� Orientation$ Vertical=�6   .+�5       �� $��Segoe UI Bold)�6   $��14(�67   $��Notifications��6E   �� 5!      -   5        $��Notifications��5!   #   ��5"      �� 5#      .R�5$   "   �� $��0,1,0,0q�6)   
���   �._�5%   &   �� $ Vertical=�61   $��	5,10,5,10q�6H   .+�5&   *   �� ��64   �� *	address+ �� StringFormat$ Address: {0}���� 5'   *   $
��Wrap=�64   ��6H   �� *notification+5(   '   5)   #   5*      5,      5-      5.      5/      �� 53   
    � IsSharedSizeScope . $ Vertical=�63   $��2��6J   $��2��6W   .+�54      �� -   5        $��btnAvailableProperties��54      $��3��63   $��Available Properties��6@   $��50��6_   $��
20,15,15,5q�6k   $��18(�6   6�   �� 55      -   5        $��btnUserManagement��55      $��3��6.   $��User Management��6;   $��50��6U   $��	15,5,15,5q�6a   $��18(�6t   6�   �� 56      -   5        $��btnPropertyManagement��56      $��3��62   $��Property Management��6?   $��50��6]   $��	15,5,15,5q�6i   $��18(�6|   6�   57      �� 59   
   $��1q�6   
���   �$��1��69   $��2��6F   $��2��6V   $��2��6g   $��10,20,10,30q�6{   $ 0q�6�   ._�5:      �� $
��Left=�6   $��370px��66   $
��Auto=�6D   .��5;      �� $ Vertical=�6   .+�5<      �� $��Segoe UI Bold)�6   $��14(�67   $��Assigned Properties��6E   �� 5=      -   5        $��AssignedProperties��5=   #   ��5>      �� 5?      .R�5@   "   �� $��0,1,0,0q�6)   
���   �._�5A   &   �� $ Vertical=�61   $��	5,10,5,10q�6H   .+�5B   *   �� ��64   �� *AddressLine1+�� 5C   *   ��64   �� *
PostCode+�� 5D   *   $��0,5,0,0q�64   ��6E   �� *RequiredMaintainance+$  Required Maintainance: {0}���� 5E   *   ��64   �� *QuarterlyInspection+$ Next Inspection: {0}���� 5F   *   ��64   �� *AnnualGasInspection+$ Next Gas Inspection: {0}���� 5G   *   ��64   �� *FiveYearElectricalInspection+$% Next Electrical Inspection: {0}���� 5H   *   $��btnReporting��61   -	   5        $��	Reporting��5H   X   $��	25,5,25,0q�6l   
 � Tag 6E   �� *Id+6   5I   '   5J   #   5K      5M      5N      5O      5P      5S      5T      