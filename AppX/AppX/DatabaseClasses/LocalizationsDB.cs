using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppX.DatabaseClasses
{
    public class LocalizationsDB
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        //public string Zwiazek { get; set; }
    }
}
