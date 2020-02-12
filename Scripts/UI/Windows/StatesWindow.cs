using System.Collections;
using System.Collections.Generic;
using System.Linq;
using theGame;
using UnityEngine;

namespace Gui
{
    
    public class StatesWindow : Window
    {
        [SerializeField] private UIMenuButtonImage[] _gameButtons;

        [SerializeField] private UIMenuButtonImage _infoButton;

        public void Start()
        {
            foreach (var uiMenuButtonImage in _gameButtons)
            {
                uiMenuButtonImage.OnClick = ClickGameButton;
            }

            _infoButton.OnClick = ClickInfoButton;
        }

        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("game");

            var p = GameData.GetPlayerData();

            for (int i = 0; i < _gameButtons.Length; i++)
            {
                var progress = p.progress.GetGameProgress(ETypeGame.States, i + 1);
                var maxStart = i + 1;
                var activeStart = progress.IsWin ? maxStart : 0;

                _gameButtons[i].Setup(maxStart, activeStart);
            }
        }

        private void ClickGameButton(GameObject go)
        {
            var index = _gameButtons.ToList().FindIndex(g => g.gameObject == go);

            WindowManager.SetGameScreen(EWindowType.GameWindow, EWindowType.None, "2", (index + 1).ToString());
        }

        private void ClickInfoButton(GameObject obj)
        {
            WindowManager.SetGameScreen(EWindowType.GameAllHelpWindow);
        }
    }

}