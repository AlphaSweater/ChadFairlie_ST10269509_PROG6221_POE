// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      Bro Code: Provided a helpful video tutorial on Lists in C#. (https://youtu.be/vQzREQUhGSA?si=zi-m4qyNKLMErAu9)

//------------------------------------------------------------------------------------------------------------------------//

using ChadFairlie_PROG6221_POE_GUI.Core;
using System;
using System.Collections.Generic;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	// Represents a cooking recipe, including its ingredients and cooking steps.
	public class Recipe : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// The name of the recipe.
		public string RecipeName { get; set; }

		// The current scale factor applied to the recipe's ingredients.
		private double _preciseCurrentScale;

		public double PreciseCurrentScale
		{
			get => _preciseCurrentScale;
			set
			{
				SetProperty(ref _preciseCurrentScale, value);
				OnPropertyChanged(nameof(PreciseCurrentScale));
			}
		}

		// A list of ingredients required to make the recipe.
		public List<Ingredient> Ingredients { get; set; }

		// A list of steps to follow for cooking the recipe.
		public List<Step> Steps { get; set; }

		// The date and time when the recipe was last accessed.
		public DateTime LastAccessed { get; set; }

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

		// Default constructor initializes lists for ingredients and steps.
		public Recipe()
		{
			this.Ingredients = new List<Ingredient>();
			this.Steps = new List<Step>();
			this.PreciseCurrentScale = 1.0;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Constructor that initializes the recipe with a name, ingredients, and steps, and sets the last accessed time to now.
		public Recipe(string recipeName, List<Ingredient> ingredients, List<Step> steps)
		{
			this.RecipeName = recipeName;
			this.Ingredients = ingredients;
			this.Steps = steps;
			this.PreciseCurrentScale = 1.0;
			LastAccessed = DateTime.Now;
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Calculates the total calories of the recipe by summing up the calories of each ingredient.
		public double CalculateTotalCalories()
		{
			double totalCalories = 0;
			foreach (var ingredient in this.Ingredients)
			{
				totalCalories += (ingredient.CaloriesPerUnit * ingredient.Quantity);
			}

			return Math.Round(totalCalories, 2);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Scales the quantity of ingredients by a specified factor and adjusts the current scale accordingly.
		public void Scale(double scale)
		{
			PreciseCurrentScale *= scale;

			foreach (var ingredient in Ingredients)
			{
				ingredient.PreciseQuantity *= scale;

				if (UnitConverter.IsConvertible(ingredient.UnitOfMeasurement))
				{
					(ingredient.PreciseQuantity, ingredient.UnitOfMeasurement, ingredient.PreciseCaloriesPerUnit) = UnitConverter.Convert(ingredient.PreciseQuantity, ingredient.UnitOfMeasurement, ingredient.PreciseCaloriesPerUnit);
				}

				if (PreciseCurrentScale == 1)
				{
					this.ResetScaling();
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// Resets the scaling of the recipe to its original state, including the quantities and units of ingredients.
		public void ResetScaling()
		{
			foreach (var ingredient in Ingredients)
			{
				ingredient.PreciseQuantity = ingredient.OriginalQuantity;
				ingredient.UnitOfMeasurement = ingredient.OriginalUnitOfMeasurement;
				ingredient.PreciseCaloriesPerUnit = ingredient.OriginalCaloriesPerUnit;
			}
			PreciseCurrentScale = 1.0;
		}
	}
}