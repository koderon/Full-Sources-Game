using System.Collections.Generic;
using Gui;
using theGame;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    public class SettingsWindow : Window
    {
        public Slider SoundVolume;

        public UIButton ResetGameButton;

        public Transform ItemsAnchor;
        public GameObject LangFlagButtonPrefab;

        [SerializeField] 
        private Sprite[] _sprites;

        private UILangFlagButton _selectFlag;

        private List<UILangFlagButton> _flagButtons = new List<UILangFlagButton>();
        private List<SystemLanguage> _flags = new List<SystemLanguage>();

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _flags.Add(SystemLanguage.English);
            _flags.Add(SystemLanguage.Russian);
            _flags.Add(SystemLanguage.Polish);
            _flags.Add(SystemLanguage.German);
            _flags.Add(SystemLanguage.French);
            _flags.Add(SystemLanguage.Portuguese);
            _flags.Add(SystemLanguage.Spanish);
            _flags.Add(SystemLanguage.Korean);
            _flags.Add(SystemLanguage.Japanese);
            _flags.Add(SystemLanguage.Chinese);
            _flags.Add(SystemLanguage.Italian);
            _flags.Add(SystemLanguage.Norwegian);
            _flags.Add(SystemLanguage.Danish);

            var curLang = Lang.Instance.CurLang;
            var maxFlagsInRow = 5;
            var offsetX = 203.0f;
            var halfWidth = (offsetX * (maxFlagsInRow - 1)) / 2;
            var offsetY = -180;
            for (int i = 0; i < _flags.Count; i++)
            {
                var x = -halfWidth + (i % maxFlagsInRow) * offsetX;
                var y = Mathf.Floor(i / maxFlagsInRow) * offsetY;

                if (i >= 10)
                    x += offsetX;

                var flag = CreateFlag(_sprites[i], _flags[i]);
                flag.SetPosition(new Vector3(x,y,0));

                
                flag.OnClick = ClickButtonChangeLang;
                flag.ActiveButton( _flags[i] == curLang );

                if (_flags[i] == curLang)
                    _selectFlag = flag;

                _flagButtons.Add(flag);
            }

            SoundVolume.value = GameData.GetSoundVolume();
            SoundVolume.onValueChanged.AddListener(delegate
            {
                GameData.SetSoundVolume(SoundVolume.value);
            });

            ResetGameButton.OnClick = go =>
            {
                GameData.NewGame();

                Debug.Log("new Game");
            };
        }

        private void ClickButtonChangeLang(GameObject go)
        {
            var flagButton = _flagButtons.Find(g => g.gameObject == go);
            if (flagButton == null)
                return;

            Lang.Instance.CurLang = flagButton.GetSystemLanguage();

            _selectFlag.ActiveButton(false);
            _selectFlag = flagButton;
            _selectFlag.ActiveButton(true);

            GameData.SaveInTime();
        }

        private UILangFlagButton CreateFlag(Sprite flagImage, SystemLanguage lang)
        {
            var go = Helper.Create<UILangFlagButton>(LangFlagButtonPrefab, ItemsAnchor);
            go.Setup(flagImage, lang);

            return go;
        }

        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("setting");
        }
    }

}