using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{



    public class UIGameAnswers : MonoBehaviour
    {
        [SerializeField]
        private Text _answersText;
        
        public void SetText(int maxAnswers, int curAnswers)
        {
            _answersText.text = curAnswers + "/" + maxAnswers;
        }
    }

}