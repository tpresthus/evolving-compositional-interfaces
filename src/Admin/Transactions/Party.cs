namespace Admin.Transactions
{
    public class Party
    {
        public Party(string name, decimal oldBalance, decimal newBalance)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            OldBalance = oldBalance;
            NewBalance = newBalance;
        }

        public string Name { get; }
        public decimal OldBalance { get; }
        public decimal NewBalance { get; }
    }
}
