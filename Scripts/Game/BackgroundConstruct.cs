using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;

namespace newGame
{



    public class BackgroundConstruct : MonoBehaviour
    {
        public UINewBackground NewBackground;

        public static BackgroundConstruct Instance;

        void Start()
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }

        public static void SetBackground(string nameBackground)
        {
            Instance.NewBackground.SetIdBackground(nameBackground, "", "");
        }
    }

}