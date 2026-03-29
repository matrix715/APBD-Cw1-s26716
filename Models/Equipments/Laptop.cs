using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Models.Equipments
{
    public class Laptop(string name, EquipmentStatus status , string processor , string ram) 
        :Equipment( name, status)    
    {
        public string Processor { get; set; } = processor;
        public string Ram { get; set; } = ram;

    }
}
