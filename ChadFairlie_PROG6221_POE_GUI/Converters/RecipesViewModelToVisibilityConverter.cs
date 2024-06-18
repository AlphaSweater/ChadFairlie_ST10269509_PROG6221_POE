using System;
using System.Globalization;
using System.Windows.Data;
using ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels;

namespace ChadFairlie_PROG6221_POE_GUI.Converters
{
	public class RecipesViewModelToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is RecipesViewModel)
				return System.Windows.Visibility.Visible;
			return System.Windows.Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}