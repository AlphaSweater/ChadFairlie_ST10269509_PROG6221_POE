using ChadFairlie_PROG6221_POE_GUI.Core;
using System;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	public class Ingredient : ObservableObject
	{
		private string _name;

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

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

		private string _originalUnitOfMeasurement;

		public string OriginalUnitOfMeasurement
		{
			get => _originalUnitOfMeasurement;
			private set => SetProperty(ref _originalUnitOfMeasurement, value);
		}

		private double _preciseQuantity;

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

		public double Quantity => Math.Round(PreciseQuantity, 2);

		private double _originalQuantity;

		public double OriginalQuantity
		{
			get => _originalQuantity;
			private set => SetProperty(ref _originalQuantity, value);
		}

		private double _preciseCaloriesPerUnit;

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

		public double CaloriesPerUnit => Math.Round(PreciseCaloriesPerUnit, 2);

		private double _originalCaloriesPerUnit;

		public double OriginalCaloriesPerUnit
		{
			get => _originalCaloriesPerUnit;
			private set => SetProperty(ref _originalCaloriesPerUnit, value);
		}

		private FoodGroup _foodGroup;

		public FoodGroup FoodGroup
		{
			get => _foodGroup;
			set => SetProperty(ref _foodGroup, value);
		}

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

		public string FormattedCaloriesPerUnit => $"{CaloriesPerUnit:F2} kcal";

		// Parameterless constructor for easier binding
		public Ingredient()
		{
		}

		public Ingredient(string name, double quantity, string unit, double calories, FoodGroup foodGroup)
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

			OriginalUnitOfMeasurement = UnitOfMeasurement;
			OriginalQuantity = PreciseQuantity;
			OriginalCaloriesPerUnit = PreciseCaloriesPerUnit;
		}
	}
}