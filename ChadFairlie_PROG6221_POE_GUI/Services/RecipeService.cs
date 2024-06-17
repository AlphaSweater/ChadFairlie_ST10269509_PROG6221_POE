using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChadFairlie_PROG6221_POE_GUI.Services
{
	public class RecipeService : INotifyPropertyChanged
	{
		private ObservableCollection<Recipe> _recipes;

		public RecipeService()
		{
			_recipes = new ObservableCollection<Recipe>();
		}

		public ObservableCollection<Recipe> GetAllRecipes() => _recipes;

		public void AddRecipe(Recipe recipe)
		{
			_recipes.Add(recipe);
			OnPropertyChanged(nameof(_recipes));
		}

		public void RemoveRecipe(Recipe recipe)
		{
			_recipes.Remove(recipe);
			OnPropertyChanged(nameof(_recipes));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}