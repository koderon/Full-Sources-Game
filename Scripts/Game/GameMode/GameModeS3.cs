using System;
using System.Collections.Generic;
using theGame;

namespace Game
{
    
    public class GameModeS3 : GameMode
    {
        public GameModeS3(IGameView gameView):base(gameView)
        {
        }

        public override void Setup(IGameDataParticleModel question, IGameDataParticleModel[] answers, Action<int> onClickButtonAnswer)
        {
            base.Setup(question, answers, onClickButtonAnswer);

            _gameView.AddSetting(question.GetName());
        }

        public override List<IGameDataParticleModel> GetData()
        {
            return GameDataModel.GetData(TypeDataModel.Capitals);
        }

        protected override string GetQuestion(IGameDataParticleModel data)
        {
            return "Capital_normal_" + data.GetID().ToString("d2");
        }

        protected override string[] GetAnswers(IGameDataParticleModel[] datas)
        {
            var states = GameDataModel.GetData(TypeDataModel.States);

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
