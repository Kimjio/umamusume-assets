using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnTextScroll : AnScrollBase
	{
		public AnTextScrollComponent Component
		{
			get
			{
				return this._component as AnTextScrollComponent;
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

		public AnText Text
		{
			get
			{
				return this._text;
			}
		}

		public void SetOtherPath(string textObjectPath, string rangeStartObjectPath, string rangeEndObjectPath)
		{
			AnUtilityString.ReplaceString(textObjectPath, ref this._textObjectPath);
			AnUtilityString.ReplaceString(rangeStartObjectPath, ref this._rangeStartObjectPath);
			AnUtilityString.ReplaceString(rangeEndObjectPath, ref this._rangeEndObjectPath);
		}

		public void SetScrollBarPath(string scrollBarMotionPath)
		{
			AnUtilityString.ReplaceString(scrollBarMotionPath, ref this._scrollBarMotionPath);
		}

		protected override bool _InitializeThisData()
		{
			base._InitializeThisData();
			if (AnUtilityString.IsEmptyString(this._textObjectPath))
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
			this._text = this._root.Find<AnText>(this._motion.GameObject, this._textObjectPath, false);
			this._rangeStartObject = this._root.FindComponent<Transform>(this._motion.GameObject, this._rangeStartObjectPath, false);
			this._rangeEndObject = this._root.FindComponent<Transform>(this._motion.GameObject, this._rangeEndObjectPath, false);
			if (this._text == null)
			{
				return false;
			}
			if (this._text.MainTextMesh == null)
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
			this._text.SetTextWrap(true);
			return true;
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this._CheckDirection();
			this._InitializeScrollBar();
		}

		private void _CheckDirection()
		{
			Vector3 vector = this._rangeEndObject.transform.position - this._rangeStartObject.transform.position;
			float num = Vector3.Dot(Vector3.right, vector.normalized);
			this._directionSign = 1;
			this._directionSign = this._directionSign;
			if (num < 0.5f && num > -0.5f)
			{
				if (this._rangeStartObject.position.y > this._rangeEndObject.position.y)
				{
					this._directionType = AnUIDirectionTypes.TopToButtom;
					return;
				}
				this._directionType = AnUIDirectionTypes.BottomToTop;
				this._directionSign = -1;
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
				this._directionSign = -1;
				return;
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
			this._scrollBar.Motion.SetVisible(false);
		}

		private void _OnDownLoopScrollBar(object arg)
		{
			bool existScrollBar = this._existScrollBar;
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
			this._scrollRange = this._rangeStartObject.transform.position.y - this._rangeEndObject.transform.position.y;
			this._allScrollLength = this._text.MainTextMeshRenderer.bounds.max.y - this._text.MainTextMeshRenderer.bounds.min.y;
			this._minScrollPosition = 0f;
			this._maxScrollPosition = this._allScrollLength - this._scrollRange;
		}

		private void _UpdateItemPosition()
		{
			this._text.GameObject.transform.localPosition = this._text.Parameter.Position;
			this._text.GameObject.transform.position = new Vector3(this._text.GameObject.transform.position.x, this._text.GameObject.transform.position.y + this._currentScrollPosition, this._text.GameObject.transform.position.z);
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
			if (this._directionType == AnUIDirectionTypes.LeftToRight)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetScrollPosition(this._currentScrollPosition + AnMonoSingleton<AnRootManager>.Instance._GetScrollIncrementValue(), true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetScrollPosition(this._currentScrollPosition - AnMonoSingleton<AnRootManager>.Instance._GetScrollIncrementValue(), true);
				}
			}
			else if (this._directionType == AnUIDirectionTypes.RightToLeft)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Right && anUIInputDirectionTypes != AnUIInputDirectionTypes.Left)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Right)
				{
					this.SetScrollPosition(this._currentScrollPosition - AnMonoSingleton<AnRootManager>.Instance._GetScrollIncrementValue(), true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Left)
				{
					this.SetScrollPosition(this._currentScrollPosition + AnMonoSingleton<AnRootManager>.Instance._GetScrollIncrementValue(), true);
				}
			}
			else if (this._directionType == AnUIDirectionTypes.TopToButtom)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this.SetScrollPosition(this._currentScrollPosition + AnMonoSingleton<AnRootManager>.Instance._GetScrollIncrementValue(), true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this.SetScrollPosition(this._currentScrollPosition - AnMonoSingleton<AnRootManager>.Instance._GetScrollIncrementValue(), true);
				}
			}
			else if (this._directionType == AnUIDirectionTypes.BottomToTop)
			{
				if (anUIInputDirectionTypes != AnUIInputDirectionTypes.Up && anUIInputDirectionTypes != AnUIInputDirectionTypes.Down)
				{
					return false;
				}
				if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Down)
				{
					this.SetScrollPosition(this._currentScrollPosition - AnMonoSingleton<AnRootManager>.Instance._GetScrollIncrementValue(), true);
				}
				else if (anUIInputDirectionTypes == AnUIInputDirectionTypes.Up)
				{
					this.SetScrollPosition(this._currentScrollPosition + AnMonoSingleton<AnRootManager>.Instance._GetScrollIncrementValue(), true);
				}
			}
			return true;
		}

		public void SetScrollPosition(float scrollPositon, bool animation)
		{
			this._UpdateForce();
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
			AnUtilityValue.LimitValue(ref this._currentScrollPosition, this._minScrollPosition, this._maxScrollPosition);
			AnUtilityValue.LimitValue(ref this._targetScrollPosition, this._minScrollPosition, this._maxScrollPosition);
			if (animation)
			{
				this._Update_ScrollSpring_Init();
			}
			this._ResetPrevValue();
			this._UpdateForce();
		}

		private string _textObjectPath = "TXT_";

		private AnText _text;

		private string _rangeStartObjectPath = "OBJ_pos_start";

		private string _rangeEndObjectPath = "OBJ_pos_end";

		private Transform _rangeStartObject;

		private Transform _rangeEndObject;

		private int _directionSign = 1;

		private bool _existScrollBar;

		private AnScrollBar _scrollBar;

		private string _scrollBarMotionPath = "MOT_scrBar_";
	}
}
