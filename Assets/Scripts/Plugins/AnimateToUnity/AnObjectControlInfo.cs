using System;

namespace AnimateToUnity
{
	public class AnObjectControlInfo
	{
		public string TargetName
		{
			get
			{
				return this._targetName;
			}
			set
			{
				this._targetName = value;
			}
		}

		public float StartTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}

		public float FixTargetTime
		{
			get
			{
				return this._fixTargetTime;
			}
			set
			{
				this._fixTargetTime = value;
			}
		}

		public bool TargetIsStop
		{
			get
			{
				return this._targetIsStop;
			}
			set
			{
				this._targetIsStop = value;
			}
		}

		public void _Initialize()
		{
			this._isActive = false;
		}

		public string _targetName;

		public float _startTime;

		public bool _targetIsStop;

		public float _fixTargetTime = -1f;

		public bool _isActive;

		public AnObjectBase _targetObjectBase;

		public AnObject _targetObject;
	}
}
