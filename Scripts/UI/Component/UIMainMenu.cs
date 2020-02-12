using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using theGame;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    
    public class UIMainMenu : Window
    {
        [SerializeField]
        private Sprite[] _sprites;

        [SerializeField]
        private Image[] _buttonImages;

        [SerializeField] private UIButton _backButton;
        [SerializeField] private UIButton _soundButton;
        [SerializeField] private UIButton _switchButton;
        [SerializeField] private UIButton _appButton;
        [SerializeField] private UIButton _settingsButton;

        private bool _activeSound = false;

        private IWindowManager _manager;

        [SerializeField]
        private SettingsScriptableObject _settings;

        void Start()
        {
            _backButton.OnClick = ClickBackButton;
            _soundButton.OnClick = ClickSoundButton;
            _switchButton.OnClick = ClickSwitchButton;
            _appButton.OnClick = ClickAppButton;
            _settingsButton.OnClick = ClickSettingsButton;
        }

        public void Setup(IWindowManager manager)
        {
            _manager = manager;

            CheckButtons();
        }

        private void ClickSettingsButton(GameObject obj)
        {
            var windowType = _manager?.GetWindowType();

            if (windowType == EWindowType.SettingsWindow)
            {
                ClickLikeButton();
                return;
            }

            _manager?.SetGameScreen(EWindowType.SettingsWindow);
        }

        private void ClickAppButton(GameObject obj)
        {
            _manager?.SetGameScreen(EWindowType.AppWindow);
        }

        private void ClickSwitchButton(GameObject obj)
        {
            DaySwitcher.Instance.IsDay = !DaySwitcher.Instance.IsDay;
            theGame.GameData.SaveInTime();

            CheckButtons();
        }

        private void ClickSoundButton(GameObject obj)
        {
            ActiveSound(!_activeSound);
        }

        public void ActiveSound(bool active)
        {
            var pd = GameData.GetPlayerData();
            _activeSound = active;
            pd.activeSound = active;

            CheckButtons();
        }

        private void ClickLikeButton()
        {
            Helper.GotoURL(_settings.UrlToLikeGame);
        }

        private void ClickBackButton(GameObject obj)
        {
            var windowType = _manager?.GetWindowType();
            if (windowType != EWindowType.MainWindow)
            {
                if (windowType == EWindowType.SettingsWindow || windowType == EWindowType.AppWindow)
                    _manager.SetGameScreen(EWindowType.MainWindow);
                else
                    _manager.GotoPrevWindow();
            }
            else
            {
                ClickLikeButton();
            }
        }

        public void CheckButtons()
        {
            var windowType = EWindowType.None;
            if (_manager != null)
                windowType = _manager.GetWindowType();

            var pd = GameData.GetPlayerData();

            Show(true);

            switch (windowType)
            {
                case EWindowType.AppWindow:
                case EWindowType.WinGameWindow:
                case EWindowType.LoseGameWindow:
                    _buttonImages[0].sprite = _sprites[6]; // картинка домика на левой кнопке
                    break;
                case EWindowType.SettingsWindow:
                    _buttonImages[0].sprite = _sprites[6]; // картинка домика на левой кнопке
                    _buttonImages[4].sprite = _sprites[0]; // картинка лайка на правой кнопке
                    break;
                case EWindowType.MainWindow:
                    _buttonImages[0].sprite = _sprites[0]; // картинка лайка на левой кнопке
                    _buttonImages[4].sprite = _sprites[7]; // картинка шестеренки на правой кнопке
                    break;
                case EWindowType.GameWindow:
                    Show(false);
                    break;
                default:
                    _buttonImages[0].sprite = _sprites[1]; // на всех остальных экранах стрелка назад
                    break;
            }

            _activeSound = pd.activeSound;
            _buttonImages[1].sprite = _sprites[pd.activeSound ? 3 : 2];

            _buttonImages[2].sprite = _sprites[DaySwitcher.Instance.IsDay ? 5 : 4];
            
            foreach (var buttonImage in _buttonImages)
            {
                buttonImage.SetNativeSize();
            }
        }
    }

}