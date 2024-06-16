using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Diagnostics;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.View
{
	public partial class HomeView : UserControl
	{
		private Point _clickPosition;
		private double _initialHorizontalOffset;
		private double _accumulatedOffset;
		private double _lastVelocity;
		private DateTime _lastMouseDownTime;
		private bool _isScrolling;

		public HomeView()
		{
			InitializeComponent();
		}

		private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is ScrollViewer)
			{
				_clickPosition = e.GetPosition(RecentRecipeScrollViewer);
				_initialHorizontalOffset = RecentRecipeScrollViewer.HorizontalOffset;
				_accumulatedOffset = 0;
				_lastVelocity = 0;
				_isScrolling = true;
				_lastMouseDownTime = DateTime.Now;
				Mouse.Capture(RecentRecipeScrollViewer);
			}
		}

		private void OnPreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && sender is ScrollViewer)
			{
				var currentPosition = e.GetPosition(RecentRecipeScrollViewer);
				var delta = currentPosition.X - _clickPosition.X;

				RecentRecipeScrollViewer.ScrollToHorizontalOffset(_initialHorizontalOffset - delta);

				// Only update velocity and accumulated offset if within the same drag session
				if (_isScrolling)
				{
					var currentTime = DateTime.Now;
					var elapsedMilliseconds = (currentTime - _lastMouseDownTime).TotalMilliseconds;

					// Calculate velocity based on distance moved and time elapsed
					_lastVelocity = delta / elapsedMilliseconds * 1000; // Convert to pixels per second
					_accumulatedOffset += delta;
				}
			}
		}

		private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (sender is ScrollViewer)
			{
				Mouse.Capture(null);
				_isScrolling = false;

				// Apply inertia only if drag duration was less than 0.3 seconds and there's accumulated offset
				if (_accumulatedOffset != 0 && (DateTime.Now - _lastMouseDownTime).TotalMilliseconds < 400)
				{
					ApplyInertia();
				}
			}
		}

		private void ApplyInertia()
		{
			double initialOffset = RecentRecipeScrollViewer.HorizontalOffset;
			double inertiaMultiplier = 0.05; // Adjust as needed for desired scroll speed

			// Calculate target offset based on accumulated velocity
			double targetOffset = initialOffset - _accumulatedOffset * inertiaMultiplier;

			// Ensure targetOffset is within valid range
			targetOffset = Math.Max(0, Math.Min(targetOffset, RecentRecipeScrollViewer.ScrollableWidth));

			var animation = new DoubleAnimation
			{
				From = initialOffset,
				To = targetOffset,
				Duration = new Duration(TimeSpan.FromSeconds(0.5)),
				EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
			};

			animation.Completed += (s, e) =>
			{
				RecentRecipeScrollViewer.ScrollToHorizontalOffset(targetOffset);
			};

			RecentRecipeScrollViewer.BeginAnimation(ScrollViewerBehavior.HorizontalOffsetProperty, animation);
		}
	}

	public static class ScrollViewerBehavior
	{
		public static readonly DependencyProperty HorizontalOffsetProperty =
			DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(ScrollViewerBehavior),
			new PropertyMetadata(0.0, OnHorizontalOffsetChanged));

		public static double GetHorizontalOffset(DependencyObject obj)
		{
			return (double)obj.GetValue(HorizontalOffsetProperty);
		}

		public static void SetHorizontalOffset(DependencyObject obj, double value)
		{
			obj.SetValue(HorizontalOffsetProperty, value);
		}

		private static void OnHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is ScrollViewer viewer)
			{
				viewer.ScrollToHorizontalOffset((double)e.NewValue);
			}
		}
	}
}