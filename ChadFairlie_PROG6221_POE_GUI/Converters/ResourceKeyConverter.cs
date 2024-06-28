using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ChadFairlie_PROG6221_POE_GUI.Converters
{
	public class ResourceKeyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string resourceKey)
			{
				return Application.Current.TryFindResource(resourceKey);
			}
			return DependencyProperty.UnsetValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// ConvertBack is not supported for this converter.
			throw new NotSupportedException();
		}
	}
}