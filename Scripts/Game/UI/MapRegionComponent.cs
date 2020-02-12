using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{



    public class MapRegionComponent : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        private MapRegionModel _mapRegion;

        private MapSettingScriptableObject _mapSetting;

        private bool _isSelect = false;
        
        public void Setup(MapRegionModel mapRegion, MapSettingScriptableObject setting)
        {
            _mapRegion = mapRegion;
            _mapSetting = setting;

            SetImage();

            SwitchDayOrNight(DaySwitcher.Instance.IsDay);
        }

        public MapRegionModel GetMapRegionModel() => _mapRegion;
        
        private void SetImage()
        {
            _image.sprite = GameStaticData.GetMap(_mapRegion.Id.ToString("D2"));
            _image.SetNativeSize();
        }

        public void SetSelect(bool select)
        {
            _isSelect = select;
            SwitchDayOrNight(DaySwitcher.Instance.IsDay);
        }

        public void SwitchDayOrNight(bool isDay)
        {
            if (_mapRegion == null)
                return;

            var index = _mapRegion.IndexInColorPalette;
            if (_isSelect)
                index = _mapSetting.IndexInPaletteSelectColor;

            _image.color = _mapSetting.GetColor(index, isDay);
        }
    }

}