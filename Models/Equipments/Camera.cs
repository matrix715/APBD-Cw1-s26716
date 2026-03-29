using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Models.Equipments
{
    public class Camera(string name, EquipmentStatus status, int megapixels, string type)
        : Equipment(name, status)
    {
        public int Megapixels { get; set; } = megapixels;
        public string Type { get; set; } = type;


    }
}
