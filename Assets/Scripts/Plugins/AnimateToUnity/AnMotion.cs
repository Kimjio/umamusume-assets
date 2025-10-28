using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    public class AnMotion : AnBase
    {
        private Hashtable _childGameObjectTable;

        private AnMotionParameter _parameter;

        private List<AnObjectBase> _objectList;

        private AnObject _parentObject;

        private bool _existParentObject;

        private AnMotion.ResetModeTypes _resetModeType;

        private AnMotion.StateTypes _currentStateType = AnMotion.StateTypes.Pause;

        public string _currentLabelName = "";

        public int _currentLabelIndex;

        public Vector2 _currentLabelTimeRange = Vector2.zero;

        public string _nextLabelName = "";

        public int _nextLabelIndex;

        public List<List<AnObjectControlInfo>> _allObjectControlInfoList;

        public List<AnObjectControlInfo> _currentObjectControlInfoList;

        public Action _labelActionStart;

        public Action _labelActionLoop;

        public Action _labelActionEnd;

        public bool _existLabelActionStart;

        public bool _existLabelActionLoop;

        public bool _existLabelActionEnd;

        public AnAction _labelFlActionStart;

        public AnAction _labelFlActionLoop;

        public AnAction _labelFlActionEnd;

        public bool _existLabelFlActionStart;

        public bool _existLabelFlActionLoop;

        public bool _existLabelFlActionEnd;

        public float _currentTime;

        public float _prevTime = -1f;

        public float _objectTime;

        public float _objectTimeWithoutLastFrame;

        public float _fixObjectTime;

        private float _restCurrentTime;

        private bool _updateLowerFlag;

        private MeshRenderer[] _meshRenderList;

        private Collider[] _colliderList;

        private Collider2D[] _collider2DList;

        private Transform[] _tempTransformList;

        private List<AnBase> _tempChildBaseList;

        private AnBase[] _childBaseList;

        private bool _existStencilRefCountUp;

        public enum ResetModeTypes
        {
            ResetAll,
            None,
            ResetLabel
        }

        public enum StateTypes
        {
            Playing,
            Pause
        }
    }
}
