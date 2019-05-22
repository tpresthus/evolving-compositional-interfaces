using Admin.Authorization;
using Admin.Navigation;

namespace Admin.Customers
{
    public class CustomersViewModel : BaseViewModel
    {
        public CustomersViewModel(Menu menu, User user, UrlFactory urlFactory)
            : base(menu, user, urlFactory)
        {
        }
    }
}
