   M S B A M L   `   `   `  ????P  Lprospecplus-interface, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:prospecplus_interface.entities.Views$prospecplus_interface.entities.Views  5   	   1    +prospecplus_interface.entities.Views.inicio   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlocal2clr-namespace:prospecplus_interface.entities.Views     5   	     ? Title$  ProspecPlus??5   	   $	??500??6   $	??950??6*   $
??1000??69    ? WindowState$ Normal=?6F    ? WindowStartupLocation$ CenterScreen=?6[   .??5	      ? .+?5
   
   ? $	??300??6   $	??250??6   ???	#FF0E0E21$
??Left=?6D   $	??230??6_    ? ColumnDefinitions 5      ?? 5      $	??27* ?6#   ?? 5      $	??93* ?6#   ?? 5      $
??130* ?6#   .+?5      ?? $	??100??6   $??
48,56,75,0q?6!   $	??Top=?65   $??/images/user.png??6M   $??2??6g   $??1??6{   ?? 5      -   5         ??Name$
 home??5      $?? ??6#   $
??Left=?6.   $??
68,235,0,0q?6I   $	??Top=?6]   $??2??6u   $??38??6?   $??60??6?   ???	#00707070$??1??6?   ??5      ?? 5       ?? ImageSource$ /images/home.png??6!    j? Stretch$ Uniform=?6@   5      ?? 5      -   5        $ config??5      $?? ??6%   $
??Left=?60   $??
68,291,0,0q?6K   $	??Top=?6_   $??2??6w   $??38??6?   $??60??6?   ???	#00707070$??1??6?   ??5      ?? 5      $ /images/settings.png??6!   $ Uniform=?6D   5      ?? 5      -   5         ??Name$
 nome??5      $
??Left=?6&   $??	3,173,0,0q?6A   $
??Wrap=?6T   $??	TextBlock??6h   $	??Top=?6y   $??2??6?   $	??189??6?   ???	#FFFBFBFB$??20(?6?    ?? TextAlignment$ Center=?6?   $??1??6?   ?? 5      -   5        	 ??Name$	 idioma??5      $??1??6'   $??	0,0,65,42q?67   $??2??6J   $??Right=?6^   $	??120??6z   $??22??6?   $??Bottom=?6?   
 ?? SelectedIndex$
 0??6?   .@?5      ?? -   5        $??
Português??5      
???????
???????$	??100??6`   ?? 5      -   5        
???????
???????$??English??5   A   $	??100??6^   ?? 5      -   5        $??Spanish??5      $??Center=?6+   $	??100??6H   
???????
???????6r   5       5!      ? 5"   
   $??	250,0,0,0q?6   .+?5#      ? .+?5$       ? -   5          ?Name$ frame??5$        ? NavigationUIVisibility$ Hidden=?6'   5%       ? RowDefinitions 5&      ?? 5'      5)      5+      5,      