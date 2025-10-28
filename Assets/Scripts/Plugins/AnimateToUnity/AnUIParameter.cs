using System;
using System.Collections;
using System.Collections.Generic;

namespace AnimateToUnity
{
    [Serializable]
    public class AnUIParameter
    {
        public AnUITypes _uiType;

        public List<string> _parameterKeyList;

        public List<string> _parameterValueList;

        private Hashtable _parameterTable;
    }
}
