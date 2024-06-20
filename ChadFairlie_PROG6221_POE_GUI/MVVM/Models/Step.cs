using ChadFairlie_PROG6221_POE_GUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	public class Step : ObservableObject
	{
		public string Description { get; set; }

		private bool _isCompleted;

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

		public Step(string description)
		{
			Description = description;
			_isCompleted = false;
		}
	}
}