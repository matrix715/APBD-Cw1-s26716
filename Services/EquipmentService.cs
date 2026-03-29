using Cwiczenia2.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Services
{
    public class EquipmentService
    {
        private readonly List<Equipment> _equipment = new();

        public void AddEquipment(Equipment item)
        {
            _equipment.Add(item);
        }

        public List<Equipment> GetAllEquipment()
        {
            return _equipment;
        }

        public List<Equipment> GetAvailableEquipment()
        {
            return _equipment.Where(e => e.Status == EquipmentStatus.Available).ToList();
        }

        public Equipment? GetById(int id)
        {
            return _equipment.FirstOrDefault(e => e.Id == id);
        }
        public void MarkAsUnavailable(int equipmentId)
    }
}
