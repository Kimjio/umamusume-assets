using System;

namespace AnimateToUnity.Debuger
{
	public class AnDebugObjectBaseComponent : AnDebugBaseComponent
	{
		public virtual AnObjectBase ObjectBase
		{
			get
			{
				return this._objectBase;
			}
			set
			{
				this._objectBase = value;
			}
		}

		protected AnObjectBase _objectBase;
	}
}
