using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{

    public class GameMapView : GameView
    {
        [SerializeField] private MapController _mapController;

        [SerializeField] private UIGameButtonText[] _answerButtons;

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

        public override void Setup(string question, string[] answers, Action<int> onActionQuestionButton)
        {
            _onClickButton = onActionQuestionButton;

            _mapController.Setup(int.Parse(question));

            for (int i = 0; i < answers.Length; i++)
            {
                _answerButtons[i].Setup(answers[i]);
            }
        }

        public override int GetMaxAnswers()
        {
            return 4;
        }

        public override void BlockButton(int index)
        {
            _answerButtons[index].BlockButton(true);
        }
    }

}
