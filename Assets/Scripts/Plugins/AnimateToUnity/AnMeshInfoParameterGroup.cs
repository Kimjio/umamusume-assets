using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnMeshInfoParameterGroup
    {
        [NonSerialized]
        private AnMeshParameter _meshParameter;

        public string _textureSetName;

        public Vector2 _textureSetSize = Vector2.zero;

        public Texture _textureSetColor;

        public Texture _textureSetAlpha;

        public AnColorTextureFormatTypes _textureSetColorFormat;

        public AnAlphaTextureFormatTypes _textureSetAlphaFormat;

        public AnTextureCombinationTypes _textureCombinationType;

        public List<AnMeshInfoParameter> _meshInfoParameterList;

        private Hashtable _meshInfoParameterTable;

        private Hashtable _materialTable;
    }
}
