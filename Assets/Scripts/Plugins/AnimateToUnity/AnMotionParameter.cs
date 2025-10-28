using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnMotionParameter
    {
        public string _id;

        public string _name;

        public AnLabelParameter[] _labelParamList;

        public List<AnObjectParameter> _objectParamList;

        public List<AnPlaneParameter> _planeParamList;

        public List<AnTextParameter> _textParamList;

        private List<AnObjectParameterBase> _objcectParamBaseList;

        private Hashtable _labelIndexTable;
    }
}
