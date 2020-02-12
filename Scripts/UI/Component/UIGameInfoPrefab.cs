using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    public class UIGameInfoPrefab : MonoBehaviour, IUIListGridElement
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private Text _text;

        private Vector3 _position;

        public void Setup(string url, string text)
        {
            var spr = GameStaticData.GetSprite(url);

            _image.sprite = spr;

            _text.text = text;

            Show(true);
        }

        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public void Refresh()
        {
            
        }

        public void SetPosition(Vector3 pos)
        {
            _position = pos;
            GetComponent<RectTransform>().localPosition = pos;
        }

        public Vector3 GetPosition()
        {
            return _position;
        }
    }

}