   M S B A M L   `   `   `  ����D  @MeteoApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MeteoApp2	MeteoApp2  5   	   #    MeteoApp2.DuplicateCityWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MeteoApp2     5   	     � Title$  Duplicate City��5   	   $��150.992��6    $��296.997��61   .��5	      � .+�5
   
   �� $T��NCette ville existe dans plusieurs pays. Veuillez préciser le pays également.��6   $
��Left=�6h   $��40��6�   $��	45,10,0,0q�6�   $	��Top=�6�    �� TextAlignment$ Center=�6�   $	��211��6�   $
��Wrap=�6�   �� 5   
   -   5         ��Name$ 
BoxCountry��5      $
��Left=�6&   $��23��6A   $��	80,53,0,0q�6M    �� TextWrapping$
 Wrap=�6`   $	��Top=�6�   $	��141��6�   ��6t   �� *	Country+ �� Mode$ TwoWay=�6�   �� 5   
   -   5         ��Name$ ButtonProceed��5      $��Valider��6D   $
��Left=�6V   $��	80,81,0,0q�6q   $	��Top=�6�   $��75��6�   ��6(   �� *	Proceed+6�   �� 5   
   $��Retour��6*   $
��Left=�6;   $��
166,81,0,0q�6V   $	��Top=�6j   $��55��6�   $��20��6�   ��6   �� *Back+6�   5      5      