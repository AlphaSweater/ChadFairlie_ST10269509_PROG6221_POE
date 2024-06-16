using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.View
{
	public partial class HomeView : UserControl
	{
		private Point _clickPosition;
		private TranslateTransform _translateTransform;
		private double _initialHorizontalOffset;

		public HomeView()
		{
			InitializeComponent();
			_translateTransform = new TranslateTransform();
			ContentGrid.RenderTransform = _translateTransform;
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
				_translateTransform.X = 0;
				_initialHorizontalOffset = RecentRecipeScrollViewer.HorizontalOffset;
			}
		}
	}
}