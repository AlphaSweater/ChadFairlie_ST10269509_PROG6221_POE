// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      ChatGPT (Helped with the formatting of the menu headers and display text)

//------------------------------------------------------------------------------------------------------------------------//
using ChadFairlie_ST10269509_PROG6221_POE.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE
{
	internal class Program
	{
		// List to hold all recipes.
		private List<Recipe> recipes = new List<Recipe>();

		private Recipe CurrentRecipe = null;

		//------------------------------------------------------------------------------------------------------------------------//
		// Main Method
		//------------------------------------------------------------------------------------------------------------------------//

		// Main method is the entry point of the console application.
		private static void Main(string[] args)
		{
			Program worker = new Program();
			worker.BeginHere();
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Program Start Method
		//------------------------------------------------------------------------------------------------------------------------//

		// BeginHere method starts the application and displays the main menu.
		private void BeginHere()
		{
			while (true)
			{
				// Display the start menu.
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("==============================================================================");
				Console.WriteLine("Welcome to the Recipe Manager");
				Console.WriteLine("==============================================================================");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("1. Create new recipe");
				Console.WriteLine("2. Display recipe");
				Console.WriteLine("3. Exit");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("==============================================================================");

				// Prompt the user to enter their choice.
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("\nEnter your choice: ");
				Console.ResetColor();
				Console.Write("/> ");

				// Read the user's choice.
				var choice = Console.ReadLine();

				// Perform the appropriate action based on the user's choice.
				switch (choice)
				{
					case "1":
						// If the user chose to create a new recipe, call the CreateNewRecipe method.
						Console.WriteLine();
						CurrentRecipe = CreateNewRecipe();
						recipes.Add(CurrentRecipe);
						break;

					case "2":
						// If the user chose to display the recipe, call the DisplayRecipe method.
						Console.WriteLine();
						if (recipes.Count == 0)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("\n==============================================================================");
							Console.WriteLine("No recipes available.");
							Console.WriteLine("==============================================================================\n");
							Console.ResetColor();
						}
						else
						{
							DisplayRecipes();
						}
						break;

					case "3":
						// If the user chose to exit, return from the method to end the application.
						return;

					default:
						// If the user entered an invalid choice, display an error message.
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Invalid choice. Please enter a valid option.");
						Console.WriteLine("==============================================================================\n");
						Console.ResetColor();
						break;
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Menu Option Methods
		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for creating a new recipe.
		private Recipe CreateNewRecipe()
		{
			// Display a message indicating the start of the recipe creation process.
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("==============================================================================");
			Console.WriteLine("Creating a New Recipe");
			Console.WriteLine("==============================================================================");
			Console.ResetColor();

			// Get the name of the recipe from the user by calling the GetRecipeName method.
			string recipeName = GetRecipeName();
			// Create a new Recipe object with the provided name.
			Recipe newRecipe = new Recipe(recipeName);

			// Prompt the user to add ingredients to the recipe.
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine($"\nGreat! Now let's add some ingredients to your {newRecipe.RecipeName} recipe.\n");

			// Get the number of ingredients from the user by calling the GetNumberOfIngredients method.
			int ingredientCount = GetNumberOfIngredients();

			// For each ingredient, get the details from the user by calling the GetIngredient method
			// and add it to the new recipe Ingredients list.
			for (int i = 0; i < ingredientCount; i++)
			{
				Ingredient ingredient = GetIngredient(i);
				newRecipe.AddIngredient(ingredient);
			}

			// Prompt the user to add steps to the recipe.
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nPerfect! Now let's add the steps to the recipe.\n");

			// Get the number of steps from the user by calling the GetNumberOfSteps method.
			int stepCount = GetNumberOfSteps();

			// For each step, get the description from the user by calling the GetStep method and add it to the recipe.
			for (int i = 0; i < stepCount; i++)
			{
				string stepDescription = GetStep(i);
				newRecipe.AddStep(stepDescription);
			}

			// Display a success message and return the newly created recipe.
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\n==============================================================================");
			Console.WriteLine("Recipe created successfully!");
			Console.WriteLine("==============================================================================\n");
			Console.ResetColor();

			return newRecipe;
		}

		//------------------------------------------------------------------------------------------------------------------------//

		private void DisplayRecipes()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("==============================================================================");
			Console.WriteLine("List of Recipes");
			Console.WriteLine("==============================================================================");
			Console.ResetColor();

			for (int i = 0; i < recipes.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {recipes[i].RecipeName}");
			}

			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nEnter the number of the recipe you want to view: ");
			Console.ResetColor();
			int choice = (int)GetNumberFromUser("Enter your choice", false); // Use GetNumberFromUser method to get the choice
			if (choice > 0 && choice <= recipes.Count)
			{
				DisplayRecipe(recipes[choice - 1]);
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n==============================================================================");
				Console.WriteLine("Invalid choice. Returning to the main menu.");
				Console.WriteLine("==============================================================================\n");
				Console.ResetColor();
			}
		}

		// This method is responsible for displaying the scaling options for a recipe.
		private bool DisplayScalingMenu(Recipe recipe)
		{
			// Display the scaling options menu.
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Recipe Scaling Options");
			Console.WriteLine("==============================================================================");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("1. Scale recipe by 0.5 (half)");
			Console.WriteLine("2. Scale recipe by 2 (double)");
			Console.WriteLine("3. Scale recipe by 3 (triple)");
			Console.WriteLine("4. Scale recipe by custom factor");
			Console.WriteLine("5. Reset recipe scaling");
			Console.WriteLine("6. Clear recipe");
			Console.WriteLine("7. Return to main menu");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("==============================================================================");
			Console.ResetColor();

			// Read the user's choice.
			Console.WriteLine("Enter your choice: ");
			Console.Write("/> ");
			var choice = Console.ReadLine();

			// Perform the appropriate action based on the user's choice.
			switch (choice)
			{
				case "1":
					// If the user chose to halve the recipe, scale the recipe by 0.5.
					recipe.Scale(0.5);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Great! Your recipe has been halved.");
					Console.WriteLine("==============================================================================");
					Console.ResetColor();
					return false;

				case "2":
					// If the user chose to double the recipe, scale the recipe by 2.
					recipe.Scale(2);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Great! Your recipe has been doubled.");
					Console.WriteLine("==============================================================================");
					Console.ResetColor();
					return false;

				case "3":
					// If the user chose to triple the recipe, scale the recipe by 3.
					recipe.Scale(3);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Great! Your recipe has been tripled.");
					Console.WriteLine("==============================================================================");
					Console.ResetColor();
					return false;

				case "4":
					// If the user chose to scale the recipe by a custom factor,
					// get the factor from the user and scale the recipe.
					double customScale = GetNumberFromUser("Enter the numeric scale factor:");
					recipe.Scale(customScale);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine($"Great! Your recipe has been scaled by a factor of {customScale}.");
					Console.WriteLine("==============================================================================");
					Console.ResetColor();
					return false;

				case "5":
					// If the user chose to reset the recipe scaling, reset the scaling.
					recipe.ResetScaling();
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Awesome! Your recipe scaling was reset successfully!");
					Console.WriteLine("==============================================================================");
					return false;

				case "6":
					// If the user chose to clear the recipe,
					// confirm the action with the user and clear the recipe if confirmed.
					if (UserConfirmation("Are you sure you want to clear the recipe? (y/n)"))
					{
						recipes.Remove(recipe);
						CurrentRecipe = null;
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Recipe cleared successfully!");
						Console.WriteLine("==============================================================================\n");
						Console.ResetColor();
						return true;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Recipe was not cleared!");
						Console.WriteLine("==============================================================================\n");
						Console.ResetColor();
						return false;
					}

				case "7":
					// If the user chose to return to the main menu, return true.
					return true;

				default:
					// If the user entered an invalid choice, display an error message.
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Invalid choice. Please enter a valid option.");
					Console.WriteLine("==============================================================================\n");
					Console.ResetColor();
					return false;
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Helper Methods
		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for getting the recipe name from the user.
		private string GetRecipeName()
		{
			// Call the GetStringFromUser method with a prompt for the recipe name.
			return GetStringFromUser("Enter recipe name:");
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for getting the number of ingredients from the user.
		private int GetNumberOfIngredients()
		{
			// Call the GetNumberFromUser method with a prompt for the number of ingredients and false for allowDecimals.
			// Cast the returned double to an int and return it as the number of ingredients.
			return (int)GetNumberFromUser("Please enter the number of ingredients in the recipe:", false);
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for getting the details of an ingredient from the user.
		private Ingredient GetIngredient(int i)
		{
			// Get the name of the ingredient from the user by calling the GetStringFromUser method.
			string ingredientName = GetStringFromUser($"\nPlease enter the name of ingredient number {i + 1}:");

			// Get the unit of measurement for the ingredient from the user by calling the GetStringFromUser method.
			string ingredientUnit = GetStringFromUser("\nEnter the unit of measurement you would use for this ingredient:\nExample: cups, grams, teaspoons, slices, etc.");

			// Get the quantity of the ingredient from the user by calling the GetNumberFromUser method.
			double ingredientQuantity = GetNumberFromUser("\nEnter the quantity of this ingredient:");

			// Create a new Ingredient object with the provided name, quantity, and unit, and return it.
			return new Ingredient(ingredientName, ingredientQuantity, ingredientUnit);
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for getting the number of steps from the user.
		private int GetNumberOfSteps()
		{
			// Call the GetNumberFromUser method with a prompt for the number of steps and false for allowDecimals.
			// Cast the returned double to an int and return it as the number of steps.
			return (int)GetNumberFromUser("Please enter the number of steps for the recipe:", false);
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for getting the description of a step from the user.
		private string GetStep(int i)
		{
			// Get the instruction for the step from the user by calling the GetStringFromUser method,
			// prefix it with the step number, and return it as the step description.
			return $"Step {i + 1} -> " + GetStringFromUser($"\nPlease enter the instruction for step number {i + 1}:");
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for displaying the details of a recipe.
		private void DisplayRecipe(Recipe recipe)
		{
			// Check if the recipe is not null.
			if (recipe != null)
			{
				// Initialize a flag to indicate whether to return to the main menu.
				bool shouldReturn = false;

				// Keep displaying the recipe details and scaling menu until the user
				// chooses to return to the main menu.
				while (!shouldReturn)
				{
					// Display the recipe details heading.
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Your Recipe Details");
					Console.WriteLine("==============================================================================");
					Console.ForegroundColor = ConsoleColor.White;

					// Print the recipe by calling the PrintRecipe method of the Recipe class.
					Console.Write(recipe.PrintRecipe());

					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("==============================================================================");

					// Display the scaling menu by calling the DisplayScalingMenu method and
					// update the shouldReturn flag based on its return value.
					shouldReturn = DisplayScalingMenu(recipe);
				}
				Console.WriteLine();
			}
			else
			{
				// If the recipe is null, display a message indicating that no recipe has been created yet.
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n==============================================================================");
				Console.WriteLine("No recipe created yet.");
				Console.WriteLine("==============================================================================\n");
				Console.ResetColor();
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for getting a yes/no confirmation from the user.
		private bool UserConfirmation(string message)
		{
			// Display the confirmation message.
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("==============================================================================");
			Console.WriteLine(message);
			Console.WriteLine("==============================================================================");
			Console.ResetColor();
			Console.Write("/> ");

			// Read the user's response and convert it to lowercase.
			string response = Console.ReadLine().ToLower();

			// Return true if the user entered 'y' or 'yes', and false otherwise.
			return response == "y" || response == "yes";
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Error Handling Methods
		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for getting a non-blank string from the user.
		private string GetStringFromUser(string prompt)
		{
			// Keep prompting the user until a non-blank string is entered.
			while (true)
			{
				// Display the prompt.
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine(prompt);
				Console.ResetColor();
				Console.Write("/> ");

				// Read the user's input.
				string input = Console.ReadLine();

				// If the input is blank, display an error message and prompt again.
				if (string.IsNullOrWhiteSpace(input))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Invalid input. Please enter a non-blank string.");
					Console.WriteLine("==============================================================================\n");
					Console.ResetColor();
				}
				else
				{
					// If the input is not blank, return it.
					return input;
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for getting a valid non-negative number from the user.
		private double GetNumberFromUser(string prompt, bool allowDecimals = true)
		{
			// Keep prompting the user until a valid number is entered.
			while (true)
			{
				try
				{
					// Display the prompt.
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine(prompt);
					Console.ResetColor();
					Console.Write("/> ");

					// Read the user's input and convert it to a double.
					double number = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

					// If the number is negative, throw an ArgumentOutOfRangeException.
					if (number <= 0)
					{
						throw new ArgumentOutOfRangeException();
					}

					// If decimals are not allowed and the number is not a whole number, throw a FormatException.
					if (!allowDecimals && number % 1 != 0)
					{
						throw new FormatException();
					}

					// If the number is valid, return it.
					return number;
				}
				catch (FormatException)
				{
					// If the user's input could not be converted to a double or is not a whole number when decimals are not allowed,
					// display an error message and prompt again.
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Invalid input. Please enter a valid number.");
					if (!allowDecimals)
					{
						Console.WriteLine("Decimals are not allowed.");
					}
					Console.WriteLine("==============================================================================\n");
					Console.ResetColor();
				}
				catch (OverflowException)
				{
					// If the user's input is too large or too small to be a double,
					// display an error message and prompt again.
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Invalid input. Number is too large or too small.");
					Console.WriteLine("==============================================================================\n");
					Console.ResetColor();
				}
				catch (ArgumentOutOfRangeException)
				{
					// If the user's input is zero or negative,
					// display an error message and prompt again.
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Invalid input. Number must be greater than zero.");
					Console.WriteLine("==============================================================================\n");
					Console.ResetColor();
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
	}
}