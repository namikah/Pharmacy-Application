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

        private int _quantity;

        public DateTime ExpirationTime { get; }

        public int Id { get; }

        private static int _idCounter;

        public int Quantity
        {
            get
            {
                return _quantity;
            }
        }

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
            _quantity += count;
            ExpirationTime = exDate;
        }

        public void IncrementQuantity(int quantity)
        {
            _quantity += quantity;
        }

        public bool DecrementQuantity(int quantity)
        {
            if (_quantity >= quantity)
            {
                _quantity -= quantity;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"[{Id}] - {Name} - {Price}AZN - {_quantity}pieces - {ExpirationTime.ToShortDateString()}";
        }
    }
}
