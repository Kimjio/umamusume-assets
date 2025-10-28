using System;
using UnityEngine;

namespace AnimateToUnity
{
    [Serializable]
    public class AnCollisionParameter
    {
        public AnCollisionTypes _collisionType;

        public Vector3 _offset = Vector3.zero;

        public float _scale;

        public bool _through;
    }
}
