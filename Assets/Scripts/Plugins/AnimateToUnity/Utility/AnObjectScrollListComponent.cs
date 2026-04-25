using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnObjectScrollListComponent : AnComponentBase
	{
		public AnObjectScrollList ScrollList
		{
			get
			{
				return this._uiBase as AnObjectScrollList;
			}
		}

		public List<object> ItemObjectList
		{
			get
			{
				return this._itemObjectList;
			}
		}

		public List<object> ItemInfoList
		{
			get
			{
				return this._itemInfoList;
			}
		}

		public List<object> ItemInfoExtendedList
		{
			get
			{
				return this._itemInfoExtendedList;
			}
		}

		protected override void _Initialize_PostProcess()
		{
			base._Initialize_PostProcess();
			this.ScrollList.FlActionScrollStart.AddAction(new Action<object>(this.DisableItemObjectList), null, -1);
			this.ScrollList.FlActionScrollOutStart.AddAction(new Action<object>(this.EnableItemObjectList), null, -1);
			this._itemObjectList = new List<object>();
		}

		protected virtual void EnableItemObjectList(object arg)
		{
			for (int i = 0; i < this._itemObjectList.Count; i++)
			{
				(this._itemObjectList[i] as AnScrollItemObject).SetEnable(true);
			}
		}

		protected virtual void DisableItemObjectList(object arg)
		{
			for (int i = 0; i < this._itemObjectList.Count; i++)
			{
				(this._itemObjectList[i] as AnScrollItemObject).SetEnable(false);
			}
		}

		protected override void _ApplyValue()
		{
			base._ApplyValue();
			string text = this._objectBase.Parameter.UIParameter._GetParameterValue("StartObject", 0);
			string text2 = this._objectBase.Parameter.UIParameter._GetParameterValue("EndObject", 0);
			string text3 = this._objectBase.Parameter.UIParameter._GetParameterValue("ItemStartPrefix", 0);
			string text4 = this._objectBase.Parameter.UIParameter._GetParameterValue("ItemEndPrefix", 0);
			string text5 = this._objectBase.Parameter.UIParameter._GetParameterValue("ScrollMode", 0);
			string text6 = this._objectBase.Parameter.UIParameter._GetParameterValue("ItemStop", 0);
			string text7 = this._objectBase.Parameter.UIParameter._GetParameterValue("ScrollBarMotion", 0);
			string text8 = this._objectBase.Parameter.UIParameter._GetParameterValue("CheckButtonListMotion", 0);
			if (!AnUtilityString.IsEmptyString(text))
			{
				text = AnValue.ObjectPrefix + text;
			}
			if (!AnUtilityString.IsEmptyString(text2))
			{
				text2 = AnValue.ObjectPrefix + text2;
			}
			if (!AnUtilityString.IsEmptyString(text3))
			{
				text3 = AnValue.ObjectPrefix + text3;
			}
			if (!AnUtilityString.IsEmptyString(text4))
			{
				text4 = AnValue.ObjectPrefix + text4;
			}
			this.ScrollList.SetOtherPath(text, text2, text3, text4);
			if (!AnUtilityString.IsEmptyString(text5))
			{
				if (text5 == "Endless")
				{
					this.ScrollList.SetScrollModeType(AnScrollBase.ScrollModeTypes.Endless);
				}
				else
				{
					this.ScrollList.SetScrollModeType(AnScrollBase.ScrollModeTypes.Normal);
				}
			}
			if (!AnUtilityString.IsEmptyString(text6))
			{
				if (text6 == "1")
				{
					this.ScrollList.SetItemStop(true);
				}
				else
				{
					this.ScrollList.SetItemStop(false);
				}
			}
			if (!AnUtilityString.IsEmptyString(text7))
			{
				text7 = AnValue.MotionPrefix + text7;
			}
			this.ScrollList.SetScrollBarPath(text7);
			if (!AnUtilityString.IsEmptyString(text8))
			{
				text8 = AnValue.MotionPrefix + text8;
			}
			this.ScrollList.SetCheckButtonListPath(text8);
		}

		public virtual void CreateItemObject<T>(GameObject prefabObject, int createCount, int sortOffset = 1000, string rootName = null, GameObject parentObject = null) where T : AnScrollItemObject, new()
		{
			if (!this._exist)
			{
				return;
			}
			if (prefabObject == null)
			{
				return;
			}
			if (prefabObject == null || createCount == 0)
			{
				return;
			}
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < createCount; i++)
			{
				GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefabObject);
				list.Add(gameObject);
			}
			this.CreateItemObject<T>(list, sortOffset, rootName, parentObject);
		}

		public virtual void CreateItemObject<T>(List<GameObject> instanceObjectList, int sortOffset = 1000, string rootName = null, GameObject parentObject = null) where T : AnScrollItemObject, new()
		{
			if (!this._exist)
			{
				return;
			}
			if (instanceObjectList == null || instanceObjectList.Count == 0)
			{
				return;
			}
			this.ScrollList._InitializeItemRootObject(rootName, parentObject);
			if (this.ScrollList.ItemRootObject == null)
			{
				return;
			}
			if (this._itemObjectList == null)
			{
				this._itemObjectList = new List<object>();
			}
			this._itemObjectList.Clear();
			for (int i = 0; i < instanceObjectList.Count; i++)
			{
				if (!(instanceObjectList[i] == null))
				{
					T t = new T();
					t.Create(instanceObjectList[i], this, sortOffset);
					this._itemObjectList.Add(t);
				}
			}
			this.CheckItemObjectListHierarchy();
			this.ScrollList.SetEnable(false, AnUIEnableTypes.Normal);
			this.ScrollList.SetEnable(true, AnUIEnableTypes.Normal);
		}

		public virtual void AddItemObject<T>(GameObject prefabObject, int objectID, int createCount, int sortOffset = 1000, string rootName = null, GameObject parentObject = null) where T : AnScrollItemObject, new()
		{
			if (!this._exist)
			{
				return;
			}
			if (prefabObject == null || createCount == 0)
			{
				return;
			}
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < createCount; i++)
			{
				GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefabObject);
				list.Add(gameObject);
			}
			this.AddItemObject<T>(list, objectID, sortOffset, rootName, parentObject);
		}

		public virtual void AddItemObject<T>(List<GameObject> instanceObjectList, int objectID, int sortOffset = 1000, string rootName = null, GameObject parentObject = null) where T : AnScrollItemObject, new()
		{
			if (!this._exist)
			{
				return;
			}
			if (instanceObjectList == null || instanceObjectList.Count == 0)
			{
				return;
			}
			if (this.ScrollList.ItemRootObject == null)
			{
				this.ScrollList._InitializeItemRootObject(rootName, parentObject);
			}
			if (this.ScrollList.ItemRootObject == null)
			{
				return;
			}
			if (this._itemObjectList == null)
			{
				this._itemObjectList = new List<object>();
			}
			for (int i = 0; i < instanceObjectList.Count; i++)
			{
				if (!(instanceObjectList[i] == null))
				{
					T t = new T();
					t.Create(instanceObjectList[i], this, sortOffset);
					t.SetObjectID(objectID);
					this._itemObjectList.Add(t);
				}
			}
			this.CheckItemObjectListHierarchy();
			this.ScrollList.SetEnable(false, AnUIEnableTypes.Normal);
			this.ScrollList.SetEnable(true, AnUIEnableTypes.Normal);
		}

		public virtual void CheckItemObjectListHierarchy()
		{
			if (this._itemObjectList == null)
			{
				return;
			}
			for (int i = 0; i < this._itemObjectList.Count; i++)
			{
				AnScrollItemObject anScrollItemObject = this._itemObjectList[i] as AnScrollItemObject;
				if (anScrollItemObject != null)
				{
					anScrollItemObject.CheckHierarchy();
				}
			}
		}

		public virtual void SetItemInfoList<T>(List<T> newScrollItemInfoList) where T : AnScrollItemInfo, new()
		{
			if (!this._exist)
			{
				return;
			}
			if (this._itemInfoList == null)
			{
				this._itemInfoList = new List<object>();
			}
			if (this._itemInfoExtendedList == null)
			{
				this._itemInfoExtendedList = new List<object>();
			}
			this._itemInfoList.Clear();
			this._itemInfoExtendedList.Clear();
			float num = 0f;
			for (int i = 0; i < newScrollItemInfoList.Count; i++)
			{
				T t = newScrollItemInfoList[i];
				if (t != null)
				{
					t.ExtendedIndex = i;
					t.Index = i;
					for (int j = 0; j < this._itemObjectList.Count; j++)
					{
						AnScrollItemObject anScrollItemObject = this._itemObjectList[j] as AnScrollItemObject;
						if (anScrollItemObject.ObjectID == t.ObjectID)
						{
							t.StartPosition = num;
							t.CenterPosition = num + anScrollItemObject.ObjectOffset;
							t.EndPosition = num + anScrollItemObject.ObjectWidth;
							num += anScrollItemObject.ObjectWidth;
							break;
						}
					}
					this._itemInfoList.Add(t);
					this._itemInfoExtendedList.Add(t);
				}
			}
			if (this._itemInfoList.Count == 0)
			{
				return;
			}
			if (this._tempItemInfoList == null)
			{
				this._tempItemInfoList = new List<object>();
			}
			this._tempItemInfoList.Clear();
			T t2 = this._itemInfoList[this._itemInfoList.Count - 1] as T;
			for (int k = 0; k < this._itemInfoList.Count; k++)
			{
				T t3 = this._itemInfoList[k] as T;
				T t4 = new T();
				t4.ExtendedIndex = k - this._itemInfoList.Count;
				t4.Index = k;
				t4.SetObjectID(t3.ObjectID);
				t4.StartPosition = -t2.EndPosition + t3.StartPosition;
				t4.CenterPosition = -t2.EndPosition + t3.CenterPosition;
				t4.EndPosition = -t2.EndPosition + t3.EndPosition;
				this._tempItemInfoList.Insert(0, t4);
			}
			for (int l = 0; l < this._tempItemInfoList.Count; l++)
			{
				this._itemInfoExtendedList.Insert(0, this._tempItemInfoList[l]);
			}
			this._tempItemInfoList.Clear();
			for (int m = 0; m < this._itemInfoList.Count; m++)
			{
				T t5 = this._itemInfoList[m] as T;
				T t6 = new T();
				t6.ExtendedIndex = this._itemInfoList.Count + m;
				t6.Index = m;
				t6.SetObjectID(t5.ObjectID);
				t6.StartPosition = t2.EndPosition + t5.StartPosition;
				t6.CenterPosition = t2.EndPosition + t5.CenterPosition;
				t6.EndPosition = t2.EndPosition + t5.EndPosition;
				this._tempItemInfoList.Add(t6);
			}
			for (int n = 0; n < this._tempItemInfoList.Count; n++)
			{
				this._itemInfoExtendedList.Add(this._tempItemInfoList[n]);
			}
			this._tempItemInfoList = null;
			for (int num2 = 0; num2 < this._itemObjectList.Count; num2++)
			{
				AnScrollItemObject anScrollItemObject2 = this._itemObjectList[num2] as AnScrollItemObject;
				if (anScrollItemObject2 != null)
				{
					anScrollItemObject2.Reset();
				}
			}
			this.ScrollList.SetEnable(false, AnUIEnableTypes.Normal);
			this.ScrollList.SetEnable(true, AnUIEnableTypes.Normal);
		}

		private List<object> _itemObjectList;

		private List<object> _itemInfoList;

		private List<object> _itemInfoExtendedList;

		private List<object> _tempItemInfoList;
	}
}
