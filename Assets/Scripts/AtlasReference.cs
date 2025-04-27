using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cute.UI
{
    [CreateAssetMenu(fileName = "AtlasReference", menuName = "ScriptableObjects/AtlasReference", order = 1)]
    public class AtlasReference : ScriptableObject
    {
        public Material material;
        public Sprite[] sprites;
    }
}
