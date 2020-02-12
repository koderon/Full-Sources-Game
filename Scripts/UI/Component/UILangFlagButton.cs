using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    public class UILangFlagButton : UIButton
    {
        [SerializeField]
        private Image _flagImage;

        private SystemLanguage _language;

        public void Setup(Sprite flagImage, SystemLanguage lang)
        {
            SetImage(flagImage);
            _language = lang;
        }

        public SystemLanguage GetSystemLanguage() => _language;

        public void SetImage(Sprite flagImage)
        {
            _flagImage.sprite = flagImage;
        }
        
        public void ActiveButton(bool active = true)
        {
            _flagImage.color = active ? Color.white : Color.grey;
        }
    }

}