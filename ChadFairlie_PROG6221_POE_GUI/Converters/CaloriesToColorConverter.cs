using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ChadFairlie_PROG6221_POE_GUI.Converters
{
	public class CaloriesToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double calories)
			{
				if (calories > 500)
				{
					return Brushes.Red;
				}
				else if (calories > 300)
				{
					return Brushes.Yellow;
				}
				else if (calories > 150)
				{
					return Brushes.Green;
				}
				else
				{
					return Brushes.Blue;
				}
			}
			return Brushes.White;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}