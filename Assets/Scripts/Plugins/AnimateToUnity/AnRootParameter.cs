using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [CreateAssetMenu(fileName = "AnRootParameter", menuName = "AnScriptableObject/AnRootParameter", order = 1)]
    public class AnRootParameter : AnScriptableObject
    {
		public string RootMotionID
		{
			get
			{
				return this._rootMotionID;
			}
			set
			{
				this._rootMotionID = value;
			}
		}

		public string LayerName
		{
			get
			{
				return this._layerName;
			}
			set
			{
				this._layerName = value;
			}
		}

		public string SortLayerName
		{
			get
			{
				return this._sortLayerName;
			}
			set
			{
				this._sortLayerName = value;
			}
		}

		public int SortOffset
		{
			get
			{
				return this._sortOffset;
			}
			set
			{
				this._sortOffset = value;
			}
		}

		public int StencilRefOffset
		{
			get
			{
				return this._stencilRefOffset;
			}
			set
			{
				this._stencilRefOffset = value;
			}
		}

		public AnStencilCompareFuncTypes StencilCompareFunc
		{
			get
			{
				return this._stencilCompareFunc;
			}
			set
			{
				this._stencilCompareFunc = value;
			}
		}

		public float DepthOffset
		{
			get
			{
				return this._depthOffset;
			}
			set
			{
				this._depthOffset = value;
			}
		}

		public float ScaleOffset
		{
			get
			{
				return this._scaleOffset;
			}
			set
			{
				this._scaleOffset = value;
			}
		}

		public float ScaleValue
		{
			get
			{
				return this._scaleValue;
			}
			set
			{
				this._scaleValue = value;
			}
		}

		public float ColliderThickness
		{
			get
			{
				return this._colliderThickness;
			}
			set
			{
				this._colliderThickness = value;
			}
		}

		public bool DrawTextLater
		{
			get
			{
				return this._drawTextLater;
			}
			set
			{
				this._drawTextLater = value;
			}
		}

		public Vector2 BaseScreenSize
		{
			get
			{
				return this._baseScreenSize;
			}
			set
			{
				this._baseScreenSize = value;
			}
		}

		public Vector2 BaseScreenFixSize
		{
			get
			{
				return this._baseScreenFixSize;
			}
			set
			{
				this._baseScreenFixSize = value;
			}
		}

		public Vector2 BaseScreenUsingSize
		{
			get
			{
				return this._baseScreenUsingSize;
			}
			set
			{
				this._baseScreenUsingSize = value;
			}
		}

		public Vector2 ScreenReferenceSize
		{
			get
			{
				return this._screenReferenceSize;
			}
			set
			{
				this._screenReferenceSize = value;
			}
		}

		public float BaseFrameRate
		{
			get
			{
				return this._baseFrameRate;
			}
			set
			{
				this._baseFrameRate = value;
			}
		}

		public float BaseNullSize
		{
			get
			{
				return this._baseNullSize;
			}
			set
			{
				this._baseNullSize = value;
			}
		}

		public AnScreenCastTypes ScreenCastType
		{
			get
			{
				return this._screenCastType;
			}
			set
			{
				this._screenCastType = value;
			}
		}

		public float BaseCameraSize
		{
			get
			{
				return this._baseCameraSize;
			}
			set
			{
				this._baseCameraSize = value;
			}
		}

		public bool FitScreen
		{
			get
			{
				return this._fitScreen;
			}
			set
			{
				this._fitScreen = value;
			}
		}

		public bool UseCustomMesh
		{
			get
			{
				return this._useCustomMesh;
			}
			set
			{
				this._useCustomMesh = value;
			}
		}

		public List<AnRootLocalizeParameter> LocalizeParameterList
		{
			get
			{
				return this._localizeParameterList;
			}
			set
			{
				this._localizeParameterList = value;
			}
		}

		public AnMotionParameterGroup MotionParameterGroup
		{
			get
			{
				return this._motionParameterGroup;
			}
			set
			{
				this._motionParameterGroup = value;
			}
		}

		public int LayerIndex
		{
			get
			{
				return this._layerIndex;
			}
		}

		public float OneFrameTime
		{
			get
			{
				return this._oneFrameTime;
			}
		}

		public void _Initialize()
		{
			this._layerIndex = AnUtilityObject.GetLayerIndex(this._layerName);
			this._oneFrameTime = Mathf.Floor(1f / this._baseFrameRate * 100000f) / 100000f;
			if (this._baseScreenFixSize.x > 0f && this._baseScreenFixSize.y > 0f)
			{
				this._baseScreenUsingSize = this._baseScreenFixSize;
			}
			else
			{
				this._baseScreenUsingSize = this._baseScreenSize;
			}
			this._motionParameterGroup._Initialize();
			this._CreateFontNameFromCommonTable();
		}

		private void _CreateFontNameFromCommonTable()
		{
			if (this._fontNameFromCommonTable == null)
			{
				this._fontNameFromCommonTable = new Hashtable();
			}
			this._fontNameFromCommonTable.Clear();
			for (int i = 0; i < this._localizeParameterList.Count; i++)
			{
				AnRootLocalizeParameter anRootLocalizeParameter = this._localizeParameterList[i];
				if (anRootLocalizeParameter.LocalizeTarget != null && !this._fontNameFromCommonTable.ContainsKey(anRootLocalizeParameter.LocalizeTarget))
				{
					Hashtable hashtable = new Hashtable();
					for (int j = 0; j < anRootLocalizeParameter.FontNameFromCommonList.Count; j++)
					{
						string text = anRootLocalizeParameter.FontNameFromCommonList[j];
						if (text != null && !(text == "") && !hashtable.ContainsKey(text))
						{
							hashtable.Add(text, text);
						}
					}
					if (hashtable.Count != 0)
					{
						this._fontNameFromCommonTable.Add(anRootLocalizeParameter.LocalizeTarget, hashtable);
					}
				}
			}
		}

		public bool _UseCommonFont(string fontName)
		{
			if (fontName == null)
			{
				return false;
			}
			if (fontName == "")
			{
				return false;
			}
			if (this._fontNameFromCommonTable == null)
			{
				return false;
			}
			if (!this._fontNameFromCommonTable.ContainsKey(AnMonoSingleton<AnRootManager>.Instance.LocalizeTarget))
			{
				return false;
			}
			Hashtable hashtable = this._fontNameFromCommonTable[AnMonoSingleton<AnRootManager>.Instance.LocalizeTarget] as Hashtable;
			return hashtable != null && hashtable.ContainsKey(fontName);
		}

		public void _InitializeEditor()
		{
			this._motionParameterGroup._Initialize();
		}

        public string _rootMotionID;

        public string _layerName;

        public string _sortLayerName;

        public int _sortOffset;

        public int _stencilRefOffset;

        public AnStencilCompareFuncTypes _stencilCompareFunc;

        public float _depthOffset;

        public float _scaleOffset;

        public float _scaleValue = 1f;

        public bool _drawTextLater;

        public float _baseFrameRate;

        public float _baseNullSize;

        public AnScreenCastTypes _screenCastType;

        public float _baseCameraSize;

        public bool _fitScreen;

        public Vector2 _baseScreenSize = Vector2.zero;

        public Vector2 _baseScreenFixSize = Vector2.zero;

        public Vector2 _screenReferenceSize = Vector2.zero;

        public bool _useCustomMesh = true;

        public float _colliderThickness = 1f;

        public List<AnRootLocalizeParameter> _localizeParameterList;

        public AnMotionParameterGroup _motionParameterGroup;

        private int _layerIndex;

        [NonSerialized]
        public float _oneFrameTime;

        [NonSerialized]
        public Vector2 _baseScreenUsingSize = Vector2.zero;

        private Hashtable _fontNameFromCommonTable;
    }
}
