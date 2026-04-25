using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnKeyInput : AnInputBase
	{
		public AnInputRepeater Repeater
		{
			get
			{
				return this._repeater;
			}
		}

		public AnKeyInput(AnUIManager uiManager, int inputIndex)
			: base(uiManager, inputIndex)
		{
			this._repeater = new AnInputRepeater();
		}

		protected override void _Update_Common_Start()
		{
			this._currentSelectDirection = AnMonoSingleton<AnRootManager>.Instance.UIManager._GetAxis(this._inputIndex);
			base._Update_Common_Start();
		}

		protected override void _Update_Wait_Init()
		{
			base._Update_Wait_Init();
		}

		protected override void _Update_Wait_Loop()
		{
			if (this._currentSelectDirection.x != 0f || this._currentSelectDirection.y != 0f)
			{
				this._Update_Select_Init();
				return;
			}
			if (this._uiManager._GetSubmitButtonDown(this._inputIndex))
			{
				this._Update_Down_Init();
				return;
			}
			base._Update_Wait_Loop();
		}

		protected override void _Update_Down_Init()
		{
			if (this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0] == null)
			{
				this._Update_Wait_Init();
				return;
			}
			this._currentInputUI = this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0];
			base._Update_Down_Init();
		}

		protected override void _Update_Down_Loop()
		{
			if (this._currentInputUI != this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0])
			{
				this._Update_Wait_Init();
				return;
			}
			if (this._currentSelectDirection.x != 0f || this._currentSelectDirection.y != 0f)
			{
				this._Update_Select_Init();
				return;
			}
			if (this._uiManager._GetSubmitButtonUp(this._inputIndex))
			{
				this._Update_Wait_Init();
				return;
			}
			base._Update_Down_Loop();
		}

		protected override void _Update_Select_Init()
		{
			this._UpdateSelection();
			base._Update_Select_Init();
		}

		protected override void _Update_Select_Loop()
		{
			if (this._currentSelectDirection.x == 0f && this._currentSelectDirection.y == 0f)
			{
				this._Update_Wait_Init();
				return;
			}
			base._Update_Select_Loop();
		}

		private void _UpdateSelection()
		{
			if (this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0] == null)
			{
				return;
			}
			AnUIBase anUIBase = this._uiManager.CurrentInputUIBaseGroupList[this._inputIndex][0];
			this._currentInputDirectionType = AnUtilityInput.GetInputDirectionType(this._currentSelectDirection, true);
			if (this._currentInputDirectionType == AnUIInputDirectionTypes.None)
			{
				return;
			}
			if (anUIBase._UpdateUI(this._currentInputDirectionType))
			{
				return;
			}
			AnUIBase anUIBase2 = null;
			if (anUIBase.ExistNextInputUI(this._currentInputDirectionType))
			{
				anUIBase2 = anUIBase.GetNextInputUI(this._currentInputDirectionType);
			}
			else
			{
				AnMonoSingleton<AnRootManager>.Instance.UIManager.CollisionManager._GetHitObjectListWithObjectRay(anUIBase.Motion.Transform.position, anUIBase.Motion.Transform.right * this._currentSelectDirection.x + anUIBase.Motion.Transform.up * this._currentSelectDirection.y, anUIBase.Motion.Transform.right * this._currentSelectDirection.y + anUIBase.Motion.Transform.up * this._currentSelectDirection.x, float.MaxValue, 1 << anUIBase.Motion.GameObject.layer, 0.025f, true, ref this._hitObjectList);
				if (this._hitObjectList.Count == 0)
				{
					return;
				}
				AnMonoSingleton<AnRootManager>.Instance.UIManager.CollisionManager._GetUIListFromHitObjectList(this._hitObjectList, ref this._hitInputUIList);
				for (int i = 0; i < this._hitInputUIList.Count; i++)
				{
					AnUIBase anUIBase3 = this._hitInputUIList[i];
					if (anUIBase3 != null && anUIBase3.Enable)
					{
						if (anUIBase3.ParentUI != null)
						{
							this._hitInputUIList[i] = anUIBase3.ParentUI;
						}
						if (this._hitInputUIList[i] == anUIBase)
						{
							this._hitInputUIList[i] = null;
						}
					}
				}
				for (int j = 0; j < this._hitInputUIList.Count; j++)
				{
					if (this._hitInputUIList[j] != null && this._hitInputUIList[j].Enable && this._hitInputUIList[j].EnableSelectInputForKeyInput)
					{
						anUIBase2 = this._hitInputUIList[j];
						break;
					}
				}
			}
			if (anUIBase2 == null)
			{
				return;
			}
			if (anUIBase2.ParentUI != null)
			{
				anUIBase2 = anUIBase2.ParentUI;
				if (anUIBase2.ParentUI != null)
				{
					anUIBase2 = anUIBase2.ParentUI;
				}
			}
			this._uiManager.SetCurrentInputUI(anUIBase2, this._inputIndex);
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

		private Vector2 _currentSelectDirection = Vector2.zero;

		private AnUIInputDirectionTypes _currentInputDirectionType = AnUIInputDirectionTypes.None;

		private AnInputRepeater _repeater;
	}
}
