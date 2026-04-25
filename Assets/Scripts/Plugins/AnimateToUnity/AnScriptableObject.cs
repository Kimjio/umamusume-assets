using UnityEngine;

namespace AnimateToUnity
{
	public class AnScriptableObject : ScriptableObject
	{
		public string ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public string _id;
	}
}
