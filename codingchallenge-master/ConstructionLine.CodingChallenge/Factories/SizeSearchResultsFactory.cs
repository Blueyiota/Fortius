using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge.Factories
{
	public class SizeSearchResultsFactory
	{
		public IList<SizeCount> Create(IList<Shirt> shirts)
		{
			var sizeCountFactory = new SizeCountFactory();

			var listOfCounts = new List<SizeCount>();
			foreach (var size in Size.All)
			{
				listOfCounts.Add(sizeCountFactory.Create(size, shirts.Where(m => m.Size == size).Count()));
			}

			return listOfCounts;
		}
	}
}
