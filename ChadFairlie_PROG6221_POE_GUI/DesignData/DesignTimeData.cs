using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
		public ObservableCollection<DetailedRecipeViewModel> FilteredRecipes { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the ViewModel with dummy data.
		public DesignTimeRecipesViewModel()
		{
			var dummyRecipes = Recipe.GetDummyRecipes();
			FilteredRecipes = new ObservableCollection<DetailedRecipeViewModel>();

			for (int i = 0; i < dummyRecipes.Count; i++)
			{
				Recipe recipe = dummyRecipes[i];
				FilteredRecipes.Add(new DetailedRecipeViewModel(recipe, i));
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

		public string CaloriesMessage { get; set; }
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
			UpdateCaloriesMessage();

			// Set the background gradient based on the index
			SetBackgroundGradient(index);

			ResetStepsCommand = new RelayCommand(ResetSteps);
		}

		private void UpdateCaloriesMessage()
		{
			if (TotalCalories > 500)
			{
				CaloriesMessage = " (High Calorie Content! Consider reducing portion size or substituting ingredients.)";
			}
			else if (TotalCalories > 300)
			{
				CaloriesMessage = " (Moderate Calorie Content. Good for a main meal.)";
			}
			else if (TotalCalories > 150)
			{
				CaloriesMessage = " (Low Calorie Content. Good for a light meal or snack.)";
			}
			else
			{
				CaloriesMessage = " (Very Low Calorie Content. Consider adding more nutritious ingredients.)";
			}
		}

		private void SetBackgroundGradient(int index)
		{
			int gradientIndex = (index % 10) + 1; // Cycle through 1 to 10 for gradients.
			BackgroundGradient = $"TertiaryColorGradient{gradientIndex}";
		}

		public ICommand ResetStepsCommand { get; }

		private void ResetSteps()
		{
			foreach (var step in Steps)
			{
				step.IsCompleted = false;
			}
		}
	}

	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// AddRecipeViewModel Design Time Data
	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// This class provides design-time data for the AddRecipeViewModel.
	public class DesignTimeAddRecipeViewModel
	{
		public string RecipeName { get; set; }
		public ObservableCollection<Ingredient> Ingredients { get; set; }
		public ObservableCollection<Step> Steps { get; set; }

		// Constructor initializes the ViewModel with dummy data.
		public DesignTimeAddRecipeViewModel()
		{
			// Set a dummy recipe name
			RecipeName = "Design Time Recipe";

			// Directly create FoodGroup instances for design-time data
			var grainsFoodGroup = new FoodGroup("Grains", "🌾");
			var sugarsFoodGroup = new FoodGroup("Sugars", "🍬");
			var dairyFoodGroup = new FoodGroup("Dairy", "🥛");

			// Initialize the Ingredients collection with some dummy ingredients
			Ingredients = new ObservableCollection<Ingredient>
			{
				new Ingredient("Flour", 2, "cups", 100, grainsFoodGroup),
				new Ingredient("Sugar", 1, "cup", 150, sugarsFoodGroup),
				new Ingredient("Flour", 2, "cups", 100, grainsFoodGroup),
				new Ingredient("Sugar", 1, "cup", 150, sugarsFoodGroup),
				new Ingredient("Flour", 2, "cups", 100, grainsFoodGroup),
				new Ingredient("Sugar", 1, "cup", 150, sugarsFoodGroup),
				new Ingredient("Eggs", 2, "units", 70, dairyFoodGroup)
			};

			// Initialize the Steps collection with some dummy steps
			Steps = new ObservableCollection<Step>
		{
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Mix all dry ingredients."),
			new Step("Add eggs and mix well."),
			new Step("Pour into a baking pan and bake.")
		};
		}
	}
}