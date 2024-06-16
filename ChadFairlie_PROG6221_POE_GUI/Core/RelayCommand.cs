// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// Ignore Spelling: PROG

using System;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.Core
{
	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	internal class RelayCommand : ICommand
	{
		//------------------------------------------------------------------------------------------------------------------------//
		private readonly Action<object> _execute;

		private readonly Func<object, bool> _canExecute;

		//------------------------------------------------------------------------------------------------------------------------//
		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

		//------------------------------------------------------------------------------------------------------------------------//
		public void Execute(object parameter) => _execute(parameter);

		//------------------------------------------------------------------------------------------------------------------------//
	}

	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
}