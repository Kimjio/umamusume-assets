using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    public class AnRoot : AnMonoBehaviour
    {
        public AnRootParameter _parameter;

        public AnMeshParameterGroup _meshParameterGroup;

        private AnMotion _rootMotion;

        private List<AnBase> _dataList;

        private List<AnMotion> _motionList;

        private List<AnObjectBase> _objectList;

        private List<AnBase> _finalizeTargetDataList;

        private int _currentCreateDataIndex;

        private Hashtable _dataTable;

        private Hashtable _meshRendererTable;

        private Hashtable _meshFilterTable;

        private Hashtable _colliderTable;

        private Hashtable _textMeshTable;

        private Transform _transform;

        private GameObject _topObject;

        private Transform _topObjectTransform;

        private Vector3 _topObjectCurrentLocalPosition = Vector3.zero;

        private Quaternion _topObjectCurrentLocalRotate = Quaternion.identity;

        private Vector3 _topObjectCurrentLocalScale = Vector3.one;

        private GameObject _parentObject;

        private Transform _parentObjectTransform;

        [NonSerialized]
        public bool _initializeFlag;

        private bool _isDestroy;

        private bool _selectedFlag;

        private int _sortOrderInterval;

        private int _sortOrderCount;

        private int _sortOderCountForDrawTextLater;

        private int _defaultSortOffset;

        private int _defaultStencilRefOffset;

        private AnStencilCompareFuncTypes _defaultStencilCompareFunc;

        private int _stencilRefInterval;

        private int _stencilRefCount;

        private float _defaultDepthOffset;

        private float _defaultScaleOffset;

        private float _defaultScaleValue;

        private float _defaultColliderThickness = 1f;

        private bool _drawTextLater;

        [NonSerialized]
        public Vector2 _screenBaseRightPosition = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenBaseLeftPosition = Vector3.zero;

        private float _screenBaseAspect;

        private Vector2 _screenReferenceSize = Vector2.zero;

        private Vector2 _screenReferenceFixSize = Vector2.zero;

        private float _screenReferenceAspect;

        [NonSerialized]
        public Vector2 _screenEdgeRightPosition = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenEdgeLeftPosition = Vector3.zero;

        private Vector2 _screenEdgeSize = Vector3.zero;

        private float _screenEdgeAspect;

        [NonSerialized]
        public Vector2 _screenMarginRightPosition = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenMarginLeftPosition = Vector3.zero;

        private Vector2 _screenMarginSize = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenMarginOffset = Vector3.zero;

        private float _screenMarginAspect;

        public bool _ignoreMargin;

        [NonSerialized]
        public Vector2 _screenRightPosition = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenLeftPosition = Vector3.zero;

        // Token: 0x04000C1A RID: 3098
        private Vector2 _screenSize = Vector3.zero;

        [NonSerialized]
        public Vector2 _screenOffset = Vector3.zero;

        [NonSerialized]
        public float _screenScale = 1f;

        private Vector2 _screenVirtualSize = Vector2.zero;

        private Vector2 _baseScreenSize = Vector2.zero;

        private AnScreenCastTypes _screenCastType;

        private float _baseCameraSize;

        [NonSerialized]
        public bool _fitScreen;

        private bool _screenSizeChangeFlag;

        [NonSerialized]
        public float _deltaTime;

        [NonSerialized]
        public float _syncTime;

        private bool _useCustomSyncTime;

        private float _customSyncTime;

        private bool _useCustomDeltaTime;

        private float _customDeltaTime;

        private int _updateInterval;

        private int _updateGroup;

        private int _currentUpdateState;

        private Hashtable _meshInfoParameterGroupMaterialTable;

        [NonSerialized]
        private Vector3 _tempVector3_0 = Vector3.zero;

        [SerializeField]
        private bool _isWriteAlphaChannel;
    }
}
