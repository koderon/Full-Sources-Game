using System;
using theGame;
using UnityEngine;

namespace Game
{
    
    public interface IGameView
    {
        void Setup(string question, string[] answerQuestions, Action<int> onClickButtonQuestion);

        void BlockButton(int index);

        int GetMaxAnswers();

        void Show(bool show);

        void AddSetting(string data);
    }

}