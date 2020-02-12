using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using theGame;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    public class GameMapHelpWindow : Window, ISwitcherDayOrNight
    {
        [SerializeField]
        private UIButton[] _gameButtons;

        [SerializeField]
        private Text _nameRegion;

        [SerializeField] 
        private MapController _mapController;

        private int _curMapRegion;

        [SerializeField]
        private int _maxMapRegion;

        [SerializeField] 
        private int _minMapRegion;

        public void Start()
        {
            foreach (var uiMenuButtonImage in _gameButtons)
            {
                uiMenuButtonImage.OnClick = ClickGameButton;
            }
        }

        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("game");

            SelectNewRegion(_minMapRegion);
        }

        private void ClickGameButton(GameObject go)
        {
            var index = _gameButtons.ToList().FindIndex(g => g.gameObject == go);

            _curMapRegion += index == 0 ? -1 : 1;

            SelectNewRegion(_curMapRegion);
        }

        private void SelectNewRegion(int region)
        {
            _curMapRegion = region;
            if (_curMapRegion > _maxMapRegion)
                _curMapRegion = _minMapRegion;
            if (_curMapRegion < _minMapRegion)
                _curMapRegion = _maxMapRegion;

            var data = GameDataModel.GetData(TypeDataModel.States);
            var regionData = data.Find(g => g.GetID() == _curMapRegion);
            if (regionData != null)
                _nameRegion.text = regionData.GetName();

            _mapController.Setup(_curMapRegion);
        }

        public void SwitchDayOrNight(bool isDay)
        {
            _mapController.SwitchColor();
        }
    }

}