using System;
using System.Collections;
using System.Collections.Generic;

namespace AnimateToUnity
{
	[Serializable]
	public class AnMotionParameterGroup
	{
		public List<AnMotionParameter> MotionParameterList
		{
			get
			{
				return this._motionParameterList;
			}
			set
			{
				this._motionParameterList = value;
			}
		}

		public Hashtable MotionParameterTable
		{
			get
			{
				return this._motionParameterTable;
			}
			set
			{
				this._motionParameterTable = value;
			}
		}

		public void _Initialize()
		{
			this._motionParameterTable = new Hashtable();
			for (int i = 0; i < this._motionParameterList.Count; i++)
			{
				this._motionParameterList[i]._Initialize();
				this._motionParameterTable.Add(this._motionParameterList[i].ID, this._motionParameterList[i]);
			}
		}

		public AnMotionParameter _GetMotionParameter(string id)
		{
			if (id == null)
			{
				return null;
			}
			if (!this._motionParameterTable.ContainsKey(id))
			{
				return null;
			}
			return this._motionParameterTable[id] as AnMotionParameter;
		}

		public List<AnMotionParameter> _motionParameterList;

		private Hashtable _motionParameterTable;
	}
}
