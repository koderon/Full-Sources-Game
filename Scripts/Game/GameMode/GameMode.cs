using System;
using System.Collections;
using System.Collections.Generic;
using theGame;
using UnityEngine;

namespace Game
{
    
    public abstract class GameMode : IGameMode
    {
        protected IGameView _gameView;

        protected GameMode(IGameView gameView)
        {
            _gameView = gameView;
        }

        public virtual void Setup(IGameDataParticleModel question, IGameDataParticleModel[] answers, Action<int> onClickButtonAnswer)
        {
            _gameView.Show(true);

            var q = GetQuestion(question);
            var a = GetAnswers(answers);

            _gameView.Setup(q, a, onClickButtonAnswer);
        }

        public int GetMaxAnswers() => _gameView.GetMaxAnswers();

        public void BlockAnswer(int index) => _gameView.BlockButton(index);

        public virtual List<IGameDataParticleModel> GetData()
        {
            return null;
        }

        protected virtual string GetQuestion(IGameDataParticleModel data)
        {
            return null;
        }

        protected virtual string[] GetAnswers(IGameDataParticleModel[] data)
        {
            return null;
        }
    }

}