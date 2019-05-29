using Admin.Navigation;
using Admin.Authorization;

namespace Admin.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(Menu menu, User user, UrlFactory urlFactory)
            : base(menu, user, urlFactory)
        {
        }
    }
}
