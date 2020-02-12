using System;
using theGame;
using UnityEngine;

namespace Game
{
    public abstract class GameView : MonoBehaviour, IGameView
    {
        public virtual void Setup(string question, string[] answerQuestions, Action<int> onClickButtonQuestion)
        {
        }

        

        public virtual void BlockButton(int index)
        {

        }

        public virtual int GetMaxAnswers()
        {
            return 4;
        }

        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public virtual void AddSetting(string data)
        {
        }
    }

}