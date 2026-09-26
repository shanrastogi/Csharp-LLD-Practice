using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.model;

namespace Splitwise.service
{
    public class DebtSimplificationService
    {
        public void SimplifyDebts(Group group)
        {
            List<User> users = new List<User>(group.Members);
            Dictionary<User, BalanceSheet> sheets = group.BalanceSheets;

            // Step 1: Calculate net balances for each user
            Dictionary<User, double> netBalances = new Dictionary<User, double>();
            foreach (User user in users)
            {
                double net = 0.0;
                Dictionary<User, double> balances = sheets[user].Balances;
                foreach (double amount in balances.Values)
                {
                    net += amount;
                }
                netBalances[user] = net;
                sheets[user].ClearBalances(); // Clear old balances before recomputing
            }

            // Step 2: Separate creditors and debtors
            // Creditors: Descending order (largest positive net balance first) using a custom comparer
            var creditorComparer = Comparer<double>.Create((x, y) => y.CompareTo(x));
            PriorityQueue<User, double> creditors = new PriorityQueue<User, double>(creditorComparer);

            // Debtors: Ascending order (most negative net balance first) using default min-heap
            PriorityQueue<User, double> debtors = new PriorityQueue<User, double>();

            foreach (User user in users)
            {
                double net = netBalances[user];
                if (net > 1e-6)
                {
                    creditors.Enqueue(user, net);
                }
                else if (net < -1e-6)
                {
                    debtors.Enqueue(user, net);
                }
            }

            // Step 3: Match debtors and creditors to settle debts
            while (creditors.Count > 0 && debtors.Count > 0)
            {
                User creditor = creditors.Dequeue();
                User debtor = debtors.Dequeue();

                double creditAmount = netBalances[creditor];
                double debitAmount = netBalances[debtor];

                double settledAmount = Math.Min(creditAmount, -debitAmount);

                // Update balances both sides
                sheets[creditor].AddBalance(debtor, settledAmount);
                sheets[debtor].AddBalance(creditor, -settledAmount);

                // Update net balances after settlement
                netBalances[creditor] = creditAmount - settledAmount;
                netBalances[debtor] = debitAmount + settledAmount;

                // If still unsettled, re-add to queues
                if (netBalances[creditor] > 1e-6)
                {
                    creditors.Enqueue(creditor, netBalances[creditor]);
                }
                if (netBalances[debtor] < -1e-6)
                {
                    debtors.Enqueue(debtor, netBalances[debtor]);
                }
            }
        }
    }
}