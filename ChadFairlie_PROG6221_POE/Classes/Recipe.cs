// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      Bro Code: Provided a helpful video tutorial on Lists in C#. (https://youtu.be/vQzREQUhGSA?si=zi-m4qyNKLMErAu9)

//------------------------------------------------------------------------------------------------------------------------//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
	public delegate void ExceededCaloriesDelegate(double totalCalories);

	// The Recipe class represents a cooking recipe.
	// It includes properties for the recipe name, current scale, list of ingredients, and list of cooking steps.
	public class Recipe
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Name of the recipe.
		public string RecipeName { get; set; }

		// Current scale of the recipe. This is used when scaling the quantity of ingredients.
		public double CurrentScale { get; private set; } = 1.0;

		// List of ingredients required for the recipe.
		public List<Ingredient> Ingredients { get; set; }

		// List of steps to follow to cook the recipe.
		public List<string> Steps { get; set; }

		// Delegate event to notify when the calories exceed 300.
		public event ExceededCaloriesDelegate OnCaloriesExceeded;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

		// Constructor for the Recipe class.
		// It initializes the recipe name and creates new lists for ingredients and steps.
		public Recipe()
		{
			this.Ingredients = new List<Ingredient>();
			this.Steps = new List<string>();
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Method to add a list of ingredients to the recipe.
		public Recipe AddIngredient(Ingredient ingredient)
		{
			this.Ingredients.Add(ingredient);

			double totalCalories = this.CalculateTotalCalories();
			if (totalCalories > 300)
			{
				OnCaloriesExceeded?.Invoke(totalCalories);
			}

			return this;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Method to add a list of steps to the recipe.
		public Recipe AddSteps(List<string> steps)
		{
			foreach (var step in steps)
			{
				this.Steps.Add(step);
			}

			return this;
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
			// Multiply the current scale by the given scale.
			CurrentScale = Math.Round(CurrentScale * scale, 2);

			// Iterate over each ingredient in the recipe.
			foreach (var ingredient in Ingredients)
			{
				// Scale the quantity of the ingredient.
				ingredient.Quantity *= scale;

				// Check if the unit of measurement of the ingredient is convertible.
				if (UnitConverter.IsConvertible(ingredient.UnitOfMeasurement))
				{
					// If it is, convert the quantity and unit of measurement of the ingredient.
					(ingredient.Quantity, ingredient.UnitOfMeasurement, ingredient.CaloriesPerUnit) = UnitConverter.Convert(ingredient.Quantity, ingredient.UnitOfMeasurement, ingredient.CaloriesPerUnit);
				}

				// If the current scale is 1, reset the quantity of the ingredient to its original quantity and measurement.
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
				ingredient.Quantity = ingredient.OriginalQuantity;
				ingredient.UnitOfMeasurement = ingredient.OriginalUnitOfMeasurement; // Reset the unit of measurement
				ingredient.CaloriesPerUnit = ingredient.OriginalCaloriesPerUnit;
			}
			CurrentScale = 1.0;
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Method to print the details of the recipe.
		// It includes the recipe name, current scale, list of ingredients, and list of steps.
		public (string, double) PrintRecipe()
		{
			// Create a StringBuilder to efficiently concatenate the recipe details into a single string.
			StringBuilder recipeDetails = new StringBuilder();

			// Add the recipe name and current scale to the string.
			recipeDetails.AppendLine($"Recipe Name: {RecipeName}");
			recipeDetails.AppendLine($"Current Scale: {CurrentScale}x");

			// Start the list of ingredients.
			recipeDetails.AppendLine("Ingredients:");

			// Add each ingredient to the string.
			foreach (Ingredient ingredient in Ingredients)
			{
				recipeDetails.AppendLine(ingredient.ToString());
			}

			// Calculate the total calories of the recipe.
			double totalCalories = CalculateTotalCalories();

			// Start the list of cooking steps.
			recipeDetails.AppendLine("Steps:");

			// Add each cooking step to the string.
			foreach (string step in Steps)
			{
				recipeDetails.AppendLine($"> {step}");
			}

			// Return the recipe details as a string and the total calories.
			return (recipeDetails.ToString(), totalCalories);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}