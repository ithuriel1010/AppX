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
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        [MaxLength(9)]
        public string Telefon 
        { 
            get; 
            set; 
        }
        public DateTime DataUrodzenia { get; set; }
        public string Zwiazek { get; set; }
        public string Zdjecie { get; set; }
        public int Wiek { get; set; }
    }
}
