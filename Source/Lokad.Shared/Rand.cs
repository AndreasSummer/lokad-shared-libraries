namespace System
{
	/// <summary>
	/// Helper class that allows to implement non-deterministic 
	/// reproducible testing.
	/// </summary>
	/// <remarks>
	/// Keep in mind, that this implementation is not thread-safe.
	/// </remarks>
	public static class Rand
	{
		static Func<int, int> _nextInt;
		static Func<Func<int, int>>  _activator;

		/// <summary>
		/// Resets everything to the default.
		/// </summary>
		public static void ResetToDefault()
		{
			_activator = () =>
			{
				var r = new Random();
				return i => r.Next(i);
			};
			_nextInt = _activator();
		}

		static Rand()
		{
			ResetToDefault();
		}

		/// <summary>
		/// Resets the random generator, using the provided activator
		/// </summary>
		public static void Reset()
		{
			_nextInt = _activator();
		}

		/// <summary>
		/// Overrides with the current activator
		/// </summary>
		/// <param name="activator">The activator.</param>
		public static void Reset(Func<Func<int, int>> activator)
		{
			_activator = activator;
			_nextInt = _activator();
		}

		/// <summary>
		/// Generates random value between 0 and <see cref="int.MaxValue"/> (exclusive)
		/// </summary>
		/// <returns>random integer</returns>
		public static int Next()
		{
			return _nextInt(int.MaxValue);
		}

		/// <summary>
		/// Generates random value between 0 and <paramref name="upperBound"/> (exclusive)
		/// </summary>
		/// <param name="upperBound">The upper bound.</param>
		/// <returns>random integer</returns>
		public static int Next(int upperBound)
		{
			return _nextInt(upperBound);
		}

		/// <summary>
		/// Generates random value between <paramref name="lowerBound"/>
		/// and <paramref name="upperBound"/> (exclusive)
		/// </summary>
		/// <param name="lowerBound">The lower bound.</param>
		/// <param name="upperBound">The upper bound.</param>
		/// <returns>random integer</returns>
		public static int Next(int lowerBound, int upperBound)
		{
			var range = upperBound - lowerBound;
			return _nextInt(range) + lowerBound;
		}

		/// <summary> Picks random item from the provided array </summary>
		/// <typeparam name="TItem">The type of the item.</typeparam>
		/// <param name="items">The items.</param>
		/// <returns>random item from the array</returns>
		public static TItem NextItem<TItem>(TItem[] items)
		{
			var index = _nextInt(items.Length);
			return items[index];
		}

		/// <summary> Picks random <see cref="Enum"/> </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>random Enum value</returns>
		public static TEnum NextEnum<TEnum>() where TEnum : struct
		{
			return NextItem(EnumUtil<TEnum>.Values);
		}

		/// <summary> Returns random double value with lowered precision </summary>
		/// <returns></returns>
		public static double NextDouble()
		{
			return (double) _nextInt(int.MaxValue)/int.MaxValue;
		}


		static readonly char[] _symbols = "!\"#%&'()*,-./:;?@[\\]_{} ".ToCharArray();
		

		/// <summary>
		/// Generates random string with the length between 
		/// <paramref name="lowerBound"/> and <paramref name="upperBound"/> (exclusive)
		/// </summary>
		/// <param name="lowerBound">The lower bound for the string length.</param>
		/// <param name="upperBound">The upper bound for the string length.</param>
		/// <returns>new random string</returns>
		public static string NextString(int lowerBound, int upperBound)
		{
			//const int surrogateStartsAt = 55296;
			int count = Next(lowerBound, upperBound);
			var array = Range.Array(count, i => Next(5) == 1 ? NextItem(_symbols) : (char) Next(48, 122));
			return new string(array);
		}
	}
}