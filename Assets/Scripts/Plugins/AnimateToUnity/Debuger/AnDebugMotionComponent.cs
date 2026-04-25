using System;

namespace AnimateToUnity.Debuger
{
	public class AnDebugMotionComponent : AnDebugBaseComponent
	{
		public AnMotion Motion
		{
			get
			{
				return this._motion;
			}
			set
			{
				this._motion = value;
			}
		}

		private AnMotion _motion;
	}
}
