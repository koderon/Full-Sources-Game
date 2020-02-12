using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Game
{



    public class MapController : MonoBehaviour
    {
        [SerializeField]
        private GameMap _mainMap;

        [SerializeField]
        private GameMap _scaleMap;

        [SerializeField]
        private MapSelect _mapSelect;

        [SerializeField] 
        private MapSettingScriptableObject _mapSetting;

        private bool _isInit = false;
        
        private void Start()
        {
        }

        
        public void Init()
        {
            if (_isInit)
                return;

            _scaleMap.Init(_mapSetting);
            _mainMap.Init(_mapSetting);

            _isInit = true;
        }

        public void Setup(int numberRegion)
        {
            Init();

            SwitchColor();

            SelectRegion(numberRegion);
        }

        private void SelectRegion(int regionId)
        {
            var r = _mapSetting.GetRegion(regionId);

            if (r.scale <= 1)
            {
                _scaleMap.Show(false);
                _mainMap.SelectRegion(r.Id);

                _mapSelect.Show(false);
                return;
            }

            _scaleMap.SelectRegion(r.Id);
            _mainMap.SelectRegion(r.Id);

            _scaleMap.MoveMap(r.Position, r.scale);
            _scaleMap.Show(true);

            var size = 200 - 20 * r.scale;
            var pos = r.Position * -1;

            _mapSelect.Show(true);
            _mapSelect.SetPosition(pos, size);
        }

        public void SwitchColor()
        {
            _scaleMap.SwitchColor();
            _mainMap.SwitchColor();
        }
    }

}