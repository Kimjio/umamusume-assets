using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnMouseInput : AnInputBase
	{
		public AnMouseInput(AnUIManager uiManager, int inputIndex)
			: base(uiManager, inputIndex)
		{
		}

		public override void _Update()
		{
			if (!Input.mousePresent)
			{
				return;
			}
			base._Update();
		}

		protected override void _Update_Common_Start()
		{
			this._currentScreenPosition = this._uiManager._GetMousePosition(this._inputIndex);
			if (this._currentScreenPosition != this._prevScreenPosition)
			{
				this._uiManager.CollisionManager._GetHitObjectListWithCameraRay(this._currentScreenPosition, true, ref this._hitObjectList);
				AnUIBase anUIBase = this._uiManager.CollisionManager._GetFirstUIListFromHitObjectList(this._hitObjectList, true);
				if (anUIBase != null)
				{
					if (anUIBase.EnableOverInputForMouseInput)
					{
						this._uiManager.SetOverInputUI(anUIBase, this._inputIndex);
					}
				}
				else
				{
					this._uiManager.SetOverInputUI(null, this._inputIndex);
				}
			}
			base._Update_Common_Start();
		}

		protected override void _Update_Common_End()
		{
			this._prevScreenPosition = this._currentScreenPosition;
			base._Update_Common_End();
		}

		private Vector3 _currentScreenPosition = Vector3.zero;

		private Vector3 _prevScreenPosition = Vector3.one;
	}
}
