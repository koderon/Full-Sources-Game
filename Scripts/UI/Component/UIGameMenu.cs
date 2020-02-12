using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Gui
{

    public class UIGameMenu : MonoBehaviour
    {
        public UIButton BackButton;

        public UIButton SettingButton;

        public UIGameAnswers GameAnswers;

        public UIGameHearts GameHearts;

        public UIGameReward GameReward;

        public void SetData(InGameProgress inGameData)
        {
            GameAnswers.SetText(inGameData.MaxQuestion, inGameData.CurQuestion);

            GameHearts.SetText(inGameData.MaxHearts, inGameData.CurHearts);

            GameReward.SetText(inGameData.Scores);
        }


    }

}