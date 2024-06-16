// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
using ChadFairlie_PROG6221_POE_GUI.MVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModel
{
	internal class HomeViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<RecipeViewModel> Recipes { get; set; }

		public HomeViewModel()
		{
			var dummyRecipes = Recipe.GetDummyRecipes();
			Recipes = new ObservableCollection<RecipeViewModel>();
			for (int i = 0; i < dummyRecipes.Count; i++)
			{
				Recipes.Add(new RecipeViewModel(dummyRecipes[i], i));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}