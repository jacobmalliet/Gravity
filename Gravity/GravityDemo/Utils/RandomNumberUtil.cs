using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityDemo.Utils
{
	public class RandomNumberUtil
	{
		public static int RandomNumber() => RandomNumber(1, 9999);
		public static int RandomNumber(int min, int max)
		{
			var random = new Random();
			return random.Next(min, max);
		}
	}
}
