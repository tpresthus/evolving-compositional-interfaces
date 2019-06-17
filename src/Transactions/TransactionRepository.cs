using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Transactions
{
    internal class Transaction
    {
        public int Id;
        public int Step;
        public string Type;
        public decimal Amount;
        public Party Payer;
        public Party Payee;
        public bool IsFraud;
        public bool IsFlaggedFraud;

        public decimal CapturedAmount;
        
        public class Party
        {
            public string Name;
            public decimal OldBalance;
            public decimal NewBalance;
        }

        internal bool CaptureAllowed => Amount > CapturedAmount;
        internal bool ReversalAllowed => CapturedAmount > 0;

        public void Capture(decimal amount)
        {
            if(!CaptureAllowed)
                throw new ApplicationException("Can not capture requested amount");

            CapturedAmount = Math.Min(Amount, CapturedAmount + amount);
        }

        public void Reverse(decimal amount)
        {
            if (!ReversalAllowed)
                throw new ApplicationException("Can not reverse requested amount");
            
            if(amount > CapturedAmount)
                throw new ApplicationException("Can not reverse a higher amount than what has been captured");

            CapturedAmount -= amount;
        }
    }
    
    public class TransactionRepository
    {
        private readonly Func<IEnumerable<Transaction>> populator;
        private List<Transaction> transactions;
        
        internal TransactionRepository(Func<IEnumerable<Transaction>> populator)
        {
            this.populator = populator;
        }

        internal IEnumerable<Transaction> All()
        {
            if (transactions == null)
            {
                transactions = populator().ToList();
            }

            return transactions.AsReadOnly();
        }
        
        internal Transaction Get(int id) => All().FirstOrDefault(t => t.Id == id);
        
        internal static IEnumerable<Transaction> ReadTransactionsFromFile(string path)
        {
            // TODO: Make this IAsyncEnumerable.
            var transactionsFilePath = Path.Combine(path, "transactions.csv");

            using (var fileStream = File.OpenRead(transactionsFilePath))
            using (var streamReader = new StreamReader(fileStream))
            {
                string line;
                var i = 0;

                // TODO: Use ReadLineAsync() with IAsyncEnumerable.
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (i++ == 0)
                    {
                        // Skip the header line
                        continue;
                    }

                    Transaction transaction;

                    try
                    {
                        transaction = ParseTransaction(line);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Error parsing the following line:");
                        Console.WriteLine(line);
                        Console.WriteLine();
                        Console.WriteLine(exception);
                        yield break;
                    }

                    yield return transaction;
                }
            }
        }

        private static Transaction ParseTransaction(string line)
        {
            var columns = line.Split(',');
            var id = Int32.Parse(columns[0]);
            var step = Int32.Parse(columns[1]);
            var type = columns[2];
            var amount = Decimal.Parse(columns[3], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var nameOrig = columns[4];
            var oldbalanceOrg = Decimal.Parse(columns[5], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var newbalanceOrig = Decimal.Parse(columns[6], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var nameDest = columns[7];
            var oldbalanceDest = Decimal.Parse(columns[8], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var newbalanceDest = Decimal.Parse(columns[9], NumberStyles.Float, NumberFormatInfo.InvariantInfo);
            var isFraud = columns[10] == "1";
            var isFlaggedFraud = columns[11] == "1";

            return new Transaction
            {
                Id = id,
                Step = step,
                Type = type,
                Amount = amount,
                Payer = new Transaction.Party
                {
                    Name = nameOrig,
                    OldBalance = oldbalanceOrg,
                    NewBalance = newbalanceOrig
                },
                Payee = new Transaction.Party
                {
                    Name = nameDest,
                    OldBalance = oldbalanceDest,
                    NewBalance = newbalanceDest
                },
                IsFraud = isFraud,
                IsFlaggedFraud = isFlaggedFraud
            };
        }
    }
    
}