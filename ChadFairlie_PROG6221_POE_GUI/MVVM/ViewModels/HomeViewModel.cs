// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	internal class HomeViewModel : INotifyPropertyChanged
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private readonly RecipeService _recipeService;

		private ObservableCollection<DetailedRecipeViewModel> _recentRecipesViewModels;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

		public HomeViewModel()
		{
			_recipeService = ServiceProviderFactory.GetService<RecipeService>();
			_recentRecipesViewModels = new ObservableCollection<DetailedRecipeViewModel>();
			RefreshRecentlyViewedRecipes();
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public ObservableCollection<DetailedRecipeViewModel> RecentRecipesViewModels
		{
			get => _recentRecipesViewModels;
			set
			{
				if (_recentRecipesViewModels != value)
				{
					_recentRecipesViewModels = value;
					OnPropertyChanged(nameof(RecentRecipesViewModels));
				}
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Methods
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public void RefreshRecentlyViewedRecipes()
		{
			var allRecipes = _recipeService.GetAllRecipes();
			var sortedRecipes = allRecipes.OrderByDescending(r => r.LastAccessed).Take(4).ToList();

			RecentRecipesViewModels.Clear();
			for (int i = 0; i < sortedRecipes.Count; i++)
			{
				RecentRecipesViewModels.Add(new DetailedRecipeViewModel(sortedRecipes[i], i));
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// INotifyPropertyChanged Implementation
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}