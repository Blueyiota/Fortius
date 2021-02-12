namespace ConstructionLine.CodingChallenge.Factories
{
	public class SizeCountFactory
	{
		public SizeCount Create(Size size, int count)
		{
			return new SizeCount{ Size = size, Count = count};
		}
	}
}
