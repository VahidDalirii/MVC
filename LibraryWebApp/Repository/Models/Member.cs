using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Models
{
    public class Member
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TelNumber { get; set; }
        public string Password { get; set; }

        public Member(string name, string address, string telNumber, string password)
        {
            Name = name;
            Address = address;
            TelNumber = telNumber;
            Password = password;
        }
    }

    public class SortMemberByName : IComparer<Member>
    {
        public int Compare(Member x, Member y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
