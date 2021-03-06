﻿using System;
using System.Collections.Generic;
using theGame;

namespace Game
{
    
    public class GameModeP1 : GameMode
    {
        public GameModeP1(IGameView gameView) : base(gameView)
        {
        }

        public override void Setup(IGameDataParticleModel question, IGameDataParticleModel[] answers, Action<int> onClickButtonAnswer)
        {
            base.Setup(question, answers, onClickButtonAnswer);
        }

        public override List<IGameDataParticleModel> GetData()
        {
            return GameDataModel.GetData(TypeDataModel.Presidents);
        }

        protected override string GetQuestion(IGameDataParticleModel data)
        {
            return "President_normal_" + data.GetID().ToString("d2");
        }

        protected override string[] GetAnswers(IGameDataParticleModel[] data)
        {
            var names = new List<string>();
            foreach (var gameDataParticleModel in data)
            {
                names.Add(gameDataParticleModel.GetName());
            }

            return names.ToArray();
        }
    }

}