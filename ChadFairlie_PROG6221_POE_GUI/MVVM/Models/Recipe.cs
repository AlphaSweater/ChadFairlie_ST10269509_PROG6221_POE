// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      Bro Code: Provided a helpful video tutorial on Lists in C#. (https://youtu.be/vQzREQUhGSA?si=zi-m4qyNKLMErAu9)

//------------------------------------------------------------------------------------------------------------------------//

using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	public delegate void ExceededCaloriesDelegate(double totalCalories);

	// The Recipe class represents a cooking recipe.
	// It includes properties for the recipe name, current scale, list of ingredients, and list of cooking steps.
	public class Recipe : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Name of the recipe.
		public string RecipeName { get; set; }

		// Current scale of the recipe. This is used when scaling the quantity of ingredients.
		public double CurrentScale { get; set; } = 1.0;

		// List of ingredients required for the recipe.
		public List<Ingredient> Ingredients { get; set; }

		// List of steps to follow to cook the recipe.
		public List<Step> Steps { get; set; }

		// Date and time when the recipe was last accessed.
		public DateTime LastAccessed { get; set; }

		// Delegate event to notify when the calories exceed 300.
		public event ExceededCaloriesDelegate OnCaloriesExceeded;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

		// Constructor for the Recipe class.
		// It initializes the recipe name and creates new lists for ingredients and steps.
		public Recipe()
		{
			this.Ingredients = new List<Ingredient>();
			this.Steps = new List<Step>();
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Constructor for the Recipe class.
		// It initializes the recipe name, ingredients, and steps.
		public Recipe(string recipeName, List<Ingredient> ingredients, List<Step> steps)
		{
			this.RecipeName = recipeName;
			this.Ingredients = ingredients;
			this.Steps = steps;
			LastAccessed = DateTime.Now;
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// This method calculates the total calories of the recipe.
		public double CalculateTotalCalories()
		{
			// Initialize a variable to store the total calories.
			double totalCalories = 0;

			// Iterate over each ingredient in the recipe.
			foreach (var ingredient in this.Ingredients)
			{
				// Add the calories of the ingredient (calories per unit * quantity) to the total calories.
				totalCalories += (ingredient.CaloriesPerUnit * ingredient.Quantity);
			}

			// Return the total calories, rounded to 2 decimal places.
			return Math.Round(totalCalories, 2);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Method to scale the quantity of ingredients.
		public void Scale(double scale)
		{
			CurrentScale = Math.Round(CurrentScale * scale, 2);

			foreach (var ingredient in Ingredients)
			{
				ingredient.PreciseQuantity *= scale;

				if (UnitConverter.IsConvertible(ingredient.UnitOfMeasurement))
				{
					(ingredient.PreciseQuantity, ingredient.UnitOfMeasurement, ingredient.PreciseCaloriesPerUnit) = UnitConverter.Convert(ingredient.PreciseQuantity, ingredient.UnitOfMeasurement, ingredient.PreciseCaloriesPerUnit);
				}

				if (CurrentScale == 1)
				{
					this.ResetScaling();
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// Method to reset the scaling of the recipe.
		// It resets the quantity of each ingredient to its original quantity and measurement and sets the current scale to 1.
		public void ResetScaling()
		{
			foreach (var ingredient in Ingredients)
			{
				ingredient.PreciseQuantity = ingredient.OriginalQuantity;
				ingredient.UnitOfMeasurement = ingredient.OriginalUnitOfMeasurement;
				ingredient.PreciseCaloriesPerUnit = ingredient.OriginalCaloriesPerUnit;
			}
			CurrentScale = 1.0;
		}
	}
}