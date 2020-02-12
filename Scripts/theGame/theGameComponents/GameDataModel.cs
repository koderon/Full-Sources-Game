using System;
using System.Collections.Generic;

namespace theGame
{
    
    public class GameDataModel: TheGameComponent
    {
        private readonly Dictionary<TypeDataModel, IGameDataModel> _data = new Dictionary<TypeDataModel, IGameDataModel>();

        public override void Init()
        {
            base.Init();

            _data.Add(TypeDataModel.Capitals, new USACapitalsDataModel());
            _data.Add(TypeDataModel.Presidents, new USAPresidentsDataModel());
            _data.Add(TypeDataModel.States, new USAStatesDataModel());

            foreach (var gameDataModel in _data)
            {
                gameDataModel.Value.Init();
            }
        }

        public static List<IGameDataParticleModel> GetData(TypeDataModel typeData)
        {
            var info = TheGame.GetComponent<GameDataModel>();
            if (info == null)
                return null;

            return info._data[typeData].GetData();
        }

        public static String GetNameSprite(TypeDataModel typeData)
        {
            var info = TheGame.GetComponent<GameDataModel>();
            if (info == null)
                return null;

            return info._data[typeData].GetNameSprite();
        }
    }

}