using System;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnButton : AnUIBase
	{
		public AnButtonComponent Component
		{
			get
			{
				return this._component as AnButtonComponent;
			}
		}

		public AnButton()
		{
			this._logTitle = "UI Button";
			this._logColor = Color.green;
			this._enableContinuousInputForTouchInput = false;
		}
	}
}
