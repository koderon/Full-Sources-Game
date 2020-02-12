using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{



    public class UISliderControlColor : MonoBehaviour, ISwitcherDayOrNight
    {
        public Image Handle;
        public Image Background;
        public Image FullBackground;

        public Color ColorBackgroundDay;
        public Color ColorHandleDay;

        public Color ColorBackgroundNight;
        public Color ColorHandleNight;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SwitchDayOrNight(bool isDay)
        {
            var bColor = isDay ? ColorBackgroundDay : ColorBackgroundNight;
            var hColor = isDay ? ColorHandleDay : ColorHandleNight;

            Background.color = FullBackground.color = bColor;
            Handle.color = hColor;
        }
    }

}