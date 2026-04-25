using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnRayInput : AnInputBase
	{
		public AnRayInput(AnUIManager uiManager, int inputIndex)
			: base(uiManager, inputIndex)
		{
			this._currentRay = default(Ray);
		}

		protected override void _Update_Common_Start()
		{
			this._UpdateSelection();
			base._Update_Common_Start();
		}

		protected override void _Update_Wait_Init()
		{
			this._currentInputUI = null;
			base._Update_Wait_Init();
		}

		protected override void _Update_Wait_Loop()
		{
			if (this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0] != null && this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0] == this._uiManager.PrevInputUIBaseGroupList[this._inputIndex][0])
			{
				this._Update_Select_Init();
			}
			base._Update_Wait_Loop();
		}

		protected override void _Update_Select_Init()
		{
			this._currentInputUI = this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0];
			base._Update_Select_Init();
		}

		protected override void _Update_Select_Loop()
		{
			if (this._currentInputUI != this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0])
			{
				this._Update_Wait_Init();
				return;
			}
			if (this._currentInputUI.EnableSubmitDelayTimeForRayInput)
			{
				if (this._currentInputUI.CustomSubmitDelayTimeForRayInput > 0f)
				{
					if (this._currentSelectTime > this._currentInputUI.CustomSubmitDelayTimeForRayInput)
					{
						this._Update_Down_Init();
						return;
					}
				}
				else if (this._currentSelectTime > AnMonoSingleton<AnRootManager>.Instance._GetRayInputSubmitDelay())
				{
					this._Update_Down_Init();
					return;
				}
			}
			else if (this._uiManager._GetSubmitButtonDown(this._inputIndex))
			{
				this._Update_Down_Init();
				return;
			}
			base._Update_Select_Loop();
		}

		protected override void _Update_Down_Init()
		{
			if (this._currentInputUI != this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0])
			{
				this._Update_Wait_Init();
				return;
			}
			base._Update_Down_Init();
		}

		protected override void _Update_Down_Loop()
		{
			if (this._currentInputUI != this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0])
			{
				this._Update_Wait_Init();
				return;
			}
			if (this._uiManager._GetSubmitButtonUp(this._inputIndex))
			{
				this._Update_Wait_Init();
				return;
			}
			base._Update_Down_Loop();
		}

		private void _UpdateSelection()
		{
			this._currentRay = this._uiManager._GetRay(this._inputIndex);
			AnMonoSingleton<AnRootManager>.Instance.UIManager.CollisionManager._GetHitObjectListWithCameraRay(this._currentRay.origin + this._currentRay.direction * 1000f, false, ref this._hitObjectList);
			AnUIBase anUIBase = this._uiManager.CollisionManager._GetFirstUIListFromHitObjectList(this._hitObjectList, true);
			if (anUIBase != null)
			{
				if (anUIBase.EnableSelectInputForRayInput)
				{
					this._uiManager.SetCurrentInputUI(anUIBase, this._inputIndex);
					return;
				}
			}
			else
			{
				this._uiManager.SetCurrentInputUI(null, this._inputIndex);
			}
		}

		public override AnInputDownTypes _GetDown(AnUIBase inputUI)
		{
			if (!this._enable)
			{
				return AnInputDownTypes.NotDown;
			}
			if (this._currentState != AnInputBase.BaseStateTypes.Down_Init)
			{
				return AnInputDownTypes.NotDown;
			}
			if (inputUI == null)
			{
				return AnInputDownTypes.NotDown;
			}
			if (this._currentInputUI == null)
			{
				return AnInputDownTypes.NotDown;
			}
			if (this._currentInputUI == inputUI)
			{
				return AnInputDownTypes.DownInRange;
			}
			return AnInputDownTypes.NotDown;
		}

		public override AnInputDownLoopTypes _GetDownLoop(AnUIBase inputUI)
		{
			if (!this._enable)
			{
				return AnInputDownLoopTypes.NotDownLoop;
			}
			if (this._currentState != AnInputBase.BaseStateTypes.Down_Loop)
			{
				return AnInputDownLoopTypes.NotDownLoop;
			}
			if (inputUI == null)
			{
				return AnInputDownLoopTypes.NotDownLoop;
			}
			if (this._currentInputUI == null)
			{
				return AnInputDownLoopTypes.NotDownLoop;
			}
			if (this._currentInputUI == inputUI)
			{
				return AnInputDownLoopTypes.DownLoopInRange;
			}
			return AnInputDownLoopTypes.NotDownLoop;
		}

		public override AnInputUpTypes _GetUp(AnUIBase inputUI)
		{
			if (!this._enable)
			{
				return AnInputUpTypes.NotUp;
			}
			if (this._currentState != AnInputBase.BaseStateTypes.Wait_Init)
			{
				return AnInputUpTypes.NotUp;
			}
			if (inputUI == null)
			{
				return AnInputUpTypes.NotUp;
			}
			if (this._currentInputUI == null)
			{
				return AnInputUpTypes.NotUp;
			}
			if (this._currentInputUI == inputUI)
			{
				return AnInputUpTypes.UpInRange;
			}
			return AnInputUpTypes.NotUp;
		}

		private AnUIBase _currentInputUI;

		private Ray _currentRay;
	}
}
