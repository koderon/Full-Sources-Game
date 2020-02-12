
using System;
using System.Collections.Generic;
using theGame;

namespace Game
{

    public interface IGameMode
    {
        void Setup(IGameDataParticleModel question, IGameDataParticleModel[] answers, Action<int> onClickButtonAnswer);

        int GetMaxAnswers();

        void BlockAnswer(int index);

        List<theGame.IGameDataParticleModel> GetData();

        //string GetQuestion(theGame.IGameDataParticleModel data);

        //string[] GetAnswers(theGame.IGameDataParticleModel[] data);
    }

}