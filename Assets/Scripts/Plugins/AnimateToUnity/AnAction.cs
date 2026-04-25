using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnAction
	{
		public List<Action<object>> ActionList
		{
			get
			{
				return this._actionList;
			}
		}

		public List<object> ValueList
		{
			get
			{
				return this._valueList;
			}
		}

		public List<int> IDList
		{
			get
			{
				return this._idList;
			}
		}

		public List<bool> LockList
		{
			get
			{
				return this._lockList;
			}
		}

		public AnAction()
		{
			this._actionList = new List<Action<object>>();
			this._valueList = new List<object>();
			this._idList = new List<int>();
			this._lockList = new List<bool>();
		}

		public void AddAction(Action<object> action, object value, int id = -1)
		{
			this._AddActionBase(action, value, id, false);
		}

		public void AddAction(Action<object> action, object value, int id, bool isLock)
		{
			this._AddActionBase(action, value, id, isLock);
		}

		private void _AddActionBase(Action<object> action, object value, int id, bool isLock)
		{
			if (action == null)
			{
				return;
			}
			if (id >= 0)
			{
				this.RemoveActionFromID(id, true);
			}
			this._actionList.Add(action);
			this._valueList.Add(value);
			if (id < 0)
			{
				this._idList.Add(global::UnityEngine.Random.Range(1000000, 99999999));
			}
			else
			{
				this._idList.Add(id);
			}
			this._lockList.Add(isLock);
		}

		public void RemoveActionFromIndex(int index, bool forceRemove = true)
		{
			if (index >= this._actionList.Count || index < 0)
			{
				return;
			}
			if (!forceRemove && this._lockList[index])
			{
				return;
			}
			this._actionList[index] = null;
			this._actionList.RemoveAt(index);
			this._valueList.RemoveAt(index);
			this._idList.RemoveAt(index);
			this._lockList.RemoveAt(index);
		}

		public void RemoveActionFromID(int id, bool forceRemove = true)
		{
			int num = -1;
			for (int i = 0; i < this._actionList.Count; i++)
			{
				if (this._idList[i] == id)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return;
			}
			this.RemoveActionFromIndex(num, forceRemove);
		}

		public void RemoveAllAction()
		{
			bool flag = false;
			for (int i = 0; i < this._lockList.Count; i++)
			{
				if (this._lockList[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this._actionList.Clear();
				this._valueList.Clear();
				this._idList.Clear();
				this._lockList.Clear();
				return;
			}
			flag = false;
			while (!flag)
			{
				flag = true;
				for (int j = 0; j < this._actionList.Count; j++)
				{
					if (!this._lockList[j])
					{
						flag = false;
						this.RemoveActionFromIndex(j, false);
						break;
					}
				}
			}
		}

		public void _ExecuteAction()
		{
			for (int i = 0; i < this._actionList.Count; i++)
			{
				if (this._actionList[i] != null)
				{
					this._actionList[i](this._valueList[i]);
				}
			}
		}

		public void _Release()
		{
			for (int i = 0; i < this._actionList.Count; i++)
			{
				this._actionList[i] = null;
				this._valueList[i] = null;
			}
			this._actionList.Clear();
			this._valueList.Clear();
			this._idList.Clear();
			this._lockList.Clear();
			this._actionList = null;
			this._valueList = null;
			this._idList = null;
			this._lockList = null;
		}

		private List<Action<object>> _actionList;

		private List<object> _valueList;

		private List<int> _idList;

		private List<bool> _lockList;
	}
}
