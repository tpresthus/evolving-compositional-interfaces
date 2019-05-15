using System;
using Admin.Navigation;
using Admin.Authorization;

namespace Admin.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(Menu menu, User user)
        {
            User = new UserViewModel(user);
            Menu = new MenuViewModel(menu);
        }

        public UserViewModel User { get; }
        public MenuViewModel Menu { get; }
    }
}