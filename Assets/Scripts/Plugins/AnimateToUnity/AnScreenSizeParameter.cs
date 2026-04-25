using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
	[Serializable]
	public class AnScreenSizeParameter
	{
		public int Priority
		{
			get
			{
				return this._priority;
			}
		}

		public List<string> DeviceModelList
		{
			get
			{
				return this._deviceModelList;
			}
		}

		public Vector2 ScreenSize
		{
			get
			{
				return this._screenSize;
			}
		}

		public float TopMargin
		{
			get
			{
				return this._topMargin;
			}
		}

		public float BottomMargin
		{
			get
			{
				return this._bottomMargin;
			}
		}

		public float LeftMargin
		{
			get
			{
				return this._leftMargin;
			}
		}

		public float RightMargin
		{
			get
			{
				return this._rightMargin;
			}
		}

		public Vector2 MaxWideSize
		{
			get
			{
				return this._maxWideSize;
			}
		}

		public Vector2 MaxNarrowSize
		{
			get
			{
				return this._maxNarrowSize;
			}
		}

		[SerializeField]
		private int _priority;

		[SerializeField]
		private List<string> _deviceModelList;

		[SerializeField]
		private Vector2 _screenSize = Vector2.zero;

		[SerializeField]
		private float _topMargin;

		[SerializeField]
		private float _bottomMargin;

		[SerializeField]
		private float _leftMargin;

		[SerializeField]
		private float _rightMargin;

		[SerializeField]
		private Vector2 _maxWideSize = Vector2.zero;

		[SerializeField]
		private Vector2 _maxNarrowSize = Vector2.zero;
	}
}
