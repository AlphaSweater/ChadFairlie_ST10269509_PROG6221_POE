using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
    // The Ingredient class represents a single ingredient used in a recipe.
    // It includes properties for the name, quantity, original quantity, and unit of measurement of the ingredient.
    internal class Ingredient
    {
        //------------------------------------------------------------------------------------------------------------------------//
        // Name of the ingredient.
        public string Name { get; set; }

        // Current quantity of the ingredient.
        public double Quantity { get; set; }

        // Original quantity of the ingredient.
        public double OriginalQuantity { get; set; }

        // Unit of measurement for the ingredient (e.g., "cup", "tablespoon", etc.).
        public string UnitOfMeasurement { get; set; }

        //------------------------------------------------------------------------------------------------------------------------//
        // Constructor for the Ingredient class.
        // It initializes the name, quantity, and unit of measurement of the ingredient.
        // If the unit of measurement is convertible (e.g., "cups" to "tablespoons"), it converts the quantity and unit of measurement.
        public Ingredient(string name, double quantity, string unit)
        {
            // Remove the 's' from the string to make the name singular
            Name = name.TrimEnd('s');
            Quantity = quantity;
            OriginalQuantity = quantity;
            // Convert to lowercase and make the unit singular for the data dictionary in UnitConverter.
            UnitOfMeasurement = unit.ToLower().TrimEnd('s');

            // Check if the unit of measurement is convertible, and if so, convert the quantity and unit of measurement.
            if (UnitConverter.IsConvertible(UnitOfMeasurement))
            {
                (Quantity, UnitOfMeasurement) = UnitConverter.Convert(Quantity, UnitOfMeasurement);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
    }
}