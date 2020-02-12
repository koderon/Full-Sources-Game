using System;
using System.Collections.Generic;
using theGame;

namespace Game
{
    
    public class GameModeS4 : GameMode
    {
        public GameModeS4(IGameView gameView):base(gameView)
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
            return "Flag_normal_" + data.GetID().ToString("d2");
        }

        protected override string[] GetAnswers(IGameDataParticleModel[] datas)
        {
            var states = GameDataModel.GetData(TypeDataModel.Capitals);

            var names = new List<string>();
            foreach (var gameDataParticleModel in datas)
            {
                var state = states.Find(g => g.GetID() == gameDataParticleModel.GetID());

                names.Add(state.GetName());
            }

            return names.ToArray();
        }
    }
    
}
