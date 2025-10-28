using System;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnTextParameter : AnObjectParameterBase
    {
        public AnTextMeshTypes _textMeshType;

        public string _text;

        public string _fontName;

        public int _fontSize;

        public float _lineSpace;

        public TextAnchor _anchor;

        public FontStyle _fontStyle;

        public TextAlignment _alignment;

        public bool _useWrap;

        public bool _useFit;

        public Color32 _textColor = Color.white;

        public Color32 _shadowColor = Color.black;

        public float _shadowOffset;

        public float _shadowAngle;

        public Color32 _outlineColor = Color.gray;

        public int _outlineQuality;

        public float _outlineOffset;

        public string _gradationStartObjectName;

        public string _gradationEndObjectName;

        private int _fixOutlineQuality;

        private float _fontUpperAnchorOffset;

        private float _fontMiddleAnchorOffset;

        private float _fontLowerAnchorOffset;

        private float _fontLeftAlignOffset;

        private float _fontCenterAlignOffset;

        private float _fontRightAlignOffset;

        private float _fontLinespaceOffset;

        private int _fontSizeOffset;

        private Vector2 _fontIconOffset = Vector2.zero;

        private float _fontIconSizeOffset;

        private bool _useCommonFont;
    }
}
