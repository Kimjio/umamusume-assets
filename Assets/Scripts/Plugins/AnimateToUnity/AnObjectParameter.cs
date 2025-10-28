using System;

namespace AnimateToUnity
{
    [Serializable]
    public class AnObjectParameter : AnObjectParameterBase
    {
        public string _childMotionID;

        public AnMotion.ResetModeTypes _motionResetModeType;
    }
}
