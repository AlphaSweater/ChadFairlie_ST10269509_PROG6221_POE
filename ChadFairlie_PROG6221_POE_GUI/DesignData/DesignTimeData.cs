using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// //<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
// Design Time Data
//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
// This namespace contains classes that provide design-time data for the ViewModels.
namespace ChadFairlie_PROG6221_POE_GUI.DesignData
{
	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// HomeViewModel Design Time Data
	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// This class provides design-time data for the HomeViewModel.
	public class DesignTimeHomeViewModel
	{
		//------------------------------------------------------------------------------------------------------------------------//
		// RecentRecipes property holds a collection of DetailedRecipeViewModels for the UI to display.
		public ObservableCollection<DetailedRecipeViewModel> RecentRecipes { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the ViewModel with dummy data.
		public DesignTimeHomeViewModel()
		{
			var dummyRecipes = Recipe.GetDummyRecipes();
			var sortedRecipes = dummyRecipes.OrderByDescending(r => r.LastAccessed).Take(4).ToList();
			RecentRecipes = new ObservableCollection<DetailedRecipeViewModel>();

			for (int i = 0; i < sortedRecipes.Count; i++)
			{
				Recipe recipe = sortedRecipes[i];
				RecentRecipes.Add(new DetailedRecipeViewModel(recipe, i));
			}
		}
	}

	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// RecipesViewModel Design Time Data
	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// This class provides design-time data for the RecipesViewModel.
	public class DesignTimeRecipesViewModel
	{
		//------------------------------------------------------------------------------------------------------------------------//
		// Recipes property holds a collection of DetailedRecipeViewModels for the UI to display.
		public ObservableCollection<DetailedRecipeViewModel> Recipes { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the ViewModel with dummy data.
		public DesignTimeRecipesViewModel()
		{
			var dummyRecipes = Recipe.GetDummyRecipes();
			Recipes = new ObservableCollection<DetailedRecipeViewModel>();

			for (int i = 0; i < dummyRecipes.Count; i++)
			{
				Recipe recipe = dummyRecipes[i];
				Recipes.Add(new DetailedRecipeViewModel(recipe, i));
			}
		}
	}

	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// DetailedRecipeViewModel Design Time Data
	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// This class provides design-time data for the RecipesViewModel.
	public class DesignTimeDetailedRecipeViewModel
	{
		private Recipe Recipe { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//
		public string RecipeName { get; set; }

		public ObservableCollection<Ingredient> Ingredients { get; private set; }
		public ObservableCollection<Step> Steps { get; private set; }
		public DateTime LastAccessed { get; set; }
		public double CurrentScale { get; set; }
		public double TotalCalories => Recipe.CalculateTotalCalories();

		public string BackgroundGradient { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the ViewModel with dummy data.
		public DesignTimeDetailedRecipeViewModel()
		{
			var dummyRecipes = Recipe.GetDummyRecipes();
			int index = 0; // Use the first recipe as an example

			Recipe = dummyRecipes[index];

			RecipeName = Recipe.RecipeName;
			Ingredients = new ObservableCollection<Ingredient>(Recipe.Ingredients);
			Steps = new ObservableCollection<Step>(Recipe.Steps);
			LastAccessed = Recipe.LastAccessed;
			CurrentScale = Recipe.CurrentScale;

			// Set the background gradient based on the index
			SetBackgroundGradient(index);
		}

		private void SetBackgroundGradient(int index)
		{
			int gradientIndex = (index % 10) + 1; // Cycle through 1 to 10 for gradients.
			BackgroundGradient = $"TertiaryColorGradient{gradientIndex}";
		}
	}
}