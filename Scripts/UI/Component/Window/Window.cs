using System.Collections;
using System.Collections.Generic;
using newGame;
using UnityEngine;

namespace Gui
{
    
    public abstract class Window : MonoBehaviour, IWindow
    {
        [SerializeField]
        protected EWindowType _windowType = EWindowType.None;

        private EWindowType _prevWindowType = EWindowType.None;

        protected IWindowManager WindowManager;

        public virtual void Setup(IWindowManager windowManager, params string[] args)
        {
            WindowManager = windowManager;
        }

        public EWindowType GetWindowType() => _windowType;

        public void SetPrevWindow(EWindowType windowType) => _prevWindowType = windowType;
        public virtual void SetBackground(string background)
        {
            BackgroundConstruct.SetBackground(background);
        }
        
        public virtual EWindowType GetPrevWindow() => _prevWindowType;


        public virtual void Show(bool active = true)
        {
            gameObject.SetActive(active);
        }

        public bool IsShow()
        {
            return gameObject.activeSelf;
        }
    }

}