// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI
//		• CoPilot

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using ChadFairlie_PROG6221_POE_GUI.Core;
using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels
{
	public class RecipesViewModel : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Fields and Services section contains private fields and services used within the ViewModel.

		private readonly RecipeService _recipeService;

		private ObservableCollection<DetailedRecipeViewModel> _recipes;
		private readonly FoodGroup[] _fullFoodGroupsList;

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor initializes the ViewModel, setting up services and loading all recipes.
		public RecipesViewModel()
		{
			_recipeService = ServiceProviderFactory.GetService<RecipeService>();
			_fullFoodGroupsList = RecipeService.FoodGroups;

			_recipes = new ObservableCollection<DetailedRecipeViewModel>();
			UpdateRecipes();

			// Populate the _recipes collection with DetailedRecipeViewModels for each recipe.

			// Set up commands
			ClearSearchFilterCommand = new RelayCommand(ClearSearch);
			ClearFoodGroupsFilterCommand = new RelayCommand(ClearFoodGroupsFilter);
			ClearCaloriesFilterCommand = new RelayCommand(ClearCaloriesFilter);

			// Initialize SelectedFoodGroups collection and subscribe to its CollectionChanged event
			SelectedFoodGroups = new ObservableCollection<FoodGroup>();
			SelectedFoodGroups.CollectionChanged += SelectedFoodGroups_CollectionChanged;

			// Call FilterRecipes to get the initial list of recipes
			FilterRecipes();
		}

		public void UpdateRecipes()
		{
			_recipes.Clear();
			var recipes = _recipeService.GetAllRecipes();

			// Populate the _recipes collection with DetailedRecipeViewModels for each recipe.
			for (int i = 0; i < recipes.Count; i++)
			{
				_recipes.Add(new DetailedRecipeViewModel(recipes[i], i));
			}

			FilteredRecipes = _recipes;
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Properties section exposes data to be bound in the UI.

		//------------------------------------------------------------------------------------------------------------------------//
		private ObservableCollection<DetailedRecipeViewModel> _filteredRecipes;

		public ObservableCollection<DetailedRecipeViewModel> FilteredRecipes
		{
			get => _filteredRecipes;
			set
			{
				if (_filteredRecipes != value)
				{
					_filteredRecipes = value;
					OnPropertyChanged(nameof(FilteredRecipes));
				}
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

		public FoodGroup[] FullFoodGroupsList
		{
			get => _fullFoodGroupsList;
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private const int SliderMaxValue = 1500;

		private const int NoLimitValue = int.MaxValue;

		private int _minCalories;

		public int MinCalories
		{
			get => _minCalories;
			set
			{
				if (_minCalories != value)
				{
					// Ensure MaxCalories is not less than new MinCalories value
					if (_maxCalories < value)
					{
						MaxCalories = value;
					}

					_minCalories = value;
					OnPropertyChanged(nameof(LowerValue));
					FilterRecipes();
				}
			}
		}

		public int LowerValue
		{
			get => MinCalories;
			set => MinCalories = value;
		}

		private int _maxCalories;

		public int MaxCalories
		{
			get => _maxCalories == SliderMaxValue ? NoLimitValue : _maxCalories;
			set
			{
				if (_maxCalories != value)
				{
					// Ensure MinCalories is not greater than new MaxCalories value
					if (_minCalories > value)
					{
						MinCalories = value;
					}

					_maxCalories = value == SliderMaxValue ? NoLimitValue : value;
					OnPropertyChanged(nameof(HigherValue));
					FilterRecipes();
				}
			}
		}

		public int HigherValue
		{
			get => MaxCalories == NoLimitValue ? SliderMaxValue : MaxCalories;
			set => MaxCalories = value;
		}

		private string _searchText;

		public string SearchText
		{
			get => _searchText;
			set
			{
				if (_searchText != value)
				{
					_searchText = value;
					OnPropertyChanged(nameof(SearchText));
					FilterRecipes();
				}
			}
		}

		private ObservableCollection<FoodGroup> _selectedFoodGroups;

		public ObservableCollection<FoodGroup> SelectedFoodGroups
		{
			get => _selectedFoodGroups;
			set
			{
				if (_selectedFoodGroups != value)
				{
					_selectedFoodGroups = value;
					OnPropertyChanged(nameof(SelectedFoodGroups));
				}
			}
		}

		private void SelectedFoodGroups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			FilterRecipes();
		}

		private void FilterRecipes()
		{
			var filtered = _recipes.AsEnumerable(); // Start with all recipes

			// Apply search text filter if search text is not empty
			if (!string.IsNullOrWhiteSpace(SearchText))
			{
				filtered = filtered.Where(recipe =>
					recipe.RecipeName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
					recipe.Ingredients.Any(ingredient => ingredient.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0));
			}

			// Apply selected food groups filter if there are selected food groups
			if (SelectedFoodGroups.Count > 0)
			{
				filtered = filtered.Where(recipe =>
					SelectedFoodGroups.All(selectedGroup =>
						recipe.Ingredients.Any(ingredient => ingredient.FoodGroup == selectedGroup)));
			}

			// Apply calorie range filter if MinCalories and MaxCalories are not both zero
			if (MinCalories > 0 || (MaxCalories < NoLimitValue && MaxCalories != 0))
			{
				filtered = filtered.Where(recipe =>
					MinCalories <= recipe.TotalCalories && recipe.TotalCalories <= MaxCalories);
			}

			// Convert the filtered enumerable back to an ObservableCollection
			FilteredRecipes = new ObservableCollection<DetailedRecipeViewModel>(filtered.ToList());
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Commands
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Commands section contains ICommand properties for binding UI actions to methods in the ViewModel.

		public ICommand ClearSearchFilterCommand { get; }

		private void ClearSearch()
		{
			SearchText = string.Empty;
		}

		public ICommand ClearFoodGroupsFilterCommand { get; }

		private void ClearFoodGroupsFilter()
		{
			SelectedFoodGroups.Clear();
		}

		public ICommand ClearCaloriesFilterCommand { get; }

		private void ClearCaloriesFilter()
		{
			LowerValue = 0;
			HigherValue = 0;
		}
	}
}