using System;
using System.Collections;
using UnityEngine;

namespace AnimateToUnity
{
    // Token: 0x0200017E RID: 382
    [Serializable]
    public class AnLabelParameter
    {
        public string _name;

        public Vector2 _timeRange = Vector2.zero;

        public string _nextLabel;

        public AnObjectControlInfoParameter[] _objectControlInfoParamList;

        [NonSerialized]
        public int _Index;

        [NonSerialized]
        public int _nextIndex;

        public Hashtable _actionStartTable;

        public Hashtable _actionLoopTable;

        public Hashtable _actionEndTable;

        public Hashtable _flActionStartTable;

        public Hashtable _flActionLoopTable;

        public Hashtable _flActionEndTable;
    }
}
