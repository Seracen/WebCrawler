   M S B A M L   `   `   `  ����E  AWebCrawler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:WebCrawler
WebCrawler  5   	       WebCrawler.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:WebCrawler     5   	     � Title$  
WebCrawler��5   	   $	��580��6   $	��820��6)   $	��820��65   $	��580��6D   .��5
      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   o� 
�������.@�5      m� $��Options��6   $��3q�6(   $
��Bold"�63   .@�5      �� -   5        .��5   6   
Podgląd6?   5      5      � 5   
   $��5q�6   
�������$��1��6-    � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5      .+�5      �� $��3��6   
���   �$��1q�6=   ._�5      � .+�5      �� -   5         ��Name$ linksListView��5      $��5q�66   .@�5      �� .��6'   Nie rozpoczęto wyszukiwania6D   5      5       5!      �� 5"      $��3��6   
���   �$��0,1,1,1q�69   ._�5#      �� .+�5$      �� $
��Bold"�6   $��14(�6.   $
��Play)�6<    � ToolTip$ Początkowy adres url��6N   .��6m   URL Address6z   �� 5%      -   5         ��Name$ 
uRLAddress��5%   /   $��5,0,5,0q�6   6/   �� 5&      $
��Bold"�6   60   �� 5'      $��1q�6   
�������$��5q�6D   ._�5(      �� .+�5)      �� $��14(�6$   $
��Bold"�62   $
��Play)�6D   $$ Metoda przeszukiwania podstron��6V   .��6   Search Method6�   	� 5*      -   5        $
��Play)�5*   *   $��2q�6<   $	��BFS��6G    d� 	IsChecked$
 TrueY�6U   6f   	� 5+      $
��Play)�6*   $��2q�6<   $	��DFS��6G   5,      5-      �� 5.      $��1q�6   
�������$��5q�6D   ._�5/      �� .+�50      �� $
��Bold"�6$   $��14(�66   $
��Play)�6D   $! Długość czasu działania��6V   .��6x   	Work time6�      WebCrawler.Time  51      -   5          Name$ workTime��51   )   $	��180��6;   $��2q�6G   52      53      �� 54      $��1q�6   
�������$��5q�6D   ._�55      �� .+�56      �� $��14(�6$   $
��Bold"�62   $
��Play)�6D   $+ %Głębokość analizy struktury sieci��6V   .��6�   Depth6�      WebCrawler.NumberControl  57      -   5          Name$ depth��57   C   $��5,0,5,2q�62   6C   58      59      �� 5:      $��1q�6   
�������$��5q�6D   ._�5;      �� .+�5<      �� $��14(�6$   $
��Bold"�62   $
��Play)�6D   $ Number of running threads��6V   .��6z   Number of threads6�     5=      -   5        $ chosenNumberOfThreads��5=   2   5>      5?      �� 5@      -   5        $
��Left=�5@      $	��150��68   $��25��6D   $��5,10,0,0q�6P   $
��Play)�6q   	 �� 	IsDefault$
	 True��6�   .��6�   Search6�   �� 5A      -	   5        $
��Left=�5A      $	��150��68   $��25��6D   $��5,10,0,0q�6P   $
��Play)�6t   .��6�   Save6�   5B      5C      5D      5E      5F      