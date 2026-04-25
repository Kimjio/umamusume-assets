using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnObjectScrollList : AnScrollBase
	{
		public AnObjectScrollListComponent Component
		{
			get
			{
				return this._component as AnObjectScrollListComponent;
			}
		}

		public string ItemStartPrefix
		{
			get
			{
				return this._itemStartPrefix;
			}
		}

		public string ItemEndPrefix
		{
			get
			{
				return this._itemEndPrefix;
			}
		}

		public GameObject ItemRootObject
		{
			get
			{
				return this._itemRootObject;
			}
		}

		public GameObject ItemScrollPositionObject
		{
			get
			{
				return this._itemScrollPositionObject;
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

		public bool IsItemStop
		{
			get
			{
				return this._isItemStop;
			}
		}

		public bool ItemStopOneByOne
		{
			get
			{
				return this._itemStopOneByOne;
			}
		}

		public AnScrollBar ScrollBar
		{
			get
			{
				return this._scrollBar;
			}
		}

		public AnCheckButtonList CheckButtonList
		{
			get
			{
				return this._checkButtonList;
			}
		}

		public int CurrentIndex
		{
			get
			{
				return this._currentIndex;
			}
		}

		public int PrevIndex
		{
			get
			{
				return this._prevIndex;
			}
		}

		public int CurrentMinIndex
		{
			get
			{
				return this._currentMinIndex;
			}
		}

		public int CurrentMaxIndex
		{
			get
			{
				return this._currentMaxIndex;
			}
		}

		public int IndexOffset
		{
			get
			{
				return this._indexOffset;
			}
		}

		public int ItemCount
		{
			get
			{
				if (!this._ExistItemList())
				{
					return 0;
				}
				return this.Component.ItemInfoList.Count;
			}
		}

		public AnObjectScrollList()
		{
			this._logTitle = "UI ObjectScrollList";
			this._logColor = new Color(1f, 0.5f, 0.25f);
			this.SetEnableSelectInput(false);
			this.SetEnableOverInput(false);
		}

		public void SetOtherPath(string rangeStartObjectPath, string rangeEndObjectPath, string itemStartObjectPrefix, string itemEndObjectPrefix)
		{
			AnUtilityString.ReplaceString(rangeStartObjectPath, ref this._rangeStartObjectPath);
			AnUtilityString.ReplaceString(rangeEndObjectPath, ref this._rangeEndObjectPath);
			AnUtilityString.ReplaceString(itemStartObjectPrefix, ref this._itemStartPrefix);
			AnUtilityString.ReplaceString(itemEndObjectPrefix, ref this._itemEndPrefix);
		}

		public void SetScrollBarPath(string scrollBarMotionPath)
		{
			AnUtilityString.ReplaceString(scrollBarMotionPath, ref this._scrollBarMotionPath);
		}

		public void SetCheckButtonListPath(string checkButtonListMotionPath)
		{
			AnUtilityString.ReplaceString(checkButtonListMotionPath, ref this._checkButtonListMotionPath);
		}

		protected override bool _InitializeThisData()
		{
			base._InitializeThisData();
			if (AnUtilityString.IsEmptyString(this._rangeStartObjectPath))
			{
				return false;
			}
			if (AnUtilityString.IsEmptyString(this._rangeEndObjectPath))
			{
				return false;
			}
			this._rangeStartObject = this._root.FindComponent<Transform>(this._rootObject, this._rangeStartObjectPath, false);
			this._rangeEndObject = this._root.FindComponent<Transform>(this._rootObject, this._rangeEndObjectPath, false);
			if (this._rangeStartObject == null)
			{
				return false;
			}
			if (this._rangeEndObject == null)
			{
				return false;
			}
			this._UpdateDirection();
			this._InitializeScrollBar();
			this._InitializeCheckButtonList();
			return true;
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

		public void _InitializeItemRootObject(string newName, GameObject parentObject)
		{
			if (!this._exist)
			{
				return;
			}
			if (!AnUtilityString.IsEmptyString(newName))
			{
				this._itemRootObjectName = newName;
			}
			if (this._itemRootObject != null)
			{
				global::UnityEngine.Object.Destroy(this._itemRootObject);
			}
			this._itemRootObject = new GameObject(this._itemRootObjectName);
			this._itemRootObject.name = newName;
			this._itemScrollPositionObject = new GameObject(this._itemScrollPositionObjectName);
			this._itemScrollPositionObject.transform.parent = this._itemRootObject.transform;
			if (parentObject == null)
			{
				return;
			}
			this._itemParentObject = parentObject;
			if (this._itemParentObject != null)
			{
				this._itemRootObject.transform.parent = this._itemParentObject.transform;
			}
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
			this._scrollBar.FlActionValueChangeStart.AddAction(new Action<object>(this._OnDownInScrollBar), null, -1);
			this._scrollBar.FlActionValueChangeLoop.AddAction(new Action<object>(this._OnDownLoopScrollBar), null, -1);
			this._scrollBar.FlActionValueChangeEnd.AddAction(new Action<object>(this._OnDownOutScrollBar), null, -1);
			this._scrollBar.Motion.SetVisible(false);
		}

		private void _OnDownInScrollBar(object arg)
		{
			if (!this._existScrollBar)
			{
				return;
			}
			this._onScrollBar = true;
			this.SetScrollPosition(this._scrollBar.Value, false);
		}

		private void _OnDownLoopScrollBar(object arg)
		{
			if (!this._existScrollBar)
			{
				return;
			}
			this.SetScrollPosition(this._scrollBar.Value, false);
		}

		private void _OnDownOutScrollBar(object arg)
		{
			if (!this._existScrollBar)
			{
				return;
			}
			this._onScrollBar = false;
		}

		public void _InitializeCheckButtonList()
		{
			this._existCheckButtonList = false;
			if (AnUtilityString.IsEmptyString(this._checkButtonListMotionPath))
			{
				return;
			}
			AnMotion anMotion = this._root.Find<AnMotion>(this._rootObject, this._checkButtonListMotionPath, false);
			if (anMotion == null)
			{
				return;
			}
			AnCheckButtonListComponent component = anMotion.ParentObject.GameObject.GetComponent<AnCheckButtonListComponent>();
			if (component == null)
			{
				return;
			}
			if (component.CheckButtonList == null)
			{
				return;
			}
			if (!component.CheckButtonList.Exist)
			{
				return;
			}
			this._existCheckButtonList = true;
			this._checkButtonList = component.CheckButtonList;
			this._checkButtonList.SetParentUI(this);
			this._checkButtonList.FlActionValueChangeStart.AddAction(new Action<object>(this._OnCheckStartCheckButtonList), null, -1);
			this._checkButtonList.Motion.SetVisible(false);
		}

		private void _OnCheckStartCheckButtonList(object arg)
		{
			if (!this._existCheckButtonList)
			{
				return;
			}
			this._onCheckButtonList = true;
			this.SetScrollPositionFromItemIndex(this._checkButtonList.CurrentIndex, true);
		}

		protected override void _ResetPrevValue()
		{
			base._ResetPrevValue();
			this._prevRangeStartPosition = Vector3.one * float.MinValue;
			this._prevRangeEndPosition = Vector3.one * float.MinValue;
			this._prevIndex = int.MinValue;
			this._prevMinIndex = int.MinValue;
			this._prevMaxIndex = int.MinValue;
		}

		public override void _Release()
		{
			base._Release();
			if (!this._exist)
			{
				return;
			}
			if (this._checkButtonList != null)
			{
				this._checkButtonList._Release();
				this._checkButtonList = null;
			}
			if (this._itemRootObject != null)
			{
				global::UnityEngine.Object.Destroy(this._itemRootObject);
				this._itemRootObject = null;
			}
			this._exist = false;
		}

		protected override void _Update_Loop_Init()
		{
			base._Update_Loop_Init();
			this._onCheckButtonList = false;
		}

		protected override void _Update_Scroll_Init()
		{
			base._Update_Scroll_Init();
			this._muteItemStop = false;
			this._scrollStartIndex = this._currentIndex;
		}

		protected override void _Update_ScrollOut_Init()
		{
			base._Update_ScrollOut_Init();
			if (this._isItemStop && !this._muteItemStop && this._itemStopOneByOne)
			{
				this._Update_ScrollSpring_Init();
			}
		}

		protected override void _Update_ScrollSpring_Init()
		{
			if (this._isItemStop && !this._muteItemStop)
			{
				float num;
				if (this._itemStopOneByOne)
				{
					if (this._scrollStartIndex == this._currentIndex)
					{
						if (this._outStartSpeed > 0f)
						{
							num = this.GetScrollPositionFromItemIndex(this._currentIndex + 1);
						}
						else if (this._outStartSpeed < 0f)
						{
							num = this.GetScrollPositionFromItemIndex(this._currentIndex - 1);
						}
						else
						{
							num = this.GetScrollPositionFromItemIndex(this._currentIndex);
						}
					}
					else
					{
						num = this.GetScrollPositionFromItemIndex(this._currentIndex);
					}
				}
				else
				{
					num = this.GetScrollPositionFromItemIndex(this._currentIndex);
				}
				this._useTargetScrollPosition = true;
				this._targetScrollPosition = num;
			}
			base._Update_ScrollSpring_Init();
		}

		protected override void _UpdateValueChange()
		{
			base._UpdateValueChange();
			this._UpdateItemRootPosition();
			this._UpdateAllItemLength();
			this._UpdateScrollIndex();
			this._CheckItemObject();
			this._UpdateItem();
			this._UpdateItemScrollPositionObject();
			this._UpdateScrollBar();
			this._UpdateCheckButtonList();
			this._CheckItemObject();
		}

		protected override void _UpdatePrevValueChange()
		{
			base._UpdatePrevValueChange();
			this._prevIndex = this._currentIndex;
			this._prevMinIndex = this._currentMinIndex;
			this._prevMaxIndex = this._currentMaxIndex;
			this._prevRangeStartPosition = this._currentRangeStartPosition;
			this._prevRangeEndPosition = this._currentRangeEndPosition;
		}

		private void _UpdateItemRootPosition()
		{
			this._currentRangeStartPosition = this._rangeStartObject.position;
			this._currentRangeEndPosition = this._rangeEndObject.position;
			if (this._currentRangeStartPosition == this._prevRangeStartPosition && this._currentRangeEndPosition == this._prevRangeEndPosition)
			{
				return;
			}
			if (this._itemRootObject != null)
			{
				this._itemRootObject.transform.position = this._currentRangeStartPosition;
			}
			if (this._directionType == AnUIDirectionTypes.TopToButtom || this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				this._itemEndPositionOffset = AnUtilityValue.GetAbsValue(this._rangeStartObject.position.y - this._rangeEndObject.position.y);
				return;
			}
			if (this._directionType == AnUIDirectionTypes.LeftToRight || this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				this._itemEndPositionOffset = AnUtilityValue.GetAbsValue(this._rangeStartObject.position.x - this._rangeEndObject.position.x);
			}
		}

		private void _UpdateAllItemLength()
		{
			if (this._currentRangeStartPosition == this._prevRangeStartPosition && this._currentRangeEndPosition == this._prevRangeEndPosition)
			{
				return;
			}
			this._scrollRange = this._itemEndPositionOffset;
			this._allScrollLength = 0f;
			this._minScrollPosition = 0f;
			this._maxScrollPosition = 0f;
			if (this.Component.ItemInfoList != null && this.Component.ItemInfoList.Count > 0)
			{
				AnScrollItemInfo anScrollItemInfo = this.Component.ItemInfoList[this.Component.ItemInfoList.Count - 1] as AnScrollItemInfo;
				if (anScrollItemInfo != null)
				{
					this._allScrollLength = anScrollItemInfo.EndPosition;
					this._maxScrollPosition = this._allScrollLength - this._scrollRange;
				}
			}
			if (this._allScrollLength == 0f)
			{
				this._currentScrollPosition = 0f;
				return;
			}
			if (this._scrollModeType != AnScrollBase.ScrollModeTypes.Endless && this._allScrollLength <= this._scrollRange)
			{
				this._currentScrollPosition = 0f;
				return;
			}
		}

		private void _UpdateScrollIndex()
		{
			this._currentIndex = -1;
			this._currentMinIndex = int.MinValue;
			this._currentMaxIndex = -1;
			if (this.Component.ItemInfoExtendedList == null)
			{
				return;
			}
			for (int i = 0; i < this.Component.ItemInfoExtendedList.Count; i++)
			{
				AnScrollItemInfo anScrollItemInfo = this.Component.ItemInfoExtendedList[i] as AnScrollItemInfo;
				if (anScrollItemInfo != null)
				{
					if (anScrollItemInfo.CenterPosition > this._currentScrollPosition && this._currentMinIndex == -2147483648)
					{
						this._currentIndex = i;
						this._currentMinIndex = i;
						this._currentMaxIndex = i;
					}
					else
					{
						this._currentMaxIndex = i;
					}
					if (anScrollItemInfo.EndPosition >= this._currentScrollPosition + base.ScrollRange)
					{
						break;
					}
				}
			}
			if (this._currentMinIndex == -2147483648)
			{
				return;
			}
			this._currentIndex -= this.Component.ItemInfoList.Count;
			this._currentMinIndex -= this.Component.ItemInfoList.Count;
			this._currentMaxIndex -= this.Component.ItemInfoList.Count;
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Normal)
			{
				AnUtilityValue.LimitValue(ref this._currentIndex, 0, this.Component.ItemInfoList.Count - 1);
				AnUtilityValue.LimitValue(ref this._currentMinIndex, 0, this.Component.ItemInfoList.Count - 1);
				AnUtilityValue.LimitValue(ref this._currentMaxIndex, 0, this.Component.ItemInfoList.Count - 1);
				return;
			}
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Endless)
			{
				if (this._currentIndex < 0)
				{
					this._currentIndex += this.Component.ItemInfoList.Count;
					this._currentMinIndex += this.Component.ItemInfoList.Count;
					this._currentMaxIndex += this.Component.ItemInfoList.Count;
					this._currentScrollPosition += this._allScrollLength;
					return;
				}
				if (this._currentIndex >= this.Component.ItemInfoList.Count)
				{
					this._currentIndex -= this.Component.ItemInfoList.Count;
					this._currentMinIndex -= this.Component.ItemInfoList.Count;
					this._currentMaxIndex -= this.Component.ItemInfoList.Count;
					this._currentScrollPosition -= this._allScrollLength;
				}
			}
		}

		private void _UpdateItem()
		{
			if (this._currentMinIndex == this._prevMinIndex && this._currentMaxIndex == this._prevMaxIndex)
			{
				return;
			}
			if (this.Component.ItemInfoList == null)
			{
				return;
			}
			if (this.Component.ItemInfoList.Count == 0)
			{
				return;
			}
			for (int i = this._currentMinIndex - this._indexOffset; i <= this._currentMaxIndex + this._indexOffset; i++)
			{
				int num = i + this.Component.ItemInfoList.Count;
				if ((this._scrollModeType != AnScrollBase.ScrollModeTypes.Normal || (num >= this.Component.ItemInfoList.Count && num < this.Component.ItemInfoList.Count * 2)) && num >= 0 && num < this.Component.ItemInfoExtendedList.Count)
				{
					AnScrollItemInfo anScrollItemInfo = this.Component.ItemInfoExtendedList[num] as AnScrollItemInfo;
					if (anScrollItemInfo != null && (anScrollItemInfo.ItemObject == null || anScrollItemInfo.ItemObject.ExtendedItemInfo != anScrollItemInfo))
					{
						this._GetFreeItemObject(anScrollItemInfo);
					}
				}
			}
		}

		private void _GetFreeItemObject(AnScrollItemInfo itemInfo)
		{
			for (int i = 0; i < this.Component.ItemObjectList.Count; i++)
			{
				AnScrollItemObject anScrollItemObject = this.Component.ItemObjectList[i] as AnScrollItemObject;
				if (anScrollItemObject != null && anScrollItemObject.ObjectID == itemInfo.ObjectID && anScrollItemObject.ExtendedItemInfo == null)
				{
					anScrollItemObject.SetItemInfo(itemInfo);
					return;
				}
			}
		}

		private void _UpdateItemScrollPositionObject()
		{
			if (this._itemRootObject == null)
			{
				return;
			}
			this._tempVector0.x = 0f;
			this._tempVector0.y = 0f;
			this._tempVector0.z = 0f;
			if (this._directionType == AnUIDirectionTypes.TopToButtom || this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				this._tempVector0.y = this._currentScrollPosition;
			}
			else if (this._directionType == AnUIDirectionTypes.LeftToRight || this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				this._tempVector0.x = -this._currentScrollPosition;
			}
			this._itemScrollPositionObject.transform.localPosition = this._tempVector0;
		}

		private void _CheckItemObject()
		{
			if (this.Component.ItemObjectList == null)
			{
				return;
			}
			for (int i = 0; i < this.Component.ItemObjectList.Count; i++)
			{
				AnScrollItemObject anScrollItemObject = this.Component.ItemObjectList[i] as AnScrollItemObject;
				if (anScrollItemObject != null)
				{
					anScrollItemObject.CheckObject();
				}
			}
		}

		private void _UpdateScrollBar()
		{
			if (!this._existScrollBar)
			{
				return;
			}
			if (this._allScrollLength > this._scrollRange)
			{
				if (!this._scrollBar.Motion.Visible)
				{
					this._scrollBar.Motion.SetVisible(true);
				}
				if (!this._onScrollBar)
				{
					this._scrollBar.SetRange(0f, this._allScrollLength, this._scrollRange);
					this._scrollBar.SetValue(this._currentScrollPosition);
					return;
				}
			}
			else if (this._scrollBar.Motion.Visible)
			{
				this._scrollBar.Motion.SetVisible(false);
			}
		}

		private void _UpdateCheckButtonList()
		{
			if (!this._existCheckButtonList)
			{
				return;
			}
			if (!this._ExistItemList())
			{
				if (this._checkButtonList.Motion.Visible)
				{
					this._checkButtonList.Motion.SetVisible(false);
				}
				return;
			}
			if (!this._checkButtonList.Motion.Visible)
			{
				this._checkButtonList.Motion.SetVisible(true);
			}
			if (!this._onCheckButtonList)
			{
				this._checkButtonList.SetCount(this.Component.ItemInfoList.Count);
				this._checkButtonList.SetIndex(this._currentIndex, true);
			}
		}

		public override bool _UpdateUI(object arg)
		{
			AnUIInputDirectionTypes anUIInputDirectionTypes = (AnUIInputDirectionTypes)arg;
			if (anUIInputDirectionTypes == AnUIInputDirectionTypes.None)
			{
				return false;
			}
			if (this._directionType == AnUIDirectionTypes.LeftToRight)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetScrollPositionFromItemIndex(this._currentMinIndex + 1, true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetScrollPositionFromItemIndex(this._currentMinIndex - 1, true);
				}
			}
			else if (this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
				}
			}
			else if (this._directionType == AnUIDirectionTypes.TopToButtom)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Down && anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
				}
			}
			else if (this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
				}
			}
			return true;
		}

		public void SetScrollModeType(AnScrollBase.ScrollModeTypes scrollModeType)
		{
			this._scrollModeType = scrollModeType;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetIndexOffset(int indexOffset)
		{
			this._indexOffset = indexOffset;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetItemStop(bool enable)
		{
			this.SetItemStop(enable, this._itemStopOneByOne);
		}

		public void SetItemStop(bool enable, bool oneByOne)
		{
			this._isItemStop = enable;
			this._itemStopOneByOne = oneByOne;
			this._ResetPrevValue();
			this._UpdateForce();
		}

		public void SetScrollPosition(float scrollPosition)
		{
			this.SetScrollPosition(scrollPosition, false);
		}

		public void SetScrollPosition(float scrollPositon, bool animation)
		{
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Endless && this._currentScrollState != AnScrollBase.ScrollStateTypes.None)
			{
				return;
			}
			this._UpdateForce();
			this._ResetPrevValue();
			this._UpdateItemRootPosition();
			this._UpdateAllItemLength();
			this._muteItemStop = true;
			if (animation)
			{
				this._targetScrollPosition = scrollPositon;
				this._useTargetScrollPosition = true;
			}
			else
			{
				this._currentScrollPosition = scrollPositon;
				this._useTargetScrollPosition = false;
			}
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Normal)
			{
				AnUtilityValue.LimitValue(ref this._currentScrollPosition, this._minScrollPosition, this._maxScrollPosition);
				AnUtilityValue.LimitValue(ref this._targetScrollPosition, this._minScrollPosition, this._maxScrollPosition);
			}
			if (animation)
			{
				this._Update_ScrollSpring_Init();
			}
			this._UpdateForce();
		}

		[Obsolete("Use SetScrollPositionFromItemIndex")]
		public void SetScrollIndex(int itemIndex)
		{
			this.SetScrollPositionFromItemIndex(itemIndex, false);
		}

		[Obsolete("Use SetScrollPositionFromItemIndex")]
		public void SetScrollIndex(int itemIndex, bool animation)
		{
			this.SetScrollPositionFromItemIndex(itemIndex, animation);
		}

		public void SetScrollPositionFromItemIndex(int itemIndex, bool animation)
		{
			float num = 0f;
			float num2 = 0f;
			this._GetDifferPositionFromItemIndex(itemIndex, ref num, ref num2);
			this.SetScrollPosition(this._currentScrollPosition + num, animation);
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
			if (!this._ExistItemList())
			{
				return;
			}
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Normal)
			{
				AnUtilityValue.LimitValue(ref itemIndex, 0, this.Component.ItemInfoList.Count - 1);
			}
			else if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Endless)
			{
				if (itemIndex < 0)
				{
					itemIndex += this.Component.ItemInfoList.Count;
				}
				else if (itemIndex > this.Component.ItemInfoList.Count - 1)
				{
					itemIndex -= this.Component.ItemInfoList.Count;
				}
			}
			int num = itemIndex;
			if (this._scrollModeType == AnScrollBase.ScrollModeTypes.Endless)
			{
				int num2;
				if ((float)this._currentIndex > (float)(this.Component.ItemInfoList.Count - 1) * 0.5f)
				{
					num2 = itemIndex + this.Component.ItemInfoList.Count;
				}
				else
				{
					num2 = itemIndex - this.Component.ItemInfoList.Count;
				}
				if (AnUtilityValue.GetAbsValue((float)(this._currentIndex - num2)) < AnUtilityValue.GetAbsValue((float)(this._currentIndex - itemIndex)))
				{
					num = num2;
				}
			}
			num += this.Component.ItemInfoList.Count;
			AnUtilityValue.LimitValue(ref num, 0, this.Component.ItemInfoExtendedList.Count - 1);
			AnScrollItemInfo anScrollItemInfo = this.Component.ItemInfoExtendedList[num] as AnScrollItemInfo;
			differStart = anScrollItemInfo.StartPosition - this._currentScrollPosition;
			differEnd = anScrollItemInfo.EndPosition - this._currentScrollPosition;
		}

		public int GetScrollItemIndexFromPosition(float position)
		{
			if (!this._ExistItemList())
			{
				return 0;
			}
			for (int i = 0; i < this.Component.ItemInfoExtendedList.Count - 1; i++)
			{
				AnScrollItemInfo anScrollItemInfo = this.Component.ItemInfoExtendedList[i] as AnScrollItemInfo;
				AnScrollItemInfo anScrollItemInfo2 = this.Component.ItemInfoExtendedList[i + 1] as AnScrollItemInfo;
				if (position >= anScrollItemInfo.StartPosition && position < anScrollItemInfo2.StartPosition)
				{
					return anScrollItemInfo.ExtendedIndex;
				}
			}
			return (this.Component.ItemInfoExtendedList[this.Component.ItemInfoExtendedList.Count - 1] as AnScrollItemInfo).ExtendedIndex;
		}

		public float GetScrollPositionFromItemIndex(int itemIndex)
		{
			if (!this._ExistItemList())
			{
				return 0f;
			}
			int num = itemIndex + this.Component.ItemInfoList.Count;
			if (num < 0)
			{
				return (this.Component.ItemInfoExtendedList[0] as AnScrollItemInfo).StartPosition;
			}
			if (num >= this.Component.ItemInfoExtendedList.Count)
			{
				return (this.Component.ItemInfoExtendedList[this.Component.ItemInfoExtendedList.Count - 1] as AnScrollItemInfo).StartPosition;
			}
			return (this.Component.ItemInfoExtendedList[num] as AnScrollItemInfo).StartPosition;
		}

		private bool _ExistItemList()
		{
			return !(this.Component == null) && this.Component.ItemObjectList != null && this.Component.ItemObjectList.Count != 0 && this.Component.ItemInfoList != null && this.Component.ItemInfoList.Count != 0 && this.Component.ItemInfoExtendedList != null && this.Component.ItemInfoExtendedList.Count != 0;
		}

		protected string _itemRootObjectName = "ScrollListItemRoot";

		protected GameObject _itemRootObject;

		protected GameObject _itemParentObject;

		protected string _itemScrollPositionObjectName = "ScrollPosition";

		protected GameObject _itemScrollPositionObject;

		protected string _rangeStartObjectPath = "OBJ_pos_start";

		protected Transform _rangeStartObject;

		protected string _rangeEndObjectPath = "OBJ_pos_end";

		protected Transform _rangeEndObject;

		protected string _itemStartPrefix = "OBJ_pos_start";

		protected string _itemEndPrefix = "OBJ_pos_end";

		protected float _itemEndPositionOffset;

		protected Vector3 _currentRangeStartPosition = Vector3.zero;

		protected Vector3 _prevRangeStartPosition = Vector3.zero;

		protected Vector3 _currentRangeEndPosition = Vector3.zero;

		protected Vector3 _prevRangeEndPosition = Vector3.zero;

		protected int _currentIndex;

		protected int _currentMinIndex;

		protected int _currentMaxIndex;

		protected int _indexOffset = 1;

		protected int _prevIndex = int.MinValue;

		protected int _prevMinIndex = int.MinValue;

		protected int _prevMaxIndex = int.MinValue;

		protected int _scrollStartIndex;

		protected bool _muteItemStop;

		protected bool _isItemStop;

		protected bool _itemStopOneByOne;

		protected bool _existScrollBar;

		protected string _scrollBarMotionPath = "MOT_scrBar_";

		protected AnScrollBar _scrollBar;

		protected bool _onScrollBar;

		protected bool _existCheckButtonList;

		protected string _checkButtonListMotionPath = "MOT_chkList_";

		protected AnCheckButtonList _checkButtonList;

		protected bool _onCheckButtonList;
	}
}
