using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.model
{
    public class BalanceSheet
    {
        public double TotalPaid { get; private set; } = 0.0;
        public double TotalExpense { get; private set; } = 0.0;
        public Dictionary<User, double> Balances { get; } = new Dictionary<User, double>();

        public void AddTotalPaid(double amount)
        {
            TotalPaid += amount;
        }

        public void AddTotalExpense(double amount)
        {
            TotalExpense += amount;
        }

        public void AddBalance(User other, double amount)
        {
            // C# equivalent of getOrDefault
            Balances.TryGetValue(other, out double currentBalance);
            double newBalance = currentBalance + amount;

            if (Math.Abs(newBalance) < 1e-6)
            {
                Balances.Remove(other);
            }
            else
            {
                Balances[other] = newBalance;
            }
        }

        public void ClearBalances()
        {
            Balances.Clear();
        }

        public void Print(User me)
        {
            double youOwe = 0.0, youGetBack = 0.0;
            foreach (double amount in Balances.Values)
            {
                if (amount < 0) youOwe += -amount;
                else youGetBack += amount;
            }

            Console.WriteLine($"💵 Balance sheet of : {me.Name}");
            Console.WriteLine($"Total You Paid : {TotalPaid}");
            Console.WriteLine($"Total Expense : {TotalExpense}");
            Console.WriteLine($"Total You Owe : {youOwe}");
            Console.WriteLine($"Total You Get Back : {youGetBack}");

            foreach (KeyValuePair<User, double> entry in Balances)
            {
                double amount = entry.Value;
                if (amount > 0)
                    Console.WriteLine($"You get back {amount} from {entry.Key.Name}");
                else if (amount < 0)
                    Console.WriteLine($"You owe {-amount} to {entry.Key.Name}");
            }
            Console.WriteLine("---------------------------------");
            Console.WriteLine("---------------------------------");
        }
    }
}