using ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.View
{
	public partial class HomeView : UserControl
	{
		public HomeView()
		{
			InitializeComponent();
			DataContext = new HomeViewModel();
		}

		private Point _clickPosition;

		private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			var scrollViewer = sender as ScrollViewer;
			_clickPosition = e.GetPosition(scrollViewer);
			Mouse.Capture(scrollViewer);
		}

		private void OnPreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && sender is ScrollViewer scrollViewer)
			{
				var currentPosition = e.GetPosition(scrollViewer);
				var delta = currentPosition.X - _clickPosition.X;

				// Calculate the new horizontal offset
				var newHorizontalOffset = scrollViewer.HorizontalOffset - delta;

				// Clamp the new horizontal offset to be within the scrollable bounds
				newHorizontalOffset = Math.Max(0, Math.Min(newHorizontalOffset, scrollViewer.ScrollableWidth));

				scrollViewer.ScrollToHorizontalOffset(newHorizontalOffset);

				_clickPosition = currentPosition;
			}
		}

		private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (sender is ScrollViewer scrollViewer)
			{
				scrollViewer.ReleaseMouseCapture();
			}
		}
	}
}