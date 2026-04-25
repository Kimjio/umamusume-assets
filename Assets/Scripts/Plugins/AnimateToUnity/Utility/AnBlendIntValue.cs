using System;

namespace AnimateToUnity.Utility
{
	public class AnBlendIntValue : AnBlendBase
	{
		public int StartValue
		{
			get
			{
				return this._startValue;
			}
		}

		public int EndValue
		{
			get
			{
				return this._endValue;
			}
		}

		public int CurrentValue
		{
			get
			{
				this.UpdateCurrentValue();
				return this._currentValue;
			}
		}

		public AnBlendIntValue(int startValue, int endValue, float blendTime, AnBlendBase.BlendTypes blendModeType)
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
			this.Update(0f);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			this.UpdateCurrentValue();
			if (this._pause)
			{
				return;
			}
			if (this._currentBlendTime <= this._blendTime * 2f)
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
			if (this._currentBlendTime >= this._blendTime || this._currentBlendTime < 0f)
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
			int num = (int)((float)(this._endValue - this._startValue) * this._currentFixBlendValue);
			this._currentValue = this._startValue + num;
		}

		public virtual void SetStartValue(int startValue)
		{
			this._startValue = startValue;
		}

		public virtual void SetEndValue(int endValue)
		{
			this._endValue = endValue;
		}

		private int _startValue;

		private int _endValue;

		private int _currentValue;
	}
}
