using UnityEngine;
using UnityEngine.UI;

namespace theGame
{


    class Localized : MonoBehaviour
    {
        public string Key;
        public bool IsUpper = false;
        private int _curLang = -1;
        

        void Awake()
        {
            OnLangChanged();
        }

        private void OnEnable()
        {
            OnLangChanged();
        }


        void Update()
        {
            if (Lang.Instance != null && _curLang != (int) Lang.Instance.CurLang)
            {
                OnLangChanged();
            }
        }

        private void OnLangChanged()
        {
            if (Lang.Instance == null)
                return;

            _curLang = (int) Lang.Instance.CurLang;
            var txt = gameObject.GetComponent<Text>();
            var text = Lang.Get(Key);

            if (IsUpper && !string.IsNullOrEmpty(text))
                text = text.ToUpper();
            if (txt != null)
            {
                txt.text = text;
            }
            else
            {
                var input = gameObject.GetComponent<InputField>();
                if (input != null)
                {
                    input.text = text;
                }
            }


        }
    }

}