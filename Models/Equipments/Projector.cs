using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Models.Equipments
{
    public class Projector(string name, EquipmentStatus status, string resolution, string lumens) 
        : Equipment(name, status)
    {
        public string Resolution { get; set; } = resolution;
        public string Lumens { get; set; } = lumens;
    }
}
