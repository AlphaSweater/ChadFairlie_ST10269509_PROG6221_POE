using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
    internal class UnitConverter
    {
        private static readonly Dictionary<string, (double, string, double)> ConversionTable = new Dictionary<string, (double, string, double)>
    {
        { "teaspoon", (0.333333, "tablespoon", 3) },
        { "tablespoon", (0.0625, "cup", 16) },
        { "fluid ounce", (0.125, "cup", 8) },
        { "cup", (0.5, "pint", 2) },
        { "pint", (0.5, "quart", 2) },
        { "quart", (0.25, "gallon", 4) },
        { "ounce", (0.0625, "pound", 16) },
        { "pound", (0.5, "kilogram", 2) },
        { "milliliter", (0.001, "liter", 1000) },
        { "liter", (1.05669, "quart", 1) },
        { "gram", (0.001, "kilogram", 1000) },
        { "kilogram", (2.20462, "pound", 1) },
        { "pinch", (0.0625, "teaspoon", 1) },
        { "dash", (0.125, "teaspoon", 1) },
        { "stick", (0.5, "cup", 2) }
    };

        public static bool IsConvertible(string unitOfMeasurement)
        {
            return ConversionTable.ContainsKey(unitOfMeasurement);
        }

        public static (double, string) Convert(double quantity, string unitOfMeasurement)
        {
            if (IsConvertible(unitOfMeasurement) && quantity >= ConversionTable[unitOfMeasurement].Item3)
            {
                quantity *= ConversionTable[unitOfMeasurement].Item1;
                unitOfMeasurement = ConversionTable[unitOfMeasurement].Item2;
            }

            return (quantity, unitOfMeasurement);
        }
    }
}