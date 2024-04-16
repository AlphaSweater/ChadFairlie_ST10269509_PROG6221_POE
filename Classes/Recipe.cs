using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_ST10269509_PROG6221_POE.Classes
{
    // The Recipe class represents a cooking recipe.
    // It includes properties for the recipe name, current scale, list of ingredients, and list of cooking steps.
    internal class Recipe
    {
        // Name of the recipe.
        public string RecipeName { get; set; }

        // Current scale of the recipe. This is used when scaling the quantity of ingredients.
        public double CurrentScale { get; private set; } = 1.0;

        // List of ingredients required for the recipe.
        public List<Ingredient> Ingredients { get; set; }

        // List of steps to follow to cook the recipe.
        public List<string> Steps { get; set; }

        //------------------------------------------------------------------------------------------------------------------------//

        // Constructor for the Recipe class.
        // It initializes the recipe name and creates new lists for ingredients and steps.
        public Recipe(string name)
        {
            this.RecipeName = name;
            this.Ingredients = new List<Ingredient>();
            this.Steps = new List<string>();
        }

        //------------------------------------------------------------------------------------------------------------------------//

        // Method to add an ingredient to the recipe.
        public void AddIngredient(Ingredient ingredient)
        {
            this.Ingredients.Add(ingredient);
        }

        //------------------------------------------------------------------------------------------------------------------------//

        // Method to add a cooking step to the recipe.
        public void AddStep(string stepDescription)
        {
            this.Steps.Add(stepDescription);
        }

        //------------------------------------------------------------------------------------------------------------------------//

        // Method to scale the quantity of ingredients.
        public void Scale(double scale)
        {
            // Multiply the current scale by the given scale.
            CurrentScale *= scale;

            // Iterate over each ingredient in the recipe.
            foreach (var ingredient in Ingredients)
            {
                // Scale the quantity of the ingredient.
                ingredient.Quantity *= scale;

                // Check if the unit of measurement of the ingredient is convertible.
                if (UnitConverter.IsConvertible(ingredient.UnitOfMeasurement))
                {
                    // If it is, convert the quantity and unit of measurement of the ingredient.
                    (ingredient.Quantity, ingredient.UnitOfMeasurement) = UnitConverter.Convert(ingredient.Quantity, ingredient.UnitOfMeasurement);
                }

                // If the current scale is 1, reset the quantity of the ingredient to its original quantity.
                if (CurrentScale == 1)
                {
                    ingredient.Quantity = ingredient.OriginalQuantity;
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------//

        // Method to reset the scaling of the recipe.
        // It resets the quantity of each ingredient to its original quantity and sets the current scale to 1.
        public void ResetScaling()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
            CurrentScale = 1.0;
        }

        //------------------------------------------------------------------------------------------------------------------------//

        // Method to print the details of the recipe.
        // It includes the recipe name, current scale, list of ingredients, and list of steps.
        public string PrintRecipe()
        {
            // Create a StringBuilder to efficiently concatenate the recipe details into a single string.
            StringBuilder recipeDetails = new StringBuilder();

            // Add the recipe name and current scale to the string.
            recipeDetails.AppendLine($"Recipe Name: {RecipeName}");
            recipeDetails.AppendLine($"Current Scale: {CurrentScale}x");

            // Start the list of ingredients.
            recipeDetails.AppendLine("Ingredients:");

            // Add each ingredient to the string.
            foreach (Ingredient ingredient in Ingredients)
            {
                // Check if the unit of measurement is not empty.
                // If not, add the unit of measurement and "of" to the ingredient string, and make the unit plural if the quantity is more than 1.
                // If the unit of measurement is empty, check if the quantity is more than 1, and if so, make the ingredient name plural.
                string unit = !string.IsNullOrEmpty(ingredient.UnitOfMeasurement) ? $"{ingredient.UnitOfMeasurement}{(ingredient.Quantity > 1 ? "s" : "")} of " : "";
                string plural = string.IsNullOrEmpty(ingredient.UnitOfMeasurement) && ingredient.Quantity > 1 ? "s" : "";
                recipeDetails.AppendLine($"> {ingredient.Quantity} {unit}{ingredient.Name}{plural}");
            }

            // Start the list of cooking steps.
            recipeDetails.AppendLine("Steps:");

            // Add each cooking step to the string.
            foreach (string step in Steps)
            {
                recipeDetails.AppendLine($"> {step}");
            }

            // Return the recipe details as a string.
            return recipeDetails.ToString();
        }

        //------------------------------------------------------------------------------------------------------------------------//
    }
}