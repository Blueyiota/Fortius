using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge.Factories
{
	public class ColorSearchResultsFactory
	{
		public IList<ColorCount> Create(IList<Shirt> shirts)
		{
			var colorCountFactory = new ColorCountFactory();

			var listOfCounts = new List<ColorCount>();
			foreach (var color in Color.All)
			{
				listOfCounts.Add(colorCountFactory.Create(color, shirts.Where(m => m.Color == color).Count()));
			}

			return listOfCounts;
		}
	}
}
