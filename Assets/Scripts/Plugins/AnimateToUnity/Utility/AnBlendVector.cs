using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnBlendVector : AnBlendBase
	{
		public Vector3 StartValue
		{
			get
			{
				return this._startValue;
			}
		}

		public Vector3 EndValue
		{
			get
			{
				return this._endValue;
			}
		}

		public Vector3 CurrentValue
		{
			get
			{
				this.UpdateCurrentValue();
				return this._currentValue;
			}
		}

		public AnBlendVector(Vector3 startValue, Vector3 endValue, float blendTime, AnBlendBase.BlendTypes blendModeType)
		{
			this._startValue = startValue;
			this._endValue = endValue;
			this._blendTime = blendTime;
			this._blendType = blendModeType;
			this.Reset();
		}

		public override void Reset()
		{
			base.Reset();
			this._currentValue = this._startValue;
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			this.UpdateCurrentValue();
			if (this._pause)
			{
				return;
			}
			if (this._currentBlendTime <= this._blendTime)
			{
				this._currentBlendTime += deltaTime;
			}
		}

		private void UpdateCurrentValue()
		{
			if (this._startValue == this._endValue)
			{
				this._currentBlendValue = 1f;
				this._currentFixBlendValue = 1f;
				this._currentValue = this._endValue;
				return;
			}
			if (this._blendTime <= 0f)
			{
				this._currentBlendValue = 1f;
				this._currentFixBlendValue = 1f;
				this._currentValue = this._endValue;
				return;
			}
			if (this._currentBlendTime > this._blendTime || this._currentBlendTime < 0f)
			{
				this._currentBlendValue = 1f;
				this._currentFixBlendValue = 1f;
				this._currentValue = this._endValue;
				return;
			}
			this._currentValue = this._startValue + (this._endValue - this._startValue) * this._currentFixBlendValue;
		}

		public virtual void SetStartValue(Vector3 startValue)
		{
			this._startValue = startValue;
		}

		public virtual void SetEndValue(Vector3 endValue)
		{
			this._endValue = endValue;
		}

		private Vector3 _startValue = Vector3.zero;

		private Vector3 _endValue = Vector3.zero;

		private Vector3 _currentValue = Vector3.zero;
	}
}
