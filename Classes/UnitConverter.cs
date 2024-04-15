using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
    internal class UnitConverter
    {
        //------------------------------------------------------------------------------------------------------------------------//
        private static readonly Dictionary<string, (double, string, double)> UpConversionTable = new Dictionary<string, (double, string, double)>
        {
            { ("teaspoon"), (0.33333, "tablespoon", 3) },
            { ("tablespoon"), (0.0625, "cup", 16) },
            { ("fluid ounce"), (0.125, "cup", 8) },
            { ("cup"), (0.5, "pint", 2) },
            { ("pint"), (0.5, "quart", 2) },
            { ("quart"), (0.25, "gallon", 4) },
            { ("ounce"), (0.0625, "pound", 16) },
            { ("milliliter"), (0.001, "liter", 1000) },
            { ("gram"), (0.001, "kilogram", 1000) },
        };

        //------------------------------------------------------------------------------------------------------------------------//
        private static readonly Dictionary<string, (double, string, double)> DownConversionTable = new Dictionary<string, (double, string, double)>
        {
            { ("tablespoon"), (3, "teaspoon", 1) },
            { ("cup"), (16, "tablespoon", 1) },
            { ("pint"), (2, "cup", 1) },
            { ("quart"), (2, "pint", 1) },
            { ("gallon"), (4, "quart", 1) },
            { ("pound"), (16, "ounce", 1) },
            { ("liter"), (1000, "milliliter", 1) },
            { ("kilogram"), (1000, "gram", 1) }
        };

        //------------------------------------------------------------------------------------------------------------------------//
        public static bool IsConvertible(string unitOfMeasurement)
        {
            return UpConversionTable.ContainsKey(unitOfMeasurement) || DownConversionTable.ContainsKey(unitOfMeasurement);
        }

        //------------------------------------------------------------------------------------------------------------------------//
        public static (double, string) Convert(double quantity, string unitOfMeasurement)
        {
            if (UpConversionTable.ContainsKey(unitOfMeasurement) && quantity >= UpConversionTable[unitOfMeasurement].Item3)
            {
                quantity *= UpConversionTable[unitOfMeasurement].Item1;
                unitOfMeasurement = UpConversionTable[unitOfMeasurement].Item2;
            }
            else if (DownConversionTable.ContainsKey(unitOfMeasurement) && quantity < DownConversionTable[unitOfMeasurement].Item3)
            {
                quantity *= DownConversionTable[unitOfMeasurement].Item1;
                unitOfMeasurement = DownConversionTable[unitOfMeasurement].Item2;
            }

            return (quantity, unitOfMeasurement);
        }

        //------------------------------------------------------------------------------------------------------------------------//
    }
}