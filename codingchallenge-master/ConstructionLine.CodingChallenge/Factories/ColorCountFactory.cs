namespace ConstructionLine.CodingChallenge.Factories
{
	public class ColorCountFactory
	{
		public ColorCount Create(Color color, int count)
		{
			return new ColorCount{ Color = color, Count = count};
		}
	}
}
