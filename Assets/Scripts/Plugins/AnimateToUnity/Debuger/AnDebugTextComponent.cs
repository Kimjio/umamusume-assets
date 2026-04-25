using System;

namespace AnimateToUnity.Debuger
{
	public class AnDebugTextComponent : AnDebugObjectBaseComponent
	{
		public AnText Text
		{
			get
			{
				return this._objectBase as AnText;
			}
			set
			{
				this._objectBase = value;
			}
		}
	}
}
