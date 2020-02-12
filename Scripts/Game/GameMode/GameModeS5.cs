using System;
using System.Collections.Generic;
using theGame;

namespace Game
{

    public class GameModeS5 : GameMode
    {
        public GameModeS5(IGameView gameView) : base(gameView)
        {
        }

        public override void Setup(IGameDataParticleModel question, IGameDataParticleModel[] answers, Action<int> onClickButtonAnswer)
        {
            base.Setup(question, answers, onClickButtonAnswer);
        }

        public override List<IGameDataParticleModel> GetData()
        {
            return GameDataModel.GetData(TypeDataModel.States);
        }

        protected override string GetQuestion(IGameDataParticleModel data)
        {
            return data.GetID().ToString();
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