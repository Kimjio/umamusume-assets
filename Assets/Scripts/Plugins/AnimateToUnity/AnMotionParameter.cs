using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnMotionParameter
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

		public AnLabelParameter[] LabelParamList
		{
			get
			{
				return this._labelParamList;
			}
			set
			{
				this._labelParamList = value;
			}
		}

		public List<AnObjectParameter> ObjectParamList
		{
			get
			{
				return this._objectParamList;
			}
			set
			{
				this._objectParamList = value;
			}
		}

		public List<AnPlaneParameter> PlaneParamList
		{
			get
			{
				return this._planeParamList;
			}
			set
			{
				this._planeParamList = value;
			}
		}

		public List<AnTextParameter> TextParamList
		{
			get
			{
				return this._textParamList;
			}
			set
			{
				this._textParamList = value;
			}
		}

		public Hashtable LabelIndexTable
		{
			get
			{
				return this._labelIndexTable;
			}
		}

		public void _Initialize()
		{
			this._labelIndexTable = new Hashtable();
			for (int i = 0; i < this._labelParamList.Length; i++)
			{
				this._labelIndexTable.Add(this._labelParamList[i].Name, i);
			}
			for (int j = 0; j < this._labelParamList.Length; j++)
			{
				this._labelParamList[j]._Initialize();
				this._labelParamList[j].Index = this._GetLabelIndex(this._labelParamList[j].Name);
				if (this._labelParamList[j].NextLabel != "")
				{
					this._labelParamList[j].NextIndex = this._GetLabelIndex(this._labelParamList[j].NextLabel);
				}
				else
				{
					this._labelParamList[j].NextIndex = -1;
				}
			}
			this._CreateObjectParamBaseList();
		}

		public virtual void _CreateHierarchy(AnRoot root, GameObject parentObject)
		{
			GameObject gameObject;
			if (parentObject == null)
			{
				gameObject = root.gameObject;
			}
			else
			{
				gameObject = new GameObject(AnValue.MotionPrefix + this._name);
				gameObject.transform.parent = parentObject.transform;
			}
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			foreach (AnObjectParameterBase anObjectParameterBase in this._objcectParamBaseList)
			{
				anObjectParameterBase._CreateHierarchy(root, gameObject);
			}
		}

		public void _ApplyData(AnObject parentObject, AnRoot root)
		{
			GameObject gameObject;
			if (parentObject != null)
			{
				gameObject = AnUtilityObject.GetChildObject(parentObject.OffsetObject, 0);
				if (gameObject == null)
				{
					return;
				}
			}
			else
			{
				gameObject = root.gameObject;
			}
			AnMotion anMotion = new AnMotion(gameObject);
			anMotion._ApplyData(this, parentObject, root);
			root.DataTable.Add(gameObject, anMotion);
			root.DataList.Add(anMotion);
			foreach (AnObjectParameterBase anObjectParameterBase in this._objcectParamBaseList)
			{
				anObjectParameterBase._ApplyData(anMotion);
			}
			if (parentObject == null)
			{
				root.RootMotion = anMotion;
			}
		}

		public void _CreateEditorData(AnObject parentObject, AnRoot root)
		{
			GameObject gameObject;
			if (parentObject != null)
			{
				gameObject = AnUtilityObject.GetChildObject(parentObject.OffsetObject, 0);
				if (gameObject == null)
				{
					return;
				}
			}
			else
			{
				gameObject = root.gameObject;
			}
			AnMotion anMotion = new AnMotion(gameObject);
			anMotion._CreateEditorData(this, parentObject, root);
			this._CreateObjectParamBaseList();
			foreach (AnObjectParameterBase anObjectParameterBase in this._objcectParamBaseList)
			{
				anObjectParameterBase._CreateEditorData(anMotion);
			}
			if (parentObject == null)
			{
				root.RootMotion = anMotion;
			}
		}

		private void _CreateObjectParamBaseList()
		{
			this._objcectParamBaseList = new List<AnObjectParameterBase>();
			foreach (AnObjectParameter anObjectParameter in this._objectParamList)
			{
				anObjectParameter._Initialize();
				this._objcectParamBaseList.Add(anObjectParameter);
			}
			foreach (AnPlaneParameter anPlaneParameter in this._planeParamList)
			{
				anPlaneParameter._Initialize();
				this._objcectParamBaseList.Add(anPlaneParameter);
			}
			foreach (AnTextParameter anTextParameter in this._textParamList)
			{
				anTextParameter._Initialize();
				this._objcectParamBaseList.Add(anTextParameter);
			}
			this._objcectParamBaseList.Sort((AnObjectParameterBase a, AnObjectParameterBase b) => a.ObjectIndex - b.ObjectIndex);
		}

		public void _UpdateMotionTime(AnMotion motion)
		{
			if (motion._timeModeType != AnTimeModeTypes.Sync || motion._currentLabelIndex != motion._nextLabelIndex)
			{
				if (this._ExecuteAction(motion, ref motion._existLabelActionStart, motion._labelActionStart, true))
				{
					return;
				}
				if (this._ExecuteFlAction(motion, ref motion._existLabelFlActionStart, motion._labelFlActionStart, true))
				{
					return;
				}
				float y = motion._currentLabelTimeRange.y;
				if (motion._currentLabelIndex >= motion._nextLabelIndex && motion.NextLabelName != "" && motion._root != null && motion._root._parameter != null)
				{
					AnUtilityValue.LimitValue(ref y, motion._currentLabelTimeRange.x, motion._currentLabelTimeRange.y - motion._root._parameter._oneFrameTime);
				}
				if (motion._currentTime < y)
				{
					if (this._ExecuteAction(motion, ref motion._existLabelActionLoop, motion._labelActionLoop, false))
					{
						return;
					}
					this._ExecuteFlAction(motion, ref motion._existLabelFlActionLoop, motion._labelFlActionLoop, false);
					return;
				}
				else if (motion._nextLabelName == "")
				{
					motion.SetMotionPause();
					if (this._ExecuteAction(motion, ref motion._existLabelActionEnd, motion._labelActionEnd, true))
					{
						return;
					}
					this._ExecuteFlAction(motion, ref motion._existLabelFlActionEnd, motion._labelFlActionEnd, true);
					return;
				}
				else
				{
					if (this._ExecuteAction(motion, ref motion._existLabelActionEnd, motion._labelActionEnd, true))
					{
						return;
					}
					if (this._ExecuteFlAction(motion, ref motion._existLabelFlActionEnd, motion._labelFlActionEnd, true))
					{
						return;
					}
					AnLabelParameter anLabelParameter = this._GetLabel(motion._nextLabelName);
					float num = motion._currentTime - motion._currentLabelTimeRange.y;
					if (num < 0f || (motion._root != null && motion._root._parameter != null && num > motion._root._parameter._oneFrameTime))
					{
						num = 0f;
					}
					float num2 = anLabelParameter._timeRange.x + num;
					this._SetMotionLabelData(motion, anLabelParameter, num2);
					return;
				}
			}
			else
			{
				if (this._ExecuteAction(motion, ref motion._existLabelActionLoop, motion._labelActionLoop, false))
				{
					return;
				}
				if (this._ExecuteFlAction(motion, ref motion._existLabelFlActionLoop, motion._labelFlActionLoop, false))
				{
					return;
				}
				if (motion._root == null || motion._root._syncTime < motion._currentLabelTimeRange.x)
				{
					return;
				}
				float num3 = motion._root._syncTime - motion._currentLabelTimeRange.x;
				num3 %= motion._currentLabelTimeRange.y - motion._currentLabelTimeRange.x;
				num3 += motion._currentLabelTimeRange.x;
				this._SetMotionLabelData(motion, this._GetLabel(motion._currentLabelName), num3);
				return;
			}
		}

		private bool _ExecuteAction(AnMotion motion, ref bool existFlag, Action action, bool playOneTime = true)
		{
			if (!existFlag)
			{
				return false;
			}
			if (playOneTime)
			{
				existFlag = false;
			}
			float currentTime = motion._currentTime;
			action();
			return currentTime != motion._currentTime;
		}

		private bool _ExecuteFlAction(AnMotion motion, ref bool existFlag, AnAction flAction, bool playOneTime = true)
		{
			if (!existFlag)
			{
				return false;
			}
			if (playOneTime)
			{
				existFlag = false;
			}
			float currentTime = motion._currentTime;
			flAction._ExecuteAction();
			return currentTime != motion._currentTime;
		}

		public AnLabelParameter _GetLabel(string labelName)
		{
			if (!this._labelIndexTable.ContainsKey(labelName))
			{
				return this._labelParamList[0];
			}
			return this._labelParamList[(int)this._labelIndexTable[labelName]];
		}

		public AnLabelParameter _GetLabel(float time)
		{
			for (int i = 0; i < this._labelParamList.Length; i++)
			{
				AnLabelParameter anLabelParameter = this._labelParamList[i];
				if (time >= anLabelParameter._timeRange.x && time < anLabelParameter._timeRange.y)
				{
					return anLabelParameter;
				}
			}
			return this._labelParamList[0];
		}

		public int _GetLabelIndex(string labelName)
		{
			if (!this._labelIndexTable.ContainsKey(labelName))
			{
				return 0;
			}
			return (int)this._labelIndexTable[labelName];
		}

		public bool _ExistLabel(string labelName)
		{
			return this._labelIndexTable.ContainsKey(labelName);
		}

		public void _SetCurrentLabel(AnMotion motion, string labelName)
		{
			AnLabelParameter anLabelParameter = this._GetLabel(labelName);
			this._SetMotionLabelData(motion, anLabelParameter, anLabelParameter.TimeRange.x);
		}

		public void _SetCurrentLabel(AnMotion motion, float time)
		{
			AnLabelParameter anLabelParameter = this._GetLabel(time);
			this._SetMotionLabelData(motion, anLabelParameter, time);
		}

		private void _SetMotionLabelData(AnMotion motion, AnLabelParameter label, float time)
		{
			motion._currentLabelName = label._name;
			motion._currentLabelTimeRange = label._timeRange;
			motion._nextLabelName = label._nextLabel;
			motion._currentLabelIndex = label._Index;
			motion._nextLabelIndex = label._nextIndex;
			motion._currentTime = time;
			this._SetCurrentObjectControlInfoList(motion);
			this._SetMotionAction(motion, label.Name);
		}

		public void _SetMotionAction(AnMotion motion, string labelName)
		{
			if (motion.CurrentLabelName != labelName)
			{
				return;
			}
			AnLabelParameter anLabelParameter = this._GetLabel(labelName);
			if (labelName != anLabelParameter.Name)
			{
				return;
			}
			motion._existLabelActionStart = false;
			motion._existLabelActionLoop = false;
			motion._existLabelActionEnd = false;
			if (anLabelParameter._actionStartTable.ContainsKey(motion))
			{
				motion._labelActionStart = anLabelParameter._actionStartTable[motion] as Action;
				if (motion._labelActionStart != null)
				{
					motion._existLabelActionStart = true;
				}
			}
			if (anLabelParameter._actionLoopTable.ContainsKey(motion))
			{
				motion._labelActionLoop = anLabelParameter._actionLoopTable[motion] as Action;
				if (motion._labelActionLoop != null)
				{
					motion._existLabelActionLoop = true;
				}
			}
			if (anLabelParameter._actionEndTable.ContainsKey(motion))
			{
				motion._labelActionEnd = anLabelParameter._actionEndTable[motion] as Action;
				if (motion._labelActionEnd != null)
				{
					motion._existLabelActionEnd = true;
				}
			}
			motion._existLabelFlActionStart = false;
			motion._existLabelFlActionLoop = false;
			motion._existLabelFlActionEnd = false;
			if (anLabelParameter._flActionStartTable.ContainsKey(motion))
			{
				motion._labelFlActionStart = anLabelParameter._flActionStartTable[motion] as AnAction;
				if (motion._labelFlActionStart != null)
				{
					motion._existLabelFlActionStart = true;
				}
			}
			if (anLabelParameter._flActionLoopTable.ContainsKey(motion))
			{
				motion._labelFlActionLoop = anLabelParameter.FlActionLoopTable[motion] as AnAction;
				if (motion._labelFlActionLoop != null)
				{
					motion._existLabelFlActionLoop = true;
				}
			}
			if (anLabelParameter._flActionEndTable.ContainsKey(motion))
			{
				motion._labelFlActionEnd = anLabelParameter._flActionEndTable[motion] as AnAction;
				if (motion._labelFlActionEnd != null)
				{
					motion._existLabelFlActionEnd = true;
				}
			}
		}

		public void _CreateAllObjectControlInfoList(AnMotion motion)
		{
			motion._allObjectControlInfoList = new List<List<AnObjectControlInfo>>();
			motion._currentObjectControlInfoList = new List<AnObjectControlInfo>();
			for (int i = 0; i < this._labelParamList.Length; i++)
			{
				if (this._labelParamList[i]._objectControlInfoParamList != null)
				{
					List<AnObjectControlInfo> list = new List<AnObjectControlInfo>();
					for (int j = 0; j < this._labelParamList[i]._objectControlInfoParamList.Length; j++)
					{
						AnObjectControlInfoParameter anObjectControlInfoParameter = this._labelParamList[i]._objectControlInfoParamList[j];
						if (anObjectControlInfoParameter._objectControlInfoType != AnObjectControlInfoTypes.None)
						{
							AnObjectBase anObjectBase = motion._root.Find<AnObjectBase>(motion.GameObject, anObjectControlInfoParameter._targetName, false);
							if (anObjectBase != null)
							{
								AnObjectControlInfo anObjectControlInfo = null;
								AnObject anObject = anObjectBase as AnObject;
								if (anObject != null && anObject.ExistChildMotion)
								{
									float num = -1f;
									if (anObjectControlInfoParameter._objectControlInfoType == AnObjectControlInfoTypes.MotionPlayByLabel)
									{
										if (!anObject.ChildMotion.Parameter._ExistLabel(anObjectControlInfoParameter._targetLabel))
										{
											goto IL_0157;
										}
										num = anObject.ChildMotion.Parameter._GetLabel(anObjectControlInfoParameter._targetLabel)._timeRange.x;
									}
									else if (anObjectControlInfoParameter._objectControlInfoType == AnObjectControlInfoTypes.MotionPlayByTime)
									{
										num = anObjectControlInfoParameter._targetTime;
									}
									if (num < 0f)
									{
										goto IL_0157;
									}
									anObjectControlInfo = new AnObjectControlInfo();
									anObjectControlInfo._targetName = anObjectControlInfoParameter._targetName;
									anObjectControlInfo._startTime = anObjectControlInfoParameter._startTime;
									anObjectControlInfo._targetIsStop = anObjectControlInfoParameter._targetIsStop;
									anObjectControlInfo._fixTargetTime = num;
									anObjectControlInfo._targetObjectBase = anObjectBase;
									anObjectControlInfo._targetObject = anObject;
								}
								if (anObjectControlInfo != null)
								{
									list.Add(anObjectControlInfo);
								}
							}
						}
						IL_0157:;
					}
					motion._allObjectControlInfoList.Add(list);
				}
			}
			this._SetCurrentObjectControlInfoList(motion);
		}

		private void _SetCurrentObjectControlInfoList(AnMotion motion)
		{
			if (motion._currentLabelIndex < 0 || motion._currentLabelIndex >= motion._allObjectControlInfoList.Count)
			{
				return;
			}
			motion._currentObjectControlInfoList = motion._allObjectControlInfoList[motion._currentLabelIndex];
			for (int i = 0; i < motion._currentObjectControlInfoList.Count; i++)
			{
				motion._currentObjectControlInfoList[i]._Initialize();
			}
		}

		public string _id;

		public string _name;

		public AnLabelParameter[] _labelParamList;

		public List<AnObjectParameter> _objectParamList;

		public List<AnPlaneParameter> _planeParamList;

		public List<AnTextParameter> _textParamList;

		private List<AnObjectParameterBase> _objcectParamBaseList;

		private Hashtable _labelIndexTable;
	}
}
