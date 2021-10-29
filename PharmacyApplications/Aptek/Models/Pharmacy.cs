using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek.Models
{
    partial class Pharmacy
    {
        public string Name { get; }

        private List<Drug> _drugList;

        public int Id { get; }

        private static int _counter;

        public Pharmacy()
        {
            _drugList = new List<Drug>();
            _counter++;
            Id = _counter;
        }

        public Pharmacy(string name) : this()
        {
            Name = name;
        }
    }
}
