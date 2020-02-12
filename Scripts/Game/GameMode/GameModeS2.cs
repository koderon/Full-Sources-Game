using System;
using System.Collections.Generic;
using theGame;

namespace Game
{
    
    public class GameModeS2 : GameMode
    {

        public GameModeS2(IGameView gameView):base(gameView)
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
            return data.GetName();
        }

        protected override string[] GetAnswers(IGameDataParticleModel[] data)
        {
            var urls = new List<string>();
            foreach (var gameDataParticleModel in data)
            {
                var url = "Flag_small_" + gameDataParticleModel.GetID().ToString("d2");

                urls.Add(url);
            }

            return urls.ToArray();
        }
    }
    
}
