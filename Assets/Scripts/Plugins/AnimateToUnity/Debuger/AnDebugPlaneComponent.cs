using System;

namespace AnimateToUnity.Debuger
{
	public class AnDebugPlaneComponent : AnDebugObjectBaseComponent
	{
		public AnPlane Plane
		{
			get
			{
				return this._objectBase as AnPlane;
			}
			set
			{
				this._objectBase = value;
			}
		}
	}
}
