// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//
using ChadFairlie_PROG6221_POE_GUI.Core;
using System;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	// The Ingredient class represents a single ingredient used in a recipe.
	// It includes properties for the name, quantity, original quantity, and unit of measurement of the ingredient.
	public class Ingredient : ObservableObject
	{
		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		private string _name;

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		private string _unitOfMeasurement;

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

		//------------------------------------------------------------------------------------------------------------------------//
		private string _originalUnitOfMeasurement;

		public string OriginalUnitOfMeasurement
		{
			get => _originalUnitOfMeasurement;
			private set => SetProperty(ref _originalUnitOfMeasurement, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		private double _preciseQuantity;

		public double PreciseQuantity
		{
			get => _preciseQuantity;
			set
			{
				if (SetProperty(ref _preciseQuantity, value))
				{
					// Notify changes for Quantity since it's derived from PreciseQuantity
					OnPropertyChanged(nameof(Quantity));
					// If FormattedUnitOfMeasurement depends on Quantity, notify it as well
					OnPropertyChanged(nameof(FormattedUnitOfMeasurement));
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//

		public double Quantity
		{
			get => Math.Round(PreciseQuantity, 2);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		private double _originalQuantity;

		public double OriginalQuantity
		{
			get => _originalQuantity;
			private set => SetProperty(ref _originalQuantity, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		private double _preciseCaloriesPerUnit;

		public double PreciseCaloriesPerUnit
		{
			get => _preciseCaloriesPerUnit;
			set
			{
				if (SetProperty(ref _preciseCaloriesPerUnit, value))
				{
					// Notify changes for CaloriesPerUnit since it's derived from PreciseCaloriesPerUnit
					OnPropertyChanged(nameof(CaloriesPerUnit));
					// Notify changes for FormattedCaloriesPerUnit as well
					OnPropertyChanged(nameof(FormattedCaloriesPerUnit));
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		public double CaloriesPerUnit
		{
			get => Math.Round(PreciseCaloriesPerUnit, 2);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		private double _originalCaloriesPerUnit;

		public double OriginalCaloriesPerUnit
		{
			get => _originalCaloriesPerUnit;
			private set => SetProperty(ref _originalCaloriesPerUnit, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
		private string _foodGroup;

		public string FoodGroup
		{
			get => _foodGroup;
			set => SetProperty(ref _foodGroup, value);
		}

		//------------------------------------------------------------------------------------------------------------------------//
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

		//------------------------------------------------------------------------------------------------------------------------//
		public string FormattedCaloriesPerUnit => $"{CaloriesPerUnit:F2} kcal";

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
		// Constructor for the Ingredient class.
		// It initializes the name, quantity, and unit of measurement of the ingredient.
		// If the unit of measurement is convertible (e.g., "cups" to "tablespoons"), it converts the quantity and unit of measurement.
		public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
		{
			Name = name.TrimEnd('s');
			PreciseQuantity = quantity;
			UnitOfMeasurement = unit.ToLower().TrimEnd('s');
			PreciseCaloriesPerUnit = calories;
			FoodGroup = foodGroup;

			if (UnitConverter.IsConvertible(UnitOfMeasurement))
			{
				(PreciseQuantity, UnitOfMeasurement, PreciseCaloriesPerUnit) = UnitConverter.Convert(PreciseQuantity, UnitOfMeasurement, PreciseCaloriesPerUnit);
			}

			// Store original values
			OriginalUnitOfMeasurement = UnitOfMeasurement;
			OriginalQuantity = PreciseQuantity;
			OriginalCaloriesPerUnit = PreciseCaloriesPerUnit;
		}

		//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	}
}