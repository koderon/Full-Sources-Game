using System;
using Game;
using theGame;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace Gui
{


    public class MainWindow : Window
    {
        [SerializeField] 
        private UIButton _buttonUsaState;

        [SerializeField]
        private UIButton _buttonUsaPresidents;

        [SerializeField] 
        private UIButton _buttonRandomGame;

        public void Start()
        {
            _buttonUsaState.OnClick = OnClickButtonUSAState;
            _buttonUsaPresidents.OnClick = OnClickButtonUSAPresident;
            _buttonRandomGame.OnClick = OnClickButtonRandomGame;
        }
        
        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("menu");

            Admob.LoadingBanner();
        }

        private void OnClickButtonUSAState(GameObject go)
        {
            WindowManager.SetGameScreen(EWindowType.UsaStateWindow);
        }

        private void OnClickButtonUSAPresident(GameObject obj)
        {
            WindowManager.SetGameScreen(EWindowType.UsaPresidentWindow);
        }

        private void OnClickButtonRandomGame(GameObject obj)
        {
            WindowManager.SetGameScreen(EWindowType.RandomGameWindow);
        }
    }

}