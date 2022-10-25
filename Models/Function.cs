namespace Evolution.Models
{
	public static class Function	
	{
		public static readonly int Left = -10;
		public static readonly int Right = 53;

		public static int GetY(int x) => 62 - 1 * x - 86 * x * x + 2 * x * x * x;

		public static List<int> GetYPoints()
		{
			var points = new List<int>();
			for(int x = Left; x <= Right; x++)
			{
				var value= GetY(x);
				points.Add(value);	
			}
			return points;
		}

    }
}
