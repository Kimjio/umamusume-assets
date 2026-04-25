using System;

namespace AnimateToUnity.Utility
{
	public class AnBlendValue : AnBlendBase
	{
		public float StartValue
		{
			get
			{
				return this._startValue;
			}
		}

		public float EndValue
		{
			get
			{
				return this._endValue;
			}
		}

		public float CurrentValue
		{
			get
			{
				this.UpdateCurrentValue();
				return this._currentValue;
			}
		}

		public AnBlendValue(float startValue, float endValue, float blendTime, AnBlendBase.BlendTypes blendModeType)
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
			if (this._blendTime == 0f)
			{
				this._currentBlendValue = 1f;
				this._currentFixBlendValue = 1f;
				this._currentValue = this._endValue;
				return;
			}
			if (this._currentBlendTime > this._blendTime + 0.2f || this._currentBlendTime < 0f)
			{
				this._currentBlendValue = 1f;
				this._currentFixBlendValue = 1f;
				this._currentValue = this._endValue;
				return;
			}
			if (this._pause)
			{
				return;
			}
			this._currentValue = this._startValue + (this._endValue - this._startValue) * this._currentFixBlendValue;
		}

		public virtual void SetStartValue(float startValue)
		{
			this._startValue = startValue;
		}

		public virtual void SetEndValue(float endValue)
		{
			this._endValue = endValue;
		}

		private float _startValue;

		private float _endValue;

		private float _currentValue;
	}
}
