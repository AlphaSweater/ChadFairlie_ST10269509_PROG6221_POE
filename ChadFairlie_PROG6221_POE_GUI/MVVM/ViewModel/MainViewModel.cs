// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using ChadFairlie_PROG6221_POE_GUI.Core;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModel
{
	internal class MainViewModel : ObservableObject
	{
		public RelayCommand HomeViewCommand { get; set; }
		public RelayCommand DiscoveryViewCommand { get; set; }

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Here we are creating instances of the ViewModel classes.
		public HomeViewModel HomeVM { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//
		public DiscoveryViewModel DiscoveryVM { get; set; }

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Here we are creating a property to hold the current view.
		private object _currentView;

		//------------------------------------------------------------------------------------------------------------------------//
		// Here we are creating a property to get and set the current view.
		public object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Here we are creating a constructor to instantiate the ViewModel classes.
		public MainViewModel()
		{
			//------------------------------------------------------------------------------------------------------------------------//
			// Here we are instantiating the ViewModel classes.
			HomeVM = new HomeViewModel();
			DiscoveryVM = new DiscoveryViewModel();

			//------------------------------------------------------------------------------------------------------------------------//
			// Here we are setting the current view to the HomeVM.
			CurrentView = HomeVM;

			//------------------------------------------------------------------------------------------------------------------------//
			// Here we are creating the RelayCommand objects.
			HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
			DiscoveryViewCommand = new RelayCommand(o => { CurrentView = DiscoveryVM; });
			//------------------------------------------------------------------------------------------------------------------------//
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}