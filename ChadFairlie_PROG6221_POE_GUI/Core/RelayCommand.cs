// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using System;
using System.Windows.Input;

namespace ChadFairlie_PROG6221_POE_GUI.Core
{
	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// RelayCommand is used to handle commands in the MVVM pattern.
	// It allows for binding UI actions to methods in the ViewModels.
	internal class RelayCommand<T> : ICommand
	{
		//------------------------------------------------------------------------------------------------------------------------//
		// Action to execute when the command is invoked.
		private readonly Action<T> _execute;

		// Function that determines whether the command can execute in its current state.
		private readonly Func<T, bool> _canExecute;

		//------------------------------------------------------------------------------------------------------------------------//
		// Event that is triggered when changes occur that affect whether the command should execute.
		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the command with the execute action and optionally a canExecute function.
		public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Determines whether the command can execute in its current state.
		public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

		//------------------------------------------------------------------------------------------------------------------------//
		// Executes the RelayCommand's action.
		public void Execute(object parameter) => _execute((T)parameter);
	}

	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// Parameterless version of the RelayCommand class.
	internal class RelayCommand : ICommand
	{
		// Action to execute when the command is invoked.
		private readonly Action _execute;

		// Function that determines whether the command can execute in its current state.
		private readonly Func<bool> _canExecute;

		//------------------------------------------------------------------------------------------------------------------------//
		// Event that is triggered when changes occur that affect whether the command should execute.
		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Constructor initializes the command with the execute action and a canExecute function.
		public RelayCommand(Action execute, Func<bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		//------------------------------------------------------------------------------------------------------------------------//
		// Determines whether the command can execute in its current state.
		public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

		//------------------------------------------------------------------------------------------------------------------------//
		// Executes the RelayCommand's action.
		public void Execute(object parameter) => _execute();
	}

	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
}