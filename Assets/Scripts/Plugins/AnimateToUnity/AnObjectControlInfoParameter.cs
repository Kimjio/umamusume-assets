using System;

namespace AnimateToUnity
{
    [Serializable]
    public class AnObjectControlInfoParameter
    {
        public string _targetName;

        public float _startTime;

        public string _targetLabel;

        public float _targetTime = -1f;

        public bool _targetIsStop;

        [NonSerialized]
        public AnObjectControlInfoTypes _objectControlInfoType;
    }
}
