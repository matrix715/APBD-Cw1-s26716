using Cwiczenia2.Models;
using Cwiczenia2.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Services
{
    public class ReportService(EquipmentService equipmentService,UserService userService,RentalService rentalService)
    {
        private readonly EquipmentService _equipmentService = equipmentService;
        private readonly UserService _userService = userService;
        private readonly RentalService _rentalService = rentalService;

      
        public string GenerateSystemReport()
        {
            var allEquipment = _equipmentService.GetAllEquipment();
            var allUsers = _userService.GetAllUsers();
            var allRentals = _rentalService.GetAllRentals();

            var availableEquipment = allEquipment.Count(e => e.Status == EquipmentStatus.Available);
            var rentedEquipment = allEquipment.Count(e => e.Status == EquipmentStatus.Rented);
            var unavailableEquipment = allEquipment.Count(e => e.Status == EquipmentStatus.Unavailable);

            var activeRentals = allRentals.Count(r => !r.IsReturned);
            var overdueRentals = allRentals.Count(r => r.IsOverdue());
            var totalPenalties = allRentals.Sum(r => r.PenaltyAmount);


            return
                "===== RENTAL REPORT =====\n" +
                $"Total users: {allUsers.Count}\n" +
                $"Total equipment: {allEquipment.Count}\n" +
                $"Available equipment: {availableEquipment}\n" +
                $"Rented equipment: {rentedEquipment}\n" +
                $"Unavailable equipment: {unavailableEquipment}\n" +
                $"Active rentals: {activeRentals}\n" +
                $"Overdue rentals: {overdueRentals}\n" + 
                $"Total penalties: {totalPenalties:C}\n";
        }

        public List<Rental> GetOverdueRentals()
        {
            return _rentalService
                .GetAllRentals()
                .Where(r => r.IsOverdue())
                .ToList();
        }

        public List<Rental> GetActiveRentalsForUser(int userId)
        {
            return _rentalService
                .GetAllRentals()
                .Where(r => r.User.Id == userId && !r.IsReturned)
                .ToList();
        }
    }
}
