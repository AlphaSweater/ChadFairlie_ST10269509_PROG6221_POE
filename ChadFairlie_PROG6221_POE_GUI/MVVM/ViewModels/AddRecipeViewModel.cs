using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	public class AddRecipeViewModel : ObservableObject
	{
		private Recipe newRecipe;

		public Recipe NewRecipe
		{
			get => newRecipe;
			set
			{
				if (newRecipe != value)
				{
					newRecipe = value;
					OnPropertyChanged();
					UpdateTotalCalories();
				}
			}
		}

		private double totalCalories;

		public double TotalCalories
		{
			get => totalCalories;
			private set
			{
				if (totalCalories != value)
				{
					totalCalories = value;
					OnPropertyChanged();
					UpdateCaloriesMessage();
				}
			}
		}

		private string caloriesMessage;

		public string CaloriesMessage
		{
			get => caloriesMessage;
			private set
			{
				if (caloriesMessage != value)
				{
					caloriesMessage = value;
					OnPropertyChanged();
				}
			}
		}

		public ICommand AddIngredientCommand { get; }
		public ICommand AddStepCommand { get; }

		public AddRecipeViewModel()
		{
			NewRecipe = new Recipe();
			AddIngredientCommand = new RelayCommand(AddIngredient);
			AddStepCommand = new RelayCommand(AddStep);
		}

		private void AddIngredient()
		{
			// Implementation for adding an ingredient
			UpdateTotalCalories();
		}

		private void AddStep()
		{
			// Implementation for adding a step
		}

		private void UpdateTotalCalories()
		{
			TotalCalories = NewRecipe.CalculateTotalCalories();
		}

		private void UpdateCaloriesMessage()
		{
			if (TotalCalories > 500)
			{
				CaloriesMessage = "High Calorie Content! Consider reducing portion size or substituting ingredients.";
			}
			else if (TotalCalories > 300)
			{
				CaloriesMessage = "Moderate Calorie Content. Good for a main meal.";
			}
			else if (TotalCalories > 150)
			{
				CaloriesMessage = "Low Calorie Content. Good for a light meal or snack.";
			}
			else
			{
				CaloriesMessage = "Very Low Calorie Content. Consider adding more nutritious ingredients.";
			}
		}
	}
}