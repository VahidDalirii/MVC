using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystemApp.Models
{
    public class Customer
    {
        public ObjectId Id { get; set; }
        public List<ObjectId> AccountIds { get; set; }
        public string Type { get; set; } 
        public string SSN { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }

        //public Customer(string type,string ssn, string password, string firstName, string lastName, string email, string mobileNumber)
        //{
        //    Type = type;
        //    SSN = ssn;
        //    Password = password;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    MobileNumber = mobileNumber;
        //}
    }
}
