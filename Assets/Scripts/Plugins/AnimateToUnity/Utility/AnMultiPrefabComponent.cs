using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnMultiPrefabComponent : MonoBehaviour
	{
		public List<AnMultiPrefabItem> PrefabItemList
		{
			get
			{
				return this._prefabItemList;
			}
		}

		private void OnValidate()
		{
			if (Application.isPlaying)
			{
				return;
			}
			this._editFromObject = false;
			this.UpdateInstanceObject();
			this._editFromObject = true;
		}

		private void OnDrawGizmosSelected()
		{
			if (Application.isPlaying)
			{
				return;
			}
			if (!this._editMode)
			{
				this.ClearChildObject();
				return;
			}
			this._editFromObject = true;
			this.CreateInstanceObject();
			this.UpdateInstanceObject();
		}

		private void Awake()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.CreateInstanceObject();
		}

		private void CreateInstanceObject()
		{
			if (Application.isPlaying)
			{
				this.ClearChildObject();
			}
			else
			{
				if (base.transform.childCount == this._prefabItemList.Count)
				{
					return;
				}
				this.ClearChildObject();
			}
			for (int i = 0; i < this._prefabItemList.Count; i++)
			{
				AnMultiPrefabItem anMultiPrefabItem = this._prefabItemList[i];
				if (anMultiPrefabItem != null)
				{
					anMultiPrefabItem.Manager = this;
					anMultiPrefabItem.Index = i;
					anMultiPrefabItem.CreateInstance();
				}
			}
		}

		private void UpdateInstanceObject()
		{
			for (int i = 0; i < this._prefabItemList.Count; i++)
			{
				AnMultiPrefabItem anMultiPrefabItem = this._prefabItemList[i];
				if (anMultiPrefabItem != null)
				{
					anMultiPrefabItem.UpdateInstanceTransform(this._editFromObject);
				}
			}
		}

		private void ClearChildObject()
		{
			if (this._childObjectList == null)
			{
				this._childObjectList = new List<GameObject>();
			}
			this._childObjectList.Clear();
			for (int i = 0; i < base.transform.childCount; i++)
			{
				this._childObjectList.Add(base.transform.GetChild(i).gameObject);
			}
			if (this._childObjectList.Count == 0)
			{
				return;
			}
			for (int j = 0; j < this._childObjectList.Count; j++)
			{
				if (Application.isPlaying)
				{
					global::UnityEngine.Object.Destroy(this._childObjectList[j]);
				}
				else
				{
					try
					{
						global::UnityEngine.Object.DestroyImmediate(this._childObjectList[j], true);
					}
					catch
					{
					}
				}
			}
		}

		[SerializeField]
		private bool _editMode;

		[SerializeField]
		private List<AnMultiPrefabItem> _prefabItemList;

		private List<GameObject> _childObjectList;

		private bool _editFromObject;
	}
}
