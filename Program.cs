using ChadFairlie_ST10269509_PROG6221_POE.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE
{
    internal class Program
    {
        //------------------------------------------------------------------------------------------------------------------------//
        private Recipe NewRecipe = null;

        //------------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------------------------------------//
        // >>>>>>>>>>>>>>>>>
        // >  Main Method  >
        // >>>>>>>>>>>>>>>>>
        private static void Main(string[] args)
        {
            Program worker = new Program();
            worker.BeginHere();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------------------------------------//
        // >>>>>>>>>>>>>>>>>>>>>>>>>>
        // >  Program Start Method  >
        // >>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BeginHere()
        {
            while (true)
            {
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nEnter your choice: ");
                Console.ResetColor();
                Console.Write("/> ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine();
                        NewRecipe = CreateNewRecipe();
                        break;

                    case "2":
                        Console.WriteLine();
                        DisplayRecipe(NewRecipe);
                        break;

                    case "3":
                        return;

                    default:
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
        //------------------------------------------------------------------------------------------------------------------------//
        // >>>>>>>>>>>>>>>>>>>>>>>>>
        // >  Menu Option Methods  >
        // >>>>>>>>>>>>>>>>>>>>>>>>>
        private Recipe CreateNewRecipe()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==============================================================================");
            Console.WriteLine("Creating a New Recipe");
            Console.WriteLine("==============================================================================");
            Console.ResetColor();

            string recipeName = GetRecipeName();
            NewRecipe = new Recipe(recipeName);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nGreat! Now let's add some ingredients to your {NewRecipe.RecipeName} recipe.\n");
            int ingredientCount = GetNumberOfIngredients();
            for (int i = 0; i < ingredientCount; i++)
            {
                Ingredient ingredient = GetIngredient(i);
                NewRecipe.AddIngredient(ingredient);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPerfect! Now let's add the steps to the recipe.\n");
            int stepCount = GetNumberOfSteps();
            for (int i = 0; i < stepCount; i++)
            {
                string stepDescription = GetStep(i);
                NewRecipe.AddStep(stepDescription);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n==============================================================================");
            Console.WriteLine("Recipe created successfully!");
            Console.WriteLine("==============================================================================\n");
            Console.ResetColor();

            return NewRecipe;
        }

        //------------------------------------------------------------------------------------------------------------------------//
        private void DisplayRecipe(Recipe recipe)
        {
            if (recipe != null)
            {
                bool shouldReturn = false;
                while (!shouldReturn)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Your Recipe Details");
                    Console.WriteLine("==============================================================================");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(recipe.PrintRecipe());

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("==============================================================================");
                    shouldReturn = DisplayScalingMenu(recipe);
                }
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n==============================================================================");
                Console.WriteLine("No recipe created yet.");
                Console.WriteLine("==============================================================================\n");
                Console.ResetColor();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
        private bool DisplayScalingMenu(Recipe recipe)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe Scaling Options");
            Console.WriteLine("==============================================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Scale recipe by 0.5 (half)");
            Console.WriteLine("2. Scale recipe by 2 (double)");
            Console.WriteLine("3. Scale recipe by 3 (triple)");
            Console.WriteLine("4. Reset recipe scaling");
            Console.WriteLine("5. Clear recipe");
            Console.WriteLine("6. Return to main menu");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==============================================================================");
            Console.ResetColor();
            Console.WriteLine("Enter your choice: ");
            Console.Write("/> ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    recipe.Scale(0.5);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Great! Your recipe has been halved.");
                    Console.WriteLine("==============================================================================");
                    Console.ResetColor();
                    return false;

                case "2":
                    recipe.Scale(2);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Great! Your recipe has been doubled.");
                    Console.WriteLine("==============================================================================");
                    Console.ResetColor();
                    return false;

                case "3":
                    recipe.Scale(3);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Great! Your recipe has been tripled.");
                    Console.WriteLine("==============================================================================");
                    Console.ResetColor();
                    return false;

                case "4":
                    recipe.ResetScaling();
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Awesome! Your recipe scaling was reset successfully!");
                    Console.WriteLine("==============================================================================");
                    return false;

                case "5":
                    if (UserConfirmation("Are you sure you want to clear the recipe? (y/n)"))
                    {
                        NewRecipe = null;
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

                case "6":
                    return true;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    Console.WriteLine("==============================================================================\n");
                    Console.ResetColor();
                    return false;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------------------------------------//
        // >>>>>>>>>>>>>>>>>>>>
        // >  Helper Methods  >
        // >>>>>>>>>>>>>>>>>>>>
        private string GetRecipeName()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter recipe name:");
            Console.ResetColor();
            Console.Write("/> ");
            return Console.ReadLine();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        private int GetNumberOfIngredients()
        {
            return (int)GetNumberFromUser("Please enter the number of ingredients in the recipe:");
        }

        //------------------------------------------------------------------------------------------------------------------------//
        private Ingredient GetIngredient(int i)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nPlease enter the name of ingredient number {i + 1}:");
            Console.ResetColor();
            Console.Write("/> ");
            string ingredientName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nEnter the unit of measurement you would use for this ingredient:");
            Console.WriteLine("Example: cups, grams, teaspoons, etc.");
            Console.ResetColor();
            Console.Write("/> ");
            string ingredientUnit = Console.ReadLine();
            double ingredientQuantity = GetNumberFromUser("\nEnter the quantity of this ingredient:");
            return new Ingredient(ingredientName, ingredientQuantity, ingredientUnit);
        }

        //------------------------------------------------------------------------------------------------------------------------//
        private int GetNumberOfSteps()
        {
            return (int)GetNumberFromUser("Please enter the number of steps for the recipe:");
        }

        //------------------------------------------------------------------------------------------------------------------------//
        private string GetStep(int i)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nPlease enter the instruction for step number {i + 1}:");
            Console.ResetColor();
            Console.Write("/> ");
            return $"Step {i + 1} -> " + Console.ReadLine();
        }

        //------------------------------------------------------------------------------------------------------------------------//
        private bool UserConfirmation(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==============================================================================");
            Console.WriteLine(message);
            Console.WriteLine("==============================================================================");
            Console.ResetColor();
            Console.Write("/> ");
            string response = Console.ReadLine().ToLower();
            return response == "y" || response == "yes";
        }

        //------------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------------------------------------//
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // >  Error Handling Methods  >
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private double GetNumberFromUser(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(prompt);
                    Console.ResetColor();
                    Console.Write("/> ");
                    double number = Convert.ToDouble(Console.ReadLine());
                    if (number < 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    return number;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.WriteLine("==============================================================================\n");
                    Console.ResetColor();
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Invalid input.Number is too large or too small.");
                    Console.WriteLine("==============================================================================\n");
                    Console.ResetColor();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n==============================================================================");
                    Console.WriteLine("Invalid input. Number must be non-negative.");
                    Console.WriteLine("==============================================================================\n");
                    Console.ResetColor();
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//
    }
}