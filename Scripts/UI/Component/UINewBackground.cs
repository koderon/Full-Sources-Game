using System;
using System.Collections;
using System.Collections.Generic;
using theGame;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    public class UINewBackground : MonoBehaviour, ISwitcherDayOrNight
    {
        public Image ColorBackgroundImage;

        //public SVGImage BackgroundBackImage;
        //public SVGImage BackgroundFrontImage;

        public Image BackgroundBackImage;
        public Image BackgroundFrontImage;

        public string IdBackground = "";

        [SerializeField]
        private Canvas _canvas;

        private Vector2 _canvasSize;

        [SerializeField]
        private int _offsetBottomPosition = 920;

        private BackgroundDataModel _backgroundDataModel;
        private bool _isDay = true;

        private float _backgroundSize = -1;
        private float _offsetBackground;

        // Start is called before the first frame update
        void Start()
        {
            CalculatePosition();

            if (!string.IsNullOrEmpty(IdBackground))
                SetIdBackground(IdBackground);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CalculatePosition()
        {
            _canvasSize = _canvas.GetComponent<RectTransform>().sizeDelta;
            var w = 1080;
            var h = 1600;

            if (_canvasSize.y < h)
                _canvasSize.y = h;

            _offsetBackground = 0;
            if (_canvasSize.y > 1920)
                _offsetBackground = (_canvasSize.y - 1920) / 2.0f;
            if (_canvasSize.y < 1920)
                _offsetBackground = (_canvasSize.y - 1920.0f) + 50.0f;

            _backgroundSize = _canvasSize.y - _offsetBottomPosition;
        }

        public void SetIdBackground(string id, string text1 = "", string text2 = "")
        {
            IdBackground = id;

            Debug.LogWarning("setbackground := " + id);

            _backgroundDataModel = TheGame.GetComponent<BackgroundData>().GetBackground(id);

            if (_backgroundDataModel == null)
                return;

            InitBackground();
        }

        public void SwitchDayOrNight(bool isDay)
        {
            _isDay = isDay;

            if (_backgroundDataModel != null)
                InitBackground();
        }

        private void InitBackground()
        {
            CalculatePosition();

            ColorBackgroundImage.color = _isDay ? Color.white : Color.black;

            var nameBackgroundBack = _isDay
                ? _backgroundDataModel.imageBackgroundDay
                : _backgroundDataModel.imageBackgroundNight;

            var nameBackgroundFront = _isDay
                ? _backgroundDataModel.imageDay
                : _backgroundDataModel.imageNight;

            var offsetFonImage = _isDay ? _backgroundDataModel.offsetDay : _backgroundDataModel.offsetNight;
            var screenWidth = 1080;

            if (!string.IsNullOrEmpty(nameBackgroundBack))
            {
                BackgroundBackImage.gameObject.SetActive(true);
                BackgroundBackImage.color = Color.white;
                //BackgroundBackImage.sprite = Load(nameBackgroundBack);
                BackgroundBackImage.sprite = Resources.Load<Sprite>("Sprites/Background/" + nameBackgroundBack);

                /// TEST!!!!!!
                if (_backgroundDataModel.bottom == -1)
                    BackgroundBackImage.rectTransform.localPosition = new Vector3(0, offsetFonImage, 0);

                BackgroundBackImage.type = Image.Type.Sliced;
                BackgroundBackImage.SetNativeSize();
            }
            else
            {
                BackgroundBackImage.gameObject.SetActive(false);
            }

            if (!string.IsNullOrEmpty(nameBackgroundFront))
            {
                var posY = _offsetBottomPosition - _canvasSize.y;

                BackgroundFrontImage.gameObject.SetActive(true);
                BackgroundFrontImage.color = Color.white;
                //BackgroundFrontImage.sprite = Load(nameBackgroundFront);
                BackgroundFrontImage.sprite = Resources.Load<Sprite>("Sprites/Background/" + nameBackgroundFront);
                BackgroundFrontImage.type = Image.Type.Sliced;
                BackgroundFrontImage.rectTransform.localPosition = new Vector3(0, posY + offsetFonImage, 0);
                BackgroundFrontImage.SetNativeSize();
                if(_backgroundDataModel.unlimitedSize)
                    BackgroundFrontImage.GetComponent<RectTransform>().sizeDelta = new Vector2(screenWidth, 5000);
            }
            else
            {
                BackgroundFrontImage.gameObject.SetActive(false);
            }

            var sizeFrontBackground = BackgroundFrontImage.GetComponent<RectTransform>().sizeDelta;
            
            if (sizeFrontBackground.y < _backgroundSize)
            {
                sizeFrontBackground = new Vector2(screenWidth, _backgroundSize);
            }

            BackgroundFrontImage.GetComponent<RectTransform>().sizeDelta = sizeFrontBackground;

            if (Math.Abs(_offsetBackground) > Mathf.Epsilon && _backgroundDataModel.needDynamicChangePosition)
            {
                var pos = BackgroundFrontImage.GetComponent<RectTransform>().localPosition;
                BackgroundFrontImage.GetComponent<RectTransform>().localPosition = pos + new Vector3(0, _offsetBackground);
            }

            BackgroundBackImage.GetComponent<RectTransform>().sizeDelta = new Vector2(screenWidth, _backgroundSize - 20 - _offsetBackground);

            if (_backgroundDataModel.bottom != -1)
            {
                var size = gameObject.GetComponent<RectTransform>().sizeDelta;
                BackgroundBackImage.GetComponent<RectTransform>().localPosition = Vector3.zero;
                BackgroundBackImage.GetComponent<RectTransform>().sizeDelta = size - new Vector2(0, _backgroundDataModel.bottom);

                BackgroundFrontImage.SetNativeSize();

                var x = 0;
                var y = size.y - _backgroundDataModel.bottom;

                BackgroundFrontImage.GetComponent<RectTransform>().localPosition = new Vector3(x, -y, 0);
            }
        }

        public Sprite Load(string url)
        {
            var spr = Resources.Load<Sprite>("Sprites/NewBackground/" + url);
            
            return spr;
        }
    }

}