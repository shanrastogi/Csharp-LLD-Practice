using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.enums;

namespace Splitwise.model
{
    public class Expense
    {
        private string Description { get; }
        private double Amount { get; }
        private User PaidBy { get; }
        private List<Split> Splits { get; }
        private SplitType SplitType { get; }

        public Expense(string description, double amount, User paidBy, List<Split> splits, SplitType splitType)
        {
            Description = description;
            Amount = amount;
            PaidBy = paidBy;
            Splits = splits;
            SplitType = splitType;
        }
    }
}