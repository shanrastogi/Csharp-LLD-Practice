using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.enums;
using Splitwise.factory;
using Splitwise.model;

namespace Splitwise.service
{
    public class ExpenseService
    {
        private readonly BalanceSheetService _balanceSheetService;

        public ExpenseService(BalanceSheetService balanceSheetService)
        {
            _balanceSheetService = balanceSheetService;
        }

        public void AddExpense(Group group, string description, double amount, User paidBy,
            List<User> participants, SplitType splitType, Dictionary<User, double> metadata)
        {
            // Assuming SplitStrategy (or ISplitStrategy) factory returns the strategy instance
            var strategy = SplitStrategyFactory.Get(splitType);
            List<Split> splits = strategy.SplitIt(amount, participants, metadata);

            Expense expense = new Expense(description, amount, paidBy, splits, splitType);
            group.AddExpense(expense);

            _balanceSheetService.UpdateBalances(group, paidBy, splits);
        }
    }
}