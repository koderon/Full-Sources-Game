using System;
using System.Collections.Generic;
using theGame;

namespace Game
{

    public class GameModeP2 : GameMode
    {
        public GameModeP2(IGameView gameView):base(gameView)
        {
        }
        
        public override List<IGameDataParticleModel> GetData()
        {
            return GameDataModel.GetData(TypeDataModel.Presidents);
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
                var url = "President_small_" + gameDataParticleModel.GetID().ToString("d2");

                urls.Add(url);
            }

            return urls.ToArray();
        }
    }

}