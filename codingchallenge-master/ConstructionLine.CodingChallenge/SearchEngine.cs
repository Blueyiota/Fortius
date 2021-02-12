using ConstructionLine.CodingChallenge.Factories;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
	public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly ILookup<SearchKey, Shirt> _shirtsKeyed;

        /// <summary>
        /// Search Engine
        /// 
        /// Explanation of my changes - Neil Beeesley
        /// I chose to do this using an lookup collection as I felt this would be the most performant but not perhaps the most pretty (avg 5ms). If performance was not an issue, then I would have 
        /// probably used the IQueryable interface in Linq to support creating a dynamic Where clause, that could be built up easily (more readable) and perhaps more maintainable in the future with the 
        /// extension of future search options. There are many ways to do this but I chose to use an algorithm that employed O{1} as opposed to O{n} which is more synonymous with Linq.
        /// </summary>
        /// <param name="shirts"></param>
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

            // use O{1} as opposed to a simple linear search of O{n}. This should improve search time (consistently) on bigger sets but less performant on small sets
            _shirtsKeyed = _shirts.ToLookup(m => new SearchKey{ SearchColorId = m.Color.Id, SearchSizeId = m.Size.Id });
        }


        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.
            var searchCombinations = new List<SearchKey>();

            //determine the search
            if (options.Colors.Any())
            {
                // we have colors as criteria
                foreach(var searchColor in options.Colors)
			    {
                    // do we have any sizes as criteria also
                    if (options.Sizes.Any())
                    {
                        foreach(var searchSize in options.Sizes)
				        {
                            searchCombinations.Add(new SearchKey{ SearchColorId = searchColor.Id, SearchSizeId = searchSize.Id });
				        }
                    }
                    else
				    {
                        foreach (var searchSize in Size.All)
                        {
                            searchCombinations.Add(new SearchKey{ SearchColorId = searchColor.Id, SearchSizeId = searchSize.Id });
                        }
				    }
			    }
            }
            else
			{
                // only have sizes as criteria
                foreach(var searchSize in options.Sizes)
				{
                    foreach (var searchColor in Color.All)
                    {
                        searchCombinations.Add(new SearchKey{ SearchColorId = searchColor.Id, SearchSizeId = searchSize.Id });
                    }
				}
			}

            var shirtsFiltered = new List<Shirt>();
            foreach(var searchCombination in searchCombinations)
            {                
                shirtsFiltered.AddRange(_shirtsKeyed[searchCombination]);
			}

            var colorCounts = new ColorSearchResultsFactory().Create(shirtsFiltered).ToList();
            var sizeCounts = new SizeSearchResultsFactory().Create(shirtsFiltered).ToList();

            return new SearchResults
            {
                Shirts = shirtsFiltered,
                ColorCounts = colorCounts,
                SizeCounts = sizeCounts
            };
        }
    }
}