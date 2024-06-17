// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI
//		• CoPilot

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	public class DetailedRecipeViewModel : INotifyPropertyChanged
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private Recipe _recipe;
		private string _backgroundGradient;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public DetailedRecipeViewModel(Recipe recipe, int index)
		{
			_recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
			Ingredients = new ObservableCollection<Ingredient>(_recipe.Ingredients);
			Steps = new ObservableCollection<string>(_recipe.Steps);
			// Subscribe to the CollectionChanged event of Ingredients
			Ingredients.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalCalories));
			// Set the background gradient based on the index
			SetBackgroundGradient(index);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public string RecipeName
		{
			get => _recipe.RecipeName;
			set
			{
				if (_recipe.RecipeName != value)
				{
					_recipe.RecipeName = value;
					OnPropertyChanged(nameof(RecipeName));
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// ObservableCollection for Ingredients to automatically update the UI on changes
		public ObservableCollection<Ingredient> Ingredients { get; private set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// ObservableCollection for Steps to automatically update the UI on changes
		public ObservableCollection<string> Steps { get; private set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// Property for displaying total calories, recalculated when ingredients change
		public double TotalCalories => _recipe.CalculateTotalCalories();

		//------------------------------------------------------------------------------------------------------------------------//
		public string BackgroundGradient
		{
			get => _backgroundGradient;
			private set
			{
				if (_backgroundGradient != value)
				{
					_backgroundGradient = value;
					OnPropertyChanged(nameof(BackgroundGradient));
				}
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Private Methods
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private void SetBackgroundGradient(int index)
		{
			int gradientIndex = (index % 10) + 1; // Cycle through 1 to 10
			BackgroundGradient = $"TertiaryColorGradient{gradientIndex}";
			OnPropertyChanged(nameof(BackgroundGradient));
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// INotifyPropertyChanged Implementation
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}