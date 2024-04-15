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
        private static void Main(string[] args)
        {
            Program worker = new Program();
            worker.BeginHere();
        }

        private void BeginHere()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=============================================================");
            Console.WriteLine("---> Welcome to the Recipe Manager <-------------------------");
            Console.WriteLine("=============================================================");
            Console.ResetColor();

            string recipeName = GetRecipeName();
            Recipe newRecipe = new Recipe(recipeName);

            Console.WriteLine($"\nGreat! Now let's add some ingredients to the {newRecipe.RecipeName} recipe.");
            int ingredientCount = GetNumberOfIngredients();
            for (int i = 0; i < ingredientCount; i++)
            {
                Ingredient ingredient = GetIngredient(i);
                newRecipe.AddIngredient(ingredient);
            }

            Console.WriteLine("\nPerfect! Now let's add the steps to the recipe.");
            int stepCount = GetNumberOfSteps();
            for (int i = 0; i < stepCount; i++)
            {
                string stepDescription = GetStep(i);
                newRecipe.AddStep(stepDescription);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=======================================");
            Console.WriteLine("Recipe created successfully!");
            Console.WriteLine("=======================================");
            Console.ResetColor();
            Console.WriteLine("Recipe Name: " + newRecipe.RecipeName);
            Console.WriteLine("Ingredients:");
            foreach (Ingredient ingredient in newRecipe.Ingredients)
            {
                Console.WriteLine("• " + ingredient.Quantity + " " + ingredient.UnitOfMeasurement + " of " + ingredient.Name);
            }
            Console.WriteLine("Steps:");
            foreach (string step in newRecipe.Steps)
            {
                Console.WriteLine("• " + step);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=======================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private string GetRecipeName()
        {
            Console.WriteLine("\nPlease enter the name of the recipe you would like to create:");
            Console.Write("/> ");
            return Console.ReadLine();
        }

        private int GetNumberOfIngredients()
        {
            Console.WriteLine("Please enter the number of ingredients in the recipe:");
            Console.Write("/> ");
            return Convert.ToInt32(Console.ReadLine());
        }

        private Ingredient GetIngredient(int i)
        {
            Console.WriteLine($"\nPlease enter the name of ingredient number {i + 1}:");
            Console.Write("/> ");
            string ingredientName = Console.ReadLine();
            Console.WriteLine("Please enter the quantity of this ingredient:");
            Console.Write("/> ");
            double ingredientQuantity = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please enter the unit of measurement you would use for this ingredient:");
            Console.WriteLine("Example: cups, grams, ounces, etc.");
            Console.Write("/> ");
            string ingredientUnit = Console.ReadLine();
            return new Ingredient(ingredientName, ingredientQuantity, ingredientUnit);
        }

        private int GetNumberOfSteps()
        {
            Console.WriteLine($"Please enter the number of steps for the recipe:");
            Console.Write("/> ");
            return Convert.ToInt32(Console.ReadLine());
        }

        private string GetStep(int i)
        {
            Console.WriteLine($"Please enter the instruction for step number {i + 1}:");
            Console.Write("/> ");
            return $"Step {i + 1} -> " + Console.ReadLine();
        }
    }
}