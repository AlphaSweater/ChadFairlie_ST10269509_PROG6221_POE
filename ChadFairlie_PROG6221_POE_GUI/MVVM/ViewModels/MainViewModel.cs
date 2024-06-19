// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
using ChadFairlie_PROG6221_POE_GUI.Core;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	// MainViewModel serves as the central ViewModel managing navigation and state across the application.
	internal class MainViewModel : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Commands for navigating between views.
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public RelayCommand<object> HomeViewCommand { get; set; }

		public RelayCommand<object> RecipesViewCommand { get; set; }

		public RelayCommand<DetailedRecipeViewModel> DetailedRecipeViewCommand { get; private set; }

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// ViewModel instances for different parts of the application.
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public HomeViewModel HomeVM { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//
		public RecipesViewModel RecipesVM { get; set; }

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// CurrentView holds the ViewModel of the currently displayed view.
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private object _currentView;

		//------------------------------------------------------------------------------------------------------------------------//
		// Here we are creating a property to get and set the current view.
		public object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged(); // Notify UI of change to update the displayed view.
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor initializes ViewModel instances and sets up commands for navigation.
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public MainViewModel()
		{
			//------------------------------------------------------------------------------------------------------------------------//
			// Instantiate ViewModel classes for different views.
			HomeVM = new HomeViewModel();
			RecipesVM = new RecipesViewModel();

			//------------------------------------------------------------------------------------------------------------------------//
			// Set the initial view to HomeVM.
			CurrentView = HomeVM;

			//------------------------------------------------------------------------------------------------------------------------//
			// Initialize commands for view navigation.
			HomeViewCommand = new RelayCommand<object>(o => CurrentView = HomeVM);

			RecipesViewCommand = new RelayCommand<object>(o => CurrentView = RecipesVM);

			DetailedRecipeViewCommand = new RelayCommand<DetailedRecipeViewModel>(detailedRecipeVM => CurrentView = detailedRecipeVM);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}