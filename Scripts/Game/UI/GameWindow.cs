using System.Collections;
using System.Collections.Generic;
using Gui;
using theGame;
using UnityEngine;
using UnityEngine.U2D;

namespace Game
{



    public class GameWindow : Window
    {
        [SerializeField] 
        private UIGameMenu _gameMenu;

        private readonly int _maxHearts = 3;
        private readonly int _maxQuestion = 50;

        private int _typeGame;
        private int _modeGame;

        private bool _isInit = false;

        [SerializeField]
        public GameView[] _gameViews;

        private GameLogicLoop _gameLogic = new GameLogicLoop();

        private Dictionary<int, EWindowType> _typeGameToData = new Dictionary<int, EWindowType>();

        private Dictionary<ETypeGame, Dictionary<int, IGameMode>> _gameMode = new Dictionary<ETypeGame, Dictionary<int, IGameMode>>();

        public void Start()
        {
            
        }

        public void Init()
        {
            if (_isInit)
                return;

            _typeGameToData.Add(1, EWindowType.UsaPresidentWindow);
            _typeGameToData.Add(2, EWindowType.UsaStateWindow);

            _gameMode.Add(ETypeGame.Presidents, new Dictionary<int, IGameMode>());
            _gameMode.Add(ETypeGame.States, new Dictionary<int, IGameMode>());

            _gameMode[ETypeGame.Presidents].Add(1, new GameModeP1(_gameViews[0]));
            _gameMode[ETypeGame.Presidents].Add(2, new GameModeP2(_gameViews[1]));

            _gameMode[ETypeGame.States].Add(1, new GameModeS1(_gameViews[0]));
            _gameMode[ETypeGame.States].Add(2, new GameModeS2(_gameViews[1]));
            _gameMode[ETypeGame.States].Add(3, new GameModeS3(_gameViews[3]));
            _gameMode[ETypeGame.States].Add(4, new GameModeS4(_gameViews[0]));
            _gameMode[ETypeGame.States].Add(5, new GameModeS5(_gameViews[2]));

            _gameMenu.BackButton.OnClick = go =>
            {
                if (!_typeGameToData.ContainsKey(_typeGame))
                    return;

                WindowManager.SetGameScreen(_typeGameToData[_typeGame]);
            };

            _gameMenu.SettingButton.OnClick = go =>
            {
                WindowManager.SetGameScreen(EWindowType.SettingsWindow);
            };

            _isInit = true;
        }


        private void Reset()
        {
            foreach (var gameView in _gameViews)
            {
                gameView.Show(false);
            }
        }

        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("game");
            
            Init();

            if (args == null || args.Length < 2)
            {
                WindowManager.SetGameScreen(EWindowType.MainWindow);
                return;
            }
            
            _typeGame = int.Parse(args[0]);
            _modeGame = int.Parse(args[1]);
            var isContinueGame = args.Length > 2;

            if (isContinueGame)
            {
                _gameLogic.ContinueGame(_maxHearts);
                return;
            }
            
            Debug.Log("typeGame := " + (ETypeGame)_typeGame + " mode := " + _modeGame);

            GameMode(_gameMode[(ETypeGame)_typeGame][_modeGame]);
        }

        
        public void GameMode(IGameMode gameMode)
        {
            Reset();

            _gameLogic.Setup(_maxHearts, _maxQuestion, gameMode, (isCompletedGame, scores) =>
            {
                Debug.LogError("Completed game := " + isCompletedGame + ", scores := " + scores);

                var window = isCompletedGame ? EWindowType.WinGameWindow : EWindowType.LoseGameWindow;

                WindowManager.SetGameScreen(window, EWindowType.None, scores.ToString(), _typeGame.ToString(), _modeGame.ToString());

                GameData.SetProgress((ETypeGame)_typeGame, _modeGame, isCompletedGame, scores);
                
            }, data =>
            {
                _gameMenu.SetData(data);
            });

            Admob.LoadingRewardVideo();
        }
    }

}