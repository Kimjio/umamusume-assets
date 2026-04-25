using System;

namespace AnimateToUnity
{
	public class AnUtilityString
	{
		public static bool IsEmptyString(string targetString)
		{
			return targetString == null || targetString == string.Empty;
		}

		public static void ReplaceString(string replaceString, ref string originalString)
		{
			if (AnUtilityString.IsEmptyString(replaceString))
			{
				return;
			}
			originalString = replaceString;
		}

		public static string GetNumberString(int value, int digit)
		{
			string text = "";
			if (digit == 0)
			{
				return value.ToString();
			}
			float absValue = AnUtilityValue.GetAbsValue((float)value);
			float absValue2 = AnUtilityValue.GetAbsValue((float)value);
			int digit2 = AnUtilityValue.GetDigit(absValue);
			if (digit < digit2)
			{
				for (int i = 0; i < digit; i++)
				{
					text += "9";
				}
			}
			else
			{
				for (int j = 0; j < digit - digit2; j++)
				{
					text += "0";
				}
				text += absValue.ToString();
			}
			if (absValue2 < 0f)
			{
				text = "-" + text;
			}
			return text;
		}
	}
}
