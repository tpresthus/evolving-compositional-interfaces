using Admin.Authorization;
using Admin.Navigation;

namespace Admin.Transactions
{
    public class TransactionsViewModel : BaseViewModel
    {
        public TransactionsViewModel(Menu menu, User user, UrlFactory urlFactory)
            : base(menu, user, urlFactory)
        {
        }
    }
}
