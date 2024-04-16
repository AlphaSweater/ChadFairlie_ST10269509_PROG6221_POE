using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
    // The UnitConverter class provides functionality for converting between different units of measurement.
    internal class UnitConverter
    {
        // UpConversionTable is used to convert from a smaller unit to a larger unit (e.g., from teaspoons to tablespoons).
        // Each entry in the table is a tuple containing the conversion factor, the name of the larger unit, and the threshold quantity for conversion.
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

        // DownConversionTable is used to convert from a larger unit to a smaller unit (e.g., from tablespoons to teaspoons).
        // Each entry in the table is a tuple containing the conversion factor, the name of the smaller unit, and the threshold quantity for conversion.
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

        // Method to check if a unit of measurement is convertible.
        // It checks if the unit is present in either the UpConversionTable or the DownConversionTable.
        public static bool IsConvertible(string unitOfMeasurement)
        {
            return UpConversionTable.ContainsKey(unitOfMeasurement) || DownConversionTable.ContainsKey(unitOfMeasurement);
        }

        //------------------------------------------------------------------------------------------------------------------------//

        // Method to convert a quantity from one unit of measurement to another.
        public static (double, string) Convert(double quantity, string unitOfMeasurement)
        {
            // Check if the unit is in the UpConversionTable and the quantity is above the threshold for conversion.
            if (UpConversionTable.ContainsKey(unitOfMeasurement) && quantity >= UpConversionTable[unitOfMeasurement].Item3)
            {
                // If it is, convert the quantity to the larger unit by multiplying it by the conversion factor.
                quantity *= UpConversionTable[unitOfMeasurement].Item1;

                // Update the unit of measurement to the larger unit.
                unitOfMeasurement = UpConversionTable[unitOfMeasurement].Item2;
            }
            // Check if the unit is in the DownConversionTable and the quantity is below the threshold for conversion.
            else if (DownConversionTable.ContainsKey(unitOfMeasurement) && quantity < DownConversionTable[unitOfMeasurement].Item3)
            {
                // If it is, convert the quantity to the smaller unit by multiplying it by the conversion factor.
                quantity *= DownConversionTable[unitOfMeasurement].Item1;

                // Update the unit of measurement to the smaller unit.
                unitOfMeasurement = DownConversionTable[unitOfMeasurement].Item2;
            }

            // Return the converted quantity and unit of measurement.
            return (quantity, unitOfMeasurement);
        }

        //------------------------------------------------------------------------------------------------------------------------//
    }
}