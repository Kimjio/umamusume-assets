using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnTouchInput : AnInputBase
	{
		public Vector3 CurrentFixScreenPosition
		{
			get
			{
				return this._currentFixScreenPosition;
			}
		}

		public Vector3 StartFixScreenPosition
		{
			get
			{
				return this._startFixScreenPosition;
			}
		}

		public Vector3 CurrentScreenPosition
		{
			get
			{
				return this._currentScreenPosition;
			}
		}

		public Vector3 StartScreenPosition
		{
			get
			{
				return this._startScreenPosition;
			}
		}

		public float CurrentFixScreenSpeed
		{
			get
			{
				return this._currentFixScreenSpeed;
			}
		}

		public float CurrentFixScreenAccel
		{
			get
			{
				return this._currentFixScreenAccel;
			}
		}

		public Vector3 FixScreenVectorFromStart
		{
			get
			{
				return this._currentFixScreenPosition - this._startFixScreenPosition;
			}
		}

		public Vector3 FixScreenNormalizeVectorFromStart
		{
			get
			{
				return this.FixScreenVectorFromStart.normalized;
			}
		}

		public float FixScreenDistanceFromStart
		{
			get
			{
				return this.FixScreenVectorFromStart.magnitude;
			}
		}

		public Vector3 AvarageFixScreenPosition
		{
			get
			{
				return this._CalculateAvarageFixScreenPosition();
			}
		}

		public Vector3 AvarageScreenPosition
		{
			get
			{
				return this._CalculateAvarageScreenPosition();
			}
		}

		public Vector3 AvarageFixScreenDirection
		{
			get
			{
				return this._CalculateAvarageFixScreenDirection();
			}
		}

		public Vector3 AvarageScreenDirection
		{
			get
			{
				return this._CalculateAvarageScreenDirection();
			}
		}

		public float AvarageFixScreenSpeed
		{
			get
			{
				return this._CalculateAvarageFixScreenSpeed();
			}
		}

		public float AvarageScreenSpeed
		{
			get
			{
				return this._CalculateAvarageScreenSpeed();
			}
		}

		public float AvarageFixScreenAccel
		{
			get
			{
				return this._CalculateAvarageFixScreenAccel();
			}
		}

		public float AvarageScreenAccel
		{
			get
			{
				return this._CalculateAvarageScreenAccel();
			}
		}

		public AnTouchInput(AnUIManager uiManager, int inputIndex)
			: base(uiManager, inputIndex)
		{
			this._fixScreenPositionList = new List<Vector3>(this._listCapacity);
			this._fixScreenDirectionList = new List<Vector3>(this._listCapacity);
			this._fixScreenSpeedList = new List<float>(this._listCapacity);
			this._fixScreenAccelList = new List<float>(this._listCapacity);
			this._screenPositionList = new List<Vector3>(this._listCapacity);
			this._screenDirectionList = new List<Vector3>(this._listCapacity);
			this._screenSpeedList = new List<float>(this._listCapacity);
			this._screenAccelList = new List<float>(this._listCapacity);
		}

		protected override void _Update_Common_End()
		{
			this._prevFixScreenPosition = this._currentFixScreenPosition;
			this._prevScreenPosition = this._currentScreenPosition;
			base._Update_Common_End();
		}

		protected override void _Update_Wait_Loop()
		{
			if (this._GetTouchDown())
			{
				this._Update_Down_Init();
				return;
			}
			base._Update_Wait_Loop();
		}

		protected override void _Update_Down_Init()
		{
			this._currentScreenPosition = this._GeTouchPosition();
			this._uiManager.CollisionManager._GetHitObjectListWithCameraRay(this._currentScreenPosition, true, ref this._hitObjectList);
			this._startScreenPosition = this._currentScreenPosition;
			AnUtilityVector.GetFixScreenPosition(this._currentScreenPosition, ref this._startFixScreenPosition);
			this._currentFixScreenPosition = this._startFixScreenPosition;
			this._prevFixScreenPosition = this._startFixScreenPosition;
			this._currentFixScreenDirection = Vector3.zero;
			this._startDownTime = Time.realtimeSinceStartup;
			this._startDownTime += 0f;
			this._currentDownTime = Time.realtimeSinceStartup;
			this._prevDownTime = Time.realtimeSinceStartup;
			this._currentFixScreenSpeed = 0f;
			this._prevFixScreenSpeed = 0f;
			this._currentFixScreenAccel = 0f;
			this._fixScreenPositionList.Clear();
			this._fixScreenDirectionList.Clear();
			this._fixScreenSpeedList.Clear();
			this._fixScreenAccelList.Clear();
			this._screenPositionList.Clear();
			this._screenDirectionList.Clear();
			this._screenSpeedList.Clear();
			this._screenAccelList.Clear();
			base._Update_Down_Init();
		}

		protected override void _Update_Down_Loop()
		{
			this._currentScreenPosition = this._GeTouchPosition();
			this._uiManager.CollisionManager._GetHitObjectListWithCameraRay(this._currentScreenPosition, true, ref this._hitObjectList);
			AnUtilityVector.GetFixScreenPosition(this._currentScreenPosition, ref this._currentFixScreenPosition);
			this._currentFixScreenDirection = this._currentFixScreenPosition - this._prevFixScreenPosition;
			this._currentScreenDirection = this._currentScreenPosition - this._prevScreenPosition;
			this._currentDownTime = Time.realtimeSinceStartup;
			this._currentFixScreenSpeed = 0f;
			this._currentFixScreenAccel = 0f;
			if (this._currentDownTime != this._prevDownTime)
			{
				this._currentFixScreenSpeed = this._currentFixScreenDirection.magnitude / (this._currentDownTime - this._prevDownTime);
				this._currentScreenSpeed = this._currentScreenDirection.magnitude / (this._currentDownTime - this._prevDownTime);
				this._currentFixScreenAccel = (this._currentFixScreenSpeed - this._prevFixScreenSpeed) / (this._currentDownTime - this._prevDownTime);
				this._currentScreenAccel = (this._currentScreenSpeed - this._prevScreenSpeed) / (this._currentDownTime - this._prevDownTime);
			}
			this._fixScreenPositionList.Add(this._currentFixScreenPosition);
			this._fixScreenDirectionList.Add(this._currentFixScreenDirection);
			this._fixScreenSpeedList.Add(this._currentFixScreenSpeed);
			this._fixScreenAccelList.Add(this._currentFixScreenAccel);
			this._screenPositionList.Add(this._currentScreenPosition);
			this._screenDirectionList.Add(this._currentScreenDirection);
			this._screenSpeedList.Add(this._currentScreenSpeed);
			this._screenAccelList.Add(this._currentScreenAccel);
			if (this._fixScreenPositionList.Count > this._listCapacity)
			{
				this._fixScreenPositionList.RemoveAt(0);
				this._screenPositionList.RemoveAt(0);
				this._fixScreenDirectionList.RemoveAt(0);
				this._screenDirectionList.RemoveAt(0);
				this._fixScreenSpeedList.RemoveAt(0);
				this._screenSpeedList.RemoveAt(0);
				this._fixScreenAccelList.RemoveAt(0);
				this._screenAccelList.RemoveAt(0);
			}
			if (this._GetTouchUp())
			{
				this._Update_Wait_Init();
				return;
			}
			this._prevFixScreenPosition = this._currentFixScreenPosition;
			this._prevFixScreenSpeed = this._currentFixScreenSpeed;
			this._prevScreenPosition = this._currentScreenPosition;
			this._prevScreenSpeed = this._currentScreenSpeed;
			this._prevDownTime = this._currentDownTime;
			base._Update_Down_Loop();
		}

		private Vector3 _CalculateAvarageFixScreenPosition()
		{
			Vector3 vector = Vector3.zero;
			if (this._fixScreenPositionList.Count > 0)
			{
				for (int i = 0; i < this._fixScreenPositionList.Count; i++)
				{
					vector += this._fixScreenPositionList[i];
				}
				vector /= (float)this._fixScreenPositionList.Count;
			}
			return vector;
		}

		private Vector3 _CalculateAvarageScreenPosition()
		{
			Vector3 vector = Vector3.zero;
			if (this._screenPositionList.Count > 0)
			{
				for (int i = 0; i < this._screenPositionList.Count; i++)
				{
					vector += this._screenPositionList[i];
				}
				vector /= (float)this._screenPositionList.Count;
			}
			return vector;
		}

		private Vector3 _CalculateAvarageFixScreenDirection()
		{
			Vector3 vector = Vector3.zero;
			if (this._fixScreenDirectionList.Count > 0)
			{
				for (int i = 0; i < this._fixScreenDirectionList.Count; i++)
				{
					vector += this._fixScreenDirectionList[i];
				}
				vector /= (float)this._fixScreenDirectionList.Count;
				vector.Normalize();
			}
			return vector;
		}

		private Vector3 _CalculateAvarageScreenDirection()
		{
			Vector3 vector = Vector3.zero;
			if (this._screenDirectionList.Count > 0)
			{
				for (int i = 0; i < this._screenDirectionList.Count; i++)
				{
					vector += this._screenDirectionList[i];
				}
				vector /= (float)this._screenDirectionList.Count;
				vector.Normalize();
			}
			return vector;
		}

		private float _CalculateAvarageFixScreenSpeed()
		{
			float num = 0f;
			if (this._fixScreenSpeedList.Count > 0)
			{
				for (int i = 0; i < this._fixScreenSpeedList.Count; i++)
				{
					num += this._fixScreenSpeedList[i];
				}
				num /= (float)this._fixScreenSpeedList.Count;
			}
			return num;
		}

		private float _CalculateAvarageScreenSpeed()
		{
			float num = 0f;
			if (this._screenSpeedList.Count > 0)
			{
				for (int i = 0; i < this._screenSpeedList.Count; i++)
				{
					num += this._screenSpeedList[i];
				}
				num /= (float)this._screenSpeedList.Count;
			}
			return num;
		}

		private float _CalculateAvarageFixScreenAccel()
		{
			float num = 0f;
			if (this._fixScreenAccelList.Count > 0)
			{
				for (int i = 0; i < this._fixScreenAccelList.Count; i++)
				{
					num += this._fixScreenAccelList[i];
				}
				num /= (float)this._fixScreenAccelList.Count;
			}
			return num;
		}

		private float _CalculateAvarageScreenAccel()
		{
			float num = 0f;
			if (this._screenAccelList.Count > 0)
			{
				for (int i = 0; i < this._screenAccelList.Count; i++)
				{
					num += this._screenAccelList[i];
				}
				num /= (float)this._screenAccelList.Count;
			}
			return num;
		}

		private float _CalculateLength()
		{
			float num = 0f;
			if (this._fixScreenPositionList.Count > 0)
			{
				for (int i = 0; i < this._fixScreenPositionList.Count; i++)
				{
					Vector3 vector = this._fixScreenPositionList[i];
					if (i > 0)
					{
						num += (vector - this._fixScreenPositionList[i - 1]).magnitude;
					}
				}
			}
			return num;
		}

		public AnInputDownTypes _GetDown(Collider collision = null)
		{
			if (!this._enable)
			{
				return AnInputDownTypes.NotDown;
			}
			if (this._currentState != AnInputBase.BaseStateTypes.Down_Init)
			{
				return AnInputDownTypes.NotDown;
			}
			if (collision == null)
			{
				return AnInputDownTypes.DownInRange;
			}
			if (this._HitCollision(collision))
			{
				return AnInputDownTypes.DownInRange;
			}
			return AnInputDownTypes.DownOutRange;
		}

		public AnInputDownLoopTypes _GetDownLoop(Collider collision)
		{
			if (!this._enable)
			{
				return AnInputDownLoopTypes.NotDownLoop;
			}
			if (this._currentState != AnInputBase.BaseStateTypes.Down_Loop)
			{
				return AnInputDownLoopTypes.NotDownLoop;
			}
			if (collision == null)
			{
				return AnInputDownLoopTypes.DownLoopInRange;
			}
			if (this._HitCollision(collision))
			{
				return AnInputDownLoopTypes.DownLoopInRange;
			}
			return AnInputDownLoopTypes.DownLoopOutRange;
		}

		public AnInputUpTypes _GetUp(Collider collision)
		{
			if (!this._enable)
			{
				return AnInputUpTypes.NotUp;
			}
			if (this._currentState != AnInputBase.BaseStateTypes.Wait_Init)
			{
				return AnInputUpTypes.NotUp;
			}
			if (collision == null)
			{
				return AnInputUpTypes.UpInRange;
			}
			if (this._HitCollision(collision))
			{
				return AnInputUpTypes.UpInRange;
			}
			return AnInputUpTypes.UpOutRange;
		}

		public bool _GetScrollStart(Collider collision, AnUIDirectionTypes directionType)
		{
			AnInputDownLoopTypes anInputDownLoopTypes = this._GetDownLoop(collision);
			if (anInputDownLoopTypes == AnInputDownLoopTypes.NotDownLoop || anInputDownLoopTypes == AnInputDownLoopTypes.DownLoopOutRange)
			{
				return false;
			}
			float num;
			if (directionType == AnUIDirectionTypes.TopToButtom || directionType == AnUIDirectionTypes.BottomToTop)
			{
				num = AnUtilityValue.GetAbsValue(this._currentFixScreenPosition.y - this._startFixScreenPosition.y);
			}
			else if (directionType == AnUIDirectionTypes.LeftToRight || directionType == AnUIDirectionTypes.RightToLeft)
			{
				num = AnUtilityValue.GetAbsValue(this._currentFixScreenPosition.x - this._startFixScreenPosition.x);
			}
			else
			{
				num = (this._currentFixScreenPosition - this._startFixScreenPosition).magnitude;
			}
			return num >= AnMonoSingleton<AnRootManager>.Instance._GetScrollStartPixel();
		}

		public bool _GetSwipeStart(Collider collision, AnUIDirectionTypes directionType)
		{
			AnInputDownLoopTypes anInputDownLoopTypes = this._GetDownLoop(collision);
			if (anInputDownLoopTypes == AnInputDownLoopTypes.NotDownLoop || anInputDownLoopTypes == AnInputDownLoopTypes.DownLoopOutRange)
			{
				return false;
			}
			float num;
			if (directionType == AnUIDirectionTypes.TopToButtom || directionType == AnUIDirectionTypes.BottomToTop)
			{
				num = AnUtilityValue.GetAbsValue(this._currentFixScreenPosition.y - this._startFixScreenPosition.y);
			}
			else if (directionType == AnUIDirectionTypes.LeftToRight || directionType == AnUIDirectionTypes.RightToLeft)
			{
				num = AnUtilityValue.GetAbsValue(this._currentFixScreenPosition.x - this._startFixScreenPosition.x);
			}
			else
			{
				num = (this._currentFixScreenPosition - this._startFixScreenPosition).magnitude;
			}
			return num >= AnMonoSingleton<AnRootManager>.Instance._GetScrollStartPixel();
		}

		private Vector3 _GetScreenPosition()
		{
			this._currentScreenPosition = this._GeTouchPosition();
			return this._currentScreenPosition;
		}

		private Vector3 _GeTouchPosition()
		{
			if (Input.touchCount != 0)
			{
				int touchCount = Input.touchCount;
				for (int i = 0; i < touchCount; i++)
				{
					Touch touch = Input.GetTouch(i);
					if (touch.fingerId == this._inputIndex)
					{
						return touch.position * AnMonoSingleton<AnRootManager>.Instance.ScreenRate;
					}
				}
				return this._prevScreenPosition;
			}
			if (!Input.mousePresent)
			{
				return Vector3.zero;
			}
			return Input.mousePosition * AnMonoSingleton<AnRootManager>.Instance.ScreenRate;
		}

		private bool _GetTouchDown()
		{
			if (Input.touchCount == 0)
			{
				return Input.mousePresent && (Input.GetMouseButton(this._inputIndex) && Input.GetMouseButtonDown(this._inputIndex));
			}
			int touchCount = Input.touchCount;
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began && touch.fingerId == this._inputIndex)
				{
					return true;
				}
			}
			return false;
		}

		private bool _GetTouchUp()
		{
			if (Input.touchCount == 0)
			{
				return Input.mousePresent && (!Input.GetMouseButton(this._inputIndex) || Input.GetMouseButtonUp(this._inputIndex));
			}
			int touchCount = Input.touchCount;
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.fingerId == this._inputIndex && touch.phase == TouchPhase.Ended)
				{
					return true;
				}
			}
			return false;
		}

		private bool _HitCollision(Collider collision)
		{
			return AnMonoSingleton<AnRootManager>.Instance.UIManager.CollisionManager._GetHitObjectFromHitObjectListByCollider(collision, this._hitObjectList) != null;
		}

		private Vector3 _startFixScreenPosition = Vector3.zero;

		private Vector3 _currentFixScreenPosition = Vector3.zero;

		private Vector3 _prevFixScreenPosition = Vector3.zero;

		private Vector3 _currentFixScreenDirection = Vector3.zero;

		private float _currentFixScreenSpeed;

		private float _prevFixScreenSpeed;

		private float _currentFixScreenAccel;

		private Vector3 _startScreenPosition = Vector3.zero;

		private Vector3 _currentScreenPosition = Vector3.zero;

		private Vector3 _prevScreenPosition = Vector3.zero;

		private Vector3 _currentScreenDirection = Vector3.zero;

		private float _currentScreenSpeed;

		private float _prevScreenSpeed;

		private float _currentScreenAccel;

		private float _startDownTime;

		private float _currentDownTime;

		private float _prevDownTime;

		public List<Vector3> _fixScreenPositionList;

		public List<Vector3> _fixScreenDirectionList;

		public List<float> _fixScreenSpeedList;

		public List<float> _fixScreenAccelList;

		public List<Vector3> _screenPositionList;

		public List<Vector3> _screenDirectionList;

		public List<float> _screenSpeedList;

		public List<float> _screenAccelList;

		private int _listCapacity = 5;
	}
}
