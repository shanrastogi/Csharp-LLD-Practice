using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATM.enums;
using ATM.model;

namespace ATM.repository
{
    public class ATMRepository
    {
        public Dictionary<string, ATMModel> atms = new();

        public void Save(ATMModel aTMModel)
        {
            atms.Add(aTMModel.Id, aTMModel);
        }

        public ATMModel GetById(string id)
        {
            return atms[id];
        }

        public void UpdateAtmStatusById(string id, ATMStatus newStatus)
        {
            atms[id].Status = newStatus;
        }
    }
}