using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.model;

namespace Splitwise.strategy
{
    public class PercentageSplitStrategy : ISplitStrategy
    {
        public List<Split> SplitIt(double totalAmount, List<User> participants, Dictionary<User, double> metadata)
        {
            // C# LINQ equivalent of Java stream sum
            double totalPercent = metadata.Values.Sum();

            // Using an epsilon tolerance for floating-point comparisons is best practice in C#
            if (Math.Abs(totalPercent - 100.0) > 1e-6)
            {
                throw new ArgumentException("Total percent should be 100");
            }

            List<Split> splits = new List<Split>();
            foreach (User user in participants)
            {
                // C# equivalent of getOrDefault for dictionaries
                double percentage = metadata.TryGetValue(user, out double val) ? val : 0.0;
                splits.Add(new Split(user, totalAmount * percentage / 100.0));
            }
            return splits;
        }
    }
}