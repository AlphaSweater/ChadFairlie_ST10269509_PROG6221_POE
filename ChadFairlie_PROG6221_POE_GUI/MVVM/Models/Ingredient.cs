// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//
using ChadFairlie_PROG6221_POE_GUI.Core;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	// The Ingredient class represents a single ingredient used in a recipe.
	// It includes properties for the name, quantity, original quantity, and unit of measurement of the ingredient.
	public class Ingredient : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// private fields for the Ingredient class

		private string _name;
		private string _unitOfMeasurement;
		private string _originalUnitOfMeasurement;
		private double _quantity;
		private double _originalQuantity;
		private double _caloriesPerUnit;
		private double _originalCaloriesPerUnit;
		private string _foodGroup;

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public string UnitOfMeasurement
		{
			get => _unitOfMeasurement;
			set
			{
				if (SetProperty(ref _unitOfMeasurement, value.ToLower().TrimEnd('s')))
				{
					OnPropertyChanged(nameof(FormattedUnitOfMeasurement));
				}
			}
		}

		public string OriginalUnitOfMeasurement
		{
			get => _originalUnitOfMeasurement;
			private set => SetProperty(ref _originalUnitOfMeasurement, value);
		}

		public double Quantity
		{
			get => _quantity;
			set => SetProperty(ref _quantity, value);
		}

		public double OriginalQuantity
		{
			get => _originalQuantity;
			private set => SetProperty(ref _originalQuantity, value);
		}

		public double CaloriesPerUnit
		{
			get => _caloriesPerUnit;
			set
			{
				if (SetProperty(ref _caloriesPerUnit, value))
				{
					OnPropertyChanged(nameof(FormattedCaloriesPerUnit));
				}
			}
		}

		public double OriginalCaloriesPerUnit
		{
			get => _originalCaloriesPerUnit;
			private set => SetProperty(ref _originalCaloriesPerUnit, value);
		}

		public string FoodGroup
		{
			get => _foodGroup;
			set => SetProperty(ref _foodGroup, value);
		}

		public string FormattedUnitOfMeasurement
		{
			get
			{
				return !string.IsNullOrEmpty(UnitOfMeasurement)
			? $"{char.ToUpper(UnitOfMeasurement[0]) + UnitOfMeasurement.Substring(1)}{(Quantity > 1 ? "s" : "")}"
			: UnitOfMeasurement;
			}
		}

		public string FormattedCaloriesPerUnit => $"{CaloriesPerUnit:F2} kcal";

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor for the Ingredient class.
		// It initializes the name, quantity, and unit of measurement of the ingredient.
		// If the unit of measurement is convertible (e.g., "cups" to "tablespoons"), it converts the quantity and unit of measurement.
		public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
		{
			Name = name.TrimEnd('s');
			Quantity = quantity;
			UnitOfMeasurement = unit.ToLower().TrimEnd('s');
			CaloriesPerUnit = calories;
			FoodGroup = foodGroup;

			// Store original values
			OriginalUnitOfMeasurement = UnitOfMeasurement;
			OriginalQuantity = Quantity;
			OriginalCaloriesPerUnit = CaloriesPerUnit;

			if (UnitConverter.IsConvertible(UnitOfMeasurement))
			{
				(Quantity, UnitOfMeasurement, CaloriesPerUnit) = UnitConverter.Convert(Quantity, UnitOfMeasurement, CaloriesPerUnit);
			}
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}