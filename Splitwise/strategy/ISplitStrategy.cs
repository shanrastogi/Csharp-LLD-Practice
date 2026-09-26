using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Splitwise.model;

namespace Splitwise.strategy
{
    public interface ISplitStrategy
    {
        List<Split> SplitIt(double totalAmount, List<User> participants, Dictionary<User, double> metadata);
    }
}