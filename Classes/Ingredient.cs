﻿// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
	// The Ingredient class represents a single ingredient used in a recipe.
	// It includes properties for the name, quantity, original quantity, and unit of measurement of the ingredient.
	internal class Ingredient
	{
		// Name of the ingredient.
		public string Name { get; set; }

		// Current quantity of the ingredient.
		public double Quantity { get; set; }

		// Original quantity of the ingredient.
		public double OriginalQuantity { get; set; }

		// Unit of measurement for the ingredient (e.g., "cup", "tablespoon", etc.).
		public string UnitOfMeasurement { get; set; }

		// Add a property to store the original unit of measurement
		public string OriginalUnitOfMeasurement { get; set; }

		public double Calories { get; set; }

		public double OriginalCalories { get; set; }

		public string FoodGroup { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//

		// Constructor for the Ingredient class.
		// It initializes the name, quantity, and unit of measurement of the ingredient.
		// If the unit of measurement is convertible (e.g., "cups" to "tablespoons"), it converts the quantity and unit of measurement.
		public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
		{
			// Remove the 's' from the string to make the name singular.
			Name = name.TrimEnd('s');
			Quantity = quantity;

			// Convert to lowercase and make the unit singular for the data dictionary in UnitConverter.
			UnitOfMeasurement = unit.ToLower().TrimEnd('s');

			// Check if the unit of measurement is convertible, and if so, convert the quantity and unit of measurement.
			if (UnitConverter.IsConvertible(UnitOfMeasurement))
			{
				(Quantity, UnitOfMeasurement) = UnitConverter.Convert(Quantity, UnitOfMeasurement);
			}

			Calories = calories;

			FoodGroup = foodGroup;

			// Store the original unit of measurement, quantity and calories
			OriginalUnitOfMeasurement = UnitOfMeasurement;
			OriginalQuantity = Quantity;
			OriginalCalories = calories;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		public override string ToString()
		{
			// Check if the unit of measurement is not empty.
			// If not, add the unit of measurement and "of" to the ingredient string, and make the unit plural if the quantity is more than 1.
			// If the unit of measurement is empty, check if the quantity is more than 1, and if so, make the ingredient name plural.
			string unit = !string.IsNullOrEmpty(UnitOfMeasurement) ? $"{UnitOfMeasurement}{(Quantity > 1 ? "s" : "")} of " : "";
			string plural = string.IsNullOrEmpty(UnitOfMeasurement) && Quantity > 1 ? "s" : "";
			return $"> {Quantity} {unit}{Name}{plural} ({Calories} calories, {FoodGroup})";
		}
	}
}