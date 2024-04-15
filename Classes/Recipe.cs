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
            CurrentScale *= scale;
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= scale;

                if (UnitConverter.IsConvertible(ingredient.UnitOfMeasurement))
                {
                    (ingredient.Quantity, ingredient.UnitOfMeasurement) = UnitConverter.Convert(ingredient.Quantity, ingredient.UnitOfMeasurement);
                }

                if (CurrentScale == 1)
                {
                    ingredient.Quantity = ingredient.OriginalQuantity;
                }
            }
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

            recipeDetails.AppendLine($"Recipe Name: {RecipeName}");
            recipeDetails.AppendLine($"Current Scale: {CurrentScale}x");
            recipeDetails.AppendLine("Ingredients:");
            foreach (Ingredient ingredient in Ingredients)
            {
                string unit = !string.IsNullOrEmpty(ingredient.UnitOfMeasurement) ? $"{ingredient.UnitOfMeasurement}{(ingredient.Quantity > 1 ? "s" : "")} of " : "";
                string plural = string.IsNullOrEmpty(ingredient.UnitOfMeasurement) && ingredient.Quantity > 1 ? "s" : "";
                recipeDetails.AppendLine($"> {ingredient.Quantity} {unit}{ingredient.Name}{plural}");
            }
            recipeDetails.AppendLine("Steps:");
            foreach (string step in Steps)
            {
                recipeDetails.AppendLine($"> {step}");
            }

            return recipeDetails.ToString();
        }
    }
}