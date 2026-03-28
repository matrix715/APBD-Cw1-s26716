using Cwiczenia2.Models.Equipments;
using Cwiczenia2.Models.Users;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Models
{
    public class Rental( User user, Equipment equipment, DateTime rentDate, DateTime dueDate)
    {
        private static int _countId = 0;

        public int Id { get; } = ++_countId;
        public User User { get; set; } = user;
        public Equipment Equipment { get; set; } = equipment;
        public DateTime Rentdate { get; set; } = rentDate;
        public DateTime DueDate { get; set; } = dueDate;
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get;  set; }




        public void Return(DateTime returnDate)
        {
            ReturnDate = returnDate;
            
        }

        public bool IsOverdue()
        {
            return !IsReturned && DateTime.Now > DueDate;
        }
    }
}
