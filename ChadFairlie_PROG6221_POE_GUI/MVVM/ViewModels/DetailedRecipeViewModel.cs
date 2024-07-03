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
using System.Windows;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	public class DetailedRecipeViewModel : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private Recipe _recipe;

		private string _backgroundGradient;
		private string _caloriesMessage;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor initializes the ViewModel with a Recipe object and an index for setting the background gradient.
		public DetailedRecipeViewModel(Recipe recipe, int index)
		{
			// Check if the recipe is null for some reason, throw an exception if it is.
			_recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");

			// Initialize the collections with the recipe's ingredients and steps.
			Ingredients = new ObservableCollection<Ingredient>(_recipe.Ingredients);
			Steps = new ObservableCollection<Step>(_recipe.Steps);

			// Set the LastAccessed property to the recipe's LastAccessed property.
			LastAccessed = _recipe.LastAccessed;

			// Subscribe to the CollectionChanged event of Ingredients
			Ingredients.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalCalories));

			// Update the CaloriesMessage property based on the total calories of the recipe.
			UpdateCaloriesMessage();

			// Set the background gradient based on the position of the recipe in the list.
			SetBackgroundGradient(index);

			// Initialize the ResetStepsCommand.
			ResetStepsCommand = new RelayCommand(ResetSteps);
			UpScaleCommand = new RelayCommand<object>(UpScale);
			DownScaleCommand = new RelayCommand<object>(DownScale);
			ResetScaleCommand = new RelayCommand(ResetScale);
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
		public ObservableCollection<Step> Steps { get; private set; }

		//------------------------------------------------------------------------------------------------------------------------//
		// TotalCalories property calculates the total calories of the recipe, updates when Ingredients change.
		public double TotalCalories => _recipe.CalculateTotalCalories();

		//------------------------------------------------------------------------------------------------------------------------//
		// LastAccessed property allows getting and setting the date and time when the recipe was last accessed.
		public DateTime LastAccessed
		{
			get => _recipe.LastAccessed;
			set
			{
				if (_recipe.LastAccessed != value)
				{
					_recipe.LastAccessed = value;
					OnPropertyChanged(nameof(LastAccessed));
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// UpdateLastAccessed method updates the LastAccessed property to the current date and time.
		public void UpdateLastAccessed()
		{
			LastAccessed = DateTime.Now;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// CurrentScale property allows getting and setting the current scale of the recipe.
		public double CurrentScale
		{
			get => _recipe.CurrentScale;
			set
			{
				if (_recipe.CurrentScale != value)
				{
					_recipe.CurrentScale = value;
					OnPropertyChanged(nameof(CurrentScale));
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// CaloriesMessage property for displaying a message based on the total calories of the recipe.
		public string CaloriesMessage
		{
			get => _caloriesMessage;
			private set
			{
				if (_caloriesMessage != value)
				{
					_caloriesMessage = value;
				}
			}
		}

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
		// UpdateCaloriesMessage sets the CaloriesMessage property based on the total calories of the recipe.
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
			OnPropertyChanged(nameof(CaloriesMessage));
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// SetBackgroundGradient calculates and sets the background gradient based on the recipe index.
		private void SetBackgroundGradient(int index)
		{
			int gradientIndex = (index % 10) + 1; // Cycle through 1 to 10 for gradients.
			BackgroundGradient = $"TertiaryColorGradient{gradientIndex}";
			OnPropertyChanged(nameof(BackgroundGradient));
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Commands
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Commands section contains ICommand properties for binding UI actions to methods in the ViewModel.

		// ResetStepsCommand resets the IsCompleted property of all steps in the recipe.
		public ICommand ResetStepsCommand { get; }

		private void ResetSteps()
		{
			foreach (var step in Steps)
			{
				step.IsCompleted = false;
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		public ICommand UpScaleCommand { get; }

		private void UpScale(object parameter)
		{
			if (double.TryParse(parameter?.ToString(), out double scale) && scale != 0)
			{
				_recipe.Scale(scale);
				OnPropertyChanged(nameof(Ingredients));
				OnPropertyChanged(nameof(_recipe.CurrentScale));
				OnPropertyChanged(nameof(TotalCalories));
				UpdateCaloriesMessage();
			}
			else
			{
				Console.WriteLine("Invalid scale parameter: " + parameter);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		public ICommand DownScaleCommand { get; }

		private void DownScale(object parameter)
		{
			if (double.TryParse(parameter?.ToString(), out double scale) && scale != 0)
			{
				_recipe.Scale(1 / scale);
				OnPropertyChanged(nameof(Ingredients));
				OnPropertyChanged(nameof(_recipe.CurrentScale));
				OnPropertyChanged(nameof(TotalCalories));
				UpdateCaloriesMessage();
			}
			else
			{
				Console.WriteLine("Invalid scale parameter: " + parameter);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		public ICommand ResetScaleCommand { get; }

		private void ResetScale()
		{
			_recipe.ResetScaling();
			OnPropertyChanged(nameof(Ingredients));
			OnPropertyChanged(nameof(_recipe.CurrentScale));
			OnPropertyChanged(nameof(TotalCalories));
			UpdateCaloriesMessage();
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}