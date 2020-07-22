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
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        [MaxLength(9)]
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Zwiazek { get; set; }
    }
}
