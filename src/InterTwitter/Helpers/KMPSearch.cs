using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Helpers
{
    public static class KMPSearch
    {
		public static int[] SearchString(string str, string pat)
		{
			List<int> retVal = new List<int>();
			int M = pat.Length;
			int N = str.Length;
			int i = 0;
			int j = 0;
			int[] lps = new int[M];

			ComputeLPSArray(pat, M, lps);

			while (i < N)
			{
				if (pat[j] == str[i])
				{
					j++;
					i++;
				}

				if (j == M)
				{
					retVal.Add(i - j);
					j = lps[j - 1];
				}

				else if (i < N && pat[j] != str[i])
				{
					if (j != 0)
						j = lps[j - 1];
					else
						i = i + 1;
				}
			}

			return retVal.ToArray();
		}

		private static void ComputeLPSArray(string pat, int m, int[] lps)
		{
			int len = 0;
			int i = 1;

			lps[0] = 0;

			while (i < m)
			{
				if (pat[i] == pat[len])
				{
					len++;
					lps[i] = len;
					i++;
				}
				else
				{
					if (len != 0)
					{
						len = lps[len - 1];
					}
					else
					{
						lps[i] = 0;
						i++;
					}
				}
			}
		}

	}
}
