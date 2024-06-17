// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI
//		• CoPilot

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	public class RecipesViewModel : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services section contains private fields and services used within the ViewModel.

		private readonly RecipeService _recipeService;

		private ObservableCollection<DetailedRecipeViewModel> _recipes;
		private DetailedRecipeViewModel _selectedRecipe;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor initializes the ViewModel, setting up services and loading all recipes.
		public RecipesViewModel()
		{
			_recipeService = ServiceProviderFactory.GetService<RecipeService>();
			var recipes = _recipeService.GetAllRecipes();
			Recipes = new ObservableCollection<DetailedRecipeViewModel>();

			// Populate the Recipes collection with DetailedRecipeViewModels for each recipe.
			for (int i = 0; i < recipes.Count; i++)
			{
				Recipes.Add(new DetailedRecipeViewModel(recipes[i], i));
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties section exposes data to be bound in the UI.

		//------------------------------------------------------------------------------------------------------------------------//
		// Recipes property holds a collection of DetailedRecipeViewModels for the UI to display.
		public ObservableCollection<DetailedRecipeViewModel> Recipes
		{
			get => _recipes;
			set
			{
				if (_recipes != value)
				{
					_recipes = value;
					OnPropertyChanged(nameof(Recipes)); // Notify UI of property change.
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// SelectedRecipe property holds the currently selected recipe in the UI.
		public DetailedRecipeViewModel SelectedRecipe
		{
			get => _selectedRecipe;
			set
			{
				if (_selectedRecipe != value)
				{
					_selectedRecipe = value;
					OnPropertyChanged(nameof(SelectedRecipe)); // Notify UI of property change.
															   // Optionally, trigger navigation to the details view here if the architecture supports it
				}
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}