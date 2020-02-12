using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    
    public class UIAsyncImage : MonoBehaviour
    {
        private Image _image;

        private bool _needLoading = false;
        private string _url;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if(_image == null)
                _image = GetComponent<Image>();
        }

        public void Update()
        {
            if (_needLoading)
            {
                _needLoading = false;
                StartCoroutine(LoadingSprite(_url));
            }
        }

        public void Setup(string url)
        {
            _needLoading = true;
            _url = url;
        }

        public void Setup(Sprite spr)
        {
            Init();

            _image.sprite = spr;
        }

        IEnumerator LoadingSprite(string nameSprite)
        {
            var request = Resources.LoadAsync<Sprite>(nameSprite);

            yield return request;

            if (request.isDone)
            {
                Sprite spr = request.asset as Sprite;
                _image.sprite = spr;
            }
        }
    }

}