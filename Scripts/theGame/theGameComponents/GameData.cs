using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gui;
using UnityEngine;

namespace theGame
{

    public class GameData : TheGameComponent
    {
        private string _fileName;

        // playerData
        private PlayerDataModel _playerDataModel;

        private Vector2 _sizeBackground = Vector2.zero;

        private bool _isNeedSave = false;
        private float _timeToNextSave = 0.0f;
        private float _constWaitTime = 2.0f;

        void Update()
        {
            UpdateTimer();
        }

        public override void Init()
        {
            _fileName =  Application.identifier + ".data";

            Load();
        }

        public static void SetProgress(ETypeGame type, int mode, bool isWin, int score)
        {
            var p = GetPlayerData();

            if (p == null)
                return;

            var progress = p.progress.GetGameProgress((ETypeGame)type, mode);
            progress.IsWin = progress.IsWin || isWin;
            progress.Score = score;

            SaveInTime();
        }

        public static PlayerDataModel GetPlayerData()
        {
            var p = TheGame.GetComponent<GameData>();
            if (p == null)
                return null;

            if (p._playerDataModel == null)
            {
                p._playerDataModel = new PlayerDataModel();
                p._playerDataModel.Init();
            }

            return p._playerDataModel;
        }

        public static void NewGame()
        {
            var p = GetPlayerData();

            if (p == null)
                return;

            p.Init();

            SaveInTime();
        }

        public static void SetSoundVolume(float volume)
        {
            var p = GetPlayerData();

            if (p == null)
                return;

            p.soundVolume = volume;
            SaveInTime();
        }

        public static float GetSoundVolume()
        {
            var p = GetPlayerData();

            if (p == null)
                return 0;

            return p.soundVolume;
        }

        #region Save / Load
        private IEnumerator SaveGameData()
        {
            while (true)
            {
                yield return new WaitForSeconds(60);

                Save();
            }
        }

        public void UpdateTimer()
        {
            if (_isNeedSave)
            {
                _timeToNextSave -= Time.deltaTime;
                if (_timeToNextSave <= 0)
                {
                    SaveToFile();
                    _isNeedSave = false;
                }
            }
        }

        void Load()
        {
            var serializedInput = FileUtils.LoadTextFromFile(_fileName);

            if (string.IsNullOrEmpty(serializedInput))
            {
                _playerDataModel = new PlayerDataModel();
                _playerDataModel.Init();
                return;
            }

            _playerDataModel = JsonUtility.FromJson<PlayerDataModel>(serializedInput);
            if ( _playerDataModel == null )
            {
                _playerDataModel = new PlayerDataModel();
                _playerDataModel.Init();
            }

            DaySwitcher.Instance.IsDay = _playerDataModel.isDay;

            if(_playerDataModel.langId != -1)
                Lang.Instance.CurLang = ((SystemLanguage)_playerDataModel.langId);
        }

        public static void SaveInTime()
        {
            if (TheGame.GetComponent<GameData>() != null)
                TheGame.GetComponent<GameData>().ResetTimeToSave();
        }

        public static void Save()
        {
            if(TheGame.GetComponent<GameData>() != null)
                TheGame.GetComponent<GameData>().SaveToFile();
        }

        private void ResetTimeToSave()
        {
            _isNeedSave = true;
            _timeToNextSave = _constWaitTime;
        }

        private void SaveToFile()
        {
            _playerDataModel = GetPlayerData();
            _playerDataModel.isDay = DaySwitcher.Instance.IsDay;
            _playerDataModel.langId = (int)Lang.Instance.CurLang;
            
            var serializedOutput = JsonUtility.ToJson(_playerDataModel);
            
            FileUtils.SaveTextToFile(_fileName, serializedOutput);

            Debug.Log("Save Game");
        }

        #endregion
    }

}