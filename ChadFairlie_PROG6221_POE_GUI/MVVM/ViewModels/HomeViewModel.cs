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
using System.Linq;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	internal class HomeViewModel : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services section contains private fields and services used within the ViewModel.

		private readonly RecipeService _recipeService;

		private ObservableCollection<DetailedRecipeViewModel> _recentRecipes;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor initializes the ViewModel, setting up services and refreshing the list of recently viewed recipes.
		public HomeViewModel()
		{
			_recipeService = ServiceProviderFactory.GetService<RecipeService>();
			_recentRecipes = new ObservableCollection<DetailedRecipeViewModel>();
			RefreshRecentlyViewedRecipes();
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties section exposes data to be bound in the UI.

		//------------------------------------------------------------------------------------------------------------------------//
		// RecentRecipes property holds a collection of DetailedRecipeViewModels for the UI to display.
		public ObservableCollection<DetailedRecipeViewModel> RecentRecipes
		{
			get => _recentRecipes;
			set
			{
				if (_recentRecipes != value)
				{
					_recentRecipes = value;
					OnPropertyChanged(nameof(RecentRecipes)); // Notify UI of property change.
				}
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Private Methods
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Private Methods section contains helper methods used internally by the ViewModel.

		//------------------------------------------------------------------------------------------------------------------------//
		// RefreshRecentlyViewedRecipes updates the RecentRecipes collection with the most recently accessed recipes.
		private void RefreshRecentlyViewedRecipes()
		{
			var allRecipes = _recipeService.GetAllRecipes();
			var sortedRecipes = allRecipes.OrderByDescending(r => r.LastAccessed).Take(4).ToList();

			RecentRecipes.Clear();
			for (int i = 0; i < sortedRecipes.Count; i++)
			{
				RecentRecipes.Add(new DetailedRecipeViewModel(sortedRecipes[i], i));
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}