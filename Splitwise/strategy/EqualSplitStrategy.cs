using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.model;

namespace Splitwise.strategy
{
    public class EqualSplitStrategy : ISplitStrategy
    {
        public List<Split> SplitIt(double totalAmount, List<User> participants, Dictionary<User, double> metadata)
        {
            double share = totalAmount / participants.Count;
            List<Split> splits = new List<Split>();
            foreach (User user in participants)
            {
                splits.Add(new Split(user, share));
            }
            return splits;
        }
    }
}