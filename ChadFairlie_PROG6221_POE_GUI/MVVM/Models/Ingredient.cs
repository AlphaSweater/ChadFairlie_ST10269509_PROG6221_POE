// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      ChatGPT provides some assistance in fixing problems with the code.

//------------------------------------------------------------------------------------------------------------------------//

using ChadFairlie_PROG6221_POE_GUI.Core;
using System;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	// Represents an ingredient with detailed information including its food group.
	public class Ingredient : ObservableObject
	{
		// Backing field for the ingredient's name.
		private string _name;

		// Gets or sets the name of the ingredient.
		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		// Backing field for the unit of measurement, normalized to singular form.
		private string _unitOfMeasurement;

		// Gets or sets the unit of measurement, automatically converting plural forms to singular.
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

		// Backing field for the original unit of measurement before any conversion.
		private string _originalUnitOfMeasurement;

		// Gets the original unit of measurement before any conversion was applied.
		public string OriginalUnitOfMeasurement
		{
			get => _originalUnitOfMeasurement;
			private set => SetProperty(ref _originalUnitOfMeasurement, value);
		}

		// Backing field for the precise quantity of the ingredient.
		private double _preciseQuantity;

		// Gets or sets the precise quantity of the ingredient, triggering updates to related properties.
		public double PreciseQuantity
		{
			get => _preciseQuantity;
			set
			{
				if (SetProperty(ref _preciseQuantity, value))
				{
					OnPropertyChanged(nameof(Quantity));
					OnPropertyChanged(nameof(FormattedUnitOfMeasurement));
				}
			}
		}

		// Rounds the precise quantity to 2 decimal places for display.
		public double Quantity => Math.Round(PreciseQuantity, 2);

		// Backing field for the original quantity before any conversion.
		private double _originalQuantity;

		// Gets the original quantity before any conversion was applied.
		public double OriginalQuantity
		{
			get => _originalQuantity;
			private set => SetProperty(ref _originalQuantity, value);
		}

		// Backing field for the precise calories per unit of the ingredient.
		private double _preciseCaloriesPerUnit;

		// Gets or sets the precise calories per unit, triggering updates to related properties.
		public double PreciseCaloriesPerUnit
		{
			get => _preciseCaloriesPerUnit;
			set
			{
				if (SetProperty(ref _preciseCaloriesPerUnit, value))
				{
					OnPropertyChanged(nameof(CaloriesPerUnit));
					OnPropertyChanged(nameof(FormattedCaloriesPerUnit));
				}
			}
		}

		// Rounds the precise calories per unit to 2 decimal places for display.
		public double CaloriesPerUnit => Math.Round(PreciseCaloriesPerUnit, 2);

		// Backing field for the original calories per unit before any conversion.
		private double _originalCaloriesPerUnit;

		// Gets the original calories per unit before any conversion was applied.
		public double OriginalCaloriesPerUnit
		{
			get => _originalCaloriesPerUnit;
			private set => SetProperty(ref _originalCaloriesPerUnit, value);
		}

		// Backing field for the food group of the ingredient.
		private FoodGroup _foodGroup;

		// Gets or sets the food group to which the ingredient belongs.
		public FoodGroup FoodGroup
		{
			get => _foodGroup;
			set => SetProperty(ref _foodGroup, value);
		}

		// Formats the unit of measurement for display, adding an 's' for plural when necessary.
		public string FormattedUnitOfMeasurement
		{
			get
			{
				if (string.IsNullOrEmpty(UnitOfMeasurement))
				{
					return UnitOfMeasurement;
				}
				else
				{
					string formattedUnit = char.ToUpper(UnitOfMeasurement[0]) + UnitOfMeasurement.Substring(1);
					if (Quantity > 1)
					{
						formattedUnit += "s";
					}
					return formattedUnit;
				}
			}
		}

		// Formats the calories per unit for display with 2 decimal places.
		public string FormattedCaloriesPerUnit => $"{CaloriesPerUnit:F2} kcal";

		// Parameterless constructor for easier data binding and instantiation without parameters.
		public Ingredient()
		{
		}

		// Constructor for creating a fully specified ingredient, including conversion if applicable.
		public Ingredient(string name, double quantity, string unit, double calories, FoodGroup foodGroup)
		{
			Name = name.TrimEnd('s');
			PreciseQuantity = quantity;
			UnitOfMeasurement = unit.ToLower().TrimEnd('s');
			PreciseCaloriesPerUnit = calories;
			FoodGroup = foodGroup;

			// Convert units and quantities if the unit is convertible.
			if (UnitConverter.IsConvertible(UnitOfMeasurement))
			{
				(PreciseQuantity, UnitOfMeasurement, PreciseCaloriesPerUnit) = UnitConverter.Convert(PreciseQuantity, UnitOfMeasurement, PreciseCaloriesPerUnit);
			}

			// Store original values for potential future reference.
			OriginalUnitOfMeasurement = UnitOfMeasurement;
			OriginalQuantity = PreciseQuantity;
			OriginalCaloriesPerUnit = PreciseCaloriesPerUnit;
		}
	}
}