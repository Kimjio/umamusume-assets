using System;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnObjectParameter : AnObjectParameterBase
	{
		public string ChildMotionID
		{
			get
			{
				return this._childMotionID;
			}
			set
			{
				this._childMotionID = value;
			}
		}

		public AnMotion.ResetModeTypes MotionResetModeType
		{
			get
			{
				return this._motionResetModeType;
			}
			set
			{
				this._motionResetModeType = value;
			}
		}

		public override void _CreateEditorData(AnMotion parentMotion)
		{
			base._CreateEditorData(parentMotion);
			if (this._targetGameObject == null)
			{
				return;
			}
			AnObject anObject = new AnObject(this._targetGameObject);
			anObject._CreateEditorData(this, parentMotion);
			AnMotionParameter anMotionParameter = parentMotion.Root.Parameter.MotionParameterGroup._GetMotionParameter(this._childMotionID);
			if (anMotionParameter != null)
			{
				anMotionParameter._CreateEditorData(anObject, parentMotion.Root);
			}
		}

		public override void _Initialize()
		{
			base._Initialize();
			this._gameObjectName = AnValue.ObjectPrefix + this._objectName;
		}

		public override void _CreateHierarchy(AnRoot root, GameObject parentObject)
		{
			base._CreateHierarchy(root, parentObject);
			this._targetGameObject.name = AnValue.ObjectPrefix + base.ObjectName;
			AnMotionParameter anMotionParameter = root.Parameter.MotionParameterGroup._GetMotionParameter(this._childMotionID);
			if (anMotionParameter != null)
			{
				anMotionParameter._CreateHierarchy(root, this._attachGameObject);
			}
		}

		public override void _ApplyData(AnMotion parentMotion)
		{
			base._ApplyData(parentMotion);
			if (this._targetGameObject == null)
			{
				return;
			}
			AnObject anObject = new AnObject(this._targetGameObject);
			anObject._ApplyData(this, parentMotion);
			parentMotion.Root.ObjectList.Add(anObject);
			parentMotion.Root.DataTable.Add(this._targetGameObject, anObject);
			parentMotion.Root.DataList.Add(anObject);
			AnMotionParameter anMotionParameter = parentMotion.Root.Parameter.MotionParameterGroup._GetMotionParameter(this._childMotionID);
			if (anMotionParameter != null)
			{
				anMotionParameter._ApplyData(anObject, parentMotion.Root);
			}
		}

		public string _childMotionID;

		public AnMotion.ResetModeTypes _motionResetModeType;
	}
}
