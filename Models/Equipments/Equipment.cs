using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Models.Equipments
{
    public abstract class Equipment(string name, bool iaAvailable, EquipmentStatus status)
    {
        private static int _idCounter = 0;
        public int Id { get; set; } = ++ _idCounter;
        public string Name { get; set; } = name;
        public bool IaAvailable { get; set; } = iaAvailable;
        public EquipmentStatus Status { get; set; } = status;

       
    }
}
