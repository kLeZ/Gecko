using System;
using System.Diagnostics;

namespace Gecko.Randomics
{
	//
	// An implementation of the Mersenne Twister algorithm (MT19937), developed
	// with reference to the C code written by Takuji Nishimura and Makoto Matsumoto
	// (http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html).
	//
	// This code is free to use for any pupose.
	//

	///
	/// A random number generator with a uniform distribution using the Mersenne
	/// Twister algorithm.
	///
	public class MersenneTwister
	{
		private const int N = 624;
		private const int M = 397;
		private const uint MATRIX_A = 0x9908b0dfu;
		private const uint UPPER_MASK = 0x80000000u;
		private const uint LOWER_MASK = 0x7fffffffu;

		private uint[] mt = new uint[N];
		private int mti = N + 1;

		/// <summary>
		/// Create a new Mersenne Twister random number generator.
		/// </summary>
		public MersenneTwister()
			: this((uint)DateTime.Now.Millisecond)
		{
		}

		/// <summary>
		/// Create a new Mersenne Twister random number generator with a
		/// particular seed.
		/// </summary>
		/// <param name="seed">The seed for the generator.</param>
		public MersenneTwister(uint seed)
		{
			mt[0] = seed;
			for (mti = 1; mti <= N - 1; mti++)
			{
				mt[mti] = (uint)(1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + (uint)mti) & 0xffffffffU;
			}
		}

		/// <summary>
		/// Create a new Mersenne Twister random number generator with a
		/// particular initial key.
		/// </summary>
		/// <param name="initialKey">The initial key.</param>
		public MersenneTwister(uint[] initialKey)
			: this(19650218u)
		{

			int i = 0;
			int j = 0;
			int k = 0;
			i = 1;
			j = 0;
			k = (int)(N > initialKey.Length ? N : initialKey.Length);

			for (; k >= 1; k += -1)
			{
				mt[i] = (uint)((mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525uL)) + initialKey[j] + (uint)j) & 0xffffffffu;
				i += 1;
				j += 1;
				if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
				if (j >= initialKey.Length) j = 0;
			}

			for (k = N - 1; k >= 1; k += -1)
			{
				mt[i] = (uint)((mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941uL)) - (uint)i) & 0xffffffffu;
				i += 1;
				if (i >= N) { mt[0] = mt[N - 1]; i = 1; }
			}

			mt[0] = 0x80000000u;
		}

		/// <summary>
		/// Generates a random number between 0 and System.UInt32.MaxValue.
		/// </summary>
		public uint NextUInt32()
		{
			uint y = 0;

			if (mti >= N)
			{
				int kk = 0;

				Debug.Assert(mti != N + 1, "Failed initialization");

				for (kk = 0; kk <= N - M - 1; kk++)
				{
					y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
					mt[kk] = mt[kk + M] ^ (y >> 1) ^ static_NextUInt32_mag01[(int)y & 0x1];
				}

				for (; kk <= N - 2; kk++)
				{
					y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
					mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ static_NextUInt32_mag01[(int)y & 0x1];
				}

				y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
				mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ static_NextUInt32_mag01[(int)y & 0x1];

				mti = 0;
			}

			y = mt[mti];
			mti += 1;

			// Tempering
			y = y ^ (y >> 11);
			y = y ^ ((y << 7) & 0x9d2c5680u);
			y = y ^ ((y << 15) & 0xefc60000u);
			y = y ^ (y >> 18);

			return y;
		}
		static uint[] static_NextUInt32_mag01 = { 0x0u, MATRIX_A };

		/// <summary>
		/// Generates a random integer between 0 and System.Int32.MaxValue.
		/// </summary>
		public int Next()
		{
			return (int)NextUInt32() >> 1;
		}

		/// <summary>
		/// Generates a random integer between 0 and maxValue.
		/// </summary>
		/// <param name="maxValue">The maximum value. Must be greater than zero.</param>
		public int Next(int maxValue)
		{
			return Next(0, maxValue);
		}

		/// <summary>
		/// Generates a random integer between minValue and maxValue.
		/// </summary>
		/// <param name="maxValue">The lower bound.</param>
		/// <param name="minValue">The upper bound.</param>
		public int Next(int minValue, int maxValue)
		{
			return (int)Math.Floor((maxValue - minValue + 1) * NextDouble() + minValue);
		}

		/// <summary>
		/// Generates a random floating point number between 0 and 1.
		/// </summary>
		public double NextDouble()
		{
			return NextUInt32() * (1.0 / 4294967295.0);
		}
	}
}
