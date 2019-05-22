using Admin.Authorization;
using Admin.Navigation;

namespace Admin
{
    public class BaseViewModel
    {
        public BaseViewModel(Menu menu, User user, UrlFactory urlFactory)
        {
            User = new UserViewModel(user);
            Menu = new MenuViewModel(menu, urlFactory);
        }

        public UserViewModel User { get; }
        public MenuViewModel Menu { get; }

    }
}
