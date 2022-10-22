namespace Evolution.Models
{
	public static class Function	
	{
		// От -128
		public static readonly int Left = -10;
		// До 127
		public static readonly int Right = 53;
		private static int? minX = null;
		public static int MinX
		{
			get {
				if (minX == null)
				{
					throw new ArgumentException("Минимум не вычисллен");
				}
				return minX.Value;
			}

		}
		public static int GetY(int x) => 62 - 1 * x - 86 * x * x + 2 * x * x * x;

		public static List<int> GetYPoints()
		{
			var points = new List<int>();
			minX=Left;
			var minY = GetY(minX.Value);
			points.Add(minY);
			for(int x = Left+1; x < Right; x++)
			{
				var value = GetY(x);
				if (value < minY)
				{
					minX = x;
					minY = value;
				}
				points.Add(value);	
			}
			return points;
		}

    }
}
