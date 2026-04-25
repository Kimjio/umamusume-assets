using System;

namespace AnimateToUnity.Utility
{
	public class AnInputRepeater
	{
		private void _Update_Common_Start()
		{
			this._repeatOn = false;
			switch (this._currentState)
			{
			case AnInputRepeater.RepeaterStateTypes.Wait_Init:
				this._currentState = AnInputRepeater.RepeaterStateTypes.Wait_Loop;
				return;
			case AnInputRepeater.RepeaterStateTypes.Wait_Loop:
			case AnInputRepeater.RepeaterStateTypes.First_Loop:
				break;
			case AnInputRepeater.RepeaterStateTypes.First_Init:
				this._currentState = AnInputRepeater.RepeaterStateTypes.First_Loop;
				return;
			case AnInputRepeater.RepeaterStateTypes.Second_Init:
				this._currentState = AnInputRepeater.RepeaterStateTypes.Second_Loop;
				break;
			default:
				return;
			}
		}

		public void _Update()
		{
			this._Update_Common_Start();
			switch (this._currentState)
			{
			case AnInputRepeater.RepeaterStateTypes.Wait_Loop:
				this._Update_Wait_Loop();
				break;
			case AnInputRepeater.RepeaterStateTypes.First_Loop:
				this._Update_First_Loop();
				break;
			case AnInputRepeater.RepeaterStateTypes.Second_Loop:
				this._Update_Second_Loop();
				break;
			}
			this._Update_Common_End();
		}

		private void _Update_Common_End()
		{
		}

		private void _Update_Wait_Init()
		{
			this._currentState = AnInputRepeater.RepeaterStateTypes.Wait_Init;
			this._currentRepeatTime = 0f;
			this._repeatStartFlag = false;
			this._repeatOn = false;
		}

		private void _Update_Wait_Loop()
		{
			if (this._repeatStartFlag)
			{
				this._Update_First_Init();
			}
		}

		private void _Update_First_Init()
		{
			this._currentState = AnInputRepeater.RepeaterStateTypes.First_Init;
			this._currentRepeatTime = 0f;
			this._repeatOn = true;
		}

		private void _Update_First_Loop()
		{
			if (this._currentRepeatTime > AnMonoSingleton<AnRootManager>.Instance._GetKeyInputChangeStartDelayTime())
			{
				this._Update_Second_Init();
				return;
			}
			if (!this._repeatStartFlag)
			{
				this._Update_Wait_Init();
				return;
			}
			this._currentRepeatTime += AnMonoSingleton<AnRootManager>.Instance.CurrentDeltaTime;
		}

		private void _Update_Second_Init()
		{
			this._currentState = AnInputRepeater.RepeaterStateTypes.Second_Init;
			this._currentRepeatTime = 0f;
			this._repeatOn = true;
		}

		private void _Update_Second_Loop()
		{
			if (this._currentRepeatTime > AnMonoSingleton<AnRootManager>.Instance._GetKeyInputChangeDelayTime())
			{
				this._Update_Second_Init();
				return;
			}
			if (!this._repeatStartFlag)
			{
				this._Update_Wait_Init();
				return;
			}
			this._currentRepeatTime += AnMonoSingleton<AnRootManager>.Instance.CurrentDeltaTime;
		}

		public bool GetRepeat()
		{
			return this._repeatOn;
		}

		public void Start()
		{
			this._repeatStartFlag = true;
		}

		public void End()
		{
			this._repeatStartFlag = false;
		}

		private AnInputRepeater.RepeaterStateTypes _currentState;

		private float _currentRepeatTime;

		private bool _repeatStartFlag;

		private bool _repeatOn;

		public enum RepeaterStateTypes
		{
			Wait_Init,
			Wait_Loop,
			First_Init,
			First_Loop,
			Second_Init,
			Second_Loop
		}
	}
}
