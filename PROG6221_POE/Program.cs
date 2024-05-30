// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//      ChatGPT (Helped with the formatting of the menu headers and display text)

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//using ChadFairlie_ST10269509_PROG6221_POE.Classes;
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
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// List to hold all recipes.
		private List<Recipe> recipes = new List<Recipe>();

		private Recipe CurrentRecipe = null;

		private bool CanelRecipe = false;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//------------------------------------------------------------------------------------------------------------------------//
		// Color Definitions
		//------------------------------------------------------------------------------------------------------------------------//

		private static readonly ConsoleColor TitleColor = ConsoleColor.Cyan;
		private static readonly ConsoleColor InstructionColor = ConsoleColor.White;
		private static readonly ConsoleColor InputLabelColor = ConsoleColor.Green;
		private static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
		private static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
		private static readonly ConsoleColor DebugColor = ConsoleColor.DarkGray;
		private static readonly ConsoleColor HighlightColor = ConsoleColor.Yellow;
		private static readonly ConsoleColor DefaultTextColor = ConsoleColor.White;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//------------------------------------------------------------------------------------------------------------------------//
		// Main Method
		//------------------------------------------------------------------------------------------------------------------------//

		// Main method is the entry point of the console application.
		private static void Main(string[] args)
		{
			Program worker = new Program();
			worker.BeginHere();
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//------------------------------------------------------------------------------------------------------------------------//
		// Program Start Method
		//------------------------------------------------------------------------------------------------------------------------//

		// BeginHere method starts the application and displays the main menu.
		private void BeginHere()
		{
			while (true)
			{
				// Display the start menu.
				Console.ForegroundColor = TitleColor;
				Console.WriteLine("==============================================================================");
				Console.WriteLine("Welcome to the Recipe Manager");
				Console.WriteLine("==============================================================================");
				Console.ForegroundColor = DefaultTextColor;
				Console.WriteLine("1. Create new recipe");
				Console.WriteLine("2. Display recipes");
				Console.WriteLine("3. Exit");
				Console.ForegroundColor = TitleColor;
				Console.WriteLine("==============================================================================");

				// Prompt the user to enter their choice.
				Console.ForegroundColor = InputLabelColor;
				Console.Write("Enter your choice: \n");
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

						// Reset the flag to cancel the recipe creation.
						CanelRecipe = false;

						// Create a new recipe.
						CurrentRecipe = CreateNewRecipe();

						// If the new recipe is not null, add it to the recipes list.
						if (CurrentRecipe != null)
						{
							recipes.Add(CurrentRecipe);
						}

						break;

					case "2":
						// If the user chose to display the recipe, call the DisplayRecipe method.
						Console.WriteLine();
						if (recipes.Count == 0)
						{
							Console.ForegroundColor = ErrorColor;
							Console.WriteLine("==============================================================================");
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
						Console.ForegroundColor = ErrorColor;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Invalid choice. Please enter a valid option.");
						Console.WriteLine("==============================================================================\n");
						Console.ResetColor();
						break;
				}
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//------------------------------------------------------------------------------------------------------------------------//
		// Menu Option Methods
		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for creating a new recipe.
		private Recipe CreateNewRecipe()
		{
			Recipe newRecipe = new Recipe();

			// Subscribe to the OnCaloriesExceeded event of the recipe.
			newRecipe.OnCaloriesExceeded += NotifyCaloriesExceeded;

			// Display a message indicating the start of the recipe creation process.
			Console.ForegroundColor = TitleColor;
			Console.WriteLine("==============================================================================");
			Console.WriteLine("Creating a New Recipe");
			Console.WriteLine("==============================================================================");
			Console.ResetColor();

			// Get the name of the recipe from the user by calling the GetRecipeName method.
			string recipeName = GetRecipeName();

			// Set the recipe name in the new recipe object.
			newRecipe.RecipeName = recipeName;

			// Prompt the user to add ingredients to the recipe.
			Console.ForegroundColor = DefaultTextColor;
			Console.WriteLine($"\nGreat! Now let's add some ingredients to your {recipeName} recipe.\n");

			// Get the number of ingredients from the user by calling the GetNumberOfIngredients method.
			int ingredientCount = GetNumberOfIngredients();

			// Explain what information the user needs to provide for each ingredient.
			Console.ForegroundColor = DefaultTextColor;
			Console.WriteLine("\nBefore we start adding ingredients, please note the following:");
			Console.WriteLine("For each ingredient, you will need to provide the following details:");
			Console.ResetColor();
			Console.WriteLine("1. Name of the ingredient.");
			Console.WriteLine("2. Unit of Measurement: The unit used to measure the quantity (e.g., cups, tablespoons).");
			Console.WriteLine("3. Quantity: The amount of the ingredient needed for the recipe.");
			Console.WriteLine("4. Calories: The number of calories per serving of the ingredient.");
			Console.WriteLine("5. Food Group: The category to which the ingredient belongs (e.g., protein, vegetables, grains).");

			// For each ingredient, get the details from the user by calling the GetIngredient method
			// and add it to the new recipe Ingredients list.
			List<Ingredient> ingredients = new List<Ingredient>();
			for (int i = 0; i < ingredientCount; i++)
			{
				Ingredient ingredient = GetIngredient(i);
				ingredients.Add(ingredient);
			}

			// Add the ingredients to the new recipe.
			newRecipe.AddIngredients(ingredients);

			// Prompt the user to add steps to the recipe.
			Console.ForegroundColor = DefaultTextColor;
			Console.WriteLine("\nPerfect! Now let's add the steps to the recipe.\n");

			// Get the number of steps from the user by calling the GetNumberOfSteps method.
			int stepCount = GetNumberOfSteps();

			// For each step, get the description from the user by calling the GetStep method and add it to the recipe.
			List<string> steps = new List<string>();
			for (int i = 0; i < stepCount; i++)
			{
				string stepDescription = GetStep(i);
				steps.Add(stepDescription);
			}

			if (CanelRecipe)
			{
				// Display a failure message and return the newly created recipe.
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("\n==============================================================================");
				Console.WriteLine("Recipe was not created!");
				Console.WriteLine("==============================================================================\n");
				Console.ResetColor();
				return null;
			}

			// Display a success message and return the newly created recipe.
			Console.ForegroundColor = SuccessColor;
			Console.WriteLine("\n==============================================================================");
			Console.WriteLine("Recipe created successfully!");
			Console.WriteLine("==============================================================================\n");
			Console.ResetColor();

			return newRecipe;
		}

		//------------------------------------------------------------------------------------------------------------------------//

		private void DisplayRecipes()
		{
			Console.ForegroundColor = TitleColor;
			Console.WriteLine("==============================================================================");
			Console.WriteLine("List of Recipes");
			Console.WriteLine("==============================================================================");
			Console.ResetColor();

			// Sort the recipes list in alphabetical order by recipe name.
			var sortedRecipes = recipes.OrderBy(r => r.RecipeName).ToList();

			for (int i = 0; i < sortedRecipes.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {sortedRecipes[i].RecipeName}");
			}
			Console.ForegroundColor = TitleColor;
			Console.WriteLine("==============================================================================");

			Console.ForegroundColor = InputLabelColor;
			Console.WriteLine("\nEnter the number of the recipe you want to view: ");
			Console.ResetColor();
			int choice = (int)GetNumberFromUser("Enter your choice", false); // Use GetNumberFromUser method to get the choice
			if (choice > 0 && choice <= sortedRecipes.Count)
			{
				DisplayRecipe(sortedRecipes[choice - 1]);
			}
			else
			{
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("\n==============================================================================");
				Console.WriteLine("Invalid choice. Returning to the main menu.");
				Console.WriteLine("==============================================================================");
				Console.ResetColor();
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// This method is responsible for displaying the scaling options for a recipe.
		private bool DisplayScalingMenu(Recipe recipe)
		{
			// Display the scaling options menu.
			Console.ForegroundColor = TitleColor;
			Console.WriteLine("Recipe Scaling Options");
			Console.WriteLine("==============================================================================");
			Console.ForegroundColor = DefaultTextColor;
			Console.WriteLine("1. Scale recipe by 0.5 (half)");
			Console.WriteLine("2. Scale recipe by 2 (double)");
			Console.WriteLine("3. Scale recipe by 3 (triple)");
			Console.WriteLine("4. Scale recipe by custom factor");
			Console.WriteLine("5. Reset recipe scaling");
			Console.WriteLine("6. Remove recipe from list");
			Console.WriteLine("7. Return to main menu");
			Console.ForegroundColor = TitleColor;
			Console.WriteLine("==============================================================================");
			Console.ResetColor();

			// Read the user's choice.
			Console.ForegroundColor = InputLabelColor;
			Console.WriteLine("Enter your choice: ");
			Console.ResetColor();
			Console.Write("/> ");
			var choice = Console.ReadLine();

			// Perform the appropriate action based on the user's choice.
			switch (choice)
			{
				case "1":
					// If the user chose to halve the recipe, scale the recipe by 0.5.
					recipe.Scale(0.5);
					Console.ForegroundColor = SuccessColor;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Great! Your recipe has been halved.");
					Console.WriteLine("==============================================================================");
					Console.ResetColor();
					return false;

				case "2":
					// If the user chose to double the recipe, scale the recipe by 2.
					recipe.Scale(2);
					Console.ForegroundColor = SuccessColor;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Great! Your recipe has been doubled.");
					Console.WriteLine("==============================================================================");
					Console.ResetColor();
					return false;

				case "3":
					// If the user chose to triple the recipe, scale the recipe by 3.
					recipe.Scale(3);
					Console.ForegroundColor = SuccessColor;
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
					Console.ForegroundColor = SuccessColor;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine($"Great! Your recipe has been scaled by a factor of {customScale}.");
					Console.WriteLine("==============================================================================");
					Console.ResetColor();
					return false;

				case "5":
					if (UserConfirmation("Are you sure you want to reset the recipe scaling? (y/n)"))
					{
						// If the user chose to reset the recipe scaling, reset the scaling.
						recipe.ResetScaling();
						Console.ForegroundColor = SuccessColor;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Awesome! Your recipe scaling was reset successfully!");
						Console.WriteLine("==============================================================================");
						return false;
					}
					else
					{
						Console.ForegroundColor = SuccessColor;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Recipe scaling was not reset!");
						Console.WriteLine("==============================================================================");
						Console.ResetColor();
						return false;
					}

				case "6":
					// If the user chose to clear the recipe,
					// confirm the action with the user and clear the recipe if confirmed.
					if (UserConfirmation("Are you sure you want to remove the recipe from the list? (y/n)"))
					{
						recipes.Remove(recipe);
						CurrentRecipe = null;
						Console.ForegroundColor = SuccessColor;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Recipe removed successfully!");
						Console.WriteLine("==============================================================================");
						Console.ResetColor();
						return true;
					}
					else
					{
						Console.ForegroundColor = SuccessColor;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Recipe was not removed!");
						Console.WriteLine("==============================================================================");
						Console.ResetColor();
						return false;
					}

				case "7":
					// If the user chose to return to the main menu, return true.
					return true;

				default:
					// If the user entered an invalid choice, display an error message.
					Console.ForegroundColor = ErrorColor;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Invalid choice. Please enter a valid option.");
					Console.WriteLine("==============================================================================");
					Console.ResetColor();
					return false;
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
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

			// Get the number of calories for the ingredient per unit of measurement from the user by calling the GetNumberFromUser method.
			double ingredientCalories = GetNumberFromUser("\nEnter the number of calories per unit of measurement for this ingredient:");

			// Get the food group of the ingredient from the user by calling the GetStringFromUser method.
			string ingredientFoodGroup = GetFoodGroup();

			// Create a new Ingredient object with the provided name, quantity, unit, calories, and food group, and return it.
			return new Ingredient(ingredientName, ingredientQuantity, ingredientUnit, ingredientCalories, ingredientFoodGroup);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// This method is responsible for getting the food group of an ingredient from the user.
		private string GetFoodGroup()
		{
			// Keep prompting the user until a valid food group is selected.
			while (true)
			{
				// Display the food group menu.
				Console.ForegroundColor = InputLabelColor;
				Console.WriteLine("\nPlease select the food group that this ingredient belongs to:");
				Console.ForegroundColor = DefaultTextColor;
				Console.WriteLine("1. Protein");
				Console.WriteLine("2. Vegetables");
				Console.WriteLine("3. Fruits");
				Console.WriteLine("4. Grains");
				Console.WriteLine("5. Dairy");
				Console.WriteLine("6. Fats and Oils");
				Console.WriteLine("7. Sweets and Snacks");
				Console.WriteLine("8. Beverages");
				Console.WriteLine("9. Learn more about each food group");
				Console.ResetColor();
				Console.Write("/> ");

				// Read the user's choice.
				var choice = Console.ReadLine();

				// Return the appropriate food group based on the user's choice.
				switch (choice)
				{
					case "1":
						return "Protein";

					case "2":
						return "Vegetables";

					case "3":
						return "Fruits";

					case "4":
						return "Grains";

					case "5":
						return "Dairy";

					case "6":
						return "Fats and Oils";

					case "7":
						return "Sweets and Snacks";

					case "8":
						return "Beverages";

					case "9":
						// If the user chose to learn more about each food group, display the information and prompt again.
						DisplayFoodGroupInformation();
						break;

					default:
						// If the user entered an invalid choice, display an error message and prompt again.
						Console.ForegroundColor = ErrorColor;
						Console.WriteLine("\n==============================================================================");
						Console.WriteLine("Invalid choice. Please enter a valid option.");
						Console.WriteLine("==============================================================================");
						Console.ResetColor();
						break;
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// This method is responsible for displaying information about each food group.
		private void DisplayFoodGroupInformation()
		{
			Console.ForegroundColor = DefaultTextColor;
			Console.WriteLine("\nHere is some information about each food group:");
			Console.ResetColor();
			Console.WriteLine("1. Protein: Includes options such as meat, poultry, fish, eggs, tofu, legumes, and nuts.");
			Console.WriteLine("2. Vegetables: Covers a wide range of vegetables like leafy greens, root vegetables, cruciferous vegetables, peppers, onions, and tomatoes.");
			Console.WriteLine("3. Fruits: Encompasses fruits of all kinds, including berries, citrus fruits, apples, bananas, and tropical fruits.");
			Console.WriteLine("4. Grains: Represents grains and grain products like rice, pasta, bread, oats, quinoa, barley, and couscous.");
			Console.WriteLine("5. Dairy: Includes dairy products such as milk, cheese, yogurt, and alternatives like plant-based milk (e.g., almond milk, soy milk).");
			Console.WriteLine("6. Fats and Oils: Covers fats and oils used in cooking and food preparation, such as olive oil, butter, avocado, and coconut oil.");
			Console.WriteLine("7. Sweets and Snacks: Represents sugary foods, desserts, and snacks, including candies, chocolates, pastries, cookies, and chips.");
			Console.WriteLine("8. Beverages: Encompasses various beverages like water, tea, coffee, fruit juices, soft drinks, and alcoholic beverages (if applicable).");
			Console.WriteLine();
			Console.ResetColor();
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
					Console.ForegroundColor = TitleColor;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Your Recipe Details");
					Console.WriteLine("==============================================================================");
					Console.ForegroundColor = DefaultTextColor;

					// Print the recipe by calling the PrintRecipe method of the Recipe class.
					var (recipeDetails, totalCalories) = recipe.PrintRecipe();

					// Print the rest of the recipe details.
					Console.Write(recipeDetails);

					string caloriesMessage;
					// Change the console color based on the total calories.
					if (totalCalories > 300)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						caloriesMessage = " (Exceeds 300 calories!)";
					}
					else if (totalCalories > 250)
					{
						Console.ForegroundColor = ConsoleColor.Yellow;
						caloriesMessage = " (Moderate in calories)";
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Green;
						caloriesMessage = " (Low in calories)";
					}

					// Display the total calories of the recipe.
					Console.WriteLine($"Total Calories: {totalCalories} kcal" + caloriesMessage);

					// Reset the console color.
					Console.ResetColor();

					Console.ForegroundColor = TitleColor;
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
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("\n==============================================================================");
				Console.WriteLine("No recipe created yet.");
				Console.WriteLine("==============================================================================");
				Console.ResetColor();
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// This method is responsible for getting a yes/no confirmation from the user.
		private bool UserConfirmation(string message)
		{
			// Display the confirmation message.
			Console.ForegroundColor = HighlightColor;
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

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		//------------------------------------------------------------------------------------------------------------------------//
		// Error Handling Methods
		//------------------------------------------------------------------------------------------------------------------------//

		// This method is responsible for Notifying the user when the calories exceed 300.
		private void NotifyCaloriesExceeded()
		{
			Console.ForegroundColor = ErrorColor;
			Console.WriteLine("\n==============================================================================");
			Console.WriteLine("Warning: This recipe exceeds 300 kcal!");
			Console.WriteLine("==============================================================================\n");
			Console.ResetColor();

			bool userConfirmation = UserConfirmation("Do you still want to create this recipe? (y/n)");
			if (!userConfirmation)
			{
				CanelRecipe = true;
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
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
				Console.ForegroundColor = InputLabelColor;
				Console.WriteLine(prompt);
				Console.ResetColor();
				Console.Write("/> ");

				// Read the user's input.
				string input = Console.ReadLine();

				// If the input is blank, display an error message and prompt again.
				if (string.IsNullOrWhiteSpace(input))
				{
					Console.ForegroundColor = ErrorColor;
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
					Console.ForegroundColor = InputLabelColor;
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
					Console.ForegroundColor = ErrorColor;
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
					Console.ForegroundColor = ErrorColor;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Invalid input. Number is too large or too small.");
					Console.WriteLine("==============================================================================\n");
					Console.ResetColor();
				}
				catch (ArgumentOutOfRangeException)
				{
					// If the user's input is zero or negative,
					// display an error message and prompt again.
					Console.ForegroundColor = ErrorColor;
					Console.WriteLine("\n==============================================================================");
					Console.WriteLine("Invalid input. Number must be greater than zero.");
					Console.WriteLine("==============================================================================\n");
					Console.ResetColor();
				}
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}