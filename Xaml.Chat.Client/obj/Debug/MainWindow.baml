   M S B A M L   `   `   `  ����K  GXaml.Chat.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Xaml.Chat.Client.ViewsXaml.Chat.Client.Views  5   	   I)clr-namespace:Xaml.Chat.Client.ViewModelsXaml.Chat.Client.ViewModels  5   	   C&clr-namespace:Xaml.Chat.Client.HelpersXaml.Chat.Client.Helpers  5      !    Xaml.Chat.Client.MainWindow   5   	   .Views$clr-namespace:Xaml.Chat.Client.Views  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @
ViewModels)clr-namespace:Xaml.Chat.Client.ViewModels     5   	   :helpers&clr-namespace:Xaml.Chat.Client.Helpers     5      -   5          � Title$  	XAML Chat��5   	    � SizeToContent$ WidthAndHeight=�6   $	��300��6:   $	��300��6I    � Icon$ Images/logo.png��5	       � DataContext 5      .   (Xaml.Chat.Client.ViewModels.AppViewModel  5   
    � 	Resources 5      8   2Xaml.Chat.Client.Helpers.BoolToVisibilityConverter   boolToVisibilityConverter �� DataType7   1Xaml.Chat.Client.ViewModels.RegisterFormViewModel-   'Xaml.Chat.Client.Views.RegisterFormView7   1Xaml.Chat.Client.ViewModels.ConversationViewModel-   'Xaml.Chat.Client.Views.ConversationView2   ,Xaml.Chat.Client.ViewModels.GeneralViewModel(   "Xaml.Chat.Client.Views.GeneralView0	   *Xaml.Chat.Client.ViewModels.LoginViewModel*
   $Xaml.Chat.Client.Views.LoginFormView5   /Xaml.Chat.Client.ViewModels.SearchFormViewModel+   %Xaml.Chat.Client.Views.SearchFormView2   ,Xaml.Chat.Client.ViewModels.ProfileViewModel(   "Xaml.Chat.Client.Views.EditProfile:   4Xaml.Chat.Client.ViewModels.ContactRequestsViewModel0   *Xaml.Chat.Client.Views.ContactRequestsView>   8Xaml.Chat.Client.ViewModels.MissedConversationsViewModel4   .Xaml.Chat.Client.Views.MissedConversationsView9   3Xaml.Chat.Client.ViewModels.ViewUserProfileVewModel0   *Xaml.Chat.Client.Views.ViewUserProfileView��%�  &	        (��      5      *, +)(�� I     5      *, +)(��      5      *, +)(�� �     5      *,	 +)(�� �     5#      *, +)(�� !    5'      *, +)(�� W    5+      *, +)(�� �    5/      *, +)(�� �    53      *, +)  5   
   6,   �� 5   
   "  5      .R�5        5      �� 5   
   "  5      .R�5        5      �� 5   
   "  5      .R�5        5      �� 5   
   " 	 5      .R�5       
  5!      �� 5"   
   "  5#      .R�5$        5%      �� 5&   
   "  5'      .R�5(        5)      �� 5*   
   "  5+      .R�5,        5-      �� 5.   
   "  5/      .R�50        51      �� 52   
   "  53      .R�54        55      56      .��57      �  � RowDefinitions 58   
   �� 59      $��45 �6   �� 5:      .+�5=   
   c� $��0��6   $��45��6   y�5>      �� *LoggedInUser+ �� 	Converter# [  .@�5@      �� ��6   �� *
GoToHome+.��5A      ��  �� Orientation$ 
Horizontal=�6   .+�5B      �� $��Images\Home.png��6   �� 5C      $
��Home��6    	 �� Padding$	 5q�6,   $��Center=�68   5D      5E      �� 5F      ��6   �� *GoToProfile+.��5G      �� $ 
Horizontal=�6   .+�5H      �� $��Images\Edit.png��6   �� 5I      $��Edit Profile��6    $	 5q�64   $��Center=�6@   5J      5K      �� 5L      ��6   �� *GoToSearhContacts+.��5M      �� $ 
Horizontal=�6   .+�5N      �� $��Images\Search.png��6   �� 5O      $��Search Contacts��6    $	 5q�67   $��Center=�6C   5P      5Q      �� 5R      ��6   �� *GoToContactRequests+.��5S      �� $ 
Horizontal=�6   .+�5T      �� $ ��Images\BusinessContact.png��6   �� 5U      $	 5q�5V       $��Center=�6,   ��5U       �� *ContactRequestsCount+
 �� StringFormat$
 Contact requests: {0}��5V   ,   5W      5Y      �� 5[      ��6   �� *GoToMissedConversations+.��5\      �� $ 
Horizontal=�6   .+�5]      �� $��Images\MissedCall.png��6   �� 5^      $	 5q�5_       $��Center=�6,   ��5^       �� *MissedConversationsCount+$
 Missed conversations: {0}��5_   ,   5`      5b      �� 5d      ��6   �� *Logout+.��5e      �� $ 
Horizontal=�6   .+�5f      �� $��Images\Logout.png��6   �� 5g      $��Logout��6    $��Center=�6.   $	 5q�6I   5h      5j      5k      �� 5l   
   $��1��6   ��5m      ��  �� Path$ CurrentViewModel�5n      5o      