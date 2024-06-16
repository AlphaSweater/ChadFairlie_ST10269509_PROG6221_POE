// Ignore Spelling: PROG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ChadFairlie_PROG6221_POE_GUI.Behaviors
{
	public static class ScrollViewerBehavior
	{
		public static readonly DependencyProperty HorizontalOffsetProperty =
			DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(ScrollViewerBehavior),
				new UIPropertyMetadata(0.0, OnHorizontalOffsetChanged));

		public static void SetHorizontalOffset(DependencyObject target, double value)
		{
			target.SetValue(HorizontalOffsetProperty, value);
		}

		public static double GetHorizontalOffset(DependencyObject target)
		{
			return (double)target.GetValue(HorizontalOffsetProperty);
		}

		private static void OnHorizontalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
		{
			var scrollViewer = target as ScrollViewer;
			if (scrollViewer != null)
			{
				scrollViewer.ScrollToHorizontalOffset((double)e.NewValue);
			}
		}
	}
}