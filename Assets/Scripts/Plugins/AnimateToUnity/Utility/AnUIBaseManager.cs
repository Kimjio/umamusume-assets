using System;
using System.Collections;
using System.Collections.Generic;

namespace AnimateToUnity.Utility
{
	public class AnUIBaseManager
	{
		public List<AnUIBase> _UIBaseList
		{
			get
			{
				return this._uiBaseList;
			}
		}

		public void _Initialize()
		{
			this._exist = false;
			this._uiBaseTable = new Hashtable();
			this._uiBaseList = new List<AnUIBase>();
			this._tempList = new List<AnUIBase>();
			this._exist = true;
		}

		public void _AddObject(AnUIBase target)
		{
			if (this._ExistObject(target))
			{
				return;
			}
			this._uiBaseTable.Add(target, target);
			this._uiBaseList.Add(target);
		}

		public bool _ExistObject(AnUIBase target)
		{
			return this._uiBaseTable.ContainsKey(target);
		}

		public void _RemoveObject(AnUIBase target)
		{
			if (!this._ExistObject(target))
			{
				return;
			}
			this._uiBaseTable.Remove(target);
			this._uiBaseList.Remove(target);
		}

		public void _OptimizeAll()
		{
			if (!this._exist)
			{
				return;
			}
			this._Optimize();
		}

		private void _Optimize()
		{
			this._tempList.Clear();
			for (int i = 0; i < this._uiBaseList.Count; i++)
			{
				AnUIBase anUIBase = this._uiBaseList[i];
				if (anUIBase != null && anUIBase.Motion != null && !(anUIBase.Motion.GameObject == null) && !(anUIBase.Motion.Root == null) && !(anUIBase.Motion.Root.gameObject == null))
				{
					this._tempList.Add(anUIBase);
				}
			}
			this._uiBaseTable.Clear();
			this._uiBaseList.Clear();
			for (int j = 0; j < this._tempList.Count; j++)
			{
				AnUIBase anUIBase2 = this._tempList[j];
				this._uiBaseTable.Add(anUIBase2, anUIBase2);
				this._uiBaseList.Add(anUIBase2);
			}
			this._tempList.Clear();
		}

		public void _UpdateFirst()
		{
			for (int i = 0; i < this._uiBaseList.Count; i++)
			{
				if (this._uiBaseList[i] != null)
				{
					this._uiBaseList[i]._UpdateInitialize();
					this._uiBaseList[i]._UpdateFirst();
				}
			}
		}

		public void _UpdateSecond()
		{
			for (int i = 0; i < this._uiBaseList.Count; i++)
			{
				if (this._uiBaseList[i] != null)
				{
					this._uiBaseList[i]._UpdateSecond();
				}
			}
		}

		private bool _exist;

		private Hashtable _uiBaseTable;

		public List<AnUIBase> _uiBaseList;

		private List<AnUIBase> _tempList;
	}
}
