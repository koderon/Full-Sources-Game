using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private EWindowType _firstScreen = EWindowType.MainWindow;

    [SerializeField]
    private WindowManager _windowManager;

    [SerializeField]
    private UIMainMenu _mainMenu;

    void Start()
    {
        _windowManager.OnActionChangeWindow = () =>
        {
            _mainMenu.CheckButtons();
        };

        _mainMenu.Setup(_windowManager);

        _windowManager.Setup();
        _windowManager.SetGameScreen(_firstScreen);
    }

}
