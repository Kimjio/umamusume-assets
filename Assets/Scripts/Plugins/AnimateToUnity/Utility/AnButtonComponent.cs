using System;

namespace AnimateToUnity.Utility
{
	public class AnButtonComponent : AnComponentBase
	{
		public AnButton Button
		{
			get
			{
				return this._uiBase as AnButton;
			}
		}
	}
}
