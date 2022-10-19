namespace Evolution.Models
{
	public static class Function	
	{
		public static double GetY(double x) => 62 - 1 * x - 86 * x * x + 2 * x * x * x;

		public static double GetMinimum(double x1,double x2)
		{
			var minimum = GetY(x1);
			for (double i = x1+1; i <= x2; x1++)
			{
				if(minimum<GetY(i)) minimum = GetY(i);
			}
			return minimum;
		}

		public static double GetMaximum(double x1, double x2)
		{
            var maximum = GetY(x1);
            for (double i = x1 + 1; i <= x2; x1++)
            {
                if (maximum > GetY(i)) maximum = GetY(i);
            }
            return maximum;
        }

    }
}
