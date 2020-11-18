using Android.Util;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppX.Classes
{
    public class PersonsDB
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
        public DateTime BirthDate { get; set; }
        public string Relationship { get; set; }
        public string Photo { get; set; }
        public int Age { get; set; }
    }
}
