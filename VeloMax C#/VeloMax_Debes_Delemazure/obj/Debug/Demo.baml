   M S B A M L   `   `   `  ����S  OVeloMax_Debes_Delemazure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:VeloMax_Debes_DelemazureVeloMax_Debes_Delemazure  5   	   #    VeloMax_Debes_Delemazure.Demo   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:VeloMax_Debes_Delemazure     5   	   J FXamlAnimatedGif, Version=1.2.3.0, Culture=neutral, PublicKeyToken=null<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  5   	     � Title$
  Demo��5	   	   $	��450��6   $	��850��6#    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency . .��5      �� ���#91D8DB �� CornerRadius$ 30��5      ._�5   
   �  � RowDefinitions 5      �� 5      $	��150 �6    5      �� 5      5      .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0��5       K� ZIndex$ 3��5      .+�5!      �� $��	MODE DEMO��6   $��45(�5"      
�������$��Center=�5$      $��	100,0,0,0q�5%      $A��;/VeloMax_Debes_Delemazure;component/Fonts/#Permanent Marker)�5&      �� 5(      $	��378��6   '  !XamlAnimatedGif.AnimationBehavior   	SourceUri$ Resources/bike.gif?�5)      $��Right=�5*      $��10,-114,0,-99q�5+      	 J� RenderTransformOrigin$	 0.797,0.502&�5,      5.      �� 50      $ 
Horizontal=�6   $��1��51      $ 4��52      .+�54      �� -   5        
 ��Name$
 datagrid��56      $��
60,0,15,10q�54      $	��378��55      
�������
���   �$A��;/VeloMax_Debes_Delemazure;component/Fonts/#Roboto Mono Thin)�59      �� 5;      $ Vertical=�6   .+�5=      �� -   5         ��Name$ 	text_demo��5A      $�� ��5=       $��20(�5>      
�������$��Consolas)�5@      $��0,50,0,0q�5B      5D      �� 5F      -   5        $ 
text_demo2��5J      $�� ��5F       $��20(�5G      
�������$��Consolas)�5I      $��0,20,0,0q�5K      5M      5O      5R      �� 5T      $��1��6   $ 5��5U      $��
0,225,0,22q�5V      $ 
Horizontal=�5W      .+�5Y      �� -   5        $��Quitter��5Y      ���#91D8DB$��14(�5[      $��Medium"�5\      $��Right=�5^      $	��154��5_      $��480,-40,0,-42q�6%   $	��135��6<   $	��Top=�6I   ��5a      �� 5b       �� 
TargetType" ��6    .�5c      ��  �� Property � �� �� Value$ Fonts/#Roboto)�6G   �� 5d       � ��
 �����6;   �� 5e       �@�Cursor$
 Hand��67    �� Triggers 5g      P� 5h   "    P� Property �@J�IsMouseOver P� Value . . �5i   &   ��  � �� �#143F3D6C   5j   #   5m      ��5p      �� 5q       �� 
TargetType" ��6*   .R�5r      �� $	��150��6%   $��40��61   $ 12��5s   %   #��z��5t   %   ._�5v   "   �� $��Center=�63   $��Center=�5w   3   5y      5z      5|      �� 5~      -   5        $��Suivant��5~      ���#91D8DB$��14(�5�      $��Medium"�5�      $��80��5�      $
��Left=�5�      $	��154��5�      $��
30,0,0,-27q�6%   $��Bottom=�69   ��5�      �� 5�      " ��6    .�5�      ��  � ��$ Fonts/#Roboto)�6G   �� 5�       � ��
 �����6;   �� 5�       �@�Cursor$
 Hand��67    5�      P� 5�   "    �@J�IsMouseOver . . �5�   &   ��  � �� �#143F3D6C   5�   #   5�      ��5�      �� 5�      " ��6*   .R�5�      �� $	��150��6%   $��40��61   $ 12��5�   %   #��z��5�   %   $��2,2,2,24q�6O   ._�5�   "   �� $��Center=�63   $��Center=�5�   3   5�      5�      5�      5�      5�      5�      5�      