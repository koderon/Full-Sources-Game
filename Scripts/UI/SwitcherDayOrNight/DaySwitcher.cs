using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gui
{


    public class DaySwitcher
    {
        public static DaySwitcher Instance { get; private set; }

        public bool IsDay = true;

        public DaySwitcher()
        {
            Instance = this;
        }
    }
}
