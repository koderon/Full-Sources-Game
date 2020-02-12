using System.Collections.Generic;
using UnityEngine;

namespace theGame
{

    public class Lang
    {
        private Dictionary<string, string> _curLang = null;

        private SystemLanguage _curSystemLanguage = SystemLanguage.English;

        private readonly Dictionary<SystemLanguage, Dictionary<string, string>> _allLang = new Dictionary<SystemLanguage, Dictionary<string, string>>();


        public static Lang Instance { get; private set; }

        public Lang()
        {
            Instance = this;
        }

        public void Load()
        {
            var lngRes = Resources.Load<TextAsset>("db/lang");
            if (lngRes != null)
            {
                var text = Helper.CheckJson(lngRes.text);
                var data = JsonUtility.FromJson<LangDataModel>(text);
                Load(data);
            }

            CurLang = Application.systemLanguage;
            Debug.Log("System Lang := " + Application.systemLanguage);
        }


        private void Load(LangDataModel data)
        {
            _allLang.Clear();
            
            foreach (var l in data.items)
            {
                AddNewElement(SystemLanguage.Russian, l.key, l.ru);
                AddNewElement(SystemLanguage.English, l.key, l.en);
                AddNewElement(SystemLanguage.Danish, l.key, l.da);
                AddNewElement(SystemLanguage.German, l.key, l.de);
                AddNewElement(SystemLanguage.Spanish, l.key, l.es);
                AddNewElement(SystemLanguage.French, l.key, l.fr);
                AddNewElement(SystemLanguage.Italian, l.key, l.it);
                AddNewElement(SystemLanguage.Japanese, l.key, l.ja);
                AddNewElement(SystemLanguage.Korean, l.key, l.ko);
                AddNewElement(SystemLanguage.Norwegian, l.key, l.nn);
                AddNewElement(SystemLanguage.Polish, l.key, l.pl);
                AddNewElement(SystemLanguage.Portuguese, l.key, l.pt);
                AddNewElement(SystemLanguage.Chinese, l.key, l.zh);
            }
        }

        public void AddNewElement(SystemLanguage lang, string key, string text)
        {
            if(!_allLang.ContainsKey(lang))
                _allLang.Add(lang, new Dictionary<string, string>());

            _allLang[lang].Add(key, text);
        }

        public string GetText(string key)
        {
            if (_curLang != null && _curLang.ContainsKey(key)) return _curLang[key];
            return key;
        }

        public static string Get(string key)
        {
            return Instance != null ? Instance.GetText(key) : key;
        }

        public static string Get(string key, int val)
        {
            return string.Format(Instance.GetText(key), val);
        }

        public static string Get(string key, int val, int val2)
        {
            return string.Format(Instance.GetText(key), val, val2);
        }

        public static string Get(string key, object val1)
        {
            return string.Format(Instance.GetText(key), val1);
        }

        public static string Get(string key, object val1, object val2)
        {
            return string.Format(Instance.GetText(key), val1, val2);
        }

        public SystemLanguage CurLang
        {
            get => _curSystemLanguage;
            set
            {
                _curSystemLanguage = value;

                if (_allLang.ContainsKey(value))
                    _curLang = _allLang[value];
                else
                {
                    if (_allLang.ContainsKey(SystemLanguage.English))
                        _curLang = _allLang[SystemLanguage.English];
                }
            }
        }
    }

}
