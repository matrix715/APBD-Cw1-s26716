using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Models.Users
{
    public class Teacher(string firstName, string lastName, string dep) : User(firstName, lastName)
    {
        public string Department { get; set; } = dep;

        public override string UserTyp => "Teacher";

        public override int MaxRentals => 5 ;
    }

}
