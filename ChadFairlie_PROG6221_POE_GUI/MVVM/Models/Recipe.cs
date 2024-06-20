// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      Bro Code: Provided a helpful video tutorial on Lists in C#. (https://youtu.be/vQzREQUhGSA?si=zi-m4qyNKLMErAu9)

//------------------------------------------------------------------------------------------------------------------------//
// Ignore Spelling: MVVM PROG

using ChadFairlie_PROG6221_POE_GUI.Core;
using System;
using System.Collections.Generic;
using System.Text;

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
				this.Steps.Add(new Step(step));
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
			foreach (var step in Steps)
			{
				recipeDetails.AppendLine($"> {step.Description}");
			}

			// Return the recipe details as a string and the total calories.
			return (recipeDetails.ToString(), totalCalories);
		}

		// TODO: Remove this later
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Method to get a list of dummy recipes for testing purposes.
		public static List<Recipe> GetDummyRecipes()
		{
			var recipes = new List<Recipe>();
			var random = new Random();
			var foodGroups = new[] { "Protein", "Vegetables", "Fruits", "Grains", "Dairy", "Fats and Oils", "Sweets and Snacks", "Beverages" };
			var units = new[] { "cup", "tablespoon", "teaspoon", "gram", "ounce", "piece", "slice" };
			var ingredientNames = new[]
			{
				"Tomato", "Banana", "Chicken", "Rice", "Milk", "Sugar", "Olive Oil", "Egg", "Flour", "Butter",
				"Garlic", "Onion", "Apple", "Beef", "Pasta", "Cheese", "Carrot", "Pepper", "Salt", "Honey"
			};

			for (int i = 1; i <= 20; i++)
			{
				var recipe = new Recipe
				{
					RecipeName = $"Recipe {i}",
					LastAccessed = DateTime.Now.AddDays(i),
					Ingredients = new List<Ingredient>(),
					Steps = new List<Step>()
				};

				int ingredientCount = random.Next(3, 7); // Each recipe will have between 3 and 6 ingredients
				for (int j = 1; j <= ingredientCount; j++)
				{
					string ingredientName = ingredientNames[random.Next(ingredientNames.Length)];
					double quantity = random.Next(1, 11); // Random quantity between 1 and 10
					string unit = units[random.Next(units.Length)];
					double calories = Math.Round(random.NextDouble() * 50 + 10, 2); // Random calories between 10 and 60
					string foodGroup = foodGroups[random.Next(foodGroups.Length)];

					recipe.Ingredients.Add(new Ingredient($"{ingredientName} {j}", quantity, unit, calories, foodGroup));
				}

				int stepsCount = random.Next(3, 6); // Each recipe will have between 3 and 5 steps
				for (int k = 1; k <= stepsCount; k++)
				{
					recipe.Steps.Add(new Step($"Step {k} of Recipe {i}: Perform action {k} on the ingredients."));
				}

				recipes.Add(recipe);
			}
			return recipes;
		}
	}
}