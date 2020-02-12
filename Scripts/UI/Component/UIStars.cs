using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{



    public class UIStars : MonoBehaviour, ISwitcherDayOrNight
    {
        [SerializeField] 
        private Color _dayColor = Color.white;

        [SerializeField] 
        private Color _nightColor = Color.white;

        [SerializeField] 
        private float _lengthSeparatedSpace = 74;

        [SerializeField]
        private Color _colorStarActiveDay = Color.white;

        [SerializeField]
        private Color _colorStarActiveNight = Color.white;

        [SerializeField]
        private bool _isHorizontal;

        [SerializeField]
        private Image _imageStar;

        private int _countActiveStars;

        private List<Image> _stars = new List<Image>();

        public void Clear()
        {
            if (_stars.Count <= 0)
                return;

            _stars.RemoveAt(0);

            foreach (var image in _stars)
            {
                DestroyImmediate(image.gameObject);
            }

            _stars.Clear();
        }

        public void SetMaxStars(int count)
        {
            if (count == _stars.Count)
                return;

            _imageStar.gameObject.SetActive(true);
            
            Clear();

            var color = DaySwitcher.Instance.IsDay ? _dayColor : _nightColor;

            _stars.Add(_imageStar);
            
            for (var i = 1; i < count; i++)
            {
                
                var img = Helper.Clone<Image>(_imageStar.gameObject, _imageStar.transform.parent);
                img.SetNativeSize();
                
                _stars.Add(img);
            }

            var length = (_stars.Count-1) * _lengthSeparatedSpace;
            for (int i = 0; i < _stars.Count; i++)
            {
                var pos = length/2 - i * _lengthSeparatedSpace;
                var x = _isHorizontal ? pos : 0;
                var y = _isHorizontal ? 0 : pos;

                _stars[i].transform.localPosition = new Vector3(x,y,0);
                _stars[i].color = color;
            }
        }

        public void SetActiveStars(int count)
        {
            var color = DaySwitcher.Instance.IsDay ? _dayColor : _nightColor;
            var colorActive = DaySwitcher.Instance.IsDay ? _colorStarActiveDay : _colorStarActiveNight;
            _countActiveStars = count;

            for (int i = 0; i < _stars.Count; i++)
            {
                if (i < count)
                    _stars[i].color = colorActive;
                else
                    _stars[i].color = color;
            }
        }

        public void SwitchDayOrNight(bool isDay)
        {
            SetActiveStars(_countActiveStars);
        }
    }

}