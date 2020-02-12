using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    
    public class GameSecondView : GameView
    {
        [SerializeField] private Text _questionText;

        [SerializeField] private UISecondViewImageButton[] _answerButtons;

        private Action<int> _onClickButton;

        public void Start()
        {
            foreach (var uiGameButtonText in _answerButtons)
            {
                uiGameButtonText.OnClickGameButton = ClickGameButton;
            }

        }

        private void ClickGameButton(GameObject go)
        {
            var index = _answerButtons.ToList().FindIndex(g => g.gameObject == go);

            _onClickButton?.Invoke(index);
        }

        public override void Setup(string question, string[] answerImage, Action<int> onActionQuestionButton)
        {
            _onClickButton = onActionQuestionButton;

            _questionText.text = question;

            for (int i = 0; i < answerImage.Length; i++)
            {
                var spr = GameStaticData.GetSprite(answerImage[i]);
                _answerButtons[i].Setup(spr);
            }
        }

        public override int GetMaxAnswers()
        {
            return 6;
        }

        public override void BlockButton(int index)
        {
            _answerButtons[index].BlockButton(true);
        }
    }

}