using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [CreateAssetMenu(fileName = "AnMeshParameter", menuName = "AnScriptableObject/AnMeshParameter", order = 1)]
    public class AnMeshParameter : AnScriptableObject
    {
        public List<AnCustomMeshInfoParameter> _customMeshInfoParameterList;

        public List<AnMeshInfoParameterGroup> _meshParameterGroupList;

        private Hashtable _customMeshInfoParameterTable;

        [NonSerialized]
        private bool _initialized;
    }
}
