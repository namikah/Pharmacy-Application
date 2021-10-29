using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek.Models
{
    class DrugType
    {
        public string TypeName { get; }

        public Guid Id { get; }

        public DrugType(string typeName)
        {
            Id = new Guid();
            TypeName = typeName;
        }

        public override string ToString()
        {
            return $"[{TypeName}]";
        }
    }
}
