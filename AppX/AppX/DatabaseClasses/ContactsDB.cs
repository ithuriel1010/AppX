using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppX.DatabaseClasses
{
    public class ContactsDB
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MaxLength(9)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Relationship { get; set; }
    }
}
