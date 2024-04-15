using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
    internal class Recipe
    {
        public string RecipeName { get; set; }
        public double CurrentScale { get; private set; } = 1.0;
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public Recipe(string name)
        {
            this.RecipeName = name;
            this.Ingredients = new List<Ingredient>();
            this.Steps = new List<string>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            this.Ingredients.Add(ingredient);
        }

        public void AddStep(string stepDescription)
        {
            this.Steps.Add(stepDescription);
        }

        public void Scale(double scale)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= scale;
            }
            CurrentScale *= scale;
        }

        public void ResetScaling()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
            CurrentScale = 1.0;
        }

        public string PrintRecipe()
        {
            StringBuilder recipeDetails = new StringBuilder();

            recipeDetails.AppendLine("Recipe Name: " + RecipeName);
            recipeDetails.AppendLine("Current Scale: " + CurrentScale);
            recipeDetails.AppendLine("Ingredients:");
            foreach (Ingredient ingredient in Ingredients)
            {
                recipeDetails.AppendLine("> " + ingredient.Quantity + " " + ingredient.UnitOfMeasurement + " of " + ingredient.Name);
            }
            recipeDetails.AppendLine("Steps:");
            foreach (string step in Steps)
            {
                recipeDetails.AppendLine("> " + step);
            }

            return recipeDetails.ToString();
        }
    }
}