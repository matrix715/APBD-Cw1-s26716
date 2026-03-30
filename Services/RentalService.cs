using Cwiczenia2.Models;
using Cwiczenia2.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Services
{
    public class RentalService(UserService userService, EquipmentService equipmentService)
    {
        private readonly List<Rental> _rentals = new();
        private readonly UserService _userService = userService;
        private readonly EquipmentService _equipmentService = equipmentService;

        public void RentEquipment(int userId, int equipmentId, int days)
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var equipment = _equipmentService.GetById(equipmentId);
            if (equipment == null)
            {
                throw new Exception("Equipment not found.");
            }

            if (equipment.Status != EquipmentStatus.Available)
            {
                throw new Exception("Equipment is not available for rent.");
            }

            var activeUserRentals = _rentals.Count(r => r.User.Id == userId && !r.IsReturned);
            if (activeUserRentals >= user.MaxRentals)
            {
                throw new Exception("User has reached the rental limit.");
            }

            var rentDate = DateTime.Now;
            var dueDate = rentDate.AddDays(days);

            var rental = new Rental(user, equipment, rentDate, dueDate);
            _rentals.Add(rental);

            equipment.Status = EquipmentStatus.Rented;
            
        }

        public void ReturnEquipment(int rentalId)
        {
            var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental == null)
            {
                throw new Exception("Rental not found.");
            }

            if (rental.IsReturned)
            {
                throw new Exception("This equipment has already been returned.");
            }

            rental.Return(DateTime.Now);
           

            rental.Equipment.Status = EquipmentStatus.Available;
            
        }

        public List<Rental> GetAllRentals()
        {
            return _rentals;
        }
    }
}