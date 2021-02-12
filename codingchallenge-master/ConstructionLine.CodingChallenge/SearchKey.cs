using System;

namespace ConstructionLine.CodingChallenge
{
	public class SearchKey
	{
		public Guid SearchColorId { get; set; }

		public Guid SearchSizeId { get; set; }

		public override bool Equals(object obj)
		{
			return obj is SearchKey key &&
				   SearchColorId.Equals(key.SearchColorId) &&
				   SearchSizeId.Equals(key.SearchSizeId);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(SearchColorId, SearchSizeId);
		}
	}
}
