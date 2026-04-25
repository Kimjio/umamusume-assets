using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnScrollItemObject
	{
		public int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		public float ObjectWidth
		{
			get
			{
				return this._objectWidth;
			}
		}

		public float ObjectOffset
		{
			get
			{
				return this._objectOffset;
			}
		}

		public AnScrollItemInfo ItemInfo
		{
			get
			{
				return this._itemInfo;
			}
		}

		public AnScrollItemInfo ExtendedItemInfo
		{
			get
			{
				return this._extendedItemInfo;
			}
		}

		public AnRoot Root
		{
			get
			{
				return this._root;
			}
		}

		public GameObject RootObject
		{
			get
			{
				return this._rootObject;
			}
		}

		public virtual void Create(GameObject instanceObject, AnObjectScrollListComponent parant, int sortOffset)
		{
			if (instanceObject == null)
			{
				return;
			}
			this._component = parant;
			this._componentBaseList = new List<AnComponentBase>();
			this._rootObject = instanceObject;
			this._root = this._rootObject.GetComponentInChildren<AnRoot>();
			this._existRoot = false;
			if (this._root != null)
			{
				this._root.SetDefaultSortOffset(parant.ScrollList.Motion.SortOrder + sortOffset);
				this._root.SetDefaultDepthOffset(0f);
				this._startObject = this._root.FindComponent<Transform>(this._root.gameObject, parant.ScrollList.ItemStartPrefix, false);
				this._endObject = this._root.FindComponent<Transform>(this._root.gameObject, parant.ScrollList.ItemEndPrefix, false);
				this._existRoot = true;
			}
			this._objectWidth = 0f;
			this._objectOffset = 0f;
			if (this._startObject == null || this._endObject == null)
			{
				Transform[] componentsInChildren = this._rootObject.GetComponentsInChildren<Transform>(true);
				Vector4 vector = new Vector4(10000000f, -10000000f, 10000000f, -10000000f);
				foreach (Transform transform in componentsInChildren)
				{
					if (transform.position.x < vector.x)
					{
						vector.x = transform.position.x;
					}
					if (transform.position.x > vector.y)
					{
						vector.y = transform.position.x;
					}
					if (transform.position.y < vector.z)
					{
						vector.z = transform.position.y;
					}
					if (transform.position.y > vector.w)
					{
						vector.w = transform.position.y;
					}
				}
				this._startObject = new GameObject("pos_start00").transform;
				this._endObject = new GameObject("pos_end00").transform;
				if (this._component.ScrollList.DirectionType == AnUIDirectionTypes.TopToButtom)
				{
					this._startObject.position = new Vector3(vector.y, vector.w, 0f);
					this._endObject.position = new Vector3(vector.x, vector.z, 0f);
				}
				else if (this._component.ScrollList.DirectionType == AnUIDirectionTypes.BottomToTop)
				{
					this._startObject.position = new Vector3(vector.x, vector.z, 0f);
					this._endObject.position = new Vector3(vector.y, vector.w, 0f);
				}
				else if (this._component.ScrollList.DirectionType == AnUIDirectionTypes.LeftToRight)
				{
					this._startObject.position = new Vector3(vector.x, vector.z, 0f);
					this._endObject.position = new Vector3(vector.y, vector.w, 0f);
				}
				else if (this._component.ScrollList.DirectionType == AnUIDirectionTypes.RightToLeft)
				{
					this._startObject.position = new Vector3(vector.y, vector.w, 0f);
					this._endObject.position = new Vector3(vector.x, vector.z, 0f);
				}
				if (this._existRoot)
				{
					this._startObject.parent = this._root.gameObject.transform;
					this._endObject.parent = this._root.gameObject.transform;
				}
				else
				{
					this._startObject.parent = this._rootObject.transform;
					this._endObject.parent = this._rootObject.transform;
				}
			}
			if (this._component.ScrollList.DirectionType == AnUIDirectionTypes.TopToButtom || this._component.ScrollList.DirectionType == AnUIDirectionTypes.BottomToTop)
			{
				this._objectWidth = AnUtilityValue.GetAbsValue(this._startObject.position.y - this._endObject.position.y);
				this._objectOffset = AnUtilityValue.GetAbsValue(this._startObject.position.y - this._rootObject.transform.position.y);
			}
			else if (this._component.ScrollList.DirectionType == AnUIDirectionTypes.LeftToRight || this._component.ScrollList.DirectionType == AnUIDirectionTypes.RightToLeft)
			{
				this._objectWidth = AnUtilityValue.GetAbsValue(this._startObject.position.x - this._endObject.position.x);
				this._objectOffset = AnUtilityValue.GetAbsValue(this._rootObject.transform.position.x - this._startObject.position.x);
			}
			AnUtilityObject.AttachObject(this._rootObject, this._component.ScrollList.ItemScrollPositionObject, Vector3.zero, Vector3.zero, Vector3.zero);
			this.CheckHierarchy();
			if (!this._existRoot)
			{
				this._rootObject.SetActive(false);
				return;
			}
			this._root.SetVisible(false);
		}

		public virtual void CheckHierarchy()
		{
			if (this._rootObject == null)
			{
				return;
			}
			if (this._componentBaseList == null)
			{
				this._componentBaseList = new List<AnComponentBase>();
			}
			this._componentBaseList.Clear();
			this._componentBaseList.AddRange(this._rootObject.GetComponentsInChildren<AnComponentBase>(true));
			for (int i = 0; i < this._componentBaseList.Count; i++)
			{
				AnUIBase uibase = this._componentBaseList[i].UIBase;
				if (uibase != null && uibase.HitAreaObject != null)
				{
					uibase.HitAreaObject.SetSubCollider(this._component.ScrollList.HitAreaObject.Collider, true);
				}
			}
		}

		public virtual void Initialize()
		{
			if (this._extendedItemInfo == null || this._itemInfo == null)
			{
				this.Reset();
				return;
			}
			this.UpdatePosition();
			this.UpdateValue();
		}

		public virtual void UpdatePosition()
		{
			if (this._extendedItemInfo == null || this._itemInfo == null)
			{
				return;
			}
			this._tempVector0.x = 0f;
			this._tempVector0.y = 0f;
			this._tempVector0.z = 0f;
			if (this._component.ScrollList.DirectionType == AnUIDirectionTypes.TopToButtom || this._component.ScrollList.DirectionType == AnUIDirectionTypes.BottomToTop)
			{
				this._tempVector0.y = -this._extendedItemInfo.CenterPosition;
			}
			else if (this._component.ScrollList.DirectionType == AnUIDirectionTypes.LeftToRight || this._component.ScrollList.DirectionType == AnUIDirectionTypes.RightToLeft)
			{
				this._tempVector0.x = this._extendedItemInfo.CenterPosition;
			}
			this._rootObject.transform.localPosition = this._tempVector0;
		}

		public virtual void UpdateValue()
		{
			if (this._extendedItemInfo == null || this._itemInfo == null)
			{
				return;
			}
			if (this._component.ScrollList.IsAutoScroll)
			{
				for (int i = 0; i < this._componentBaseList.Count; i++)
				{
					AnUIBase uibase = this._componentBaseList[i].UIBase;
					if (uibase != null && uibase.Exist)
					{
						int scrollItemIndexFromPosition = this._component.ScrollList.GetScrollItemIndexFromPosition(this._extendedItemInfo.StartPosition);
						uibase.FlActionSelectInStart.AddAction(new Action<object>(this._OnItemSelectInStart), scrollItemIndexFromPosition, this._component.ScrollList.SelectActionId, true);
					}
				}
			}
		}

		public void _OnItemSelectInStart(object arg)
		{
			this._component.ScrollList.SetScrollPositionInRangeFromItemIndex((int)arg, this._component.ScrollList.IsAutoScrollAnimation);
		}

		public virtual void UpdateEnd()
		{
			AnScrollItemInfo extendedItemInfo = this._extendedItemInfo;
		}

		public virtual void SetItemInfo(AnScrollItemInfo itemInfo)
		{
			if (itemInfo == null)
			{
				return;
			}
			if (itemInfo.ObjectID != this._objectID)
			{
				return;
			}
			if (!this._existRoot)
			{
				this._rootObject.SetActive(true);
			}
			else
			{
				this._root.SetVisible(true);
			}
			this._extendedItemInfo = itemInfo;
			this._extendedItemInfo.ItemObject = this;
			this._itemInfo = this._component.ItemInfoList[this._extendedItemInfo.Index] as AnScrollItemInfo;
			this.Initialize();
		}

		public virtual void SetEnable(bool enable)
		{
			for (int i = 0; i < this._componentBaseList.Count; i++)
			{
				if (this._componentBaseList[i].Exist)
				{
					this._componentBaseList[i].UIBase.SetParentEnable(enable);
					if (AnMonoSingleton<AnRootManager>.Instance.UIManager.CurrentInputUIBaseGroupList[0][0] == this._componentBaseList[i].UIBase)
					{
						AnMonoSingleton<AnRootManager>.Instance.UIManager.SetCurrentInputUI(null, 0);
					}
				}
			}
		}

		public virtual void SetObjectID(int objectID)
		{
			this._objectID = objectID;
		}

		public virtual void CheckObject()
		{
			if (this._extendedItemInfo == null || this._itemInfo == null)
			{
				this.Reset();
				return;
			}
			if (!this.IsActiveComponent())
			{
				this.Reset();
				return;
			}
			if (this._extendedItemInfo.ExtendedIndex < this._component.ScrollList.CurrentMinIndex - this._component.ScrollList.IndexOffset || this._extendedItemInfo.ExtendedIndex > this._component.ScrollList.CurrentMaxIndex + this._component.ScrollList.IndexOffset)
			{
				this.Reset();
				return;
			}
			if (this._component.ScrollList.ScrollModeType == AnScrollBase.ScrollModeTypes.Normal && this._extendedItemInfo.ExtendedIndex >= 0)
			{
				int extendedIndex = this._extendedItemInfo.ExtendedIndex;
				int count = this._component.ItemInfoList.Count;
				this.UpdateEnd();
				return;
			}
			this.UpdateEnd();
		}

		public virtual void Reset()
		{
			this._itemInfo = null;
			this._extendedItemInfo = null;
			if (!this._existRoot)
			{
				if (this._rootObject.activeSelf)
				{
					this._rootObject.SetActive(false);
				}
			}
			else if (this._root.Visible)
			{
				this._root.SetVisible(false);
			}
			this._rootObject.transform.localPosition = Vector3.one * 90000f;
		}

		public virtual bool IsActiveComponent()
		{
			return !(this._component == null) && this._component.ScrollList != null && this._component.ItemInfoList != null && this._component.ItemInfoList.Count != 0 && this._component.ItemInfoExtendedList != null && this._component.ItemInfoExtendedList.Count != 0 && this._component.ItemObjectList != null && this._component.ItemObjectList.Count != 0;
		}

		protected int _objectID;

		protected GameObject _rootObject;

		protected bool _existRoot;

		protected AnRoot _root;

		protected AnObjectScrollListComponent _component;

		protected AnScrollItemInfo _itemInfo;

		protected AnScrollItemInfo _extendedItemInfo;

		protected List<AnComponentBase> _componentBaseList;

		protected Transform _startObject;

		protected Transform _endObject;

		protected float _objectWidth;

		protected float _objectOffset;

		protected Vector3 _tempVector0 = Vector3.zero;
	}
}
