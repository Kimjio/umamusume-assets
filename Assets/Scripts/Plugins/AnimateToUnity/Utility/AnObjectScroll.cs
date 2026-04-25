using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnObjectScroll : AnScrollBase
	{
		public AnObjectScrollComponent Component
		{
			get
			{
				return this._component as AnObjectScrollComponent;
			}
		}

		public AnMotion ItemRootMotion
		{
			get
			{
				return this._itemRootMotion;
			}
		}

		public Transform RangeStartObject
		{
			get
			{
				return this._rangeStartObject;
			}
		}

		public Transform RangeEndObject
		{
			get
			{
				return this._rangeEndObject;
			}
		}

		public AnScrollBar ScrollBar
		{
			get
			{
				return this._scrollBar;
			}
		}

		public AnObjectScroll()
		{
			this._logTitle = "UI ObjectScroll";
			this._logColor = new Color(0.5f, 1f, 0.25f);
			this.SetEnableSelectInput(false);
			this.SetEnableOverInput(false);
		}

		public void SetOtherPath(string itemRootMotionPath, string rangeStartObjectPath, string rangeEndObjectPath, string itemStartObjectPreffix)
		{
			AnUtilityString.ReplaceString(itemRootMotionPath, ref this._itemRootMotionPath);
			AnUtilityString.ReplaceString(rangeStartObjectPath, ref this._rangeStartObjectPath);
			AnUtilityString.ReplaceString(rangeEndObjectPath, ref this._rangeEndObjectPath);
			AnUtilityString.ReplaceString(itemStartObjectPreffix, ref this._itemStartObjectPreffix);
		}

		public void SetScrollBarPath(string scrollBarMotionPath)
		{
			AnUtilityString.ReplaceString(scrollBarMotionPath, ref this._scrollBarMotionPath);
		}

		protected override bool _InitializeThisData()
		{
			base._InitializeThisData();
			if (AnUtilityString.IsEmptyString(this._itemRootMotionPath))
			{
				return false;
			}
			if (AnUtilityString.IsEmptyString(this._rangeStartObjectPath))
			{
				return false;
			}
			if (AnUtilityString.IsEmptyString(this._rangeEndObjectPath))
			{
				return false;
			}
			if (AnUtilityString.IsEmptyString(this._itemStartObjectPreffix))
			{
				return false;
			}
			this._itemRootMotion = this._root.Find<AnMotion>(this._motion.GameObject, this._itemRootMotionPath, false);
			this._rangeStartObject = this._root.FindComponent<Transform>(this._motion.GameObject, this._rangeStartObjectPath, false);
			this._rangeEndObject = this._root.FindComponent<Transform>(this._motion.GameObject, this._rangeEndObjectPath, false);
			if (this._itemRootMotion == null)
			{
				return false;
			}
			if (this._rangeStartObject == null)
			{
				return false;
			}
			if (this._rangeEndObject == null)
			{
				return false;
			}
			this._UpdateDirection();
			this._InitializeChildComponentList();
			return this._itemStartObjectList.Count != 0;
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this._UpdateDirection();
			this._InitializeScrollBar();
			base.FlActionScrollStart.AddAction(new Action<object>(this.DisableItemObjectList), null, -1);
			base.FlActionScrollOutStart.AddAction(new Action<object>(this.EnableItemObjectList), null, -1);
		}

		private void _InitializeChildComponentList()
		{
			if (this._itemStartObjectList == null)
			{
				this._itemStartObjectList = new List<Transform>();
			}
			this._itemStartObjectList.Clear();
			for (int i = 0; i < 100; i++)
			{
				string text = this._itemStartObjectPreffix + i.ToString("D2");
				Transform transform = this._motion.Root.FindComponent<Transform>(this._itemRootMotion.GameObject, text, false);
				if (transform == null)
				{
					break;
				}
				this._itemStartObjectList.Add(transform);
			}
			if (this._itemStartObjectList.Count == 0)
			{
				return;
			}
			this._existChildComponentList = false;
			if (this._childComponentBaseList == null)
			{
				this._childComponentBaseList = new List<AnComponentBase>();
			}
			this._childComponentBaseList.Clear();
			AnComponentBase[] componentsInChildren = this._itemRootMotion.GameObject.GetComponentsInChildren<AnComponentBase>(true);
			if (componentsInChildren != null)
			{
				this._childComponentBaseList = new List<AnComponentBase>(componentsInChildren);
			}
			for (int j = 0; j < this._childComponentBaseList.Count; j++)
			{
				AnUIBase uibase = this._childComponentBaseList[j].UIBase;
				if (uibase != null && uibase.Exist)
				{
					int num = this._GetScrollItemIndexFromWorldPosition(uibase.Motion.GameObject.transform.position);
					uibase.FlActionSelectInStart.AddAction(new Action<object>(this._OnItemSelectInStart), num, this._selectActionId, true);
					if (uibase.HitAreaObject != null)
					{
						uibase.HitAreaObject.SetSubCollider(this._hitAreaObject.Collider, true);
					}
				}
			}
			if (this._childComponentBaseList.Count > 0)
			{
				this._existChildComponentList = true;
			}
		}

		protected virtual void EnableItemObjectList(object arg)
		{
			if (!this._existChildComponentList)
			{
				return;
			}
			for (int i = 0; i < this._childComponentBaseList.Count; i++)
			{
				if (this._childComponentBaseList[i].Exist && this._childComponentBaseList[i].UIBase != null)
				{
					this._childComponentBaseList[i].UIBase.SetParentEnable(true);
					if (AnMonoSingleton<AnRootManager>.Instance.UIManager.CurrentInputUIBaseGroupList[0][0] == this._childComponentBaseList[i].UIBase)
					{
						AnMonoSingleton<AnRootManager>.Instance.UIManager.SetCurrentInputUI(null, 0);
					}
				}
			}
		}

		protected virtual void DisableItemObjectList(object arg)
		{
			if (!this._existChildComponentList)
			{
				return;
			}
			for (int i = 0; i < this._childComponentBaseList.Count; i++)
			{
				if (this._childComponentBaseList[i].Exist && this._childComponentBaseList[i].UIBase != null)
				{
					this._childComponentBaseList[i].UIBase.SetParentEnable(false);
					if (AnMonoSingleton<AnRootManager>.Instance.UIManager.CurrentInputUIBaseGroupList[0][0] == this._childComponentBaseList[i].UIBase)
					{
						AnMonoSingleton<AnRootManager>.Instance.UIManager.SetCurrentInputUI(null, 0);
					}
				}
			}
		}

		public void _OnItemSelectInStart(object arg)
		{
			this.SetScrollPositionInRangeFromItemIndex((int)arg, this._isAutoScrollAnimation);
		}

		public void _InitializeScrollBar()
		{
			this._existScrollBar = false;
			if (AnUtilityString.IsEmptyString(this._scrollBarMotionPath))
			{
				return;
			}
			AnMotion anMotion = this._root.Find<AnMotion>(this._rootObject, this._scrollBarMotionPath, false);
			if (anMotion == null)
			{
				return;
			}
			AnScrollBarComponent component = anMotion.ParentObject.GameObject.GetComponent<AnScrollBarComponent>();
			if (component == null)
			{
				return;
			}
			if (component.ScrollBar == null)
			{
				return;
			}
			if (!component.ScrollBar.Exist)
			{
				return;
			}
			this._existScrollBar = true;
			this._scrollBar = component.ScrollBar;
			this._scrollBar.SetParentUI(this);
			this._scrollBar.Motion.SetVisible(false);
		}

		private void _OnDownLoopScrollBar(object arg)
		{
			if (!this._existScrollBar)
			{
				return;
			}
			this.SetScrollPosition(this._scrollBar.Value, false);
		}

		private void _UpdateDirection()
		{
			if (this._rangeStartObject == null)
			{
				return;
			}
			if (this._rangeEndObject == null)
			{
				return;
			}
			Vector3 vector = this._rangeEndObject.transform.position - this._rangeStartObject.transform.position;
			vector.Normalize();
			if (vector.y > 0.5f || vector.y < -0.5f)
			{
				if (this._rangeStartObject.position.y > this._rangeEndObject.position.y)
				{
					this._directionType = AnUIDirectionTypes.TopToButtom;
					return;
				}
				this._directionType = AnUIDirectionTypes.BottomToTop;
				return;
			}
			else
			{
				if (this._rangeStartObject.position.x > this._rangeEndObject.position.x)
				{
					this._directionType = AnUIDirectionTypes.RightToLeft;
					return;
				}
				this._directionType = AnUIDirectionTypes.LeftToRight;
				return;
			}
		}

		public override void _Release()
		{
			base._Release();
			if (!this._exist)
			{
				return;
			}
			if (this._scrollBar != null)
			{
				this._scrollBar._Release();
				this._scrollBar = null;
			}
			this._exist = false;
		}

		protected override void _UpdateValueChange()
		{
			base._UpdateValueChange();
			this._UpdateScrollLength();
			this._UpdateItemPosition();
			this._UpdateScrollBar();
		}

		private void _UpdateScrollLength()
		{
			if (this._directionType == AnUIDirectionTypes.TopToButtom)
			{
				this._scrollRange = this._rangeStartObject.transform.position.y - this._rangeEndObject.transform.position.y;
				this._allScrollLength = this._itemStartObjectList[0].transform.position.y - this._itemStartObjectList[this._itemStartObjectList.Count - 1].transform.position.y;
			}
			this._minScrollPosition = 0f;
			this._maxScrollPosition = this._allScrollLength - this._scrollRange;
			if (this._allScrollLength <= this._scrollRange)
			{
				this._currentScrollPosition = 0f;
			}
		}

		private void _UpdateItemPosition()
		{
			this._itemRootMotion.GameObject.transform.localPosition = Vector3.zero;
			this._itemRootMotion.GameObject.transform.position = new Vector3(this._itemRootMotion.GameObject.transform.position.x, this._itemRootMotion.GameObject.transform.position.y + this._currentScrollPosition, this._itemRootMotion.GameObject.transform.position.z);
		}

		private void _UpdateScrollBar()
		{
			if (!this._existScrollBar)
			{
				return;
			}
			if (this._allScrollLength > this._scrollRange)
			{
				this._scrollBar.Motion.SetVisible(true);
				this._scrollBar.SetRange(0f, this._allScrollLength, this._scrollRange);
				this._scrollBar.SetValue(this._currentScrollPosition);
				return;
			}
			if (this._scrollBar.Motion.Visible)
			{
				this._scrollBar.Motion.SetVisible(false);
			}
		}

		public override bool _UpdateUI(object arg)
		{
			AnUIInputDirectionTypes anUIInputDirectionTypes = (AnUIInputDirectionTypes)arg;
			if (anUIInputDirectionTypes == AnUIInputDirectionTypes.None)
			{
				return false;
			}
			if (this._swipeDirectionType == AnUIDirectionTypes.LeftToRight)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetScrollPosition(this._currentScrollPosition + 1f, false);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetScrollPosition(this._currentScrollPosition - 1f, false);
				}
			}
			else if (this._swipeDirectionType == AnUIDirectionTypes.RightToLeft)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetScrollPosition(this._currentScrollPosition - 1f, false);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetScrollPosition(this._currentScrollPosition + 1f, false);
				}
			}
			else if (this._swipeDirectionType == AnUIDirectionTypes.TopToButtom)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this.SetScrollPosition(this._currentScrollPosition + 1f, false);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this.SetScrollPosition(this._currentScrollPosition - 1f, false);
				}
			}
			else if (this._swipeDirectionType == AnUIDirectionTypes.BottomToTop)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this.SetScrollPosition(this._currentScrollPosition - 1f, false);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this.SetScrollPosition(this._currentScrollPosition + 1f, false);
				}
			}
			return false;
		}

		public void SetScrollPosition(float scrollPositon, bool animation)
		{
			if (animation)
			{
				this._targetScrollPosition = scrollPositon;
				this._useTargetScrollPosition = true;
			}
			else
			{
				this._currentScrollPosition = scrollPositon;
			}
			AnUtilityValue.LimitValue(ref this._currentScrollPosition, this._minScrollPosition, this._maxScrollPosition);
			AnUtilityValue.LimitValue(ref this._targetScrollPosition, this._minScrollPosition, this._maxScrollPosition);
			if (animation)
			{
				this._Update_ScrollSpring_Init();
			}
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetScrollPositionFromItemIndex(int itemIndex, bool animation)
		{
			float num = 0f;
			float num2 = 0f;
			this._GetDifferPositionFromItemIndex(itemIndex, ref num, ref num2);
			if (num > 0f)
			{
				this.SetScrollPosition(this._currentScrollPosition + num, animation);
				return;
			}
			if (num < 0f)
			{
				this.SetScrollPosition(this._currentScrollPosition + num, animation);
			}
		}

		public void SetScrollPositionInRangeFromItemIndex(int itemIndex, bool animation)
		{
			float num = 0f;
			float num2 = 0f;
			this._GetDifferPositionFromItemIndex(itemIndex, ref num, ref num2);
			if (num > 0f && num2 > this._scrollRange)
			{
				this.SetScrollPosition(this._currentScrollPosition + num2 - this._scrollRange, animation);
				return;
			}
			if (num < 0f)
			{
				this.SetScrollPosition(this._currentScrollPosition + num, animation);
			}
		}

		private void _GetDifferPositionFromItemIndex(int itemIndex, ref float differStart, ref float differEnd)
		{
			if (itemIndex < 0)
			{
				itemIndex = 0;
			}
			else if (itemIndex >= this._itemStartObjectList.Count - 1)
			{
				itemIndex = this._itemStartObjectList.Count - 2;
			}
			Transform transform = this._itemStartObjectList[0];
			Transform transform2 = this._itemStartObjectList[itemIndex];
			Transform transform3 = this._itemStartObjectList[itemIndex + 1];
			if (this._directionType == AnUIDirectionTypes.TopToButtom)
			{
				float num = transform.transform.position.y - transform2.transform.position.y;
				float num2 = transform.transform.position.y - transform3.transform.position.y;
				differStart = num - this._currentScrollPosition;
				differEnd = num2 - this._currentScrollPosition;
				return;
			}
			if (this._directionType == AnUIDirectionTypes.LeftToRight)
			{
				float num = transform2.transform.position.x - transform.transform.position.x;
				float num2 = transform3.transform.position.x - transform.transform.position.x;
				differStart = num - this._currentScrollPosition;
				differEnd = num2 - this._currentScrollPosition;
			}
		}

		private int _GetScrollItemIndexFromWorldPosition(Vector3 worldPosition)
		{
			if (this._itemStartObjectList == null)
			{
				return 0;
			}
			if (this._itemStartObjectList.Count == 0)
			{
				return 0;
			}
			float num = 0f;
			if (this._directionType == AnUIDirectionTypes.TopToButtom)
			{
				num = this._itemStartObjectList[0].position.y - worldPosition.y;
			}
			return this.GetScrollItemIndexFromPosition(num);
		}

		public int GetScrollItemIndexFromPosition(float position)
		{
			if (this._itemStartObjectList == null)
			{
				return 0;
			}
			if (this._itemStartObjectList.Count <= 1)
			{
				return 0;
			}
			for (int i = 0; i < this._itemStartObjectList.Count - 1; i++)
			{
				float num = 0f;
				float num2 = 0f;
				if (this._directionType == AnUIDirectionTypes.TopToButtom)
				{
					num = this._itemStartObjectList[0].position.y - this._itemStartObjectList[i].position.y;
					num2 = this._itemStartObjectList[0].position.y - this._itemStartObjectList[i + 1].position.y;
				}
				if (position >= num && position < num2)
				{
					return i;
				}
			}
			return this._itemStartObjectList.Count - 1;
		}

		public float GetScrollPositionFromItemIndex(int itemIndex)
		{
			if (itemIndex < 0)
			{
				return 0f;
			}
			if (itemIndex >= this._itemStartObjectList.Count)
			{
				return this._maxScrollPosition;
			}
			float num = 0f;
			if (this._directionType == AnUIDirectionTypes.TopToButtom)
			{
				num = this._itemStartObjectList[0].position.y - this._itemStartObjectList[itemIndex].position.y;
			}
			return num;
		}

		protected string _itemRootMotionPath = "MOT_frm_itemRoot";

		protected AnMotion _itemRootMotion;

		protected string _rangeStartObjectPath = "OBJ_pos_start";

		protected string _rangeEndObjectPath = "OBJ_pos_end";

		protected Transform _rangeStartObject;

		protected Transform _rangeEndObject;

		protected string _itemStartObjectPreffix = "OBJ_pos_start";

		protected List<Transform> _itemStartObjectList;

		protected bool _existScrollBar;

		protected AnScrollBar _scrollBar;

		protected string _scrollBarMotionPath = "MOT_scrBar_";

		protected List<AnComponentBase> _childComponentBaseList;

		protected bool _existChildComponentList;
	}
}
