using System.Collections;
using System.Collections.Generic;
using System.Linq;
using theGame;
using UnityEngine;

namespace Gui
{


    public class AllHelpWindow : Window
    {
        [SerializeField]
        private UIMenuButtonImage[] _gameButtons;

        private Dictionary<int, TypeDataModel> _map = new Dictionary<int, TypeDataModel>();

        public void Start()
        {
            foreach (var uiMenuButtonImage in _gameButtons)
            {
                uiMenuButtonImage.OnClick = ClickGameButton;
            }

            _map.Add(0, TypeDataModel.States);
            _map.Add(1, TypeDataModel.Capitals);
            _map.Add(2, TypeDataModel.Map);
            _map.Add(3, TypeDataModel.Presidents);
        }

        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("menu1");
        }

        private void ClickGameButton(GameObject go)
        {
            var index = _gameButtons.ToList().FindIndex(g => g.gameObject == go);
            var type = _map[index];
            

            if (type != TypeDataModel.Map)
            {
                WindowManager.SetGameScreen(EWindowType.GameHelpWindow, EWindowType.None,  ((int)type).ToString());
                return;
            }

            WindowManager.SetGameScreen(EWindowType.GameMapHelpWindow);
        }

        
        public override EWindowType GetPrevWindow()
        {
            return EWindowType.MainWindow;
        }
    }

}