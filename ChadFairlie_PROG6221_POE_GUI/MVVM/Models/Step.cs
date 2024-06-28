// Chad Fairlie
// ST10269509
// Group 2

//------------------------------------------------------------------------------------------------------------------------//

// References For This Class:
//      ChatGPT gave me the idea to make this into a class.

//------------------------------------------------------------------------------------------------------------------------//
using ChadFairlie_PROG6221_POE_GUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	// Represents a single step in a cooking recipe.
	public class Step : ObservableObject
	{
		// The detailed description of what the step involves.
		public string Description { get; set; }

		// Backing field to track whether the step has been completed.
		private bool _isCompleted;

		// Property to get or set the completion status of the step.
		// Notifies observers of changes to ensure UI updates.
		public bool IsCompleted
		{
			get => _isCompleted;
			set
			{
				if (_isCompleted != value)
				{
					_isCompleted = value;
					OnPropertyChanged(nameof(IsCompleted));
				}
			}
		}

		// Constructor to create a step with a specific description.
		// By default, a step is not completed upon creation.
		public Step(string description)
		{
			Description = description;
			_isCompleted = false;
		}
	}
}