using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnCollisionManager
	{
		public void _Initialize()
		{
			this._exist = false;
			this._rootTable = new Hashtable(8);
			this._objectTable = new Hashtable(8);
			this._tempObjectList = new List<object>(8);
			this._tempRaycastHitList = new RaycastHit[this._maxHitCount];
			this._tempObjectRaycastHitList = new List<RaycastHit>(8);
			this._tempObjectRayHitObjectList = new List<AnObjectBase>(8);
			this._tempCameraRaycastHitList = new List<RaycastHit>(8);
			this._tempCameraRayHitObjectList = new List<AnObjectBase>(8);
			this._exist = true;
		}

		public void _AddRoot(AnRoot root)
		{
			if (root == null)
			{
				return;
			}
			if (root.ColliderTable.Count == 0)
			{
				return;
			}
			if (root.ObjectList.Count == 0)
			{
				return;
			}
			if (this._ExistRoot(root))
			{
				return;
			}
			this._rootTable.Add(root, root);
			foreach (AnObjectBase anObjectBase in root.ObjectList)
			{
				this._AddObject(anObjectBase);
			}
		}

		public void _AddObject(AnObjectBase targetObject)
		{
			if (targetObject.Collider == null)
			{
				return;
			}
			if (this._ExistObject(targetObject))
			{
				return;
			}
			if (this._objectTable.ContainsKey(targetObject.Collider))
			{
				return;
			}
			this._objectTable.Add(targetObject.Collider, targetObject);
		}

		public bool _ExistRoot(AnRoot targetRoot)
		{
			return this._rootTable.ContainsKey(targetRoot);
		}

		public bool _ExistObject(AnObjectBase targetObject)
		{
			return this._objectTable.ContainsKey(targetObject.Collider);
		}

		public void _OptimizeAll()
		{
			if (!this._exist)
			{
				return;
			}
			this._Optimize();
		}

		private void _Optimize()
		{
			this._tempObjectList.Clear();
			foreach (object obj in this._rootTable.Values)
			{
				AnRoot anRoot = (AnRoot)obj;
				if (!(anRoot == null) && !(anRoot.gameObject == null))
				{
					this._tempObjectList.Add(anRoot);
				}
			}
			this._rootTable.Clear();
			foreach (object obj2 in this._tempObjectList)
			{
				AnRoot anRoot2 = (AnRoot)obj2;
				this._rootTable.Add(anRoot2, anRoot2);
			}
			this._tempObjectList.Clear();
			foreach (object obj3 in this._objectTable.Values)
			{
				AnObjectBase anObjectBase = (AnObjectBase)obj3;
				if (anObjectBase != null && !(anObjectBase.GameObject == null) && !(anObjectBase.Collider == null))
				{
					this._tempObjectList.Add(anObjectBase);
				}
			}
			this._objectTable.Clear();
			foreach (object obj4 in this._tempObjectList)
			{
				AnObjectBase anObjectBase2 = (AnObjectBase)obj4;
				this._objectTable.Add(anObjectBase2.Collider, anObjectBase2);
			}
			this._tempObjectList.Clear();
		}

		public void _GetHitObjectListWithObjectRay(Vector3 objectRayPosition, Vector3 objectRayDirection, Vector3 objectRayUpDirection, float distance, int layerMask, float radius, bool useCameraRay, ref List<AnObjectBase> hitObjectList)
		{
			if (hitObjectList == null)
			{
				hitObjectList = new List<AnObjectBase>();
			}
			hitObjectList.Clear();
			this._UpdateRaycastHitList(objectRayPosition, objectRayDirection, objectRayUpDirection, distance, layerMask, radius, ref this._tempObjectRaycastHitList);
			if (!useCameraRay)
			{
				this._GetHitObjectListFromRaycastHitList(this._tempObjectRaycastHitList, ref hitObjectList);
				return;
			}
			for (int i = 0; i < this._tempObjectRaycastHitList.Count; i++)
			{
				RaycastHit raycastHit = this._tempObjectRaycastHitList[i];
				if (!(raycastHit.collider == null) && raycastHit.collider.enabled && !(raycastHit.collider.gameObject == null) && raycastHit.collider.gameObject.activeInHierarchy)
				{
					AnMonoSingleton<AnRootManager>.Instance.UIManager.CameraManager._GetTargetCamera(raycastHit.collider.gameObject, ref this._tempTargetCamera);
					if (!(this._tempTargetCamera == null))
					{
						AnUtilityVector.GetOrthogonalProjectionVector(objectRayDirection, raycastHit.collider.transform.position - objectRayPosition, ref this._tempOrthogonalProjectionVector);
						this._tempFixColliderPosition = objectRayPosition + this._tempOrthogonalProjectionVector;
						this._GetHitObjectListWithCameraRay(this._tempTargetCamera, this._tempFixColliderPosition, false, layerMask, ref this._tempObjectRayHitObjectList);
						if (this._tempObjectRayHitObjectList.Count != 0)
						{
							AnObjectBase anObjectBase = this._GetFirstHitObjectFromHitObjectList(this._tempObjectRayHitObjectList, false);
							if (anObjectBase != null)
							{
								hitObjectList.Add(anObjectBase);
							}
						}
					}
				}
			}
		}

		public void _GetHitObjectListWithCameraRay(Camera targetCamera, Vector3 targetPosition, bool targetPositionIsScreen, int layerMask, ref List<AnObjectBase> hitObjectList)
		{
			if (hitObjectList == null)
			{
				hitObjectList = new List<AnObjectBase>();
			}
			hitObjectList.Clear();
			if (targetCamera == null)
			{
				return;
			}
			if (!targetCamera.enabled)
			{
				return;
			}
			if (!targetCamera.gameObject.activeInHierarchy)
			{
				return;
			}
			this._tempNearPosition = targetPosition;
			this._tempFarPosition = targetPosition;
			if (targetPositionIsScreen)
			{
				this._tempNearPosition.z = targetCamera.nearClipPlane;
				this._tempNearPosition = targetCamera.ScreenToWorldPoint(this._tempNearPosition);
				this._tempFarPosition.z = targetCamera.farClipPlane;
				this._tempFarPosition = targetCamera.ScreenToWorldPoint(this._tempFarPosition);
			}
			else
			{
				this._tempNearPosition = targetCamera.WorldToScreenPoint(this._tempNearPosition);
				this._tempNearPosition.z = targetCamera.nearClipPlane;
				this._tempNearPosition = targetCamera.ScreenToWorldPoint(this._tempNearPosition);
			}
			this._tempDirection = this._tempFarPosition - this._tempNearPosition;
			this._tempDirection.Normalize();
			this._UpdateRaycastHitList(this._tempNearPosition, this._tempDirection, Vector3.up, float.MaxValue, layerMask, 0f, ref this._tempCameraRaycastHitList);
			this._GetHitObjectListFromRaycastHitList(this._tempCameraRaycastHitList, ref this._tempCameraRayHitObjectList);
			if (this._tempCameraRayHitObjectList == null)
			{
				return;
			}
			if (this._tempCameraRayHitObjectList.Count == 0)
			{
				return;
			}
			hitObjectList.AddRange(this._tempCameraRayHitObjectList);
			this._SortHitObjectList(ref hitObjectList);
		}

		public void _GetHitObjectListWithCameraRay(Vector3 targetPosition, bool targetPositionIsScreen, ref List<AnObjectBase> hitObjectList)
		{
			if (hitObjectList == null)
			{
				hitObjectList = new List<AnObjectBase>();
			}
			hitObjectList.Clear();
			if (float.IsNaN(targetPosition.x) || float.IsNaN(targetPosition.y))
			{
				return;
			}
			for (int i = 0; i < AnMonoSingleton<AnRootManager>.Instance.UIManager.CameraManager.ActiveCameraList.Count; i++)
			{
				Camera camera = AnMonoSingleton<AnRootManager>.Instance.UIManager.CameraManager.ActiveCameraList[i];
				this._tempNearPosition = targetPosition;
				this._tempFarPosition = targetPosition;
				if (targetPositionIsScreen)
				{
					this._tempNearPosition.z = camera.nearClipPlane;
					this._tempNearPosition = camera.ScreenToWorldPoint(this._tempNearPosition);
					this._tempFarPosition.z = camera.farClipPlane;
					this._tempFarPosition = camera.ScreenToWorldPoint(this._tempFarPosition);
				}
				else
				{
					this._tempNearPosition = camera.WorldToScreenPoint(this._tempNearPosition);
					this._tempNearPosition.z = camera.nearClipPlane;
					this._tempNearPosition = camera.ScreenToWorldPoint(this._tempNearPosition);
				}
				this._tempDirection = this._tempFarPosition - this._tempNearPosition;
				this._tempDirection.Normalize();
				this._UpdateRaycastHitList(this._tempNearPosition, this._tempDirection, Vector3.up, float.MaxValue, camera.cullingMask, 0f, ref this._tempCameraRaycastHitList);
				this._GetHitObjectListFromRaycastHitList(this._tempCameraRaycastHitList, ref this._tempCameraRayHitObjectList);
				if (this._tempCameraRayHitObjectList != null && this._tempCameraRayHitObjectList.Count != 0)
				{
					hitObjectList.AddRange(this._tempCameraRayHitObjectList);
				}
			}
			this._SortHitObjectList(ref hitObjectList);
		}

		private void _UpdateRaycastHitList(Vector3 position, Vector3 direction, Vector3 upVector, float distance, int layerMask, float radius, ref List<RaycastHit> raycastHitList)
		{
			if (raycastHitList == null)
			{
				raycastHitList = new List<RaycastHit>();
			}
			raycastHitList.Clear();
			if (layerMask == 0)
			{
				return;
			}
			this._UpdateRaycastHitList(position, direction, upVector, distance, 0f, layerMask, ref raycastHitList);
			if (radius > 0f)
			{
				this._UpdateRaycastHitList(position, direction, upVector + direction, distance, radius, layerMask, ref raycastHitList);
				this._UpdateRaycastHitList(position, direction, upVector + direction, distance, -radius, layerMask, ref raycastHitList);
			}
			raycastHitList.Sort((RaycastHit t1, RaycastHit t2) => AnCollisionManager._CompareFuncForRaycastHit(t1, t2));
		}

		private void _UpdateRaycastHitList(Vector3 position, Vector3 direction, Vector3 upVector, float distance, float upDistance, int layerMask, ref List<RaycastHit> raycastHitList)
		{
			if (raycastHitList == null)
			{
				raycastHitList = new List<RaycastHit>();
			}
			raycastHitList.Clear();
			if (layerMask == 0)
			{
				return;
			}
			direction.Normalize();
			upVector.Normalize();
			int num = 0;
			if (layerMask > 0)
			{
				num = Physics.RaycastNonAlloc(position + upVector * upDistance, direction, this._tempRaycastHitList, distance, layerMask);
			}
			else if (layerMask < 0)
			{
				num = Physics.RaycastNonAlloc(position + upVector * upDistance, direction, this._tempRaycastHitList, distance);
			}
			if (num == 0)
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = this._tempRaycastHitList[i];
				if (!(raycastHit.collider == null) && raycastHit.collider.enabled && !(raycastHit.collider.gameObject == null) && raycastHit.collider.gameObject.activeInHierarchy)
				{
					raycastHitList.Add(raycastHit);
				}
			}
		}

		private static int _CompareFuncForRaycastHit(RaycastHit first, RaycastHit second)
		{
			if (first.collider == null)
			{
				if (second.collider == null)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (second.collider == null)
				{
					return -1;
				}
				if (first.distance < second.distance)
				{
					return -1;
				}
				if (first.distance > second.distance)
				{
					return 1;
				}
				return 0;
			}
		}

		private void _GetHitObjectListFromRaycastHitList(List<RaycastHit> raycastHitList, ref List<AnObjectBase> hitObjectList)
		{
			if (hitObjectList == null)
			{
				hitObjectList = new List<AnObjectBase>();
			}
			hitObjectList.Clear();
			for (int i = 0; i < raycastHitList.Count; i++)
			{
				RaycastHit raycastHit = raycastHitList[i];
				if (!(raycastHit.collider == null) && raycastHit.collider.enabled && !(raycastHit.collider.gameObject == null) && raycastHit.collider.gameObject.activeInHierarchy && this._objectTable.ContainsKey(raycastHit.collider))
				{
					AnObjectBase anObjectBase = this._objectTable[raycastHit.collider] as AnObjectBase;
					if (anObjectBase != null)
					{
						hitObjectList.Add(anObjectBase);
					}
				}
			}
		}

		private void _SortHitObjectList(ref List<AnObjectBase> hitObjectList)
		{
			if (hitObjectList == null)
			{
				return;
			}
			if (hitObjectList.Count <= 1)
			{
				return;
			}
			hitObjectList.Sort((AnObjectBase p1, AnObjectBase p2) => AnCollisionManager._CompareFuncForHitObject(p1, p2));
		}

		private static int _CompareFuncForHitObject(AnObjectBase first, AnObjectBase second)
		{
			if (first == null)
			{
				if (second == null)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (second == null)
				{
					return -1;
				}
				int num = AnMonoSingleton<AnRootManager>.Instance.UIManager.CameraManager._GetLayerPriority(1 << second.GameObject.layer) - AnMonoSingleton<AnRootManager>.Instance.UIManager.CameraManager._GetLayerPriority(1 << first.GameObject.layer);
				if (num != 0)
				{
					return num;
				}
				if (first.SortLayerName == null)
				{
					if (second.SortLayerName == null)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					if (second.SortLayerName == null)
					{
						return -1;
					}
					int num2 = AnMonoSingleton<AnRootManager>.Instance._GetSortingLayerIndex(second.SortLayerName) - AnMonoSingleton<AnRootManager>.Instance._GetSortingLayerIndex(first.SortLayerName);
					if (num2 != 0)
					{
						return num2;
					}
					int num3 = second.SortOrder - first.SortOrder;
					if (num3 != 0)
					{
						return num3;
					}
					if (second.GameObject.transform.position.z - first.GameObject.transform.position.z > 0f)
					{
						return -1;
					}
					return 1;
				}
			}
		}

		public AnUIBase _GetUIFromHitObject(AnObjectBase hitObject)
		{
			if (hitObject == null)
			{
				return null;
			}
			for (int i = 0; i < AnMonoSingleton<AnRootManager>.Instance.UIManager.UIBaseManager._UIBaseList.Count; i++)
			{
				AnUIBase anUIBase = AnMonoSingleton<AnRootManager>.Instance.UIManager.UIBaseManager._UIBaseList[i];
				if (anUIBase != null && anUIBase.HitAreaObject == hitObject)
				{
					return anUIBase;
				}
			}
			return null;
		}

		public void _GetUIListFromHitObjectList(List<AnObjectBase> hitObjectList, ref List<AnUIBase> resultUIList)
		{
			if (resultUIList == null)
			{
				resultUIList = new List<AnUIBase>();
			}
			resultUIList.Clear();
			if (hitObjectList == null)
			{
				return;
			}
			for (int i = 0; i < hitObjectList.Count; i++)
			{
				AnUIBase anUIBase = this._GetUIFromHitObject(hitObjectList[i]);
				if (anUIBase != null)
				{
					resultUIList.Add(anUIBase);
				}
			}
		}

		public AnUIBase _GetFirstUIListFromHitObjectList(List<AnObjectBase> hitObjectList, bool useSubCollider)
		{
			if (hitObjectList == null)
			{
				return null;
			}
			AnObjectBase anObjectBase = this._GetFirstHitObjectFromHitObjectList(hitObjectList, useSubCollider);
			if (anObjectBase == null)
			{
				return null;
			}
			AnUIBase anUIBase = this._GetUIFromHitObject(anObjectBase);
			if (anUIBase == null)
			{
				return null;
			}
			return anUIBase;
		}

		public AnObjectBase _GetFirstHitObjectFromHitObjectList(List<AnObjectBase> hitObjectList, bool useSubCollider)
		{
			if (hitObjectList == null)
			{
				return null;
			}
			for (int i = 0; i < hitObjectList.Count; i++)
			{
				AnObjectBase anObjectBase = hitObjectList[i];
				if (!(anObjectBase.Collider == null) && anObjectBase.Collider.enabled && !anObjectBase.ColliderThrough)
				{
					if (!useSubCollider)
					{
						return anObjectBase;
					}
					if (anObjectBase.ExistSubCollider == 0)
					{
						return anObjectBase;
					}
					if (anObjectBase.ExistSubCollider == 1 && !(anObjectBase.SubCollider == null) && anObjectBase.SubCollider.enabled && this._GetHitObjectFromHitObjectListByCollider(anObjectBase.SubCollider, hitObjectList) != null)
					{
						return anObjectBase;
					}
				}
			}
			return null;
		}

		public AnObjectBase _GetHitObjectFromHitObjectListByCollider(Collider targetCollider, List<AnObjectBase> hitObjectList)
		{
			if (hitObjectList == null)
			{
				return null;
			}
			if (hitObjectList.Count == 0)
			{
				return null;
			}
			if (targetCollider == null)
			{
				return null;
			}
			if (!targetCollider.enabled)
			{
				return null;
			}
			for (int i = 0; i < hitObjectList.Count; i++)
			{
				AnObjectBase anObjectBase = hitObjectList[i];
				if (!(anObjectBase.Collider == null) && anObjectBase.Collider.enabled)
				{
					if (targetCollider == anObjectBase.Collider)
					{
						if (!(anObjectBase.SubCollider != null) || this._GetHitObjectFromHitObjectListByCollider(anObjectBase.SubCollider, hitObjectList) != null)
						{
							return anObjectBase;
						}
					}
					else if (!anObjectBase.ColliderThrough && !(anObjectBase.SubCollider != null))
					{
						break;
					}
				}
			}
			return null;
		}

		private const int COLLISION_LIST_NUM = 8;

		private bool _exist;

		private Hashtable _rootTable;

		private Hashtable _objectTable;

		private List<object> _tempObjectList;

		private int _maxHitCount = 20;

		private RaycastHit[] _tempRaycastHitList;

		private List<RaycastHit> _tempObjectRaycastHitList;

		private List<AnObjectBase> _tempObjectRayHitObjectList;

		private List<RaycastHit> _tempCameraRaycastHitList;

		private List<AnObjectBase> _tempCameraRayHitObjectList;

		private Camera _tempTargetCamera;

		private Vector3 _tempNearPosition = Vector3.zero;

		private Vector3 _tempFarPosition = Vector3.zero;

		private Vector3 _tempDirection = Vector3.zero;

		private Vector3 _tempOrthogonalProjectionVector = Vector3.zero;

		private Vector3 _tempFixColliderPosition = Vector3.zero;
	}
}
