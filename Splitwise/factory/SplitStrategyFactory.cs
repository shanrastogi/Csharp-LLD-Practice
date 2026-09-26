using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.enums;
using Splitwise.strategy;

namespace Splitwise.factory
{
    public static class SplitStrategyFactory
    {
        public static ISplitStrategy Get(SplitType splitType) =>

             splitType switch
             {
                 SplitType.EQUAL => new EqualSplitStrategy(),
                 SplitType.PERCENTAGE => new PercentageSplitStrategy(),
                 _ => throw new ArgumentException("Invalid Split Type!")
             };

    }
}