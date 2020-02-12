using System.Collections;
using System.Collections.Generic;
using theGame;
using UnityEngine;


namespace Game
{

    public class InGameProgress
    {
        public int MaxHearts;
        public int CurHearts;

        public int MaxQuestion;
        public int CurQuestion;

        public int Scores;

        private int _incrementScores;

        public void Reset(int maxHearts, int maxQuestion)
        {
            MaxHearts = maxHearts;
            CurHearts = maxHearts;

            MaxQuestion = maxQuestion;
            CurQuestion = 0;

            Scores = 0;

            _incrementScores = 1;
        }

        public void ResetIncrementScores()
        {
            _incrementScores = 1;
        }


        public void IncrementScores()
        {
            Scores += _incrementScores;
            _incrementScores++;
        }
    }

    public class GameLogicController
    {
        private List<IGameDataParticleModel> _gameData = new List<IGameDataParticleModel>();

        private List<int> _notUsedId = new List<int>();
        private int _countUsedId;

        public void Setup(List<IGameDataParticleModel> gameData)
        {
            _gameData.Clear();
            _gameData.AddRange(gameData);

            Init();
        }

        public void Setup(params IGameDataModel[] models)
        {
            _gameData.Clear();
            for (int i = 0; i < models.Length; i++)
            {
                _gameData.AddRange(models[i].GetData());
            }

            Init();
        }

        private void Init()
        {
            _countUsedId = 0;
            _notUsedId.Clear();

            for (int i = 0; i < _gameData.Count; i++)
            {
                _notUsedId.Add(i);
            }
        }

        public IGameDataParticleModel GetRandomQuestion()
        {
            if (_gameData.Count <= _countUsedId)
            {
                Init();
            }

            var randIndex = Random.Range(0, _notUsedId.Count);

            _countUsedId++;
            var index = _notUsedId[randIndex];
            _notUsedId.RemoveAt(randIndex);

            return _gameData[index];
        }

        public List<IGameDataParticleModel> GetAnswers(int idQuestion, int countAnswers)
        {
            var countRand = 0;
            
            
            var answers = new List<IGameDataParticleModel>();
            while (true)
            {
                countRand++;
                if (countRand >= 100)
                    return null;

                var randIndex = Random.Range(0, _gameData.Count);

                var model = _gameData[randIndex];
                if(model.GetID() == idQuestion)
                    continue;

                var index = answers.FindIndex(g => g.GetID() == model.GetID());
                if(index != -1)
                    continue;

                answers.Add(model);
                if (answers.Count >= (countAnswers - 1))
                    break;
            }

            var randIndexQuestion = Random.Range(0, countAnswers);
            var answerModel = _gameData.Find(g => g.GetID() == idQuestion);

            answers.Insert(randIndexQuestion, answerModel);

            return answers;
        }


    }

}