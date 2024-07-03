using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChadFairlie_PROG6221_POE_GUI.Services
{
	// Provides functionality to manage recipes, including adding, removing, and retrieving recipes.
	public class RecipeService : ObservableObject
	{
		// Holds the collection of recipes.
		private ObservableCollection<Recipe> _recipes;

		// Static list of available food groups with their corresponding emoji.
		public static readonly FoodGroup[] FoodGroups = new[]
		{
				new FoodGroup("Protein", "🍗"),
				new FoodGroup("Vegetables", "🥕"),
				new FoodGroup("Fruits", "🍎"),
				new FoodGroup("Grains", "🌾"),
				new FoodGroup("Dairy", "🥛"),
				new FoodGroup("Fats and Oils", "🥑"),
				new FoodGroup("Spices", "🌶️"),
				new FoodGroup("Sweets and Snacks", "🍪"),
				new FoodGroup("Beverages", "☕"),
			};

		// List of standard units of measurement used in recipes.
		public static List<string> UnitsOfMeasurement { get; } = new List<string>
			{
				"cups",
				"tablespoons",
				"teaspoons",
				"grams",
				"kilograms",
				"milliliters",
				"liters"
			};

		// Initializes the service with an empty collection of recipes.
		public RecipeService()
		{
			_recipes = new ObservableCollection<Recipe>();
		}

		// Retrieves all recipes in the collection.
		public ObservableCollection<Recipe> GetAllRecipes() => _recipes;

		// Adds a new recipe to the collection and notifies observers of the change.
		public void AddRecipe(Recipe recipe)
		{
			_recipes.Add(recipe);
			OnPropertyChanged(nameof(_recipes));
		}

		// Removes a recipe from the collection and notifies observers of the change.
		public void RemoveRecipe(Recipe recipe)
		{
			_recipes.Remove(recipe);
			OnPropertyChanged(nameof(_recipes));
		}
	}
}