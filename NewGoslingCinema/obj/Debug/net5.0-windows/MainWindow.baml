   M S B A M L   `   `   `  ����K  GNewGoslingCinema, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:NewGoslingCinemaNewGoslingCinema  5   	   !    NewGoslingCinema.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:NewGoslingCinema     5   	   -   5          � Title$  Кинотеатр��5   	   $	��552��6   $	��816��6(    � 
ResizeMode$ NoResize=�6K    � WindowStartupLocation$ CenterScreen=�6x   .��5	      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ Black��6    � Offset$ 1Q�6-   � 5      $ 	#FF425B8F��6   $ 0Q�61   5      .+�5   
   �� -   5         ��Name$ Scroll��5      $
��Left=�6)   $	��396��6D   $��
408,0,0,10q�6P   ��5      �� 5      $ 0.5,1&�6&   $ 0.5,0&�67   .��5      � $ Black��6#   $ 1Q�61   � 5      $ 	#FF425B8F��6#   $ 0Q�65   5      5      �� 5   
   $
��Left=�6   $��1330,244,0,0q�6-   $��TextBox��6C    �� TextWrapping$
 Wrap=�6R   $	��Top=�6f   $	��120��6~   c� 5   
   $��	0,0,758,0q�6   ���	#FFEEF5FD6%   9� 5   
   	 9� ExpandDirection$	 Right=�6   $��Корзина��6+   $
��Left=�6<   $	��210��6W   $��7,9,0,0q�6d   $	��Top=�6u   $	��340��6�   $��8(�6�   .��5      �� -   5        
 ��Name$

 Cage��5      $	��293��6>   $	��250��6J   $
��Left=�6W   $��Center=�6r   ��5      �� 5       �� 
TargetType" ��6   .�5       ��  �� Property �@�ContextMenu �� Value 5!      �� 5"   "   .@�5#   &   m�  m�Name$ Delete��6A   $��Очистить��6/   ��5$   *   �� 5%   .   " m�64   .�5&   2   -   5        5'   /   5)   '   m� 5*   &   $	 Buy��6?   $��Купить��6/   ��5+   *   �� 5,   .   " m�64   .�5-   2   -   5        5.   /   50   '   51   #   53      54      56      57      9� 58   
   $	 Right=�6   $��Билеты��6+   $��5,220,471,99q�6;   $��10(�6Q   .��59      �� -   5        $
 Tickets��59      $	��277��6A   $	��310��6N   $	��Top=�6Z   $
��Left=�6r   5:      �� 5;   
   -   5         ��Name$
 Exit��5;      $��
Выход��6   $
��Left=�6/   $��	4,495,0,0q�6J   $	��Top=�6]   $��37��6u   $��21��6�   ���	#FFEEF5FD$
��Bold"�6�   $��10(�6�   ��6�   Z� 6�   5<      5=      