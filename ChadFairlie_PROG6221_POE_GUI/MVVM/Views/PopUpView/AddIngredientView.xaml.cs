using ChadFairlie_PROG6221_POE_GUI.MVVM.Models;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System.Text;
using System.Windows;

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
			string errorMessage = ValidateInputs();

			if (!string.IsNullOrEmpty(errorMessage))
			{
				MessageBox.Show(errorMessage, "Input Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			Ingredient = new Ingredient(
				NameTextBox.Text.Trim(),
				double.Parse(QuantityTextBox.Text),
				UnitComboBox.Text.Trim(),
				double.Parse(CaloriesTextBox.Text),
				(FoodGroup)FoodGroupComboBox.SelectedItem
			);

			DialogResult = true;
			Close();
		}

		private string ValidateInputs()
		{
			StringBuilder errorMessages = new StringBuilder();

			string name = NameTextBox.Text.Trim();
			if (string.IsNullOrEmpty(name))
			{
				errorMessages.AppendLine("Name is required.");
			}

			if (!double.TryParse(QuantityTextBox.Text, out double quantity))
			{
				errorMessages.AppendLine("Quantity must be a number.");
			}
			else if (quantity <= 0)
			{
				errorMessages.AppendLine("Quantity must be a positive number.");
			}

			string unit = UnitComboBox.Text.Trim();
			if (string.IsNullOrEmpty(unit))
			{
				errorMessages.AppendLine("Unit of Measurement is required.");
			}

			if (!double.TryParse(CaloriesTextBox.Text, out double calories))
			{
				errorMessages.AppendLine("Calories must be a number.");
			}
			else if (calories <= 0)
			{
				errorMessages.AppendLine("Calories must be a positive number.");
			}

			if (!(FoodGroupComboBox.SelectedItem is FoodGroup))
			{
				errorMessages.AppendLine("Food Group is required.");
			}

			return errorMessages.ToString();
		}

		private void FoodGroupHelpButton_Click(object sender, RoutedEventArgs e)
		{
			// Toggle the visibility of the food group information section
			if (InfoSection.Visibility == Visibility.Collapsed)
			{
				// Close the units info section if it's open
				if (UnitsInfoSection.Visibility == Visibility.Visible)
				{
					UnitsInfoSection.Visibility = Visibility.Collapsed;
					UnitsHelpButton.Content = "?";
					UnitsHelpButton.ToolTip = "Click to learn more about each unit of measurement.";
				}

				InfoSection.Visibility = Visibility.Visible;
				InfoColumn.Width = new GridLength(1, GridUnitType.Auto);
				this.Width = 670;
				FoodGroupHelpButton.Content = "✖";
				FoodGroupHelpButton.ToolTip = "Click to collapse the food group information.";
			}
			else
			{
				InfoSection.Visibility = Visibility.Collapsed;
				InfoColumn.Width = new GridLength(0);
				this.Width = 360;
				FoodGroupHelpButton.Content = "?";
				FoodGroupHelpButton.ToolTip = "Click to learn more about each food group.";
			}
		}

		private void UnitsHelpButton_Click(object sender, RoutedEventArgs e)
		{
			// Toggle the visibility of the units of measurement information section
			if (UnitsInfoSection.Visibility == Visibility.Collapsed)
			{
				// Close the food group info section if it's open
				if (InfoSection.Visibility == Visibility.Visible)
				{
					InfoSection.Visibility = Visibility.Collapsed;
					FoodGroupHelpButton.Content = "?";
					FoodGroupHelpButton.ToolTip = "Click to learn more about each food group.";
				}

				UnitsInfoSection.Visibility = Visibility.Visible;
				InfoColumn.Width = new GridLength(1, GridUnitType.Auto);
				this.Width = 670;
				UnitsHelpButton.Content = "✖";
				UnitsHelpButton.ToolTip = "Click to collapse the units of measurement information.";
			}
			else
			{
				UnitsInfoSection.Visibility = Visibility.Collapsed;
				InfoColumn.Width = new GridLength(0);
				this.Width = 360;
				UnitsHelpButton.Content = "?";
				UnitsHelpButton.ToolTip = "Click to learn more about each unit of measurement.";
			}
		}
	}
}