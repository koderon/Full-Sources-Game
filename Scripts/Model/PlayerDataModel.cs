using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theGame
{
    
    [Serializable]
    public class PlayerDataModel
    {
        public void Init()
        {
            progress.NewGame();
        }

        public bool isDay = true;
        public int langId = -1;
        public float soundVolume = 1.0f;
        public bool activeSound = true;

        public GameProgress progress = new GameProgress();
    }
}