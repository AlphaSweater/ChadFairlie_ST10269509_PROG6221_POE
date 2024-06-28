using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Views.PopUpView
{
	/// <summary>
	/// Interaction logic for AddIngredientView.xaml
	/// </summary>
	public partial class AddIngredientView : Window
	{
		public Ingredient Ingredient { get; private set; }

		public AddIngredientView()
		{
			InitializeComponent();

			UnitComboBox.ItemsSource = RecipeService.UnitsOfMeasurement;
			FoodGroupComboBox.ItemsSource = RecipeService.FoodGroups;
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			string name = NameTextBox.Text.Trim();
			if (string.IsNullOrEmpty(name))
			{
				MessageBox.Show("Name is required.");
				return;
			}

			if (!double.TryParse(QuantityTextBox.Text, out double quantity) || quantity <= 0)
			{
				MessageBox.Show("Quantity must be a positive number.");
				return;
			}

			string unit = UnitComboBox.Text.Trim();
			if (string.IsNullOrEmpty(unit))
			{
				MessageBox.Show("Unit of Measurement is required.");
				return;
			}

			if (!double.TryParse(CaloriesTextBox.Text, out double calories) || calories <= 0)
			{
				MessageBox.Show("Calories must be a positive number.");
				return;
			}

			if (!(FoodGroupComboBox.SelectedItem is FoodGroup selectedFoodGroup))
			{
				MessageBox.Show("Food Group is required.");
				return;
			}

			var foodGroup = selectedFoodGroup;

			Ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
			DialogResult = true;
			Close();
		}
	}
}