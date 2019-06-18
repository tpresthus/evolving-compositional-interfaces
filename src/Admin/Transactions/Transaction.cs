using System;

namespace Admin.Transactions
{
    public class Transaction
    {
        public Transaction(int id, int step, string type, decimal amount, decimal capturedAmount, Party payer, Party payee, bool isFraud, bool isFlaggedFraud)
        {
            // TOOD: Reduce the number of constructor arguments.
            Id = id;
            Step = step;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Amount = amount;
            CapturedAmount = capturedAmount;
            Payer = payer ?? throw new ArgumentNullException(nameof(payer));
            Payee = payee ?? throw new ArgumentNullException(nameof(payee));
            IsFraud = isFraud;
            IsFlaggedFraud = isFlaggedFraud;
        }

        public int Id { get; }
        public int Step { get; }
        public string Type { get; }
        public decimal Amount { get;}
        public decimal CapturedAmount { get; }
        public Party Payer { get; }
        public Party Payee { get; }
        public bool IsFraud { get; }
        public bool IsFlaggedFraud { get; }
    }
}
