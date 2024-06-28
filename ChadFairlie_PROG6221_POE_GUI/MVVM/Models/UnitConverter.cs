// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      ChatGPT helped make this class.

//------------------------------------------------------------------------------------------------------------------------//

using System;
using System.Collections.Generic;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	// The UnitConverter class provides functionality for converting between different units of measurement.
	public class UnitConverter
	{
		// Conversion table for common cooking measurements
		private static readonly Dictionary<string, (double Factor, string LargerUnit)> UpConversionTable = new Dictionary<string, (double Factor, string LargerUnit)>
		{
			{ "teaspoon", (1.0 / 3.0, "tablespoon") },
			{ "tablespoon", (1.0 / 16.0, "cup") },
			{ "milliliter", (0.001, "liter") },
			{ "gram", (0.001, "kilogram") }
		};

		private static readonly Dictionary<string, (double Factor, string SmallerUnit)> DownConversionTable = new Dictionary<string, (double Factor, string SmallerUnit)>
		{
			{ "tablespoon", (3, "teaspoon") },
			{ "cup", (16, "tablespoon") },
			{ "liter", (1000, "milliliter") },
			{ "kilogram", (1000, "gram") }
		};

		// Method to check if a unit of measurement is convertible.
		public static bool IsConvertible(string unitOfMeasurement)
		{
			return UpConversionTable.ContainsKey(unitOfMeasurement) || DownConversionTable.ContainsKey(unitOfMeasurement);
		}

		// Method to convert a quantity from one unit of measurement to another.
		public static (double, string, double) Convert(double quantity, string unitOfMeasurement, double caloriesPerUnit)
		{
			double originalQuantity = quantity;
			double totalCalories = originalQuantity * caloriesPerUnit;

			// Upconversion
			while (UpConversionTable.ContainsKey(unitOfMeasurement))
			{
				var (factor, largerUnit) = UpConversionTable[unitOfMeasurement];
				if (quantity >= 1 / factor)
				{
					quantity *= factor;
					unitOfMeasurement = largerUnit;
				}
				else
				{
					break;
				}
			}

			// Downconversion
			while (DownConversionTable.ContainsKey(unitOfMeasurement))
			{
				var (factor, smallerUnit) = DownConversionTable[unitOfMeasurement];
				if (quantity < 1)
				{
					quantity *= factor;
					unitOfMeasurement = smallerUnit;
				}
				else
				{
					break;
				}
			}

			// Round the final quantity to 2 decimal places
			quantity = Math.Round(quantity, 2);

			// Calculate the new CaloriesPerUnit value
			double newCaloriesPerUnit = Math.Round(totalCalories / quantity, 2);

			// Return the converted quantity, unit of measurement, and CaloriesPerUnit
			return (quantity, unitOfMeasurement, newCaloriesPerUnit);
		}
	}
}