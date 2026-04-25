using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnPlayerSetting
	{
		public List<string> RuntimeKeyInputHorizontalNameList
		{
			get
			{
				return this._runtimeKeyInputHorizontalNameList;
			}
		}

		public List<string> RuntimeKeyInputVerticalNameList
		{
			get
			{
				return this._runtimeKeyInputVerticalNameList;
			}
		}

		public List<string> RuntimeKeyInputSubmitNameList
		{
			get
			{
				return this._runtimeKeyInputSubmitNameList;
			}
		}

		public List<string> RuntimeKeyInputCancelNameList
		{
			get
			{
				return this._runtimeKeyInputCancelNameList;
			}
		}

		public AnPlayerSetting()
		{
			this._ResetKeyInput();
		}

		public void _Initialize()
		{
			this._ResetKeyInput();
		}

		public void _ResetKeyInput()
		{
			this._SetKeyInputHorizontalName(this._keyInputHorizontalName);
			this._SetKeyInputVerticalName(this._keyInputVerticalName);
			this._SetKeyInputSubmitName(this._keyInputSubmitName);
			this._SetKeyInputCancelName(this._keyInputCancelName);
		}

		public void _SetKeyInputHorizontalName(string name)
		{
			if (this._runtimeKeyInputHorizontalNameList == null)
			{
				this._runtimeKeyInputHorizontalNameList = new List<string>();
			}
			this._runtimeKeyInputHorizontalNameList.Clear();
			if (name == null || name == "")
			{
				return;
			}
			string[] array = name.Split(new char[] { ',' });
			this._runtimeKeyInputHorizontalNameList.AddRange(array);
		}

		public void _SetKeyInputVerticalName(string name)
		{
			if (this._runtimeKeyInputVerticalNameList == null)
			{
				this._runtimeKeyInputVerticalNameList = new List<string>();
			}
			this._runtimeKeyInputVerticalNameList.Clear();
			if (name == null || name == "")
			{
				return;
			}
			string[] array = name.Split(new char[] { ',' });
			this._runtimeKeyInputVerticalNameList.AddRange(array);
		}

		public void _SetKeyInputSubmitName(string name)
		{
			if (this._runtimeKeyInputSubmitNameList == null)
			{
				this._runtimeKeyInputSubmitNameList = new List<string>();
			}
			this._runtimeKeyInputSubmitNameList.Clear();
			if (name == null || name == "")
			{
				return;
			}
			string[] array = name.Split(new char[] { ',' });
			this._runtimeKeyInputSubmitNameList.AddRange(array);
		}

		public void _SetKeyInputCancelName(string name)
		{
			if (this._runtimeKeyInputCancelNameList == null)
			{
				this._runtimeKeyInputCancelNameList = new List<string>();
			}
			this._runtimeKeyInputCancelNameList.Clear();
			if (name == null || name == "")
			{
				return;
			}
			string[] array = name.Split(new char[] { ',' });
			this._runtimeKeyInputCancelNameList.AddRange(array);
		}

		[SerializeField]
		private string _keyInputHorizontalName;

		[SerializeField]
		private string _keyInputVerticalName;

		[SerializeField]
		private string _keyInputSubmitName;

		[SerializeField]
		private string _keyInputCancelName;

		private List<string> _runtimeKeyInputHorizontalNameList;

		private List<string> _runtimeKeyInputVerticalNameList;

		private List<string> _runtimeKeyInputSubmitNameList;

		private List<string> _runtimeKeyInputCancelNameList;
	}
}
