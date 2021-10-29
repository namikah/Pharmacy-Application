using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek.Models
{
    class Drug
    {
        public string Name { get; }

        public DrugType Type { get; }

        public double Price { get; }

        private int _amount;

        public int Amount
        {
            get
            {
                return _amount;
            }
        }

        private DateTime ExpirationTime;

        public int Id { get; }

        private static int _idCounter;

        public Drug()
        {
            _idCounter++;
            Id = _idCounter;
        }

        public Drug(string name, DrugType type, int count,double price, DateTime exDate) : this()
        {
            Name = name;
            Type = type;
            Price = price;
            _amount += count;
            ExpirationTime = exDate;
        }

        //public void IncrementCount(int count)
        //{
        //    Count += count;
        //}

        public bool DecrementCount(int count)
        {
            if (_amount >= count)
            {
                _amount -= count;
                return true;
            }
                return false;
        }

        public override string ToString()
        {
            return $"[{Id}] - {Name} - {Price}AZN - {_amount} - {ExpirationTime}";
        }
    }
}
