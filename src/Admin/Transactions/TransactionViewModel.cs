using Admin.Authorization;
using Admin.Navigation;

namespace Admin.Transactions
{
    public class TransactionViewModel
    {
        public TransactionViewModel(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new System.ArgumentNullException(nameof(transaction));
            }

            Step = transaction.Step;
            Type = transaction.Type;
            Amount = transaction.Amount.ToString("c");
            Payer = new PartyViewModel(transaction.Payer);
            Payee = new PartyViewModel(transaction.Payee);
            IsFraud = transaction.IsFraud;
            IsFlaggedFraud = transaction.IsFlaggedFraud;
        }

        public int Step { get; }
        public string Type { get; }
        public string Amount { get; }
        public PartyViewModel Payer { get; }
        public PartyViewModel Payee { get; }
        public bool IsFraud { get; }
        public bool IsFlaggedFraud { get; }
    }
}
