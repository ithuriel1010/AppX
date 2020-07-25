using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppX.DatabaseClasses
{
    class LocalizationsDB
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        //public string Zwiazek { get; set; }
    }
}
