using System.Collections.Generic;
using Gui;
using UnityEngine;

namespace Game
{


    public class GameMap : MonoBehaviour
    {
        [SerializeField] private RectTransform _anchorMoveMap;

        [SerializeField] private Transform _anchorForMapRegions;

        [SerializeField] private MapRegionComponent _mapRegionComponentPrefab;

        [SerializeField] private MapRegionComponent _selectRegionInMap;

        private MapSettingScriptableObject _mapSetting;

        private readonly List<MapRegionComponent> _regions = new List<MapRegionComponent>();

        public void Init(MapSettingScriptableObject setting)
        {
            _mapSetting = setting;

            foreach (var region in _mapSetting.Regions)
            {
                var r = CreateRegion(region);

                _regions.Add(r);
            }

            _selectRegionInMap.SetSelect(true);
        }

        public void MoveMap(Vector3 move, float scale)
        {
            _anchorMoveMap.transform.localPosition = move * scale;
            _anchorMoveMap.transform.localScale = Vector3.one * scale;
        }

        public void SelectRegion(int regionId)
        {
            var r = _regions.Find(g => g.GetMapRegionModel().Id == regionId);
            if (r == null)
                return;

            _selectRegionInMap.Setup(r.GetMapRegionModel(), _mapSetting);
        }

        private MapRegionComponent CreateRegion(MapRegionModel region)
        {
            var r = Helper.Create<MapRegionComponent>(_mapRegionComponentPrefab.gameObject, _anchorForMapRegions);
            r.Setup(region, _mapSetting);

            return r;
        }

        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public void SwitchColor()
        {
            var isDay = DaySwitcher.Instance.IsDay;
            foreach (var mapRegionComponent in _regions)
            {
                mapRegionComponent.SwitchDayOrNight(isDay);
            }

            _selectRegionInMap.SwitchDayOrNight(isDay);
        }
    }

}