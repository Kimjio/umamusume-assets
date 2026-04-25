using System;
using System.Collections;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnLabelParameter
	{
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		public Vector2 TimeRange
		{
			get
			{
				return this._timeRange;
			}
			set
			{
				this._timeRange = value;
			}
		}

		public string NextLabel
		{
			get
			{
				return this._nextLabel;
			}
			set
			{
				this._nextLabel = value;
			}
		}

		public int Index
		{
			get
			{
				return this._Index;
			}
			set
			{
				this._Index = value;
			}
		}

		public int NextIndex
		{
			get
			{
				return this._nextIndex;
			}
			set
			{
				this._nextIndex = value;
			}
		}

		public AnObjectControlInfoParameter[] ObjectControlInfoParamList
		{
			get
			{
				return this._objectControlInfoParamList;
			}
			set
			{
				this._objectControlInfoParamList = value;
			}
		}

		public Hashtable ActionStartTable
		{
			get
			{
				return this._actionStartTable;
			}
			set
			{
				this._actionStartTable = value;
			}
		}

		public Hashtable ActionLoopTable
		{
			get
			{
				return this._actionLoopTable;
			}
			set
			{
				this._actionLoopTable = value;
			}
		}

		public Hashtable ActionEndTable
		{
			get
			{
				return this._actionEndTable;
			}
			set
			{
				this._actionEndTable = value;
			}
		}

		public Hashtable FlActionStartTable
		{
			get
			{
				return this._flActionStartTable;
			}
			set
			{
				this._flActionStartTable = value;
			}
		}

		public Hashtable FlActionLoopTable
		{
			get
			{
				return this._flActionLoopTable;
			}
			set
			{
				this._flActionLoopTable = value;
			}
		}

		public Hashtable FlActionEndTable
		{
			get
			{
				return this._flActionEndTable;
			}
			set
			{
				this._flActionEndTable = value;
			}
		}

		public void _Initialize()
		{
			this._actionStartTable = new Hashtable();
			this._actionLoopTable = new Hashtable();
			this._actionEndTable = new Hashtable();
			this._flActionStartTable = new Hashtable();
			this._flActionLoopTable = new Hashtable();
			this._flActionEndTable = new Hashtable();
			if (this._objectControlInfoParamList != null)
			{
				for (int i = 0; i < this._objectControlInfoParamList.Length; i++)
				{
					this._objectControlInfoParamList[i]._Initialize();
				}
			}
		}

		public string _name;

		public Vector2 _timeRange = Vector2.zero;

		public string _nextLabel;

		public AnObjectControlInfoParameter[] _objectControlInfoParamList;

		[NonSerialized]
		public int _Index;

		[NonSerialized]
		public int _nextIndex;

		public Hashtable _actionStartTable;

		public Hashtable _actionLoopTable;

		public Hashtable _actionEndTable;

		public Hashtable _flActionStartTable;

		public Hashtable _flActionLoopTable;

		public Hashtable _flActionEndTable;
	}
}
