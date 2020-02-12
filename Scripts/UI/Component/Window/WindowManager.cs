using System;
using System.Linq;
using theGame;
using UnityEngine;

namespace Gui
{


    public class WindowManager : MonoBehaviour, IWindowManager
    {
        [SerializeField]  
        private Window[] _windows;

        private IWindow _window;

        public Action OnActionChangeWindow;

        public void Setup()
        {
            foreach (var window in _windows)
            {
                window.Show(false);
            }
        }

        public IWindow SetGameScreen(EWindowType windowType, EWindowType prevWindowType = EWindowType.None, params string[] args)
        {
            var oldWindow = _window;
            _window = GetWindow(windowType);

            oldWindow?.Show(false);

            if (oldWindow != null)
                prevWindowType = prevWindowType == EWindowType.None ? oldWindow.GetWindowType() : prevWindowType;

            _window.SetPrevWindow(prevWindowType);

            _window.Setup(this, args);

            _window.Show(true);

            OnActionChangeWindow?.Invoke();

            return _window;
        }

        public void GotoPrevWindow()
        {
            if (_window.GetPrevWindow() == EWindowType.None)
                return;
            
            SetGameScreen(_window.GetPrevWindow());
        }

        public EWindowType GetWindowType()
        {
            if (_window == null)
                return EWindowType.None;

            return _window.GetWindowType();
        }

        public IWindow GetWindow(EWindowType windowType)
        {
            var window = _windows.ToList().Find(g => g.GetWindowType() == windowType);
            if (window == null)
                return GetWindow(EWindowType.MainWindow);

            return window;
        }
    }

}