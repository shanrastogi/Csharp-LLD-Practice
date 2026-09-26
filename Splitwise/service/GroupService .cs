using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.enums;
using Splitwise.model;
using Splitwise.repository;

namespace Splitwise.service
{
    public class GroupService
    {
        private readonly IGroupRepository _repo;
        private readonly ExpenseService _expenseService;
        private readonly DebtSimplificationService _simplifier;

        public GroupService(IGroupRepository repo, ExpenseService expenseService, DebtSimplificationService simplifier)
        {
            _repo = repo;
            _expenseService = expenseService;
            _simplifier = simplifier;
        }

        public string CreateGroup(string name, List<User> members)
        {
            string id = Guid.NewGuid().ToString();
            Group g = new Group(id, name);
            foreach (var member in members)
            {
                g.AddMember(member);
            }

            _repo.Save(g);
            return id;
        }

        public void AddMember(string groupId, User user)
        {
            Get(groupId).AddMember(user);
        }

        public void AddExpense(string groupId, string description, double amount,
            User paidBy, List<User> participants, SplitType splitType, Dictionary<User, double> meta)
        {
            _expenseService.AddExpense(Get(groupId), description, amount, paidBy, participants, splitType, meta);
        }

        public void SimplifyDebts(string groupId)
        {
            _simplifier.SimplifyDebts(Get(groupId));
        }

        public void PrintBalances(string groupId)
        {
            Group g = Get(groupId);
            foreach (var u in g.Members)
            {
                var sheet = g.GetBalanceSheet(u);

                double owe = 0, get = 0;
                foreach (double v in sheet.Balances.Values)
                {
                    if (v < 0) owe += -v; else get += v;
                }

                Console.WriteLine($"""
                    💵 {u.Name}
                    Paid: {sheet.TotalPaid:F2}  Expense: {sheet.TotalExpense:F2}
                    You owe: {owe:F2}, You get: {get:F2}
                    """);

                foreach (var entry in sheet.Balances)
                {
                    var other = entry.Key;
                    double val = entry.Value;
                    string direction = val > 0 ? "← get" : "→ owe";
                    Console.WriteLine($"  {direction} {Math.Abs(val):F2} {other.Name}");
                }
                Console.WriteLine("--------------------------");
            }
        }

        private Group Get(string id)
        {
            return _repo.FindById(id) ?? throw new ArgumentException($"Group not found: {id}");
        }
    }
}