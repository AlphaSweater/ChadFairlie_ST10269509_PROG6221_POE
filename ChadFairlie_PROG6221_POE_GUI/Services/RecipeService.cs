using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChadFairlie_PROG6221_POE_GUI.Services
{
	public class RecipeService : ObservableObject
	{
		private ObservableCollection<Recipe> _recipes;

		// Static list of available food groups
		public static readonly FoodGroup[] FoodGroups = new[]
		{
			new FoodGroup("Protein", "🍗"),
			new FoodGroup("Vegetables", "🥕"),
			new FoodGroup("Fruits", "🍎"),
			new FoodGroup("Grains", "🌾"),
			new FoodGroup("Dairy", "🥛"),
			new FoodGroup("Fats and Oils", "🥑"),
			new FoodGroup("Sweets and Snacks", "🍪"),
			new FoodGroup("Beverages", "☕"),
			new FoodGroup("Spices", "🌶️"),
		};

		public RecipeService()
		{
			_recipes = new ObservableCollection<Recipe>();
		}

		public ObservableCollection<Recipe> GetAllRecipes() => _recipes;

		public void AddRecipe(Recipe recipe)
		{
			_recipes.Add(recipe);
			OnPropertyChanged(nameof(_recipes));
		}

		public void RemoveRecipe(Recipe recipe)
		{
			_recipes.Remove(recipe);
			OnPropertyChanged(nameof(_recipes));
		}
	}
}