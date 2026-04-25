using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public abstract class AnBlendBase
	{
		public AnBlendBase.BlendTypes BlendType
		{
			get
			{
				return this._blendType;
			}
		}

		public float CurrentBlendTime
		{
			get
			{
				return this._currentBlendTime;
			}
		}

		public float BlendTime
		{
			get
			{
				return this._blendTime;
			}
		}

		public float CurrentBlendValue
		{
			get
			{
				return this._currentBlendValue;
			}
		}

		public float CurrentFixBlendValue
		{
			get
			{
				return this._currentFixBlendValue;
			}
		}

		public bool Pause
		{
			get
			{
				return this._pause;
			}
		}

		public virtual void Reset()
		{
			this._currentBlendTime = 0f;
			this.Update(0f);
		}

		public virtual void Update(float deltaTime)
		{
			if (this._blendTime <= 0f)
			{
				this._currentBlendValue = 1f;
				this._currentFixBlendValue = 1f;
				return;
			}
			this._currentBlendValue = Mathf.Min(1f, this._currentBlendTime / this._blendTime);
			this._currentBlendValue = Mathf.Max(0f, this._currentBlendValue);
			if (this._currentBlendValue >= 1f)
			{
				this._currentFixBlendValue = 1f;
				return;
			}
			if (this._currentBlendValue <= 0f)
			{
				this._currentFixBlendValue = 0f;
				return;
			}
			switch (this._blendType)
			{
			case AnBlendBase.BlendTypes.Linear:
				this._currentFixBlendValue = this._currentBlendValue;
				return;
			case AnBlendBase.BlendTypes.Up:
				this._currentFixBlendValue = Mathf.Pow(this._currentBlendValue, 2f);
				return;
			case AnBlendBase.BlendTypes.Down:
				this._currentFixBlendValue = 1f - Mathf.Pow(this._currentBlendValue - 1f, 2f);
				return;
			case AnBlendBase.BlendTypes.UpDown:
				if (this._currentBlendValue < 0.5f)
				{
					this._currentFixBlendValue = Mathf.Pow(this._currentBlendValue, 2f) * 2f;
					return;
				}
				this._currentFixBlendValue = 1f - Mathf.Pow(this._currentBlendValue - 1f, 2f) * 2f;
				return;
			case AnBlendBase.BlendTypes.Up2:
				this._currentFixBlendValue = Mathf.Pow(this._currentBlendValue, 4f);
				return;
			case AnBlendBase.BlendTypes.Down2:
				this._currentFixBlendValue = 1f - Mathf.Pow(this._currentBlendValue - 1f, 4f);
				return;
			case AnBlendBase.BlendTypes.UpDown2:
				if (this._currentBlendValue < 0.5f)
				{
					this._currentFixBlendValue = Mathf.Pow(this._currentBlendValue, 4f) * 8f;
					return;
				}
				this._currentFixBlendValue = 1f - Mathf.Pow(this._currentBlendValue - 1f, 4f) * 8f;
				return;
			default:
				return;
			}
		}

		public virtual void SetBlendType(AnBlendBase.BlendTypes blendType)
		{
			this._blendType = blendType;
		}

		public virtual void SetBlendTime(float blendTime)
		{
			this._blendTime = blendTime;
		}

		public virtual void SetCurrentBlendTime(float currentBlendTime)
		{
			this._currentBlendTime = currentBlendTime;
		}

		public virtual void SetPause(bool pause)
		{
			this._pause = pause;
		}

		protected AnBlendBase.BlendTypes _blendType;

		protected float _currentBlendTime;

		protected float _blendTime;

		protected float _currentBlendValue;

		protected float _currentFixBlendValue;

		protected bool _pause;

		public enum BlendTypes
		{
			Linear,
			Up,
			Down,
			UpDown,
			Up2,
			Down2,
			UpDown2
		}
	}
}
