using ChadFairlie_PROG6221_POE_GUI.MVVM.ViewModels;
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

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Views
{
	/// <summary>
	/// Interaction logic for DetailedRecipeView.xaml
	/// </summary>
	public partial class DetailedRecipeView : UserControl
	{
		public DetailedRecipeView()
		{
			InitializeComponent();

			// Subscribe to the Loaded event of the UserControl.
			this.Loaded += DetailedRecipeView_Loaded;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// This method is called when the UserControl is loaded.
		private void DetailedRecipeView_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.DataContext is DetailedRecipeViewModel viewModel)
			{
				viewModel.UpdateLastAccessed();
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// This method is called when the user clicks on the border of a step.
		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is Border border)
			{
				var grid = border.Child as Grid;
				var checkBox = grid.Children.OfType<CheckBox>().FirstOrDefault();

				if (checkBox != null)
				{
					checkBox.IsChecked = !checkBox.IsChecked;
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// This method is called when the user clicks on the Reset Progress button.
		private void ResetProgress_Click(object sender, RoutedEventArgs e)
		{
			// Scroll to the top of the ScrollViewer
			StepsScrollViewer.ScrollToTop();
		}
	}
}