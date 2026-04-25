using System;
using System.Collections;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnLog
	{
		private static void _Initialize()
		{
			if (!AnMonoSingleton<AnRootManager>.Instance.UseDebugLog)
			{
				return;
			}
			if (AnLog._initialized)
			{
				return;
			}
			if (AnLog._titleTable == null)
			{
				AnLog._titleTable = new Hashtable();
			}
			AnLog._titleTable.Clear();
			int num = Enum.GetNames(typeof(AnLogTitleTypes)).Length;
			for (int i = 0; i < num; i++)
			{
				Hashtable titleTable = AnLog._titleTable;
				object obj = (AnLogTitleTypes)i;
				AnLogTitleTypes anLogTitleTypes = (AnLogTitleTypes)i;
				titleTable.Add(obj, anLogTitleTypes.ToString());
			}
			if (AnLog._logTable == null)
			{
				AnLog._logTable = new Hashtable();
			}
			AnLog._logTable.Clear();
			int num2 = Enum.GetNames(typeof(AnLogTypes)).Length;
			for (int j = 0; j < num2; j++)
			{
				Hashtable logTable = AnLog._logTable;
				object obj2 = (AnLogTypes)j;
				AnLogTypes anLogTypes = (AnLogTypes)j;
				logTable.Add(obj2, anLogTypes.ToString().Replace("__s__", " "));
			}
			if (AnLog._colorTable == null)
			{
				AnLog._colorTable = new Hashtable();
			}
			AnLog._colorTable.Clear();
			int num3 = Enum.GetNames(typeof(AnLogTypes)).Length;
			for (int k = 0; k < num3; k++)
			{
				Hashtable colorTable = AnLog._colorTable;
				object obj3 = (AnLogColorTypes)k;
				AnLogColorTypes anLogColorTypes = (AnLogColorTypes)k;
				colorTable.Add(obj3, anLogColorTypes.ToString().Replace("color_", "#"));
			}
			AnLog._initialized = true;
		}

		public static void _Log(AnLogTypes logType, AnLogColorTypes colorType, AnLogTitleTypes titleType, GameObject target)
		{
		}

		public static void _Log(AnLogTypes logType, string colorString, string title, GameObject target)
		{
		}

		public static void _Log(string content, string colorString, string title, GameObject target)
		{
		}

		private static bool _initialized;

		private static Hashtable _logTable;

		private static Hashtable _colorTable;

		private static Hashtable _titleTable;
	}
}
