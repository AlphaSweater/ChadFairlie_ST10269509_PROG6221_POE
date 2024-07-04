// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      ChatGPT provided some great insight into how best to use this class.

//------------------------------------------------------------------------------------------------------------------------//

using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Views.PopUps;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	// Delegate for handling events when total calories exceed a certain threshold.
	public delegate void ExceededCaloriesDelegate(double totalCalories);

	// ViewModel for adding a new recipe through the UI.
	public class AddRecipeViewModel : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private string _recipeName;

		private ObservableCollection<Ingredient> _ingredients;
		private ObservableCollection<Step> _steps;
		private string _stepDescription;

		private double _totalCalories;
		private string _caloriesMessage;

		// Event triggered when the total calories of the recipe exceed a certain limit.
		public event ExceededCaloriesDelegate OnCaloriesExceeded;

		// Flag to track if the calories exceeded notification has been triggered.
		private bool _caloriesExceededNotified = false;

		private readonly RecipeService _recipeService;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor initializes the ViewModel, including commands and services.
		public AddRecipeViewModel()
		{
			_recipeService = ServiceProviderFactory.GetService<RecipeService>();

			_ingredients = new ObservableCollection<Ingredient>();
			_steps = new ObservableCollection<Step>();

			AddIngredientCommand = new RelayCommand(OpenAddIngredientWindow);
			AddStepCommand = new RelayCommand(AddStep);

			SubmitRecipeCommand = new RelayCommand(SubmitRecipe);
			ClearRecipeCommand = new RelayCommand(ClearRecipe);

			OnCaloriesExceeded += HandleCaloriesExceeded; // Subscribe to the event
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties for data binding in the UI.

		//------------------------------------------------------------------------------------------------------------------------//
		// Gets or sets the name of the recipe.
		public string RecipeName
		{
			get => _recipeName;
			set => SetProperty(ref _recipeName, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Collection of ingredients in the recipe.
		public ObservableCollection<Ingredient> Ingredients
		{
			get => _ingredients;
			set => SetProperty(ref _ingredients, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Collection of cooking steps in the recipe.
		public ObservableCollection<Step> Steps
		{
			get => _steps;
			set => SetProperty(ref _steps, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Gets or sets the description of the current cooking step.
		public string StepDescription
		{
			get => _stepDescription;
			set => SetProperty(ref _stepDescription, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Gets the total calories of the recipe.
		public double TotalCalories
		{
			get => _totalCalories;
			private set => SetProperty(ref _totalCalories, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Gets the message to display based on the calorie content of the recipe.
		public string CaloriesMessage
		{
			get => _caloriesMessage;
			private set => SetProperty(ref _caloriesMessage, value);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Methods for calculating and updating the total calories of the recipe.

		//------------------------------------------------------------------------------------------------------------------------//
		// Calculates the total calories of the recipe.
		private double CalculateTotalCalories()
		{
			double totalCalories = 0;
			foreach (var ingredient in Ingredients)
			{
				totalCalories += ingredient.PreciseCaloriesPerUnit * ingredient.PreciseQuantity;
			}

			// Check if total calories exceed 300 and if the notification has not been triggered before
			if (totalCalories > 300 && !_caloriesExceededNotified)
			{
				OnCaloriesExceeded?.Invoke(totalCalories);
				_caloriesExceededNotified = true; // Set flag to true after notification
			}

			return totalCalories;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Updates the total calories and the message based on the calorie content.
		private void UpdateTotalCalories()
		{
			TotalCalories = CalculateTotalCalories();
			UpdateCaloriesMessage();

			// Check if total calories exceed 300 and trigger the event if needed
			if (TotalCalories > 300 && !_caloriesExceededNotified)
			{
				OnCaloriesExceeded?.Invoke(TotalCalories);
				_caloriesExceededNotified = true; // Set flag to true after notification
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Updates the message displayed to the user based on the total calorie content.
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

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Commands for UI actions.

		//------------------------------------------------------------------------------------------------------------------------//
		// Command to open the window to add a new ingredient.
		public ICommand AddIngredientCommand { get; }

		// Opens the window to add a new ingredient.
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

		//------------------------------------------------------------------------------------------------------------------------//
		// Command to add a new cooking step to the recipe.
		public ICommand AddStepCommand { get; }

		// Adds a new cooking step to the recipe, including the step number in the description.
		private void AddStep()
		{
			if (!string.IsNullOrEmpty(StepDescription))
			{
				// Calculate the step number based on the current number of steps
				int stepNumber = Steps.Count + 1;
				string formattedStepDescription = $"Step {stepNumber} -> {StepDescription}";

				Steps.Add(new Step(formattedStepDescription));
				StepDescription = string.Empty; // Clear the input field after adding the step
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Command to submit the new recipe.
		public ICommand SubmitRecipeCommand { get; }

		// Submits the new recipe, performing validation and using the RecipeService to save it.
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

			var newRecipe = new Recipe(RecipeName, new List<Ingredient>(Ingredients), new List<Step>(Steps));
			_recipeService.AddRecipe(newRecipe);

			MessageBox.Show("Recipe submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

			// Clear the form after successful submission.
			RecipeName = string.Empty;
			Ingredients.Clear();
			Steps.Clear();
			TotalCalories = 0;
			CaloriesMessage = string.Empty;
			_caloriesExceededNotified = false; // Reset the flag
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Command to clear all data in the recipe form.
		public ICommand ClearRecipeCommand { get; }

		// Clears all data in the recipe form.
		private void ClearRecipe()
		{
			var result = MessageBox.Show("Are you sure you want to clear all the data?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				RecipeName = string.Empty;
				Ingredients.Clear();
				Steps.Clear();
				TotalCalories = 0;
				CaloriesMessage = string.Empty;
				_caloriesExceededNotified = false; // Reset the flag
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Event handling methods.

		//------------------------------------------------------------------------------------------------------------------------//
		// Handles the event when the total calories of a recipe exceed a certain limit, displaying an alert.
		private void HandleCaloriesExceeded(double totalCalories)
		{
			string message = $"Alert: The total calories of this recipe exceed 300. Current Total Calories: {totalCalories} kcal\n\n" +
							 "You are approaching the recommended calorie intake for a good meal. Here are some suggestions:\n\n" +
							 "- Consider reducing the portion sizes of high-calorie ingredients.\n" +
							 "- Look for lower-calorie alternatives for some ingredients.\n" +
							 "- Balance your meal with more low-calorie vegetables or side dishes.\n\n" +
							 "Remember, the recommended daily calorie intake varies based on age, sex, and activity level.";
			MessageBox.Show(message, "Calories Exceeded", MessageBoxButton.OK, MessageBoxImage.Warning);
		}
	}
}

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//