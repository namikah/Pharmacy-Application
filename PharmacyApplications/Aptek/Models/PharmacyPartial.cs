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
        public List<Drug> DrugsList()
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
            var Drugs = _drugList.FindAll(x => x.Name.ToLower().Contains(name.ToLower()));
            return Drugs;
        }

        public bool RemoveDrug(int id)
        {
            var Drug = _drugList.Find(x => x.Id == id);
            if (Drug == null)
                return false;

            _drugList.Remove(Drug);
            return true;
        }

        public bool IsExistDrug(string drugName)
        {
            var drug = _drugList.Find(x => x.Name.ToLower().Contains(drugName.ToLower()));
            if (drug != null)
            {
                return true;
            }
            return false;
        }

        public bool UpdateQuantity(int id, int quantity)
        {
            foreach (var item in _drugList)
            {
                if(item.Id == id)
                {
                    item.IncrementQuantity(quantity);
                    return true;
                }
            }

            return false;
        }

        public Drug FindDrug(Predicate<Drug> predicate)
        {
            var drug = _drugList.Find(predicate);
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
