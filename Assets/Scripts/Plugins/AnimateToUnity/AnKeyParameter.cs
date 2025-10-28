using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnKeyParameter
    {
        public List<Vector2> _keyList;

        [NonSerialized]
        public int _keyCount;
    }
}
