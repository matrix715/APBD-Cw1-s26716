using Cwiczenia2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Services
{
    public class RentalService(UserService userService,EquipmentService equipmentService)
    {
        private readonly List<Rental> _rentals = new();
        private readonly UserService _userService = userService;
        private readonly EquipmentService _equipmentService = equipmentService;
       




        public void RentEquipment(int userId, int equipmentId, int days)
        {
            
        }

        public void ReturnEquipment(int rentalId)
        {
            
        }

        public List<Rental> GetAllRentals()
        {
            return _rentals;
        }
    }
}
