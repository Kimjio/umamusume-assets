using System;
using System.Collections;
using System.Collections.Generic;
using AnimateToUnity.Utility;

namespace AnimateToUnity
{
	[Serializable]
	public class AnUIParameter
	{
		public AnUITypes UIType
		{
			get
			{
				return this._uiType;
			}
			set
			{
				this._uiType = value;
			}
		}

		public List<string> ParameterKeyList
		{
			get
			{
				return this._parameterKeyList;
			}
			set
			{
				this._parameterKeyList = value;
			}
		}

		public List<string> ParameterValueList
		{
			get
			{
				return this._parameterValueList;
			}
			set
			{
				this._parameterValueList = value;
			}
		}

		public void _CreateData(AnObjectBase objectBase)
		{
			this._parameterTable = new Hashtable();
			if (this._uiType != AnUITypes.None)
			{
				this._CreateParameterTable();
			}
			if (this._uiType == AnUITypes.Button)
			{
				objectBase.GameObject.AddComponent<AnButtonComponent>().Initialize<AnButton>();
				return;
			}
			if (this._uiType == AnUITypes.CheckButton)
			{
				objectBase.GameObject.AddComponent<AnCheckButtonComponent>().Initialize<AnCheckButton>();
				return;
			}
			if (this._uiType == AnUITypes.ImageNumber)
			{
				objectBase.GameObject.AddComponent<AnImageNumberComponent>().Initialize<AnImageNumber>();
				return;
			}
			if (this._uiType == AnUITypes.ProgressBar)
			{
				objectBase.GameObject.AddComponent<AnProgressBarComponent>().Initialize<AnProgressBar>();
				return;
			}
			if (this._uiType == AnUITypes.SlideBar)
			{
				objectBase.GameObject.AddComponent<AnSlideBarComponent>().Initialize<AnSlideBar>();
				return;
			}
			if (this._uiType == AnUITypes.ScrollBar)
			{
				objectBase.GameObject.AddComponent<AnScrollBarComponent>().Initialize<AnScrollBar>();
				return;
			}
			if (this._uiType == AnUITypes.CheckButtonList)
			{
				objectBase.GameObject.AddComponent<AnCheckButtonListComponent>().Initialize<AnCheckButtonList>();
				return;
			}
			if (this._uiType == AnUITypes.TextScroll)
			{
				objectBase.GameObject.AddComponent<AnTextScrollComponent>().Initialize<AnTextScroll>();
				return;
			}
			if (this._uiType == AnUITypes.ObjectScroll)
			{
				objectBase.GameObject.AddComponent<AnObjectScrollComponent>().Initialize<AnObjectScroll>();
				return;
			}
			if (this._uiType == AnUITypes.ObjectScrollList)
			{
				objectBase.GameObject.AddComponent<AnObjectScrollListComponent>().Initialize<AnObjectScrollList>();
				return;
			}
			if (this._uiType == AnUITypes.UpDownArrow)
			{
				objectBase.GameObject.AddComponent<AnUpDownArrowComponent>().Initialize<AnUpDownArrow>();
			}
		}

		public void _CreateParameterTable()
		{
			if (this._parameterTable == null)
			{
				this._parameterTable = new Hashtable();
			}
			if (this._parameterKeyList.Count == this._parameterValueList.Count && this._parameterKeyList.Count > 0 && this._parameterValueList.Count > 0)
			{
				for (int i = 0; i < this._parameterKeyList.Count; i++)
				{
					this._parameterTable.Add(this._parameterKeyList[i], this._parameterValueList[i]);
				}
			}
		}

		public string _GetParameterValue(string key, int type = 0)
		{
			string text = "";
			if (this._parameterTable != null && this._parameterTable.ContainsKey(key))
			{
				text = this._parameterTable[key].ToString();
				if (type == 1)
				{
					text = text.Replace("_", ".");
				}
				else if (type == 2)
				{
					if (text == "0")
					{
						text = "false";
					}
					else
					{
						text = "true";
					}
				}
			}
			return text;
		}

		public AnUITypes _uiType;

		public List<string> _parameterKeyList;

		public List<string> _parameterValueList;

		private Hashtable _parameterTable;
	}
}
