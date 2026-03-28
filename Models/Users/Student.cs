using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Models.Users
{
    public class Student(string firstName, string lastName, string studentNuber) : User( firstName,  lastName)
    {
        public string StudentNum { get; set; } = studentNuber;

        public override string UserTyp => "Student";
        public override int MaxRentals => 2;
    }
    
}
