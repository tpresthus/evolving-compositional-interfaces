using System;
using Admin.Navigation;
using Admin.Authorization;

namespace Admin.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(Menu menu, User user, UrlFactory urlFactory)
        {
            User = new UserViewModel(user);
            Menu = new MenuViewModel(menu, urlFactory);
        }

        public UserViewModel User { get; }
        public MenuViewModel Menu { get; }
    }
}
