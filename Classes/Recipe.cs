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
            string step = $"Step {Steps.Count + 1}: \n" + stepDescription + "\n";
            this.Steps.Add(step);
        }
    }
}