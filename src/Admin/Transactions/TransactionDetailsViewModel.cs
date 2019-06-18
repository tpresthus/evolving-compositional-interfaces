using Admin.Authorization;
using Admin.Navigation;

namespace Admin.Transactions
{
    public class TransactionDetailsViewModel : BaseViewModel
    {
        public TransactionViewModel Transaction { get; }
        
        public decimal AmountToCapture { get; set; }
        public decimal AmountToReverse { get; set; }

        public TransactionDetailsViewModel(Transaction transaction, Menu menu, User user, UrlFactory urlFactory) : base(menu, user, urlFactory)
        {
            Transaction = new TransactionViewModel(transaction);
        }
    }
}