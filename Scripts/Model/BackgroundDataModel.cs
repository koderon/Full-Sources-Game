using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theGame
{

    [Serializable]
    public class BackgroundsDataModel
    {
        public List<BackgroundDataModel> backgrounds = new List<BackgroundDataModel>(); 
    }

    [Serializable]
    public class BackgroundDataModel
    {
        public string id;

        public string imageBackgroundDay;
        public string imageBackgroundNight;

        public string imageDay;
        public string imageNight;

        public int offsetDay;
        public int offsetNight;

        public int bottom = -1;

        public bool sliced = false;

        public bool unlimitedSize = false;
        public bool needDynamicChangePosition = true;
    }
}