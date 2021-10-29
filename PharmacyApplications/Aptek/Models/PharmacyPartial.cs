using Aptek.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek.Models
{
    partial class Pharmacy
    {
        public List<Drug> ShowDrugs()
        {
            if (_drugList == null)
            {
                return new List<Drug>();
            }

            return _drugList;
        }
        
        public void AddDrug(Drug drug)
        {
            _drugList.Add(drug);
        }

        public List<Drug> SearchDrug(string name)
        {
            var Drugs = _drugList.FindAll(x => x.Name.ToLower().Contains(name.Trim().ToLower()));
            return Drugs;
        }

        public bool RemoveDrug(int id)
        {
            var Drugs = _drugList.Find(x => x.Id == id);
            if (Drugs == null)
                return false;

            _drugList.Remove(Drugs);
            return true;
        }

        public int SaleDrug(Drug drug, int drugCount)
        {
            if (drug.Amount == 0) 
                return 0;

            else if (drugCount != 0 && drug.Amount >= drugCount)
            {
                drug.DecrementCount(drugCount);
                return 1;
            }

            return -1;
        }

        public Drug IsExistDrug(string drugName)
        {
            var drug = _drugList.Find(x => x.Name.Contains(drugName.ToLower()));
            return drug;
        }

        public override string ToString()
        {
            const string underline = "\x1B[4m";
            const string reset = "\x1B[0m";
            return $"[{Id}] {underline}{Name.ToUpper()}{reset}";
        }
    }
}
