using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnUtility
	{
		public static AnRoot GetRoot(GameObject rootObject, bool fromChildren = false)
		{
			if (!fromChildren)
			{
				AnRoot anRoot = rootObject.GetComponentInParent<AnRoot>();
				if (anRoot != null)
				{
					return anRoot;
				}
				return rootObject.GetComponentInChildren<AnRoot>();
			}
			else
			{
				AnRoot anRoot = rootObject.GetComponentInChildren<AnRoot>();
				if (anRoot != null)
				{
					return anRoot;
				}
				return rootObject.GetComponentInParent<AnRoot>();
			}
		}

		public static GameObject Find(GameObject rootObject, string path, bool fullMatch = false)
		{
			if (path == "" || path == null)
			{
				return rootObject;
			}
			string[] array = path.Split(new string[] { "/" }, StringSplitOptions.None);
			GameObject gameObject = rootObject;
			foreach (string text in array)
			{
				gameObject = AnUtility.FindLoop(gameObject, text, 10, fullMatch);
				if (gameObject == null)
				{
					break;
				}
			}
			return gameObject;
		}

		public static T Find<T>(GameObject rootObject, string path, bool fullMatch = false) where T : Component
		{
			if (path == "" || path == null)
			{
				return rootObject.GetComponent<T>();
			}
			string[] array = path.Split(new string[] { "/" }, StringSplitOptions.None);
			GameObject gameObject = rootObject;
			foreach (string text in array)
			{
				gameObject = AnUtility.FindLoop(gameObject, text, 10, fullMatch);
				if (gameObject == null)
				{
					break;
				}
			}
			if (gameObject == null)
			{
				return default(T);
			}
			return gameObject.GetComponent<T>();
		}

		public static T FindUI<T>(AnRoot flRoot, GameObject rootObject, string path, bool fullMatch = false) where T : AnBase
		{
			GameObject gameObject = AnUtility.Find(rootObject, path, fullMatch);
			if (gameObject == null)
			{
				return default(T);
			}
			if (!flRoot.DataTable.ContainsKey(gameObject))
			{
				return default(T);
			}
			T t = flRoot.DataTable[gameObject] as T;
			if (t == null)
			{
				return default(T);
			}
			return t;
		}

		private static GameObject FindLoop(GameObject rootObject, string name, int searchDepth, bool fullMatch)
		{
			GameObject gameObject = null;
			bool flag = false;
			if (searchDepth < 0)
			{
				return null;
			}
			if (!fullMatch)
			{
				if (rootObject.name.Contains(name))
				{
					return rootObject;
				}
			}
			else if (rootObject.name == name)
			{
				return rootObject;
			}
			foreach (object obj in rootObject.transform)
			{
				Transform transform = (Transform)obj;
				if (!(transform == rootObject.transform))
				{
					if (!fullMatch)
					{
						if (transform.name.Contains(name))
						{
							gameObject = transform.gameObject;
							flag = true;
							break;
						}
					}
					else if (transform.name == name)
					{
						gameObject = transform.gameObject;
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				return gameObject;
			}
			foreach (object obj2 in rootObject.transform)
			{
				Transform transform2 = (Transform)obj2;
				if (!(rootObject.transform == transform2))
				{
					gameObject = AnUtility.FindLoop(transform2.gameObject, name, searchDepth - 1, fullMatch);
					if (gameObject != null)
					{
						break;
					}
				}
			}
			return gameObject;
		}

		public static string GetObjectPath(GameObject target, GameObject rootObject, bool withoutUIObj = false, bool start = true)
		{
			if (target == null)
			{
				return "";
			}
			string text = target.name;
			if (target.transform.parent != null)
			{
				GameObject gameObject = target.transform.parent.gameObject;
				string text2 = target.name;
				if (target.name.Length > 5)
				{
					text2 = target.name.Substring(0, target.name.Length - 3);
				}
				bool flag = false;
				foreach (object obj in gameObject.transform)
				{
					Transform transform = (Transform)obj;
					if (!(transform == gameObject.transform) && !(transform == target.transform) && transform.name.IndexOf(text2) == 0)
					{
						flag = true;
						break;
					}
				}
				string text3 = AnUtility.GetObjectPath(gameObject, rootObject, withoutUIObj, false);
				string text4 = "/" + target.name;
				string text5 = AnValue.ObjectPrefix;
				if (!withoutUIObj)
				{
					text5 += "object";
				}
				if (target.name == AnValue.RootName)
				{
					text3 = "";
					text4 = "";
				}
				else if (rootObject == target)
				{
					text3 = "";
					text4 = "";
				}
				else if (target.name == AnValue.ObjectOffsetName)
				{
					if (!start)
					{
						text4 = "";
					}
				}
				else if (target.name.IndexOf(text5) == 0 && !start && !flag)
				{
					text4 = "";
				}
				text = text3 + text4;
			}
			if (text.IndexOf("/") == 0)
			{
				text = text.Substring(1);
			}
			return text;
		}
	}
}
