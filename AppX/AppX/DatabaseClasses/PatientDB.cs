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
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        [MaxLength(9)]
        public string Telefon { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string Hobby { get; set; }
        public string Zdjecie { get; set; }
        public int Wiek { get; set; }
        public int LocalizationMinutes { get; set; }
        public int FallSeconds { get; set; }
    }
}
