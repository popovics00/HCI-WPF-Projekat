   M S B A M L   `   `   `  ����I  ENetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:NetworkService.ViewsNetworkService.Views  5      C&clr-namespace:NetworkService.ViewModelNetworkService.ViewModel  5      ;"clr-namespace:NetworkService.ModelNetworkService.Model  5      .    (NetworkService.Views.NetworkEntitiesView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:NetworkService.Views     5      <	viewModel&clr-namespace:NetworkService.ViewModel     5      3data"clr-namespace:NetworkService.Model     5      .��5      � .+�5   
   ��   �� Orientation$  Vertical=�6   $	��450��6,   $��
0,0,0,-0.4q�69   $��Bottom=�6M   .+�5      �� $  
Horizontal=�6   $
��Left=�62   $	��Top=�6M   $	��741��6e   .+�5      �� $  Vertical=�6   $��Center=�64   .+�5      	� -   5         	�Name$ RbName��5   "   $
��Bold"�62    	� 	GroupName$ Search��6D   $��Naziv��6y   $��5 0 5 0q�6�    � ToolTip$ Pretraga po nazivu��6�   
������� d� 	IsChecked 6W   �� *NameChecked+6�   	� 5      -   5        $ RbType��5   "   $
��Bold"�62   $ Search��6D   $	��Tip��6y   $��5 0 5 0q�6�   $ Pretraga po tipu��6�   
������� 6W   �� *TypeChecked+6�   5      �� 5      $  Vertical=�6   .+�5      �� -   5         ��Name$ 	TextBoxId��5      
 � Tag$  Search��6O   $	��150��6]   $��25��6i   $��	5 20 10 5q�6u   $* $Ovde unesite kljucnu rec za pretragu��6�   ��61   �� *
SearchText+6�   �� 5      
���  ��6!   5      �� 5      �� 5      -   5         ��Name$ ButtonSearch��5      $��Search��6Q   $
��Bold"�6c   $	��100��6u   $��20��6�   $��5 5 0 5q�6�   $ Kliknite za pretragu��6�   ��6/   �� *SearchCommand+6�   �� 5      -   5        $ ButtonReset��5      $��Reset��6O   $
��Bold"�6_   $	��100��6q   $��20��6}   $��5 5 0 5q�6�   $( "Klik za ponistavanje opcije Search��6�   ��6.   �� *ResetCommand+6�   �� 5      -   5        $ btnAddSearch��5      $	��ADD��6/   $	��100��6=   $��20��6v   $��10 5 0 5q�6�   ��6I   �� *SaveCurrentSearchCommand+6�   �� 5      -   5         ��Name$ 	cmbSearch��5      	 �� SelectedIndex$	 0��6R   
 �� 
IsReadOnly
 . $	��100��5      $��20��6"   $�� 5 2 5 5q�6Z   ��5   .   �� *SavedSearch+ �� SelectedItem 5   .   �� *CurrentSearchText+5       5!      &   System.Windows.Controls.DataGrid  5"      -   5          Name$ 
GridTabela��5"         
IsReadOnly .    SelectionMode$ Single=�6�      AutoGenerateColumns .  $
��Left=�5#      $	��233��6.   $	��Top=�6;   $	��764��6S   $��10,5,0,0q�6_   $��19(�6q   $��Kartika)�6   ��5"   +   �� *GeneratoriSearch+ 6T   �� *SelectedGenerator+   Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+6L     5&      $ Naziv��6)   $ * 6R    68   �� *Naziv+6R     5'      $	 Tip��6)   $ * 6S    66   �� *
Tip.Name+6S     5(      $ Vrednost��6)   $ * 6U    6;   �� *Value+6U   5*      �� 5+      $��10��6   �� 5,      $  
Horizontal=�6   $��Bottom=�62   $��Center=�6M   $��
10,0,220,0q�6j   $��76��6~   $	��570��6�   .+�5-      �� $  Vertical=�6   .+�5.      �� $	��Tip��6    $��20(�6,   $
��Bold"�6:   $��80��6L   $��25��6W    �� TextAlignment$ Center=�6c   
�������6z   5/      �� 50      $  Vertical=�6   .+�51      �� -	   5        $ ComboBoxType��51       �� Text$ Select option��6�   $	��120��6�   $��25��6�   $ Odaberite tip iz liste��6�   ��66   �� *TipoviComboBox+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� 6�   �� *SelectedTip+6�   52      �� 53      $��10��6   �� 54      -
   5         ��Name$ ImageSrc��54      $��83��6l   $��0,2,0,8q�6w   ��6*   �� *	ImagePath+$ PropertyChanged=�6w   �� 55      $��10��6   �� 56      $  Vertical=�6   $��Center=�64   .+�57      �� -   5        $ 	ButtonAdd��57      $��Dodaj��60   $��80��6@   $��30��6K   $
��Bold"�6W   $  Klik za dodavanje elementa��6�   ��6i   �� *
AddCommand+6�   �� 58      $��10��6    �� 59      -   5        $ ButtonDelete��59      $��Obrisi��63   $��80��6D   $��30��6O   $
��Bold"�6[   $ Klik za brisanje elementa��6�   ��6m   �� *DeleteCommand+6�   5:      5<      �� 5=      $  Vertical=�6   $��Center=�60   .+�5>      �� -   5        $ 
HelpButton��5>      $��?��6-   ���	#FF79B6F0$��12(�6P   $��	ExtraBold"�6^   $��21��6u   $��364,0,401,0q�6�    J� RenderTransformOrigin$ 
1.34,0.379&�6�   $ Pomoc��6�   ��6�   �� *ShowHelpCommand+6�   �� 5?      -   5        $ ButtonStartSimulator��5?      $��Start Simulator��67   $
��Bold"�6Q   $��24��6c   $��
677,5,10,5q�6o   $ Pokrenite simulator��6�   ��6�   �� *StartSimulatorCommand+5A      5B      5C      5D      