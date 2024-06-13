using ChadFairlie_PROG6221_POE.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ChadFairlie_PROG6221_POE.Tests
{
	[TestClass]
	public class RecipeTests
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// This test checks if the CalculateTotalCalories method returns 0 when there are no ingredients in the recipe.
		[TestMethod]
		public void CalculateTotalCalories_ShouldReturnZero_WhenNoIngredients()
		{
			// Arrange: Create a new recipe with no ingredients.
			var recipe = new Recipe();
			recipe.Ingredients = new List<Ingredient>();

			// Act: Calculate the total calories of the recipe.
			double totalCalories = recipe.CalculateTotalCalories();

			// Assert: The total calories should be 0.
			Assert.AreEqual(0, totalCalories);
		}

		//--------------------------------------------------------------------------------------------------------------------------//
		// This test checks if the CalculateTotalCalories method returns the correct total when there is a single ingredient in the recipe.
		[TestMethod]
		public void CalculateTotalCalories_ShouldReturnCorrectTotal_WhenSingleIngredient()
		{
			// Arrange: Create a new recipe with a single ingredient.
			var recipe = new Recipe();
			recipe.Ingredients = new List<Ingredient>
			{
				new Ingredient("Test Ingredient", 5, "unit", 20, "Test Group")
			};

			// Act: Calculate the total calories of the recipe.
			double totalCalories = recipe.CalculateTotalCalories();

			// Assert: The total calories should be 100 (5 units * 20 calories/unit).
			Assert.AreEqual(100, totalCalories);
		}

		//--------------------------------------------------------------------------------------------------------------------------//
		// This test checks if the CalculateTotalCalories method returns the correct total when there are multiple ingredients in the recipe.
		[TestMethod]
		public void CalculateTotalCalories_ShouldReturnCorrectTotal_WhenMultipleIngredients()
		{
			// Arrange: Create a new recipe with multiple ingredients.
			var recipe = new Recipe();
			recipe.Ingredients = new List<Ingredient>
			{
				new Ingredient("Test Ingredient 1", 2, "unit", 100, "Test Group 1"),
				new Ingredient("Test Ingredient 2", 3, "unit", 200, "Test Group 2")
			};

			// Act: Calculate the total calories of the recipe.
			double totalCalories = recipe.CalculateTotalCalories();

			// Assert: The total calories should be 800 ((2 units * 100 calories/unit) + (3 units * 200 calories/unit)).
			Assert.AreEqual(800, totalCalories);
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}