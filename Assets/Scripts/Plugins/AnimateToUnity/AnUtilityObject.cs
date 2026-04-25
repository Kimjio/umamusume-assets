using System;
using System.Collections.Generic;
using AnimateToUnity.Utility;
using UnityEngine;
using UnityEngine.Rendering;

namespace AnimateToUnity
{
	public class AnUtilityObject
	{
		public static T FindComponent<T>(GameObject rootObject, string path, bool fullMatch = false) where T : Component
		{
			if (path == "" || path == null)
			{
				return rootObject.GetComponent<T>();
			}
			string[] array = path.Split(new string[] { "/" }, StringSplitOptions.None);
			GameObject gameObject = rootObject;
			foreach (string text in array)
			{
				gameObject = AnUtilityObject.FindGameObjectLoop(gameObject, text, 10, fullMatch);
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

		public static T FindInstance<T>(AnRoot flRoot, GameObject rootObject, string path, bool fullMatch = false) where T : AnBase
		{
			GameObject gameObject = AnUtilityObject.FindGameObject(rootObject, path, fullMatch);
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

		public static List<T> FindInstancesInChildren<T>(AnRoot flRoot, GameObject rootObject) where T : AnBase
		{
			if (flRoot == null)
			{
				return null;
			}
			if (rootObject == null)
			{
				return null;
			}
			Transform[] componentsInChildren = rootObject.GetComponentsInChildren<Transform>(true);
			if (componentsInChildren.Length == 0)
			{
				return null;
			}
			List<T> list = new List<T>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				T t = flRoot.DataTable[componentsInChildren[i].gameObject] as T;
				if (t != null)
				{
					list.Add(t);
				}
			}
			return list;
		}

		public static T FindUIInstance<T>(AnRoot flRoot, GameObject rootObject, string path, bool fullMatch = false) where T : AnUIBase
		{
			AnObject anObject = AnUtilityObject.FindInstance<AnObject>(flRoot, rootObject, path, fullMatch);
			if (anObject == null)
			{
				return default(T);
			}
			AnComponentBase component = anObject.GameObject.GetComponent<AnComponentBase>();
			if (component == null)
			{
				return default(T);
			}
			T t = component.UIBase as T;
			if (t == null)
			{
				return default(T);
			}
			return t;
		}

		public static T FindAndInitUIInstance<T>(AnRoot flRoot, GameObject rootObject, string path, bool fullMatch = false) where T : AnUIBase, new()
		{
			AnUIBase anUIBase = AnUtilityObject.FindUIInstance<AnUIBase>(flRoot, rootObject, path, fullMatch);
			if (anUIBase == null)
			{
				return default(T);
			}
			return anUIBase.ComponentBase.Initialize<T>();
		}

		public static GameObject FindGameObject(GameObject rootObject, string path, bool fullMatch = false)
		{
			if (path == "" || path == null)
			{
				return rootObject;
			}
			string[] array = path.Split(new string[] { "/" }, StringSplitOptions.None);
			GameObject gameObject = rootObject;
			foreach (string text in array)
			{
				gameObject = AnUtilityObject.FindGameObjectLoop(gameObject, text, 10, fullMatch);
				if (gameObject == null)
				{
					break;
				}
			}
			return gameObject;
		}

		private static GameObject FindGameObjectLoop(GameObject rootObject, string name, int searchDepth, bool fullMatch)
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
					gameObject = AnUtilityObject.FindGameObjectLoop(transform2.gameObject, name, searchDepth - 1, fullMatch);
					if (gameObject != null)
					{
						break;
					}
				}
			}
			return gameObject;
		}

		public static bool CheckParentVisibleInHierarchy(AnMotion motion)
		{
			if (motion.ExistParentObject)
			{
				return motion.ParentObject.VisibleInHierarchy && motion.ParentObject.Visible && !(motion.ParentObject.GameObject == null) && motion.ParentObject.GameObject.activeInHierarchy && AnUtilityObject.CheckParentVisibleInHierarchy(motion.ParentObject);
			}
			return motion.Root.VisibleInHierarchy && motion.Root.Visible && !(motion.Root.gameObject == null) && motion.Root.gameObject.activeInHierarchy;
		}

