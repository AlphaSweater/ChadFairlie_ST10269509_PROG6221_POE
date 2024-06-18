// Ignore Spelling: PROG

using ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels;
using ChadFairlie_PROG6221_POE_GUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChadFairlie_PROG6221_POE_GUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void MaximizeButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
		}

		private void MinimizeButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// Check if the left mouse button is pressed to initiate dragging
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				this.DragMove();
			}
		}
	}
}