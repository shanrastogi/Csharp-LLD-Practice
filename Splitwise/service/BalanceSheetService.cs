using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.model;

namespace Splitwise.service
{
    public class BalanceSheetService
    {
        public void UpdateBalances(Group group, User paidBy, List<Split> splits)
        {
            // C# equivalent of Java streams sum
            double totalAmount = splits.Sum(s => s.Amount);
            group.GetBalanceSheet(paidBy)?.AddTotalPaid(totalAmount);

            foreach (var split in splits)
            {
                User user = split.User;
                double amt = split.Amount;

                group.GetBalanceSheet(user)?.AddTotalExpense(amt);

                if (!user.Equals(paidBy))
                {
                    group.GetBalanceSheet(user)?.AddBalance(paidBy, -amt);
                    group.GetBalanceSheet(paidBy)?.AddBalance(user, amt);
                }
            }
        }
    }
}