using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	public class AddRecipeViewModel : ObservableObject
	{
		private Recipe newRecipe;
		private readonly FoodGroup[] _fullFoodGroupsList;

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

		public FoodGroup[] FullFoodGroupsList => _fullFoodGroupsList;

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

		private string newIngredientName;

		public string NewIngredientName
		{
			get => newIngredientName;
			set
			{
				if (newIngredientName != value)
				{
					newIngredientName = value;
					OnPropertyChanged();
				}
			}
		}

		private string newIngredientQuantity;

		public string NewIngredientQuantity
		{
			get => newIngredientQuantity;
			set
			{
				if (newIngredientQuantity != value)
				{
					newIngredientQuantity = value;
					OnPropertyChanged();
				}
			}
		}

		private string selectedMeasurementUnit;

		public string SelectedMeasurementUnit
		{
			get => selectedMeasurementUnit;
			set
			{
				if (selectedMeasurementUnit != value)
				{
					selectedMeasurementUnit = value;
					OnPropertyChanged();
				}
			}
		}

		private string newIngredientCaloriesPerUnit;

		public string NewIngredientCaloriesPerUnit
		{
			get => newIngredientCaloriesPerUnit;
			set
			{
				if (newIngredientCaloriesPerUnit != value)
				{
					newIngredientCaloriesPerUnit = value;
					OnPropertyChanged();
				}
			}
		}

		private FoodGroup selectedFoodGroup;

		public FoodGroup SelectedFoodGroup
		{
			get => selectedFoodGroup;
			set
			{
				if (selectedFoodGroup != value)
				{
					selectedFoodGroup = value;
					OnPropertyChanged();
				}
			}
		}

		public ICommand AddIngredientCommand { get; }
		public ICommand AddStepCommand { get; }

		public AddRecipeViewModel()
		{
			NewRecipe = new Recipe();
			_fullFoodGroupsList = RecipeService.FoodGroups;

			AddIngredientCommand = new RelayCommand(AddIngredient);
			AddStepCommand = new RelayCommand(AddStep);
		}

		private void AddIngredient()
		{
			if (double.TryParse(NewIngredientQuantity, out double quantity) &&
				double.TryParse(NewIngredientCaloriesPerUnit, out double caloriesPerUnit))
			{
				var ingredient = new Ingredient
				{
					Name = NewIngredientName,
					PreciseQuantity = quantity,
					UnitOfMeasurement = SelectedMeasurementUnit,
					PreciseCaloriesPerUnit = caloriesPerUnit,
					FoodGroup = SelectedFoodGroup
				};

				NewRecipe.AddIngredient(ingredient);
				UpdateTotalCalories();
			}
			else
			{
				// Handle invalid input
			}
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