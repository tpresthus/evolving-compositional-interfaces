using System;
using Admin.Authorization;

namespace Admin.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(User user)
        {
            User = new UserViewModel(user);
        }

        public UserViewModel User { get; }
    }
}