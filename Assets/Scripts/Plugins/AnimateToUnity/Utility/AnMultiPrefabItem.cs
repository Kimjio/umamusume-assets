using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	[Serializable]
	public class AnMultiPrefabItem
	{
		public AnMultiPrefabComponent Manager
		{
			get
			{
				return this._manager;
			}
			set
			{
				this._manager = value;
			}
		}

		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		public GameObject PrefabObject
		{
			get
			{
				return this._prefabObject;
			}
		}

		public GameObject InstanceObject
		{
			get
			{
				return this._instanceObject;
			}
		}

		public Vector3 CenterPosition
		{
			get
			{
				return this._centerPosition;
			}
		}

		public Vector3 CenterRotate
		{
			get
			{
				return this._centerRotate;
			}
		}

		public Vector3 CenterScaleOffset
		{
			get
			{
				return this._centerScaleOffset;
			}
		}

		public Vector3 LocalPosition
		{
			get
			{
				return this._localPosition;
			}
		}

		public Vector3 LocalRotate
		{
			get
			{
				return this._localRotate;
			}
		}

		public Vector3 LocalScaleOffset
		{
			get
			{
				return this._localScaleOffset;
			}
		}

		public AnRoot Root
		{
			get
			{
				return this._root;
			}
		}

		public void CreateInstance()
		{
			if (this._instanceObject != null)
			{
				if (!Application.isPlaying)
				{
					return;
				}
				global::UnityEngine.Object.Destroy(this._instanceObject);
			}
			if (this._prefabObject != null)
			{
				this._instanceObject = global::UnityEngine.Object.Instantiate<GameObject>(this._prefabObject);
				if (this._instanceObject != null)
				{
					this._instanceObject.name = this._prefabObject.name + "_" + this._index.ToString("D2");
					this._centerObject = new GameObject();
					this._centerObject.name = this._instanceObject.name + this._centerSuffix;
					this._centerObject.transform.parent = this._manager.transform;
					this._instanceObject.transform.parent = this._centerObject.transform;
					if (Application.isPlaying)
					{
						this._root = this._instanceObject.GetComponentInChildren<AnRoot>();
						if (this._root != null)
						{
							this._root.SetDefaultSortOffset(this._root.DefaultSortOffset + this._sortOffset);
							this._root.SetDefaultDepthOffset(this._root.DefaultDepthOffset + this._depthOffset);
							this._root.SetDefaultStencilRefOffset(this._root.DefaultStencilRefOffset + this._stencilRefOffset);
						}
					}
					this.UpdateInstanceTransform(false);
				}
			}
		}

		public void UpdateInstanceTransform(bool valueFromObject)
		{
			this.UpdateTransformBase(this._instanceObject, this._centerObject, valueFromObject);
		}

		private void UpdateTransformBase(GameObject targetObject, GameObject centerObject, bool valueFromObject)
		{
			if (targetObject == null)
			{
				return;
			}
			if (centerObject == null)
			{
				return;
			}
			if (valueFromObject)
			{
				if (centerObject.transform.localPosition != this._centerPosition)
				{
					this._centerPosition = this._centerObject.transform.localPosition;
				}
				if (centerObject.transform.localRotation.eulerAngles != this._centerRotate)
				{
					this._centerRotate = centerObject.transform.localRotation.eulerAngles;
				}
				if (centerObject.transform.localScale != this._centerScaleOffset + Vector3.one)
				{
					this._centerScaleOffset = centerObject.transform.localScale - Vector3.one;
				}
				if (targetObject.transform.localPosition != this._localPosition)
				{
					this._localPosition = targetObject.transform.localPosition;
				}
				if (targetObject.transform.localRotation.eulerAngles != this._localRotate)
				{
					this._localRotate = targetObject.transform.localRotation.eulerAngles;
				}
				if (targetObject.transform.localScale != this._localScaleOffset + Vector3.one)
				{
					this._localScaleOffset = targetObject.transform.localScale - Vector3.one;
					return;
				}
			}
			else
			{
				centerObject.transform.localPosition = this._centerPosition;
				centerObject.transform.localRotation = Quaternion.Euler(this._centerRotate);
				centerObject.transform.localScale = this._centerScaleOffset + Vector3.one;
				targetObject.transform.localPosition = this._localPosition;
				targetObject.transform.localRotation = Quaternion.Euler(this._localRotate);
				targetObject.transform.localScale = this._localScaleOffset + Vector3.one;
			}
		}

		[SerializeField]
		private GameObject _prefabObject;

		[SerializeField]
		private Vector3 _centerPosition = Vector3.zero;

		[SerializeField]
		private Vector3 _centerRotate = Vector3.zero;

		[SerializeField]
		private Vector3 _centerScaleOffset = Vector3.zero;

		[SerializeField]
		private Vector3 _localPosition = Vector3.zero;

		[SerializeField]
		private Vector3 _localRotate = Vector3.zero;

		[SerializeField]
		private Vector3 _localScaleOffset = Vector3.zero;

		[SerializeField]
		private int _sortOffset;

		[SerializeField]
		private float _depthOffset;

		[SerializeField]
		private int _stencilRefOffset;

		[NonSerialized]
		private AnMultiPrefabComponent _manager;

		private int _index;

		private GameObject _centerObject;

		private GameObject _instanceObject;

		private AnRoot _root;

		private string _centerSuffix = "_center";
	}
}
