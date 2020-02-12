using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    
    public class UIGameHearts : MonoBehaviour
    {
        [SerializeField]
        private Image _heartBackground;

        [SerializeField]
        private Text _heartsCountText;

        public void SetText(int maxHearts, int curHearts)
        {
            var text = curHearts <= 0 ? "" : curHearts.ToString();

            _heartsCountText.text = text;
            _heartBackground.fillAmount = (float)curHearts / (float)maxHearts;
        }
    }
}