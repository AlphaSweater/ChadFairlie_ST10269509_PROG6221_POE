using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	// Represents a food category with a name and an icon.
	public class FoodGroup
	{
		// The name of the food group.
		public string Name { get; set; }

		// The icon for the food group, e.g., an emoji or image path.
		public string Icon { get; set; }

		// Combines Name and Icon for display purposes.
		public string FormattedName => $"{Icon} | {Name}";

		// Constructor to set the food group's name and icon.
		public FoodGroup(string name, string icon)
		{
			Name = name;
			Icon = icon;
		}

		// Returns the food group's name and icon as a string.
		public override string ToString()
		{
			return $"{Icon} | {Name}";
		}
	}
}