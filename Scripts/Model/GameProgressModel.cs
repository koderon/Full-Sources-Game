using System;
using System.Collections.Generic;
using newGame;

namespace theGame
{

    [Serializable]
    public class GameProgress
    {
        public List<GameProgressModel> GameRegionProgresses = new List<GameProgressModel>();

        public void NewGame()
        {
            GameRegionProgresses.Clear();
            
            GameRegionProgresses.Add(new GameProgressModel(ETypeGame.Presidents, 1));
            GameRegionProgresses.Add(new GameProgressModel(ETypeGame.Presidents, 2));

            GameRegionProgresses.Add(new GameProgressModel(ETypeGame.States, 1));
            GameRegionProgresses.Add(new GameProgressModel(ETypeGame.States, 2));
            GameRegionProgresses.Add(new GameProgressModel(ETypeGame.States, 3));
            GameRegionProgresses.Add(new GameProgressModel(ETypeGame.States, 4));
            GameRegionProgresses.Add(new GameProgressModel(ETypeGame.States, 5));
        }

        public GameProgressModel GetGameProgress(ETypeGame type, int mode)
        {
            var p = GameRegionProgresses.Find(g => g.TypeGame == type && g.ModeGame == mode);
            return p;
        }

        public void SetGameProgress(ETypeGame type, int mode, bool isWin = false, int score = 0)
        {
            var p = GetGameProgress(type, mode);
            p.IsWin = isWin;
            p.Score = score;
        }
    }


    [Serializable]
    public class GameProgressModel
    {
        public GameProgressModel() { }

        public GameProgressModel(ETypeGame typeGame, int mode)
        {
            Reset(typeGame, mode);
        }

        public void Reset(ETypeGame typeGame, int mode)
        {
            TypeGame = typeGame;
            ModeGame = mode;
            Score = 0;
            IsWin = false;
        }

        public ETypeGame TypeGame;

        public int ModeGame;

        public bool IsWin = false;

        public int Score;
    }

}