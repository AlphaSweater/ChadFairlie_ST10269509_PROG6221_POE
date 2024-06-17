// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI
//		• CoPilot

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using ChadFairlie_PROG6221_POE_GUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	public class RecipesViewModel : INotifyPropertyChanged
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private readonly RecipeService _recipeService;

		private ObservableCollection<DetailedRecipeViewModel> _recipesViewModels;
		private DetailedRecipeViewModel _selectedDetailedRecipeViewModel;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public RecipesViewModel()
		{
			_recipeService = ServiceProviderFactory.GetService<RecipeService>();
			var recipes = _recipeService.GetAllRecipes();
			RecipesViewModels = new ObservableCollection<DetailedRecipeViewModel>();

			for (int i = 0; i < recipes.Count; i++)
			{
				RecipesViewModels.Add(new DetailedRecipeViewModel(recipes[i], i));
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public ObservableCollection<DetailedRecipeViewModel> RecipesViewModels
		{
			get => _recipesViewModels;
			set
			{
				if (_recipesViewModels != value)
				{
					_recipesViewModels = value;
					OnPropertyChanged(nameof(RecipesViewModels));
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		public DetailedRecipeViewModel SelectedDetailedRecipeViewModel
		{
			get => _selectedDetailedRecipeViewModel;
			set
			{
				if (_selectedDetailedRecipeViewModel != value)
				{
					_selectedDetailedRecipeViewModel = value;
					OnPropertyChanged(nameof(SelectedDetailedRecipeViewModel));
					// Optionally, trigger navigation to the details view here if the architecture supports it
				}
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