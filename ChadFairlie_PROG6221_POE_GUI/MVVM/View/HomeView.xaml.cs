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

				// Reduce sensitivity of the drag scroll
				delta *= 1.0;

				// Translate the content smoothly
				_translateTransform.X = delta;

				// Determine if we need to apply elasticity
				if (_initialHorizontalOffset + delta < 0 || _initialHorizontalOffset + delta > RecentRecipeScrollViewer.ScrollableWidth)
				{
					// Apply a reduced delta to simulate elasticity
					delta *= 0.2;
				}

				// Smoothly scroll to the new offset
				RecentRecipeScrollViewer.ScrollToHorizontalOffset(_initialHorizontalOffset - delta);
			}
		}

		private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (sender is ScrollViewer)
			{
				Mouse.Capture(null);

				// Animate the translation back to the original position if necessary
				if (_translateTransform.X != 0)
				{
					var animation = new DoubleAnimation
					{
						To = 0,
						Duration = TimeSpan.FromMilliseconds(400),
						EasingFunction = new ElasticEase { EasingMode = EasingMode.EaseOut, Oscillations = 1, Springiness = 2 }
					};
					animation.Completed += (s, a) => ResetTransform(); // Reset transform and update offset
					_translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);
				}
			}
		}

		private void ResetTransform()
		{
			_translateTransform.X = 0;
			_initialHorizontalOffset = RecentRecipeScrollViewer.HorizontalOffset;
		}
	}
}