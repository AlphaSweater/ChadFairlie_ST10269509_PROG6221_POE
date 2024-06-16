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
		private double _velocity;
		private DateTime _lastMoveTime;
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
				Mouse.Capture(RecentRecipeScrollViewer);
				_velocity = 0;
				_isScrolling = true;
				_lastMoveTime = DateTime.Now;
			}
		}

		private void OnPreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && sender is ScrollViewer)
			{
				var currentPosition = e.GetPosition(RecentRecipeScrollViewer);
				var delta = currentPosition.X - _clickPosition.X;

				RecentRecipeScrollViewer.ScrollToHorizontalOffset(_initialHorizontalOffset - delta);

				var currentTime = DateTime.Now;
				_velocity = delta / (currentTime - _lastMoveTime).TotalSeconds;
				_lastMoveTime = currentTime;
			}
		}

		private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (sender is ScrollViewer)
			{
				Mouse.Capture(null);
				_isScrolling = false;
				ApplyInertia();
			}
		}

		private void ApplyInertia()
		{
			double initialOffset = RecentRecipeScrollViewer.HorizontalOffset;
			double inertiaEffect = 0.1; // Reduced multiplier for a smoother effect
			double targetOffset = initialOffset - (_velocity * inertiaEffect);

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