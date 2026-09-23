using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.cor
{
    public static class CashDispenserChainBuilder
    {
        public static CashDispenser BuildChain()
        {
            CashDispenser d1 = new TwoThousandDispenser();
            CashDispenser d2 = new FiveHundredDispenser();
            CashDispenser d3 = new OneHundredDispenser();

            d1.SetNextDispenser(d2);
            d2.SetNextDispenser(d3);
            return d1;

        }
    }
}