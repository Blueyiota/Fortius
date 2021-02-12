using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineTestsBase
    {
        protected static void AssertResults(List<Shirt> shirts, SearchOptions options)
        {
            Assert.That(shirts, Is.Not.Null);

            var resultingShirtIds = shirts.Select(s => s.Id).ToList();
            var sizeIds = options.Sizes.Select(s => s.Id).ToList();
            var colorIds = options.Colors.Select(c => c.Id).ToList();

            foreach (var shirt in shirts)
            {
                if (sizeIds.Contains(shirt.Size.Id)
                    && colorIds.Contains(shirt.Color.Id)
                    && !resultingShirtIds.Contains(shirt.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }


        protected static void AssertSizeCounts(List<Shirt> shirts, SearchOptions searchOptions, List<SizeCount> sizeCounts)
        {
			Assert.That(sizeCounts, Is.Not.Null);

			var sizesThatHaveCounts = searchOptions.Sizes;
			if (!searchOptions.Sizes.Any())
			{
				// go through them all because there is no criteria set for sizes
				sizesThatHaveCounts = Size.All;
			}
			else
			{
				// check the zero counts if criteria for size have been supplied
				foreach (var size in Size.All.Where(m => !searchOptions.Sizes.Contains(m)))
				{
					var sizeCount = sizeCounts.SingleOrDefault(s => s.Size.Id == size.Id);
					Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size.Name}' not found in results");

					Assert.That(sizeCount.Count, Is.EqualTo(0),
						$"Size count for '{sizeCount.Size.Name}' showing '{sizeCount.Count}' should be '0'");
				}
			}

			// check sizes that should have counts
			foreach (var size in sizesThatHaveCounts)
			{
				var sizeCount = sizeCounts.SingleOrDefault(s => s.Size.Id == size.Id);
				Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size.Name}' not found in results");

				var expectedSizeCount = shirts
					.Count(s => s.Size.Id == size.Id
								&& (!searchOptions.Colors.Any() || searchOptions.Colors.Select(c => c.Id).Contains(s.Color.Id)));

				Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount),
					$"Size count for '{sizeCount.Size.Name}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
			}
		}


		protected static void AssertColorCounts(List<Shirt> shirts, SearchOptions searchOptions, List<ColorCount> colorCounts)
        {
			Assert.That(colorCounts, Is.Not.Null);

			var colorsThatHaveCounts = searchOptions.Colors;
			if (!searchOptions.Colors.Any())
			{
				// go through them all because there is no criteria set for colors
				colorsThatHaveCounts = Color.All;
			}
			else
			{
				// check the zero counts if criteria for colors have been supplied
				foreach (var color in Color.All.Where(m => !searchOptions.Colors.Contains(m)))
				{
					var colorCount = colorCounts.SingleOrDefault(s => s.Color.Id == color.Id);
					Assert.That(colorCount, Is.Not.Null, $"Color count for '{color.Name}' not found in results");

					Assert.That(colorCount.Count, Is.EqualTo(0),
						$"Color count for '{colorCount.Color.Name}' showing '{colorCount.Count}' should be '0'");
				}
			}

			// check colors that should have counts
			foreach (var color in colorsThatHaveCounts)
			{
				var colorCount = colorCounts.SingleOrDefault(s => s.Color.Id == color.Id);
				Assert.That(colorCount, Is.Not.Null, $"Color count for '{color.Name}' not found in results");

				var expectedColorCount = shirts
					.Count(c => c.Color.Id == color.Id
								&& (!searchOptions.Sizes.Any() || searchOptions.Sizes.Select(s => s.Id).Contains(c.Size.Id)));

				Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount),
					$"Color count for '{colorCount.Color.Name}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
			}
		}
    }
}