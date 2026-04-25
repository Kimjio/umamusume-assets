using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnCameraManager
	{
		public List<Camera> ActiveCameraList
		{
			get
			{
				return this._activeCameraList;
			}
		}

		public void _Initialize()
		{
			this._exist = false;
			this._allCameraList = new Camera[16];
			this._activeCameraList = new List<Camera>();
			this._tempTargetCameraList = new List<Camera>();
			this._layerBitFlagSortedList = new List<int>();
			this._exist = true;
		}

		public void _Update()
		{
			if (!this._exist)
			{
				return;
			}
			this._UpdateCameraList();
			this._UpdateLayerBitFlagSortedList();
			this._prevCameraCullingMaskSum = this._cameraCullingMaskSum;
			this._prevCameraDepthSum = this._cameraDepthSum;
		}

		private void _UpdateCameraList()
		{
			int allCamerasCount = Camera.allCamerasCount;
			if (this._allCameraList.Length < allCamerasCount)
			{
				this._allCameraList = new Camera[allCamerasCount];
			}
			else if (this._allCameraList.Length != allCamerasCount)
			{
				Array.Clear(this._allCameraList, allCamerasCount, this._allCameraList.Length - allCamerasCount);
			}
			Camera.GetAllCameras(this._allCameraList);
			this._activeCameraList.Clear();
			this._cameraCullingMaskSum = 0;
			this._cameraDepthSum = 0f;
			for (int i = 0; i < allCamerasCount; i++)
			{
				if (!(this._allCameraList[i] == null) && !(this._allCameraList[i].gameObject == null) && this._allCameraList[i].gameObject.activeInHierarchy && this._allCameraList[i].enabled && this._allCameraList[i].rect.x < 1f && this._allCameraList[i].rect.y < 1f && this._allCameraList[i].rect.width > 0f && this._allCameraList[i].rect.height > 0f && this._allCameraList[i].cullingMask != 0)
				{
					bool flag = false;
					for (int j = 0; j < AnMonoSingleton<AnRootManager>.Instance.ActiveLayerBitFlagList.Count; j++)
					{
						if ((this._allCameraList[i].cullingMask & AnMonoSingleton<AnRootManager>.Instance.ActiveLayerBitFlagList[j]) != 0)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						this._cameraCullingMaskSum += this._allCameraList[i].cullingMask;
						this._cameraDepthSum += this._allCameraList[i].depth;
						this._activeCameraList.Add(this._allCameraList[i]);
					}
				}
			}
			if (this._cameraCullingMaskSum != this._prevCameraCullingMaskSum || this._cameraDepthSum != this._prevCameraDepthSum)
			{
				this._activeCameraList.Sort(new Comparison<Camera>(this._CompareFuncForCamera));
			}
		}

		private void _UpdateLayerBitFlagSortedList()
		{
			if (this._cameraCullingMaskSum == this._prevCameraCullingMaskSum && this._cameraDepthSum == this._prevCameraDepthSum)
			{
				return;
			}
			if (this._layerBitFlagSortedList == null)
			{
				this._layerBitFlagSortedList = new List<int>();
			}
			this._layerBitFlagSortedList.Clear();
			if (this._activeCameraList == null)
			{
				return;
			}
			if (this._activeCameraList.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this._activeCameraList.Count; i++)
			{
				for (int j = 0; j < AnMonoSingleton<AnRootManager>.Instance.LayerBitFlagList.Count; j++)
				{
					if ((this._activeCameraList[i].cullingMask & AnMonoSingleton<AnRootManager>.Instance.LayerBitFlagList[j]) != 0 && !this._layerBitFlagSortedList.Contains(AnMonoSingleton<AnRootManager>.Instance.LayerBitFlagList[j]))
					{
						this._layerBitFlagSortedList.Add(AnMonoSingleton<AnRootManager>.Instance.LayerBitFlagList[j]);
					}
				}
			}
		}

		public int _GetLayerPriority(int layerBitFlag)
		{
			if (this._layerBitFlagSortedList == null)
			{
				return -1;
			}
			if (this._layerBitFlagSortedList.Count == 0)
			{
				return -1;
			}
			if (this._layerBitFlagSortedList.Contains(layerBitFlag))
			{
				return this._layerBitFlagSortedList.Count - 1 - this._layerBitFlagSortedList.IndexOf(layerBitFlag);
			}
			return -1;
		}

		public void _GetTargetCamera(GameObject targetObject, ref Camera targetCamera)
		{
			if (!this._exist)
			{
				return;
			}
			targetCamera = null;
			this._tempTargetCameraList.Clear();
			if (targetObject == null)
			{
				return;
			}
			if (!targetObject.activeInHierarchy)
			{
				return;
			}
			int num = 1 << targetObject.layer;
			for (int i = 0; i < this._activeCameraList.Count; i++)
			{
				if (!(this._activeCameraList[i] == null) && !(this._activeCameraList[i].gameObject == null) && this._activeCameraList[i].gameObject.activeInHierarchy && this._activeCameraList[i].enabled && this._activeCameraList[i].cullingMask != 0 && (this._activeCameraList[i].cullingMask & num) != 0)
				{
					this._tempTargetCameraList.Add(this._activeCameraList[i]);
				}
			}
			if (this._tempTargetCameraList.Count == 0)
			{
				return;
			}
			if (this._tempTargetCameraList.Count == 1)
			{
				targetCamera = this._tempTargetCameraList[0];
				return;
			}
			this._tempTargetCameraList.Sort(new Comparison<Camera>(this._CompareFuncForCamera));
			targetCamera = this._tempTargetCameraList[0];
		}

		private int _CompareFuncForCamera(Camera first, Camera second)
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
				float num = second.depth - first.depth;
				if (num > 0f)
				{
					return 1;
				}
				if (num < 0f)
				{
					return -1;
				}
				return 1;
			}
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
			this._activeCameraList.Clear();
			this._tempTargetCameraList.Clear();
		}

		public void _Release()
		{
			this._exist = false;
			this._activeCameraList = null;
			this._tempTargetCameraList = null;
			this._layerBitFlagSortedList = null;
		}

		private const int CAMERA_LIST_NUM = 16;

		private bool _exist;

		private Camera[] _allCameraList;

		private List<Camera> _activeCameraList;

		private int _cameraCullingMaskSum;

		private int _prevCameraCullingMaskSum = -1;

		private float _cameraDepthSum;

		private float _prevCameraDepthSum = -1f;

		private List<int> _layerBitFlagSortedList;

		private List<Camera> _tempTargetCameraList;
	}
}
