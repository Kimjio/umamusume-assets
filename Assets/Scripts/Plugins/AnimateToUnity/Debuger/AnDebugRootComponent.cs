using System;

namespace AnimateToUnity.Debuger
{
	public class AnDebugRootComponent : AnDebugBaseComponent
	{
		public AnRoot Root
		{
			get
			{
				return this._root;
			}
			set
			{
				this._root = value;
			}
		}

		private AnRoot _root;
	}
}
