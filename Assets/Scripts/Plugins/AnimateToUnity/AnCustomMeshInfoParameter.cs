using System;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnCustomMeshInfoParameter
    {
        [NonSerialized]
        private AnMeshParameter _meshParameter;

        public string _textureName;

        public AnPrimitiveMeshTypes _primitiveMeshType;

        public Mesh _customMesh;

        public Texture _textureColor;

        public Texture _textureAlpha;

        public Vector3 _positionOffset = Vector3.zero;

        public Vector3 _rotateOffset = Vector3.zero;

        public Vector3 _scaleOffset = Vector3.zero;

        public Vector2 _uvPositionOffset = Vector3.zero;

        public Vector2 _uvScaleOffset = Vector3.zero;

        public bool _cullingOn;

        public bool _invertNormal;

        public bool _keepMeshSize;

        public bool _keepMeshAspect;

        public float _marginTop;

        public float _marginButtom;

        public float _marginRight;

        public float _marginLeft;
    }
}
