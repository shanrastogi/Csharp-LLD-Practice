using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.model
{
    public class Group
    {
        public string Id { get; }
        public string Name { get; }
        public List<User> Members { get; } = new List<User>();
        public List<Expense> Expenses { get; } = new List<Expense>();
        public Dictionary<User, BalanceSheet> BalanceSheets { get; } = new Dictionary<User, BalanceSheet>();

        public Group(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddMember(User user)
        {
            Members.Add(user);
            // C# equivalent of putIfAbsent
            if (!BalanceSheets.ContainsKey(user))
            {
                BalanceSheets[user] = new BalanceSheet();
            }
        }

        public void AddExpense(Expense expense)
        {
            Expenses.Add(expense);
        }

        public BalanceSheet? GetBalanceSheet(User user)
        {
            BalanceSheets.TryGetValue(user, out var balanceSheet);
            return balanceSheet;
        }
    }
}