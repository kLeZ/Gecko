using System;

namespace Gecko.Randomics
{
	public class Randomizer
	{
		public static string RandomString(string pattern, bool staticSeed)
		{
			uint seed = 33550336;
			if (!staticSeed) seed = 0;
			return RandomString(pattern, seed);
		}

		public static string RandomString(string pattern, uint seed)
		{

			MersenneTwister oRandom = null;
			if (seed == 0) oRandom = new MersenneTwister();
			else oRandom = new MersenneTwister(seed);
			return RandomString(oRandom, pattern);
		}

		public static string RandomString(MersenneTwister oRandom, string pattern)
		{
			string ret = "";
			//Dichiarazione delle costanti
			const char LNV = '@';
			//Consonante Minuscola
			const char UNV = '$';
			//Consonante Maiuscola
			const char LV = '!';
			//Vocale Minuscola
			const char UV = '&';
			//Vocale Maiuscola
			const char AL = '*';
			//Qualsiasi lettera minuscola
			const char AU = '-';
			//Qualsiasi lettera maiuscola
			const char I = '#';
			//Numero Intero
			const char OU = '^';
			//Una 'o' o una 'u'
			const char UOU = '>';
			//Una 'O' o una 'U'
			const char AE = '%';
			//Una 'a' o una 'e'
			const char UAE = '_';
			//Una 'A' o una 'E'
			//------------------------------------------------------------------
			//Dichiarazione array di caratteri
			char[] ArrayAlfabeto = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 
    'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
    'u', 'v', 'w', 'x', 'y', 'z' };
			char[] ArrayConsonanti = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 
    'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 
    'z' };
			char[] ArrayVocali = { 'a', 'e', 'i', 'o', 'u' };
			//------------------------------------------------------------------
			foreach (char character in pattern.ToCharArray())
			{
				switch (character)
				{
					case LNV:
						ret += ArrayConsonanti.GetValue(oRandom.Next(20)).ToString().ToLower();
						break;
					case UNV:
						ret += ArrayConsonanti.GetValue(oRandom.Next(20)).ToString().ToUpper();
						break;
					case LV:
						ret += ArrayVocali.GetValue(oRandom.Next(4)).ToString().ToLower();
						break;
					case UV:
						ret += ArrayVocali.GetValue(oRandom.Next(4)).ToString().ToUpper();
						break;
					case AL:
						ret += ArrayAlfabeto.GetValue(oRandom.Next(25)).ToString().ToLower();
						break;
					case AU:
						ret += ArrayAlfabeto.GetValue(oRandom.Next(25)).ToString().ToUpper();
						break;
					case I:
						ret += oRandom.Next(9);
						break;
					case OU:
						ret += ArrayVocali.GetValue(oRandom.Next(3, 4)).ToString().ToLower();
						break;
					case UOU:
						ret += ArrayVocali.GetValue(oRandom.Next(3, 4)).ToString().ToUpper();
						break;
					case AE:
						ret += ArrayVocali.GetValue(oRandom.Next(1)).ToString().ToLower();
						break;
					case UAE:
						ret += ArrayVocali.GetValue(oRandom.Next(1)).ToString().ToUpper();
						break;
					default:
						ret += character;
						break;
				}
			}
			return ret;
		}

		public static string RandomString(int Length)
		{
			string CharList = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return RandomString(CharList, Length);
		}

		public static string RandomString(string CharList, int Length)
		{
			string RndString = "";
			int i;

			Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));

			for (i = 1; i <= Length; i++)
				RndString += CharList.Substring(rdm.Next(CharList.Length - 1) + 1, 1);

			return RndString;
		}
	}
}
