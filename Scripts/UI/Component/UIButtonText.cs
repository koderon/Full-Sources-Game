using Gui;
using theGame;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonText : UIButton
{
    [SerializeField] 
    private string _langKey;

    void Start()
    {
        Debug.Log("UiButtonText Start");

        base.Start();

        var text = GetComponentInChildren<Text>();

        if (text != null)
            text.text = Lang.Get(_langKey);
    }
}
