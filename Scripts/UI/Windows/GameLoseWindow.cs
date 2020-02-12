using System.Collections;
using System.Collections.Generic;
using theGame;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    
    public class GameLoseWindow : Window, ISwitcherDayOrNight
    {
        [SerializeField] private UIButton _continueButton;

        [SerializeField] private UIButton _repeatButton;

        [SerializeField] private UIButton _homeButton;

        [SerializeField] private Text _scoreText;

        [SerializeField] private Image[] _imageBackground;

        [SerializeField] private SettingsScriptableObject _settings;

        private string _typeGame;
        private string _modeGame;

        private void Start()
        {
            _continueButton.OnClick = go =>
            {
                Admob.ShowRewardVideo();
            };

            _repeatButton.OnClick = go =>
                WindowManager.SetGameScreen(EWindowType.GameWindow, EWindowType.None, _typeGame, _modeGame);

            _homeButton.OnClick = go => WindowManager.SetGameScreen(EWindowType.MainWindow);


            Admob.AddActionForRewardVideo(() =>
            {
                if (!IsShow())
                    return;

                WindowManager.SetGameScreen(EWindowType.GameWindow, EWindowType.None, _typeGame, _modeGame, "true");
            });
        }

        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("gameWinOrLose");

            if (args == null || args.Length < 3)
            {
                WindowManager.SetGameScreen(EWindowType.MainWindow);
                return;
            }

            var scores = args[0];
            _typeGame = args[1];
            _modeGame = args[2];

            _scoreText.text = scores;

            SwitchDayOrNight(DaySwitcher.Instance.IsDay);
        }

        public void SwitchDayOrNight(bool isDay)
        {
            _imageBackground[0].gameObject.SetActive(isDay);
            _imageBackground[1].gameObject.SetActive(!isDay);
        }
    }
}