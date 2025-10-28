using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimateToUnity
{
    public class AnMonoBehaviour : MonoBehaviour
    {
        public bool Visible
        {
            get
            {
                return this._visible;
            }
        }

        public bool VisibleInHierarchy
        {
            get
            {
                return this._visibleInHierarchy;
            }
        }

        public virtual void SetVisible(bool visible)
        {
            this._visible = visible;
        }

        protected bool _visible;

        [NonSerialized]
        public bool _visibleInHierarchy;
    }
}
