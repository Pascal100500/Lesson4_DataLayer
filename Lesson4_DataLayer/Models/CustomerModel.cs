using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4_DataLayer.Models
{
    internal class CustomerModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime BirthDate { get; set; }

        public CustomerModel(int id, string firstName, string lastName, DateTime birthdate)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthdate;
        }

        public override string ToString()
        {
            return $"{ID,-4} {FirstName,-15} {LastName,-15} {BirthDate.ToShortDateString()}";
        }
    }
}
