using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace AnimateToUnity
{
    [Serializable]
    public class AnPlaneParameter : AnObjectParameterBase
    {
        public List<string> _textureNameList;

        public bool _fullNineSlice;

        public List<Color> _vertexColorList;

        public List<Vector2> _uvColorList;

        public List<Vector2> _uvAlphaList;

        public AnKeyParameter _textureKeyParam;
    }
}
