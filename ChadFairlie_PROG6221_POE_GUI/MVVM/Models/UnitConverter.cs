// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      ChatGPT (Helped with the idea and creation of the Data Dictionary)

//------------------------------------------------------------------------------------------------------------------------//
using System;
using System.Collections.Generic;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	// The UnitConverter class provides functionality for converting between different units of measurement.
	public class UnitConverter
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// UpConversionTable is used to convert from a smaller unit to a larger unit (e.g., from teaspoons to tablespoons).
		// Each entry in the table is a tuple containing the conversion factor, the name of the larger unit, and the threshold quantity for conversion.
		private static readonly Dictionary<string, (double, string, double)> UpConversionTable = new Dictionary<string, (double, string, double)>
		{
			{ ("teaspoon"), (0.33333, "tablespoon", 3) },
			{ ("tablespoon"), (0.0625, "cup", 16) },
			{ ("cup"), (16, "gallon", 16) },  // Convert to gallons only when above 16 cups
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
			{ ("gallon"), (16, "cup", 1) },  // Convert gallons back to cups when quantity is below 1 gallon
			{ ("pound"), (16, "ounce", 1) },
			{ ("liter"), (1000, "milliliter", 1) },
			{ ("kilogram"), (1000, "gram", 1) }
		};

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Method to check if a unit of measurement is convertible.
		// It checks if the unit is present in either the UpConversionTable or the DownConversionTable.
		public static bool IsConvertible(string unitOfMeasurement)
		{
			return UpConversionTable.ContainsKey(unitOfMeasurement) || DownConversionTable.ContainsKey(unitOfMeasurement);
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// Method to convert a quantity from one unit of measurement to another.
		public static (double, string, double) Convert(double quantity, string unitOfMeasurement, double caloriesPerUnit)
		{
			double originalQuantity = quantity;
			double totalCalories = originalQuantity * caloriesPerUnit;

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

			// Round the final quantity to 2 decimal places.
			quantity = Math.Round(quantity, 2);

			// Calculate the new CaloriesPerUnit value.
			double newCaloriesPerUnit = Math.Round(totalCalories / quantity, 2);

			// Return the converted quantity, unit of measurement, and CaloriesPerUnit.
			return (quantity, unitOfMeasurement, newCaloriesPerUnit);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}