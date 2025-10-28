using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnObjectParameterBase
    {

        public string _objectName;

        public int _objectIndex;

        public AnObjectTypes _objectType = AnObjectTypes.Object;

        public AnBlendModeTypes _blendModeType;

        public Vector2 _timeRange = Vector2.zero;

        public Vector2 _size = Vector2.zero;

        public Vector3 _position = Vector3.zero;

        public Vector3 _positionOffset = Vector3.zero;

        public Vector3 _rotate = Vector3.zero;

        public Vector3 _scale = Vector3.one;

        public Vector2 _shear = Vector2.zero;

        public Color _color = Color.white;

        public Color _colorOffset = Color.black;

        public bool _isStabilizeRotation;

        public AnPlaceAnchorTypes _placeAnchorType;

        public AnPlaceAnchorAttachTypes _placeAnchorAttachType;

        public AnPlaceAnchorScaleTypes _placeAnchorScaleType;

        public string _layerName;

        public string _sortLayerName;

        public int _sortOffset;

        public float _depthOffset;

        public int _stencilRef;

        public AnStencilCompareFuncTypes _stencilCompareFunc = AnStencilCompareFuncTypes.None;

        public AnTimeModeTypes _timeModeType;

        public Vector2 _blurValue = Vector2.zero;

        public int _blurQuality;

        public int _blurPrecision = 1;

        public List<AnKeyParameter> _positionKeyParamList;

        public List<AnKeyParameter> _positionOffsetKeyParamList;

        public List<AnKeyParameter> _rotateKeyParamList;

        public List<AnKeyParameter> _scaleKeyParamList;

        public List<AnKeyParameter> _shearKeyParamList;

        public List<AnKeyParameter> _colorKeyParamList;

        public List<AnKeyParameter> _colorOffsetKeyParamList;

        public List<AnKeyParameter> _blurKeyParamList;

        public AnCollisionTypes _collisionType;

        public AnCollisionParameter[] _collisionParamList;

        public List<int> _flagKeyList;

        public List<string> _flagValueList;

        public AnUIParameter _uiParameter;

        protected string _gameObjectName;

        protected GameObject _targetGameObject;

        protected GameObject _offsetGameObject;

        protected GameObject _attachGameObject;

        [NonSerialized]
        public AnTransformAnimationFlags _transformAnimationFlag;

        [NonSerialized]
        public AnColorAnimationFlags _colorAnimationFlag;

        [NonSerialized]
        public AnBlurAnimationFlags _blurAnimationFlag;

        private Hashtable _flagTable;
    }
}
