// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI
//		• CoPilot

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using ChadFairlie_PROG6221_POE_GUI.Core;
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
	public class DetailedRecipeViewModel : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private Recipe _recipe;

		private string _backgroundGradient;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor initializes the ViewModel with a Recipe object and an index for setting the background gradient.
		public DetailedRecipeViewModel(Recipe recipe, int index)
		{
			_recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");
			Ingredients = new ObservableCollection<Ingredient>(_recipe.Ingredients);
			Steps = new ObservableCollection<string>(_recipe.Steps);
			LastAccessed = recipe.LastAccessed;
			// Subscribe to the CollectionChanged event of Ingredients
			Ingredients.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalCalories));
			// Set the background gradient based on the index
			SetBackgroundGradient(index);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties section exposes data to be bound in the UI.

		//------------------------------------------------------------------------------------------------------------------------//
		// RecipeName property allows getting and setting the name of the recipe.
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
		// Ingredients collection for UI binding, updates automatically due to ObservableCollection.
		public ObservableCollection<Ingredient> Ingredients { get; private set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// Steps collection for UI binding, updates automatically due to ObservableCollection.
		public ObservableCollection<string> Steps { get; private set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// TotalCalories property calculates the total calories of the recipe, updates when Ingredients change.
		public double TotalCalories => _recipe.CalculateTotalCalories();

		//------------------------------------------------------------------------------------------------------------------------//
		// LastAccessed property allows getting and setting the date and time when the recipe was last accessed.
		public DateTime LastAccessed { get; set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// BackgroundGradient property for setting the UI background based on the recipe index.
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
		// Private Methods section contains helper methods used internally by the ViewModel.

		//------------------------------------------------------------------------------------------------------------------------//
		// SetBackgroundGradient calculates and sets the background gradient based on the recipe index.
		private void SetBackgroundGradient(int index)
		{
			int gradientIndex = (index % 10) + 1; // Cycle through 1 to 10 for gradients.
			BackgroundGradient = $"TertiaryColorGradient{gradientIndex}";
			OnPropertyChanged(nameof(BackgroundGradient));
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}