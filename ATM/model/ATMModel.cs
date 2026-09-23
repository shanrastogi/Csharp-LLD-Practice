using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.enums;

namespace ATM.model
{
    public class ATMModel
    {
        public string Id { get; }
        public ATMStatus Status { get; set; }
        public double CashAvailable { get; set; }
        public int twoThousandCount { get; set; }
        public int fiveHundredCount { get; set; }
        public int oneHundredCount { get; set; }
        public ATMModel(string _id, int _twoThousandCount, int _fiveHundredCount, int _oneHundredCount)
        {
            Id = _id;
            CashAvailable = 2000 * _twoThousandCount + 500 * _fiveHundredCount + 100 * _oneHundredCount;
            Status = ATMStatus.IDLE;
            twoThousandCount = _twoThousandCount;
            fiveHundredCount = _fiveHundredCount;
            oneHundredCount = _oneHundredCount;
        }
    }
}