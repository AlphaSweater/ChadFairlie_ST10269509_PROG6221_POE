using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChadFairlie_PROG6221_POE_GUI.MVVM.Models
{
	public class FoodGroup
	{
		public string Name { get; set; }
		public string Icon { get; set; }
		public string FormattedName => $"{Name} {Icon}";

		public FoodGroup(string name, string icon)
		{
			Name = name;
			Icon = icon;
		}

		public override string ToString()
		{
			return $"{Name} {Icon}";
		}
	}
}