namespace Admin.Transactions
{
    public class PartyViewModel
    {
        public PartyViewModel(Party party)
        {
            if (party == null)
            {
                throw new System.ArgumentNullException(nameof(party));
            }

            Name = party.Name;
            OldBalance = party.OldBalance.ToString("c");
            NewBalance = party.NewBalance.ToString("c");
        }

        public string Name { get; }
        public string OldBalance { get; }
        public string NewBalance { get; }
    }
}
