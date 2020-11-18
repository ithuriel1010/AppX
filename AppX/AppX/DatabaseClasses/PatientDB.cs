using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppX.DatabaseClasses
{
    class PatientDB
    {

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public bool HaveData {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MaxLength(9)]
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Hobby { get; set; }
        public string Photo { get; set; }
        public int Age { get; set; }
        public int LocalizationMinutes { get; set; }
        public int FallSeconds { get; set; }
    }
}
