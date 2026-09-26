using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.model
{
    public class Split
    {
        public User User { get; }
        public double Amount { get; set; }
        public Split(User user, double amount)
        {
            User = user;
            Amount = amount;
        }
    }
}