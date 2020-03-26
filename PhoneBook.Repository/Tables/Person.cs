using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Repository.Tables
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreateStamp { get; set; }
        public DateTime? ModifStamp { get; set; }
    }
}
