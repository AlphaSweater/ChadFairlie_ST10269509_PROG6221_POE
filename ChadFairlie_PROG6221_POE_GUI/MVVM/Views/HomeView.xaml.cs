// Ignore Spelling: PROG MVVM

using ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Views
{
	public partial class HomeView : UserControl
	{
		private Point _clickPosition;
		private double _initialHorizontalOffset;

		public HomeView()
		{
			InitializeComponent();
			//DataContext = new HomeViewModel();
			RecentRecipeScrollViewer.MouseWheel += OnPreviewMouseWheelScroll;
		}

		private void OnPreviewMouseWheelScroll(object sender, MouseWheelEventArgs e)
		{
			if (sender is ScrollViewer)
			{
				// Determine the direction and amount to scroll
				var offsetChange = e.Delta > 0 ? -50 : 50; // Adjust the value to control scroll speed

				// Adjust the horizontal offset based on the mouse wheel movement
				RecentRecipeScrollViewer.ScrollToHorizontalOffset(RecentRecipeScrollViewer.HorizontalOffset + offsetChange);

				// Prevent the event from bubbling further to avoid double handling
				e.Handled = true;
			}
		}

		private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is ScrollViewer)
			{
				_clickPosition = e.GetPosition(RecentRecipeScrollViewer);
				_initialHorizontalOffset = RecentRecipeScrollViewer.HorizontalOffset;
				Mouse.Capture(RecentRecipeScrollViewer);
			}
		}

		private void OnPreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && sender is ScrollViewer)
			{
				var currentPosition = e.GetPosition(RecentRecipeScrollViewer);
				var delta = currentPosition.X - _clickPosition.X;

				// Smoothly scroll to the new offset
				RecentRecipeScrollViewer.ScrollToHorizontalOffset(_initialHorizontalOffset - delta);
			}
		}

		private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (sender is ScrollViewer)
			{
				Mouse.Capture(null);
			}
		}
	}
}