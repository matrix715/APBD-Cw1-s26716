using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Models.Users
{
    public abstract class User(string firstName , string lastName)
    {
        private static int _counterId = 0;
        public int Id { get; set; } = ++ _counterId;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;


        public abstract string UserTyp { get; }
        public abstract int MaxRentals { get; }

    }
}
