using System;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnFontSizeParameter
	{
		public int FontSize
		{
			get
			{
				return this._fontSize;
			}
		}

		public float UpperAnchorOffset
		{
			get
			{
				return this._upperAnchorOffset;
			}
		}

		public float MiddleAnchorOffset
		{
			get
			{
				return this._middleAnchorOffset;
			}
		}

		public float LowerAnchorOffset
		{
			get
			{
				return this._lowerAnchorOffset;
			}
		}

		public float LeftAlignOffset
		{
			get
			{
				return this._leftAlignOffset;
			}
		}

		public float CenterAlignOffset
		{
			get
			{
				return this._centerAlignOffset;
			}
		}

		public float RightAlignOffset
		{
			get
			{
				return this._rightAlignOffset;
			}
		}

		public float LineSpaceOffset
		{
			get
			{
				return this._lineSpaceOffset;
			}
		}

		public int SizeOffset
		{
			get
			{
				return this._sizeOffset;
			}
		}

		public Vector2 IconOffset
		{
			get
			{
				return this._iconOffset;
			}
		}

		public float IconSizeOffset
		{
			get
			{
				return this._iconSizeOffset;
			}
		}

		[SerializeField]
		private int _fontSize;

		[SerializeField]
		private float _leftAlignOffset;

		[SerializeField]
		private float _centerAlignOffset;

		[SerializeField]
		private float _rightAlignOffset;

		[SerializeField]
		private float _upperAnchorOffset;

		[SerializeField]
		private float _middleAnchorOffset;

		[SerializeField]
		private float _lowerAnchorOffset;

		[SerializeField]
		private float _lineSpaceOffset;

		[SerializeField]
		private int _sizeOffset;

		[SerializeField]
		private Vector2 _iconOffset = Vector2.zero;

		[SerializeField]
		private float _iconSizeOffset;
	}
}
