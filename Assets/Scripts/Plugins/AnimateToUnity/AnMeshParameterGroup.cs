using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnMeshParameterGroup
    {
        public List<AnMeshParameter> _meshParameterList;

        private List<Mesh> _notSharedMeshList;

        private Hashtable _notSharedMaterialTable;
    }
}
