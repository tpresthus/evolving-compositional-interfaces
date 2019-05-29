using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Admin.Authorization;
using Admin.Navigation;

namespace Admin.Transactions
{
    public class TransactionsViewModel : BaseViewModel, IEnumerable<TransactionViewModel>
    {
        private readonly IEnumerable<TransactionViewModel> transactions;

        public TransactionsViewModel(IEnumerable<Transaction> transactions, Menu menu, User user, UrlFactory urlFactory)
            : base(menu, user, urlFactory)
        {
            if (transactions == null)
            {
                throw new ArgumentNullException(nameof(transactions));
            }

            this.transactions = transactions.Select(transaction => new TransactionViewModel(transaction));
        }

        public IEnumerator<TransactionViewModel> GetEnumerator()
        {
            return this.transactions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.transactions.GetEnumerator();
        }
    }
}
