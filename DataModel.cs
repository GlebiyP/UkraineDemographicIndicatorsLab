using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace UkraineDemographicIndicatorsLab
{
    public class DataModel
    {
        public DataModel() { }

        public DataModel(string year, double urbanPopulation, double ruralPopulation) 
        {
            Year = year;
            UrbanPopulation = urbanPopulation;
            RuralPopulation = ruralPopulation;
            Total = urbanPopulation + ruralPopulation;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Year { get; set; }
        public double UrbanPopulation { get; set; }
        public double RuralPopulation { get; set; }
        public double Total { get; set; }
    }
}