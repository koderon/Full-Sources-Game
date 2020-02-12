using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    public class UIButtonPushEffect : MonoBehaviour
    {
        [SerializeField]
        private Image _pushImage;

        void Start()
        {
            PushButton(false);
        }

        public void PushButton(bool push = true, bool isDay = true)
        {
            if (_pushImage == null)
                return;

            _pushImage.gameObject.SetActive(push);
            _pushImage.color = Helper.GetColorShadow(isDay);
        }
    }

}