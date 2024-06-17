// Chad Fairlie
// ST10269509
// Group 2

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

// References For This Class:
//		• https://www.youtube.com/watch?v=PzP8mw7JUzI

//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChadFairlie_PROG6221_POE_GUI.Core
{
	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
	// ObservableObject implements INotifyPropertyChanged to support automatic UI updates.
	public class ObservableObject : INotifyPropertyChanged
	{
		//------------------------------------------------------------------------------------------------------------------------//
		// Event declared from INotifyPropertyChanged interface.
		// It's triggered whenever a property value changes.
		public event PropertyChangedEventHandler PropertyChanged;

		//------------------------------------------------------------------------------------------------------------------------//
		// Method to call when a property value changes.
		// It triggers the PropertyChanged event, passing the name of the property that changed.
		// [CallerMemberName] attribute automatically captures the caller property name if not explicitly provided.
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			// If there are any subscribers to the PropertyChanged event, invoke the event.
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	//<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>//
}