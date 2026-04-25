using System;

namespace AnimateToUnity
{
	[Serializable]
	public class AnObjectControlInfoParameter
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

		public string TargetLabel
		{
			get
			{
				return this._targetLabel;
			}
			set
			{
				this._targetLabel = value;
			}
		}

		public float TargetTime
		{
			get
			{
				return this._targetTime;
			}
			set
			{
				this._targetTime = value;
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

		public AnObjectControlInfoTypes ObjectControlInfoType
		{
			get
			{
				return this._objectControlInfoType;
			}
			set
			{
				this._objectControlInfoType = value;
			}
		}

		public void _Initialize()
		{
			this._objectControlInfoType = AnObjectControlInfoTypes.None;
			if (this._targetLabel != null && this._targetLabel != "")
			{
				this._objectControlInfoType = AnObjectControlInfoTypes.MotionPlayByLabel;
			}
			if (this._objectControlInfoType == AnObjectControlInfoTypes.None && this._targetTime >= 0f)
			{
				this._objectControlInfoType = AnObjectControlInfoTypes.MotionPlayByTime;
			}
		}

		public string _targetName;

		public float _startTime;

		public string _targetLabel;

		public float _targetTime = -1f;

		public bool _targetIsStop;

		[NonSerialized]
		public AnObjectControlInfoTypes _objectControlInfoType;
	}
}
