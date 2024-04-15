using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
    internal class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double OriginalQuantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string OriginalUnitOfMeasurement { get; set; }


        public Ingredient(string name, double quantity, string unit)
        {
            Name = name;
            Quantity = quantity;
            OriginalQuantity = quantity;
            UnitOfMeasurement = unit;
            OriginalUnitOfMeasurement = unit;
        }
    }
}