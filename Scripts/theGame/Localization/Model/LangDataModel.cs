using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theGame
{

    [Serializable]
    public class LangDataModel
    {
        public List<LanguageDataModel> items = new List<LanguageDataModel>();

    }

    [Serializable]
    public class LanguageDataModel
    {
        public string key;
                
        public string en;
        public string de;
        public string fr;
        public string pl;
        public string ru;
        public string pt;
        public string es;
        public string ko;
        public string ja;
        public string zh;
        public string it;
        public string nn;
        public string da;
    }

}