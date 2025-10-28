using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [CreateAssetMenu(fileName = "AnRootParameter", menuName = "AnScriptableObject/AnRootParameter", order = 1)]
    public class AnRootParameter : AnScriptableObject
    {
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
