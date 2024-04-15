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
            Console.WriteLine("---> Welcome to the Recipe Manager <---");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Please enter the name of the recipe you would like to create:");
            Console.Write("/> ");
            string recipeName = Console.ReadLine();
            Classes.Recipe newRecipe = new Classes.Recipe(recipeName);
        }
    }
}