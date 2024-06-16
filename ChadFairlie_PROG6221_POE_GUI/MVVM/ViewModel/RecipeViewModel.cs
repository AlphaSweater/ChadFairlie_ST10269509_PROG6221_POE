// Ignore Spelling: PROG MVVM

using ChadFairlie_PROG6221_POE_GUI.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModel
{
	internal class RecipeViewModel : INotifyPropertyChanged
	{
		private Recipe _recipe;

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

		// ObservableCollection for Ingredients to automatically update the UI on changes
		public ObservableCollection<Ingredient> Ingredients { get; private set; }

		// ObservableCollection for Steps to automatically update the UI on changes
		public ObservableCollection<string> Steps { get; private set; }

		// Property for displaying total calories, recalculated when ingredients change
		public double TotalCalories => _recipe.CalculateTotalCalories();

		public string BackgroundGradient { get; private set; }

		public RecipeViewModel(Recipe recipe, int index)
		{
			_recipe = recipe;
			Ingredients = new ObservableCollection<Ingredient>(_recipe.Ingredients);
			Steps = new ObservableCollection<string>(_recipe.Steps);

			// Subscribe to the CollectionChanged event of Ingredients
			Ingredients.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalCalories));

			// Set the background gradient based on the index
			SetBackgroundGradient(index);
		}

		private void SetBackgroundGradient(int index)
		{
			int gradientIndex = (index % 10) + 1; // Cycle through 1 to 10
			BackgroundGradient = $"TertiaryColorGradient{gradientIndex}";
			OnPropertyChanged(nameof(BackgroundGradient));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}