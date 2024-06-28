using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Views.PopUpView;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	public class AddRecipeViewModel : ObservableObject
	{
		private string _recipeName;

		private ObservableCollection<Ingredient> _ingredients;
		private ObservableCollection<Step> _steps;
		private string _stepDescription;

		private double _totalCalories;
		private string _caloriesMessage;

		private readonly RecipeService _recipeService;

		public AddRecipeViewModel()
		{
			_recipeService = ServiceProviderFactory.GetService<RecipeService>();

			_ingredients = new ObservableCollection<Ingredient>();
			_steps = new ObservableCollection<Step>();

			AddIngredientCommand = new RelayCommand(OpenAddIngredientWindow);
			AddStepCommand = new RelayCommand(AddStep);
			SubmitRecipeCommand = new RelayCommand(SubmitRecipe);
		}

		public string RecipeName
		{
			get => _recipeName;
			set => SetProperty(ref _recipeName, value);
		}

		public ObservableCollection<Ingredient> Ingredients
		{
			get => _ingredients;
			set => SetProperty(ref _ingredients, value);
		}

		public ObservableCollection<Step> Steps
		{
			get => _steps;
			set => SetProperty(ref _steps, value);
		}

		public string StepDescription
		{
			get => _stepDescription;
			set => SetProperty(ref _stepDescription, value);
		}

		public double TotalCalories
		{
			get => _totalCalories;
			private set => SetProperty(ref _totalCalories, value);
		}

		public string CaloriesMessage
		{
			get => _caloriesMessage;
			private set => SetProperty(ref _caloriesMessage, value);
		}

		private double CalculateTotalCalories()
		{
			double total = 0;
			foreach (var ingredient in Ingredients)
			{
				total += ingredient.PreciseCaloriesPerUnit * ingredient.PreciseQuantity;
			}
			return total;
		}

		private void UpdateTotalCalories()
		{
			TotalCalories = CalculateTotalCalories();
			UpdateCaloriesMessage();
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

		public ICommand AddIngredientCommand { get; }

		private void OpenAddIngredientWindow()
		{
			var ingredientWindow = new AddIngredientView();
			if (ingredientWindow.ShowDialog() == true)
			{
				Ingredients.Add(ingredientWindow.Ingredient);
				OnPropertyChanged(nameof(TotalCalories));
				UpdateTotalCalories();
			}
		}

		public ICommand AddStepCommand { get; }

		private void AddStep()
		{
			if (!string.IsNullOrEmpty(StepDescription))
			{
				Steps.Add(new Step(StepDescription));
				StepDescription = string.Empty;
			}
		}

		public ICommand SubmitRecipeCommand { get; }

		private void SubmitRecipe()
		{
			StringBuilder errorMessage = new StringBuilder();
			bool hasError = false;

			if (string.IsNullOrEmpty(RecipeName))
			{
				errorMessage.AppendLine("Recipe Name is required.");
				hasError = true;
			}

			if (Ingredients.Count == 0)
			{
				errorMessage.AppendLine("At least one ingredient is required.");
				hasError = true;
			}

			if (Steps.Count == 0)
			{
				errorMessage.AppendLine("At least one step is required.");
				hasError = true;
			}

			if (hasError)
			{
				MessageBox.Show(errorMessage.ToString(), "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Create a new Recipe object
			var newRecipe = new Recipe(RecipeName, new List<Ingredient>(Ingredients), new List<Step>(Steps));

			// Use the RecipeService to submit the recipe
			_recipeService.AddRecipe(newRecipe);

			// Show success message
			MessageBox.Show("Recipe submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

			// Clear the fields
			RecipeName = string.Empty;
			Ingredients.Clear();
			Steps.Clear();
			TotalCalories = 0;
			CaloriesMessage = string.Empty;
		}
	}
}