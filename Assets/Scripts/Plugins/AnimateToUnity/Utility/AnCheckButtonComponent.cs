using System;

namespace AnimateToUnity.Utility
{
	public class AnCheckButtonComponent : AnComponentBase
	{
		public AnCheckButton CheckButton
		{
			get
			{
				return this._uiBase as AnCheckButton;
			}
		}
	}
}
