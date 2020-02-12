using System;
using System.Collections;
using System.Collections.Generic;
using theGame;
using UnityEngine;
using UnityEngine.U2D;

namespace Game
{

    public class GameLogicLoop
    {
        private Dictionary<TypeDataModel, List<IGameDataParticleModel>> _data = new Dictionary<TypeDataModel, List<IGameDataParticleModel>>();
        private Dictionary<TypeDataModel, string> _dataUrl = new Dictionary<TypeDataModel, string>();

        private GameLogicController _gameLogic = new GameLogicController();

        private InGameProgress _progress = new InGameProgress();

        private Action<bool, int> _onActionCompleteGame;
        private Action<InGameProgress> _onActionUpdateInfo;

        private IGameMode _gameMode;
        private int _maxAnswers = 4;

        public void InitDataUrl(TypeDataModel type, string url)
        {
            _dataUrl.Add(type, url);
        }

        public void Init(TypeDataModel type, IGameDataModel gameData)
        {
            gameData.Init();

            _data.Add(type, gameData.GetData());
        }

        public void ContinueGame(int addHearts)
        {
            _progress.CurHearts += addHearts;
            _onActionUpdateInfo?.Invoke(_progress);
        }

        public void Setup(int maxHearts, int maxQuestion, IGameMode gameMode, Action<bool, int> actionCompleteGame = null, Action<InGameProgress> actionUpdateInfo = null)
        {
            _gameMode = gameMode;

            _progress.Reset(maxHearts, maxQuestion);

            _gameLogic.Setup(gameMode.GetData());

            _onActionCompleteGame = actionCompleteGame;
            _onActionUpdateInfo = actionUpdateInfo;

            _maxAnswers = _gameMode.GetMaxAnswers();

            Next();
        }

        public void Next()
        {
            var question = _gameLogic.GetRandomQuestion();
            var answers = _gameLogic.GetAnswers(question.GetID(), _maxAnswers);
            var answerIndex = answers.FindIndex(g => g.GetID() == question.GetID());

            Debug.LogError("answer := " + (answerIndex + 1));

            _gameMode.Setup(question, answers.ToArray(), indexButton =>
            {
                if (indexButton == answerIndex)
                {
                    CheckGameLoop(true);
                }
                else
                {
                    _gameMode.BlockAnswer(indexButton);

                    CheckGameLoop(false);
                }
            });

            _progress.CurQuestion++;

            _onActionUpdateInfo?.Invoke(_progress);
        }

        private void CheckGameLoop(bool answerAccept = false)
        {
            if (answerAccept)
            {
                _progress.IncrementScores();

                if (_progress.MaxQuestion > _progress.CurQuestion)
                {
                    Next();

                    PlaySound(ESound.AcceptClick);
                }
                else
                {
                    PlaySound(ESound.WinSound);

                    _onActionUpdateInfo?.Invoke(_progress);
                    _onActionCompleteGame?.Invoke(true, _progress.Scores);
                }

                return;
            }

            _progress.ResetIncrementScores();
            _progress.CurHearts--;
            
            _onActionUpdateInfo?.Invoke(_progress);

            if (_progress.CurHearts <= 0)
            {
                PlaySound(ESound.LoseSound);

                _onActionCompleteGame?.Invoke(false, _progress.Scores);
                return;
            }

            PlaySound(ESound.WrongClick);
        }

        private void PlaySound(ESound soundId)
        {
            SoundController.PlaySound(soundId);
        }
    }

}