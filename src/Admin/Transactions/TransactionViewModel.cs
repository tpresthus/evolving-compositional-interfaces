using System;

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

            Id = transaction.Id;
            Step = transaction.Step;
            Type = transaction.Type;
            Amount = transaction.Amount.ToString("c");
            CapturedAmount = transaction.CapturedAmount.ToString("c");
            Payer = new PartyViewModel(transaction.Payer);
            Payee = new PartyViewModel(transaction.Payee);
            IsFraud = transaction.IsFraud;
            IsFlaggedFraud = transaction.IsFlaggedFraud;

            Console.WriteLine($"Amount: {transaction.Amount}");
            Console.WriteLine($"Captured amount: {transaction.CapturedAmount}");
            HasCapture = transaction.CapturedAmount > 0;
            CanCapture = transaction.CapturedAmount < transaction.Amount;
            CanReverse = transaction.CapturedAmount > 0;
        }

        public int Id { get; }
        public int Step { get; }
        public string Type { get; }
        public string Amount { get; }
        public string CapturedAmount { get; }
        public PartyViewModel Payer { get; }
        public PartyViewModel Payee { get; }
        public bool IsFraud { get; }
        public bool IsFlaggedFraud { get; }
        
        public bool HasCapture { get; }
        public bool CanCapture { get; }
        public bool CanReverse { get; }
    }
}
