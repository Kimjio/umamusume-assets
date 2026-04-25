using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity.Utility
{
	public class AnCheckButton : AnUIBase
	{
		public AnCheckButtonComponent Component
		{
			get
			{
				return this._component as AnCheckButtonComponent;
			}
		}

		public bool Check
		{
			get
			{
				return this.GetCheck();
			}
		}

		public Action ActionCheckStart { get; set; }

		public Action ActionUncheckLoop { get; set; }

		public Action ActionChecked { get; set; }

		public Action ActionUncheckStart { get; set; }

		public Action ActionCheckLoop { get; set; }

		public Action ActionUnchecked { get; set; }

		public AnAction FlActionCheckStart { get; protected set; }

		public AnAction FlActionUncheckLoop { get; protected set; }

		public AnAction FlActionChecked { get; protected set; }

		public AnAction FlActionUncheckStart { get; protected set; }

		public AnAction FlActionCheckLoop { get; protected set; }

		public AnAction FlActionUnchecked { get; protected set; }

		public AnCheckButton()
		{
			this._logTitle = "UI CheckButton";
			this._logColor = new Color(0.5f, 0.5f, 1f);
			this._labelCheck = this._labelCheck ?? "";
			this._enableContinuousInputForTouchInput = false;
		}

		protected override void _InitializeThisData_PostProcess()
		{
			base._InitializeThisData_PostProcess();
			this._InitializeCheckLabelList();
			this.FlActionCheckStart = base._AddAction();
			this.FlActionUncheckLoop = base._AddAction();
			this.FlActionChecked = base._AddAction();
			this.FlActionUncheckStart = base._AddAction();
			this.FlActionCheckLoop = base._AddAction();
			this.FlActionUnchecked = base._AddAction();
		}

		protected override void _InitializeUILabelNameTable()
		{
			base._InitializeUILabelNameTable();
			this._uiLabelNameTable.Clear();
			this._uiLabelNameTable.Add(this._checkLabelLoop, this._checkLabelLoop);
			this._uiLabelNameTable.Add(this._checkLabelDisable, this._checkLabelDisable);
			this._uiLabelNameTable.Add(this._checkLabelDownIn, this._checkLabelDownIn);
			this._uiLabelNameTable.Add(this._checkLabelDownLoop, this._checkLabelDownLoop);
			this._uiLabelNameTable.Add(this._checkLabelDownOut, this._checkLabelDownOut);
			this._uiLabelNameTable.Add(this._checkLabelCheck, this._checkLabelCheck);
			this._uiLabelNameTable.Add(this._checkLabelSelectIn, this._checkLabelSelectIn);
			this._uiLabelNameTable.Add(this._checkLabelSelectLoop, this._checkLabelSelectLoop);
			this._uiLabelNameTable.Add(this._checkLabelSelectOut, this._checkLabelSelectOut);
			this._uiLabelNameTable.Add(this._checkLabelOverIn, this._checkLabelOverIn);
			this._uiLabelNameTable.Add(this._checkLabelOverLoop, this._checkLabelOverLoop);
			this._uiLabelNameTable.Add(this._checkLabelOverOut, this._checkLabelOverOut);
			this._uiLabelNameTable.Add(this._checkLabelLoop2, this._checkLabelLoop2);
			this._uiLabelNameTable.Add(this._checkLabelDisable2, this._checkLabelDisable2);
			this._uiLabelNameTable.Add(this._checkLabelDownIn2, this._checkLabelDownIn2);
			this._uiLabelNameTable.Add(this._checkLabelDownLoop2, this._checkLabelDownLoop2);
			this._uiLabelNameTable.Add(this._checkLabelDownOut2, this._checkLabelDownOut2);
			this._uiLabelNameTable.Add(this._checkLabelCheck2, this._checkLabelCheck2);
			this._uiLabelNameTable.Add(this._checkLabelSelectIn2, this._checkLabelSelectIn2);
			this._uiLabelNameTable.Add(this._checkLabelSelectLoop2, this._checkLabelSelectLoop2);
			this._uiLabelNameTable.Add(this._checkLabelSelectOut2, this._checkLabelSelectOut2);
			this._uiLabelNameTable.Add(this._checkLabelOverIn2, this._checkLabelOverIn2);
			this._uiLabelNameTable.Add(this._checkLabelOverLoop2, this._checkLabelOverLoop2);
			this._uiLabelNameTable.Add(this._checkLabelOverOut2, this._checkLabelOverOut2);
		}

		private void _InitializeCheckLabelList()
		{
			this._checkButtonType = AnCheckButton.CheckButtonTypes.Simple;
			if (this._motion.Parameter._ExistLabel(this._checkLabelCheck) && this._motion.Parameter._ExistLabel(this._checkLabelCheck2))
			{
				this._checkButtonType = AnCheckButton.CheckButtonTypes.Normal;
			}
			if (this._checkLabelList == null)
			{
				this._checkLabelList = new List<List<string>>();
			}
			this._checkLabelList.Clear();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (this._checkButtonType == AnCheckButton.CheckButtonTypes.Simple)
			{
				this._checkLabelLoop = "Loop";
				this._checkLabelDownIn = "_Loop";
				this._checkLabelDownLoop = "_Loop";
				this._checkLabelDownOut = "_Loop";
				this._checkLabelCheck = "DownIn";
				this._checkLabelLoop2 = "DownLoop";
				this._checkLabelDownIn2 = "_DownLoop";
				this._checkLabelDownLoop2 = "_DownLoop";
				this._checkLabelDownOut2 = "_DownLoop";
				this._checkLabelCheck2 = "DownOut";
				this._checkLabelDisable = "Disable";
				this._checkLabelDisable2 = "Disable2";
				this._checkLabelSelectIn = "SelectIn";
				this._checkLabelSelectIn2 = "SelectIn2";
				this._checkLabelSelectLoop = "SelectLoop";
				this._checkLabelSelectLoop2 = "SelectLoop2";
				this._checkLabelSelectOut = "SelectOut";
				this._checkLabelSelectOut2 = "SelectOut2";
				this._checkLabelOverIn = "OverIn";
				this._checkLabelOverIn2 = "OverIn2";
				this._checkLabelOverLoop = "OverLoop";
				this._checkLabelOverLoop2 = "OverLoop2";
				this._checkLabelOverOut = "OverOut";
				this._checkLabelOverOut2 = "OverOut2";
			}
			list.Add(this._checkLabelLoop);
			list.Add(this._checkLabelDownIn);
			list.Add(this._checkLabelDownLoop);
			list.Add(this._checkLabelDownOut);
			list.Add(this._checkLabelCheck2);
			list.Add(this._checkLabelDisable);
			list.Add(this._checkLabelSelectIn);
			list.Add(this._checkLabelSelectLoop);
			list.Add(this._checkLabelSelectOut);
			list.Add(this._checkLabelOverIn);
			list.Add(this._checkLabelOverLoop);
			list.Add(this._checkLabelOverOut);
			list2.Add(this._checkLabelLoop2);
			list2.Add(this._checkLabelDownIn2);
			list2.Add(this._checkLabelDownLoop2);
			list2.Add(this._checkLabelDownOut2);
			list2.Add(this._checkLabelCheck);
			list2.Add(this._checkLabelDisable2);
			list2.Add(this._checkLabelSelectIn2);
			list2.Add(this._checkLabelSelectLoop2);
			list2.Add(this._checkLabelSelectOut2);
			list2.Add(this._checkLabelOverIn2);
			list2.Add(this._checkLabelOverLoop2);
			list2.Add(this._checkLabelOverOut2);
			this._checkLabelList.Add(list);
			this._checkLabelList.Add(list2);
			this._UpdateLabelName();
		}

		private void _UpdateLabelName()
		{
			this._labelLoop = this._GetLabel(AnCheckButton.LabelTypes.Loop);
			this._labelDownIn = this._GetLabel(AnCheckButton.LabelTypes.DownIn);
			this._labelDownLoop = this._GetLabel(AnCheckButton.LabelTypes.DownLoop);
			this._labelDownOut = this._GetLabel(AnCheckButton.LabelTypes.DownOut);
			this._labelCheck = this._GetLabel(AnCheckButton.LabelTypes.Check);
			this._labelDisable = this._GetLabel(AnCheckButton.LabelTypes.Disable);
			this._labelSelectIn = this._GetLabel(AnCheckButton.LabelTypes.SelectIn);
			this._labelSelectLoop = this._GetLabel(AnCheckButton.LabelTypes.SelectLoop);
			this._labelSelectOut = this._GetLabel(AnCheckButton.LabelTypes.SelectOut);
			this._labelOverIn = this._GetLabel(AnCheckButton.LabelTypes.OverIn);
			this._labelOverLoop = this._GetLabel(AnCheckButton.LabelTypes.OverLoop);
			this._labelOverOut = this._GetLabel(AnCheckButton.LabelTypes.OverOut);
		}

		private string _GetLabel(AnCheckButton.LabelTypes labelType)
		{
			return this._checkLabelList[this._checkFlag][(int)labelType];
		}

		protected override void _Update_Common_Start()
		{
			base._Update_Common_Start();
			if (this._currentState == AnCheckButton.StateTypes.Check_Init)
			{
				this._currentState = AnCheckButton.StateTypes.Check_Loop;
			}
		}

		protected override void _Update(bool update = true)
		{
			base._Update(update);
			if (this._currentState == AnCheckButton.StateTypes.Check_Loop)
			{
				this._Update_Check_Loop();
			}
		}

		protected override void _Update_Loop_Init()
		{
			base._Update_Loop_Init();
			this._UpdateLabelName();
			if (this._checkChanged)
			{
				if (this.GetCheck())
				{
					if (this._executeCheckAction)
					{
						this._ExecuteAction(this.ActionChecked, this.FlActionChecked);
						base._SetLog(AnLogTypes.Checked);
					}
				}
				else if (this._executeCheckAction)
				{
					this._ExecuteAction(this.ActionUnchecked, this.FlActionUnchecked);
					base._SetLog(AnLogTypes.UnChecked);
				}
			}
			this._currentState = AnCheckButton.StateTypes.None;
			this._checkChanged = false;
			this._executeCheckAction = false;
			this._PlayMotion(this._GetLabel(AnCheckButton.LabelTypes.Loop), false);
		}

		protected override void _Update_DownOut_Init()
		{
			base._Update_DownOut_Init();
			if (this._inputUpType == AnInputUpTypes.UpInRange)
			{
				this._executeCheckAction = true;
				this._Update_Check_Init();
			}
		}

		private void _Update_Check_Init()
		{
			this._currentBaseState = AnUIBase.FlUIBaseStateTypes.None;
			this._currentState = AnCheckButton.StateTypes.Check_Init;
			if (!this.GetCheck())
			{
				if (this._executeCheckAction)
				{
					this._ExecuteAction(this.ActionCheckStart, this.FlActionCheckStart);
					base._SetLog(AnLogTypes.CheckStart);
				}
				this._checkFlag = 1;
			}
			else
			{
				if (this._executeCheckAction)
				{
					this._ExecuteAction(this.ActionUncheckStart, this.FlActionUncheckStart);
					base._SetLog(AnLogTypes.UnCheckStart);
				}
				this._checkFlag = 0;
			}
			this._UpdateLabelName();
			this._PlayMotion(this._GetLabel(AnCheckButton.LabelTypes.Check), true);
		}

		private void _Update_Check_Loop()
		{
			if (this._checkFlag == 1)
			{
				if (this._executeCheckAction)
				{
					this._ExecuteAction(this.ActionCheckLoop, this.FlActionCheckLoop);
				}
			}
			else if (this._executeCheckAction)
			{
				this._ExecuteAction(this.ActionUncheckLoop, this.FlActionUncheckLoop);
			}
			if (this._motion.Parameter._ExistLabel(this._GetLabel(AnCheckButton.LabelTypes.Check)))
			{
				if (this._motion.CurrentLabelName != this._GetLabel(AnCheckButton.LabelTypes.Check))
				{
					this._checkChanged = true;
					this._currentState = AnCheckButton.StateTypes.None;
					this._Update_Loop_Init();
					return;
				}
			}
			else
			{
				this._checkChanged = true;
				this._currentState = AnCheckButton.StateTypes.None;
				this._Update_Loop_Init();
			}
		}

		public override bool _IsDownState()
		{
			return base._IsDownState() || (this._currentBaseState == AnUIBase.FlUIBaseStateTypes.None || this._currentState == AnCheckButton.StateTypes.Check_Init || this._currentState == AnCheckButton.StateTypes.Check_Loop);
		}

		public bool GetCheck()
		{
			return this._motion._currentLabelName == this._checkLabelLoop2 || this._motion._currentLabelName == this._checkLabelDownIn2 || this._motion._currentLabelName == this._checkLabelDownLoop2 || this._motion._currentLabelName == this._checkLabelDownOut2 || this._motion._currentLabelName == this._checkLabelCheck || this._motion._currentLabelName == this._checkLabelDisable2 || this._motion._currentLabelName == this._checkLabelSelectIn2 || this._motion._currentLabelName == this._checkLabelSelectLoop2 || this._motion._currentLabelName == this._checkLabelSelectOut2 || this._motion._currentLabelName == this._checkLabelOverIn2 || this._motion._currentLabelName == this._checkLabelOverLoop2 || this._motion._currentLabelName == this._checkLabelOverOut2;
		}

		public void SetCheck(bool value)
		{
			this.SetCheck(value, false, false);
		}

		public void SetCheck(bool value, bool animation)
		{
			this.SetCheck(value, animation, false);
		}

		public void SetCheck(bool value, bool animation, bool executeAction)
		{
			this._executeCheckAction = executeAction;
			this._checkChanged = true;
			if (!animation)
			{
				if (this.GetCheck() != value)
				{
					this._checkFlag = 0;
					if (value)
					{
						this._checkFlag = 1;
					}
				}
				else
				{
					this._checkChanged = false;
				}
				this._Update_Loop_Init();
			}
			else if (this.GetCheck() != value)
			{
				this._Update_Check_Init();
			}
			this._UpdateForce();
		}

		public override void SetEnable(bool enable, AnUIEnableTypes enableType = AnUIEnableTypes.Normal)
		{
			if (this.GetCheck())
			{
				this._checkFlag = 1;
			}
			else
			{
				this._checkFlag = 0;
			}
			this._UpdateLabelName();
			base.SetEnable(enable, enableType);
		}

		public AnCheckButton.CheckButtonTypes _checkButtonType = AnCheckButton.CheckButtonTypes.Normal;

		private AnCheckButton.StateTypes _currentState;

		private bool _checkChanged;

		private bool _executeCheckAction = true;

		private int _checkFlag;

		private List<List<string>> _checkLabelList;

		private string _labelCheck = "Check";

		private string _checkLabelLoop = "Loop";

		private string _checkLabelDownIn = "DownIn";

		private string _checkLabelDownLoop = "DownLoop";

		private string _checkLabelDownOut = "DownOut";

		private string _checkLabelCheck = "Check";

		private string _checkLabelLoop2 = "Loop2";

		private string _checkLabelDownIn2 = "DownIn2";

		private string _checkLabelDownLoop2 = "DownLoop2";

		private string _checkLabelDownOut2 = "DownOut2";

		private string _checkLabelCheck2 = "Check2";

		private string _checkLabelDisable = "Disable";

		private string _checkLabelDisable2 = "Disable2";

		private string _checkLabelSelectIn = "SelectIn";

		private string _checkLabelSelectIn2 = "SelectIn2";

		private string _checkLabelSelectLoop = "SelectLoop";

		private string _checkLabelSelectLoop2 = "SelectLoop2";

		private string _checkLabelSelectOut = "SelectOut";

		private string _checkLabelSelectOut2 = "SelectOut2";

		private string _checkLabelOverIn = "OverIn";

		private string _checkLabelOverIn2 = "OverIn2";

		private string _checkLabelOverLoop = "OverLoop";

		private string _checkLabelOverLoop2 = "OverLoop2";

		private string _checkLabelOverOut = "OverOut";

		private string _checkLabelOverOut2 = "OverOut2";

		public enum CheckButtonTypes
		{
			Simple,
			Normal
		}

		public enum StateTypes
		{
			None,
			Check_Init,
			Check_Loop
		}

		public enum LabelTypes
		{
			Loop,
			DownIn,
			DownLoop,
			DownOut,
			Check,
			Disable,
			SelectIn,
			SelectLoop,
			SelectOut,
			OverIn,
			OverLoop,
			OverOut
		}
	}
}
