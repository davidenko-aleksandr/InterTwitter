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
			int m = pat.Length;
			int n = str.Length;
			int i = 0;
			int j = 0;
			int[] lps = new int[m];

			ComputeLPSArray(pat, m, lps);

			while (i < n)
			{
				if (pat[j] == str[i])
				{
					j++;
					i++;
				}

				if (j == m)
				{
					retVal.Add(i - j);
					j = lps[j - 1];
				}
				else if (i < n && pat[j] != str[i])
				{
					if (j != 0)
                    {
						j = lps[j - 1];
					}
					else
                    {
						i += 1;
					}
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
