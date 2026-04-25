using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnGlobalDataMediator : ScriptableObject
	{
		public AnGlobalData GlobalData
		{
			get
			{
				return this._globalData;
			}
		}

		public void _SetGlobalData(AnGlobalData data)
		{
			this._globalData = data;
		}

		[SerializeField]
		private AnGlobalData _globalData;
	}
}
