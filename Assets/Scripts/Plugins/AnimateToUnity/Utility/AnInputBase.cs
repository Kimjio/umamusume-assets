using System;
using System.Collections.Generic;

namespace AnimateToUnity.Utility
{
	public class AnInputBase
	{
		public bool Enable
		{
			get
			{
				return this._enable;
			}
		}

		public int InputIndex
		{
			get
			{
				return this._inputIndex;
			}
		}

		public AnInputBase(AnUIManager uiManager, int inputIndex)
		{
			this._uiManager = uiManager;
			this._inputIndex = inputIndex;
			this._hitObjectList = new List<AnObjectBase>(8);
			this._hitInputUIList = new List<AnUIBase>(8);
		}

		public virtual void _Reset()
		{
			this._Update_Wait_Init();
		}

		public virtual void _Update()
		{
			if (!this._enable)
			{
				this._currentState = AnInputBase.BaseStateTypes.Wait_Init;
				this._prevEnable = false;
				return;
			}
			if (this._enable && this._enable != this._prevEnable)
			{
				this._Update_Wait_Init();
			}
			this._Update_Common_Start();
			switch (this._currentState)
			{
			case AnInputBase.BaseStateTypes.Wait_Loop:
				this._Update_Wait_Loop();
				break;
			case AnInputBase.BaseStateTypes.Down_Loop:
				this._Update_Down_Loop();
				break;
			case AnInputBase.BaseStateTypes.Select_Loop:
				this._Update_Select_Loop();
				break;
			}
			this._Update_Common_End();
		}

		protected virtual void _Update_Common_Start()
		{
			switch (this._currentState)
			{
			case AnInputBase.BaseStateTypes.Wait_Init:
				this._currentState = AnInputBase.BaseStateTypes.Wait_Loop;
				return;
			case AnInputBase.BaseStateTypes.Wait_Loop:
			case AnInputBase.BaseStateTypes.Down_Loop:
				break;
			case AnInputBase.BaseStateTypes.Down_Init:
				this._currentState = AnInputBase.BaseStateTypes.Down_Loop;
				return;
			case AnInputBase.BaseStateTypes.Select_Init:
				this._currentState = AnInputBase.BaseStateTypes.Select_Loop;
				break;
			default:
				return;
			}
		}

		protected virtual void _Update_Common_End()
		{
			this._prevEnable = this._enable;
		}

		protected virtual void _Update_Wait_Init()
		{
			this._currentState = AnInputBase.BaseStateTypes.Wait_Init;
		}

		protected virtual void _Update_Wait_Loop()
		{
		}

		protected virtual void _Update_Down_Init()
		{
			this._currentState = AnInputBase.BaseStateTypes.Down_Init;
			this._currentDownLoopTime = 0f;
		}

		protected virtual void _Update_Down_Loop()
		{
			this._currentDownLoopTime += AnMonoSingleton<AnRootManager>.Instance.CurrentDeltaTime;
		}

		protected virtual void _Update_Select_Init()
		{
			this._currentState = AnInputBase.BaseStateTypes.Select_Init;
			this._currentSelectTime = 0f;
		}

		protected virtual void _Update_Select_Loop()
		{
			this._currentSelectTime += AnMonoSingleton<AnRootManager>.Instance.CurrentDeltaTime;
		}

		public virtual void _SetEnable(bool enable)
		{
			this._enable = enable;
			this._prevEnable = !this._enable;
		}

		public virtual AnInputDownTypes _GetDown(AnUIBase inputUI)
		{
			return AnInputDownTypes.NotDown;
		}

		public virtual AnInputDownLoopTypes _GetDownLoop(AnUIBase inputUI)
		{
			return AnInputDownLoopTypes.NotDownLoop;
		}

		public virtual AnInputUpTypes _GetUp(AnUIBase inputUI)
		{
			return AnInputUpTypes.NotUp;
		}

		private const int HITOBJECT_NUM = 8;

		private const int HITINPUT_UI_NUM = 8;

		protected AnInputBase.BaseStateTypes _currentState;

		protected AnUIManager _uiManager;

		protected bool _enable;

		protected bool _prevEnable;

		protected int _inputIndex;

		protected float _currentDownLoopTime;

		protected float _currentSelectTime;

		protected List<AnObjectBase> _hitObjectList;

		protected List<AnUIBase> _hitInputUIList;

		public enum BaseStateTypes
		{
			Wait_Init,
			Wait_Loop,
			Down_Init,
			Down_Loop,
			Select_Init,
			Select_Loop
		}
	}
}