		public static bool CheckParentVisibleInHierarchy(AnObjectBase objectBase)
		{
			return objectBase.ParentMotion.VisibleInHierarchy && objectBase.ParentMotion.Visible && !(objectBase.ParentMotion.GameObject == null) && objectBase.ParentMotion.GameObject.activeInHierarchy && AnUtilityObject.CheckParentVisibleInHierarchy(objectBase.ParentMotion);
		}

		public static bool CheckParentVisibleInHierarchy(AnBase flbase)
		{
			AnObjectBase anObjectBase = flbase as AnObjectBase;
			if (anObjectBase != null)
			{
				return AnUtilityObject.CheckParentVisibleInHierarchy(anObjectBase);
			}
			AnMotion anMotion = flbase as AnMotion;
			return anMotion != null && AnUtilityObject.CheckParentVisibleInHierarchy(anMotion);
		}

		public static void AttachObject(GameObject targetObject, GameObject parentObject, Vector3 positionOffset, Vector3 rotateOffset, Vector3 scaleOffset)
		{
			targetObject.transform.parent = parentObject.transform;
			targetObject.transform.localPosition = Vector3.zero + positionOffset;
			targetObject.transform.localRotation = Quaternion.Euler(Vector3.zero + rotateOffset);
			targetObject.transform.localScale = Vector3.one + scaleOffset;
		}

		public static GameObject GetChildObject(GameObject parenObject, int index)
		{
			int num = 0;
			foreach (object obj in parenObject.transform)
			{
				Transform transform = (Transform)obj;
				if (!(transform == parenObject.transform))
				{
					if (num == index)
					{
						return transform.gameObject;
					}
					num++;
				}
			}
			return null;
		}

		public static GameObject GetChildObject(GameObject parenObject, string name)
		{
			foreach (object obj in parenObject.transform)
			{
				Transform transform = (Transform)obj;
				if (!(transform == parenObject.transform) && (transform.name == AnValue.ObjectPrefix + name || transform.name == AnValue.PlanePrefix + name || transform.name == AnValue.TextPrefix + name || transform.name == name))
				{
					return transform.gameObject;
				}
			}
			return null;
		}

		public static int GetLayerIndex(string layerName)
		{
			int num = LayerMask.NameToLayer(layerName);
			if (num == -1)
			{
				num = 0;
			}
			return num;
		}

		public static string GetLayerName(int layerIndex)
		{
			return LayerMask.LayerToName(layerIndex);
		}

		public static void SetLayer(GameObject rootObject, int layerIndex)
		{
			rootObject.layer = layerIndex;
			foreach (object obj in rootObject.transform)
			{
				Transform transform = (Transform)obj;
				transform.gameObject.layer = layerIndex;
				AnUtilityObject.SetLayer(transform.gameObject, layerIndex);
			}
		}

		public static void SetLayerFromName(GameObject rootObject, string name)
		{
			int num = LayerMask.NameToLayer(name);
			string text = LayerMask.LayerToName(num);
			if (num >= 0 && text != "")
			{
				rootObject.layer = num;
				AnUtilityObject.SetLayer(rootObject, num);
			}
		}

		public static void SetMeshRendererDefaultValue(MeshRenderer srcMeshRenderer)
		{
			srcMeshRenderer.enabled = false;
			srcMeshRenderer.receiveShadows = false;
			srcMeshRenderer.lightProbeUsage = LightProbeUsage.Off;
			srcMeshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			srcMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
		}

		public static void CopyMeshRendererValue(MeshRenderer srcMeshRenderer, MeshRenderer destMeshRenderer)
		{
			destMeshRenderer.enabled = srcMeshRenderer.enabled;
			destMeshRenderer.receiveShadows = srcMeshRenderer.receiveShadows;
			destMeshRenderer.lightProbeUsage = srcMeshRenderer.lightProbeUsage;
			destMeshRenderer.shadowCastingMode = srcMeshRenderer.shadowCastingMode;
			destMeshRenderer.reflectionProbeUsage = srcMeshRenderer.reflectionProbeUsage;
		}
	}
}
