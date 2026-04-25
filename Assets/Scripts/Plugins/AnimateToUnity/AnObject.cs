using System;
using UnityEngine;

namespace AnimateToUnity
{
	public class AnObject : AnObjectBase
	{
		public AnObjectParameter ObjectParameter
		{
			get
			{
				return this._objParam;
			}
		}

		public AnMotion ChildMotion
		{
			get
			{
				return this._childMotion;
			}
			set
			{
				this._childMotion = value;
			}
		}

		public bool ExistChildMotion
		{
			get
			{
				return this._existChildMotion;
			}
		}

		public AnObject(GameObject gameObject)
			: base(gameObject)
		{
		}

		public override void _CreateEditorData(AnObjectParameterBase parameter, AnMotion parentMotion)
		{
			base._CreateEditorData(parameter, parentMotion);
			this._objParam = parameter as AnObjectParameter;
		}

		public override void _ApplyData(AnObjectParameterBase parameter, AnMotion parentMotion)
		{
			base._ApplyData(parameter, parentMotion);
			this._objParam = parameter as AnObjectParameter;
		}

		public override void _CreateData()
		{
			base._CreateData();
			this._existChildMotion = false;
			if (this._objParam.ChildMotionID != "")
			{
				this._existChildMotion = true;
			}
			if (this._existCollider != 0)
			{
				this._parentMotion.Root.SortOrderCount += AnValue.SortOrderIntervalForObject;
			}
			this._sortOrderIndex = this._root.SortOrderCount;
			this._sortOrderIndexForDrawTextLater = this._root.SortOrderCountForDrawTextLater;
		}

		public override void _FixData()
		{
			base._FixData();
		}

		public override void _Update()
		{
			base._Update();
			if (this._existChildMotion)
			{
				this._childMotion._Update();
			}
		}

		public override void _UpdateForce()
		{
			base._UpdateForce();
			if (this._existChildMotion)
			{
				this._childMotion._UpdateForce();
			}
		}

		protected override void _UpdateColor()
		{
			this._currentColor = this._baseColor;
			this._currentColorOffset = this._baseColorOffset;
			base._UpdateColor();
		}

		protected override void _UpdateTransform(bool forceUpdate)
		{
			base._UpdateTransform(forceUpdate);
			if (this._scaleChanged)
			{
				this._transform.localScale = this._currentScale;
			}
		}

		public override void _UpdateStencilRef(bool affectChildren)
		{
			base._UpdateStencilRef(affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion._UpdateStencilRef(affectChildren);
			}
		}

		public override void _UpdateStencilCompareFunc(bool affectChildren)
		{
			base._UpdateStencilCompareFunc(affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion._UpdateStencilCompareFunc(affectChildren);
			}
		}

		protected override void _SetGrayscaleBase(bool enable)
		{
			base._SetGrayscaleBase(enable);
			if (this._existChildMotion)
			{
				this._childMotion.SetGrayscale(enable);
			}
		}

		public override void _ResetTime()
		{
			base._ResetTime();
			if (this._existChildMotion)
			{
				this._childMotion._ResetTime();
			}
		}

		public override void SetSortOffset(int sortOffset)
		{
			base.SetSortOffset(sortOffset);
			if (this._existChildMotion)
			{
				this._childMotion.SetSortOffset(sortOffset);
			}
		}

		public override void SetSortLayer(string sortLayerName)
		{
			base.SetSortLayer(sortLayerName);
			if (this._existChildMotion)
			{
				this._childMotion.SetSortLayer(sortLayerName);
			}
		}

		public override void SetTimeModeType(AnTimeModeTypes timeModeType, bool affectChildren)
		{
			base.SetTimeModeType(timeModeType, affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion.SetTimeModeType(timeModeType, affectChildren);
			}
		}

		public override void SetMotionSpeed(float speed, bool affectChildren)
		{
			base.SetMotionSpeed(speed, affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion.SetMotionSpeed(speed, affectChildren);
			}
		}

		public override void SetColliderThrough(bool through, bool affectChildren)
		{
			base.SetColliderThrough(through, affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion.SetColliderThrough(through, affectChildren);
			}
		}

		public override void SetColliderThicknessOffset(float thicknessOffset, bool affectChildren)
		{
			base.SetColliderThicknessOffset(thicknessOffset, affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion.SetColliderThicknessOffset(thicknessOffset, affectChildren);
			}
		}

		public override void _UpdateColliderThickness(bool affectChildren)
		{
			base._UpdateColliderThickness(affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion._UpdateColliderThickness(affectChildren);
			}
		}

		public override void SetEnableCollider(bool enable, bool affectChildren)
		{
			base.SetEnableCollider(enable, affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion.SetEnableCollider(enable, affectChildren);
			}
		}

		public override void SetSubCollider(Collider subCollider, bool affectChildren)
		{
			base.SetSubCollider(subCollider, affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion.SetSubCollider(subCollider, affectChildren);
			}
		}

		public override void SetBlurQuality(int blurQuality, int blurPrecision, bool affectChildren)
		{
			base.SetBlurQuality(blurQuality, blurPrecision, affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion.SetBlurQuality(blurQuality, blurPrecision, affectChildren);
			}
		}

		public override void SetBlurValue(Vector2 blurValue, bool affectChildren)
		{
			base.SetBlurValue(blurValue, affectChildren);
			if (this._existChildMotion)
			{
				this._childMotion.SetBlurValue(blurValue, affectChildren);
			}
		}

		public override void _UpdateScreenSize()
		{
			base._UpdateScreenSize();
			if (this._existChildMotion)
			{
				this._childMotion._UpdateScreenSize();
			}
		}

		private AnObjectParameter _objParam;

		private AnMotion _childMotion;

		private bool _existChildMotion;
	}
}
