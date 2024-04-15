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
            Console.WriteLine("=======================================");
            Console.WriteLine("---> Welcome to the Recipe Manager <---");
            Console.WriteLine("=======================================");
            Console.ResetColor();

            Console.WriteLine("\nPlease enter the name of the recipe you would like to create:");
            Console.Write("/> ");
            string recipeName = Console.ReadLine();
            Classes.Recipe newRecipe = new Classes.Recipe(recipeName);

            Console.WriteLine($"\nGreat! Now let's add some ingredients to the {newRecipe.RecipeName} recipe.");
            Console.WriteLine("Please enter the number of ingredients in the recipe:");
            Console.Write("/> ");
            int ingredientCount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < ingredientCount; i++)
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
                Classes.Ingredient ingredient = new Classes.Ingredient(ingredientName, ingredientQuantity, ingredientUnit);
                newRecipe.AddIngredient(ingredient);
            }

            Console.WriteLine("\nPerfect! Now let's add the steps to the recipe.");
            Console.WriteLine($"Please enter the number of steps for the {newRecipe.RecipeName} recipe:");
            Console.Write("/> ");
            int stepCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            for (int i = 0; i < stepCount; i++)
            {
                Console.WriteLine($"Please enter the instruction for step number {i + 1}:");
                Console.Write("/> ");
                string stepDescription = Console.ReadLine();
                newRecipe.AddStep(stepDescription);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=======================================");
            Console.WriteLine("Recipe created successfully!");
            Console.WriteLine("=======================================");
            Console.ResetColor();
            Console.WriteLine("Recipe Name: " + newRecipe.RecipeName);
            Console.WriteLine("Ingredients:");
            foreach (Classes.Ingredient ingredient in newRecipe.Ingredients)
            {
                Console.WriteLine("• " + ingredient.Quantity + " " + ingredient.UnitOfMeasurement + " of " + ingredient.Name);
            }
            Console.WriteLine("Steps:");
            foreach (string step in newRecipe.Steps)
            {
                Console.WriteLine("• " + step);
            }
            Console.WriteLine("=======================================");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}