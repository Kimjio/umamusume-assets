using System;

namespace AnimateToUnity.Debuger
{
	public class AnDebugObjectComponent : AnDebugObjectBaseComponent
	{
		public AnObject Object
		{
			get
			{
				return this._objectBase as AnObject;
			}
			set
			{
				this._objectBase = value;
			}
		}
	}
}
