using System;

namespace Admin.Transactions
{
    public class Transaction
    {
        public Transaction(int step, string type, decimal amount, Party payer, Party payee, bool isFraud, bool isFlaggedFraud)
        {
            // TOOD: Reduce the number of constructor arguments.
            Step = step;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Amount = amount;
            Payer = payer ?? throw new ArgumentNullException(nameof(payer));
            Payee = payee ?? throw new ArgumentNullException(nameof(payee));
            IsFraud = isFraud;
            IsFlaggedFraud = isFlaggedFraud;
        }

        public int Step { get; }
        public string Type { get; }
        public decimal Amount { get;}
        public Party Payer { get; }
        public Party Payee { get; }
        public bool IsFraud { get; }
        public bool IsFlaggedFraud { get; }
    }
}
