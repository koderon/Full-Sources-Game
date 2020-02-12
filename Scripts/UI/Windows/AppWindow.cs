using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    
    public class AppWindow : Window
    {
        [SerializeField] 
        private Sprite _defaultImage;

        [SerializeField]
        private Color _defaultColor = Color.white;

        [SerializeField]
        private Transform _buttonsAnchor;

        [SerializeField] 
        private GameObject _buttonPrefab;

        [SerializeField]
        private SettingsScriptableObject _settings;

        [SerializeField]
        private ScrollRect _scroll;

        private List<JsonAppInfoModel> _apps;

        private List<UIButtonAppIcon> _buttons = new List<UIButtonAppIcon>();

        [SerializeField]
        private UIListGridComponent _gridComponent;

        public void Start()
        {
            Init();
        }

        private void Init()
        {
            var offset = new Vector2(330, -330);
            
            _apps = new GameAppDataModel().GetApps(_settings);

            var size = _apps.Count;
            size = Helper.ToMod(size, 3);
            if (size < 12)
                size = 12;

            if (size > 12)
                _scroll.vertical = true;
            
            for (int i = 0; i < size; i++)
            {
                var spr = _defaultImage;
                var color = _defaultColor;
                var activeSwitch = true;
                if (i < _apps.Count)
                {
                    spr = _apps[i].GetSprite();// icons[i];
                    color = Color.white;
                    activeSwitch = false;
                }

                var button = CreateButton(spr, color);
                button.ActiveSwitchDayNight(activeSwitch);
                button.ActiveShadow(!activeSwitch);

                button.ActiveButton(!activeSwitch);

                _gridComponent.AddItem(button);

                //var x = (i % 3) * offset.x;
                //var y = Mathf.Ceil(i / 3) * offset.y;
                //button.SetPosition(new Vector3(x, y, 0));
                button.OnClick = ClickToAppButton;

                _buttons.Add(button);
            }

            _gridComponent.Refresh();     
        }

        private void ClickToAppButton(GameObject go)
        {
            var index = _buttons.FindIndex(g => g.gameObject == go);
            if (index == -1)
                return;

            if (_apps == null || index >= _apps.Count)
                return;

            Helper.GotoURL(_apps[index].appUrl);
        }

        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("app");
        }

        private UIButtonAppIcon CreateButton(Sprite image, Color color)
        {
            var go = Helper.Create<UIButtonAppIcon>(_buttonPrefab, _buttonsAnchor);
            go.SetImage(image);
            go.SetColor(color);

            return go;
        }
    }

}