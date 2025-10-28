using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnMeshInfoParameter
    {
        [NonSerialized]
        private AnMeshInfoParameterGroup _meshInfoParameterGroup;

        public string _textureName;

        public string _fixTextureName;

        public Vector2 _size = Vector2.zero;

        public Vector2 _offset = Vector2.zero;

        public Vector2 _uvSize = Vector2.zero;

        public Vector2 _uvOffset = Vector2.zero;

        public bool _rotated;

        public AnMeshTypes _meshType;

        public Vector4 _sliceRange;

        [NonSerialized]
        private Mesh _baseMesh;

        [NonSerialized]
        public Vector3[] _baseMeshVertices;

        [NonSerialized]
        private Mesh _baseCustomMesh;

        [NonSerialized]
        public Vector3[] _baseCustomMeshVertices;
    }
}
