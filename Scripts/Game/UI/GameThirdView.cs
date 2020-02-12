using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gui;
using theGame;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{

    public class GameThirdView : GameView
    {
        [SerializeField]
        private UIAsyncImage _image;

        [SerializeField]
        private UIGameButtonText[] _gameButton;

        [SerializeField]
        private Text _name;

        private Action<int> _onClickButton;

        public void Start()
        {
            foreach (var uiGameButtonText in _gameButton)
            {
                uiGameButtonText.OnClickGameButton = ClickGameButton;
            }
        }

        private void ClickGameButton(GameObject go)
        {
            var index = _gameButton.ToList().FindIndex(g => g.gameObject == go);

            _onClickButton?.Invoke(index);
        }

        public override void Setup(string url, string[] buttonText, Action<int> onActionQuestionButton)
        {
            _onClickButton = onActionQuestionButton;

            var spr = GameStaticData.GetSprite(url);
            _image.Setup(spr);

            for (int i = 0; i < _gameButton.Length; i++)
            {
                _gameButton[i].Setup(buttonText[i]);
            }
        }

        public override void AddSetting(string data)
        {
            _name.text = data;
        }

        public override void BlockButton(int index)
        {
            _gameButton[index].BlockButton(true);
        }
    }

}