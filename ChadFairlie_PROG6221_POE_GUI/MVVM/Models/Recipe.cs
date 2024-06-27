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

		// TODO: Remove this later
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Method to get a list of dummy recipes for testing purposes.
		public static List<Recipe> GetDummyRecipes()
		{
			var recipes = new List<Recipe>();
			var random = new Random();
			FoodGroup[] foodGroups = RecipeService.FoodGroups;
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
					LastAccessed = DateTime.Now.AddDays(-1),
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
					FoodGroup foodGroup = foodGroups[random.Next(foodGroups.Length)];

					recipe.Ingredients.Add(new Ingredient($"{ingredientName}", quantity, unit, calories, foodGroup.ToString()));
				}

				int stepsCount = random.Next(6, 16); // Each recipe will have between 3 and 5 steps
				for (int k = 1; k <= stepsCount; k++)
				{
					recipe.Steps.Add(new Step($"Step {k} of Recipe {i}: Perform action {k} on the ingredients."));
				}

				recipes.Add(recipe);
			}
			return recipes;
		}

		public static List<Recipe> GetSpecialDummyRecipes()
		{
			var specialRecipes = new List<Recipe>
			{
				new Recipe
				{
					RecipeName = "Special Recipe 1",
					LastAccessed = DateTime.Now.AddDays(-1).AddDays(-1),
					Ingredients = new List<Ingredient>
					{
						new Ingredient("Flour", 16, "tablespoon", 50, "Grains"), // Should convert to cups
						new Ingredient("Sugar", 8, "tablespoon", 30, "Sweets and Snacks"), // Should convert to cups
					},
					Steps = new List<Step>
					{
						new Step("Step 1: Mix all ingredients."),
						new Step("Step 2: Bake at 350 degrees for 30 minutes.")
					}
				},
				new Recipe
				{
					RecipeName = "Special Recipe 2",
					LastAccessed = DateTime.Now.AddDays(-1),
					Ingredients = new List<Ingredient>
					{
						new Ingredient("Milk", 0.25, "cup", 100, "Dairy"), // Should convert to tablespoons
						new Ingredient("Olive Oil", 1.5, "teaspoon", 120, "Fats and Oils"), // Should stay as teaspoons
					},
					Steps = new List<Step>
					{
						new Step("Step 1: Combine ingredients."),
						new Step("Step 2: Stir until blended.")
					}
				},
				new Recipe
				{
					RecipeName = "Special Recipe 3",
					LastAccessed = DateTime.Now.AddDays(-1),
					Ingredients = new List<Ingredient>
					{
						new Ingredient("Butter", 8, "ounce", 100, "Dairy"), // Should convert to pounds
						new Ingredient("Honey", 300, "milliliter", 50, "Sweets and Snacks"), // Should convert to liters
					},
					Steps = new List<Step>
					{
						new Step("Step 1: Melt butter."),
						new Step("Step 2: Mix in honey."),
						new Step("Step 3: Cool down before serving.")
					}
				},
				new Recipe
				{
					RecipeName = "Special Recipe 4",
					LastAccessed = DateTime.Now.AddDays(-1),
					Ingredients = new List<Ingredient>
					{
						new Ingredient("Salt", 0.02, "cup", 0, "Spices"), // Should convert to teaspoons
						new Ingredient("Water", 1000, "milliliter", 0, "Beverages"), // Should convert to liters
					},
					Steps = new List<Step>
					{
						new Step("Step 1: Dissolve salt in water."),
						new Step("Step 2: Boil for 5 minutes.")
					}
				}
			};

			return specialRecipes;
		}
	}
}