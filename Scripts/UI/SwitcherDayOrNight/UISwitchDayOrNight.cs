using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;

namespace Gui
{


    public class UISwitchDayOrNight : MonoBehaviour
    {
        private bool _dayOrNight;

        // Use this for initialization
        void Awake()
        {
            OnChangeToDayOrNight();
        }

        private void OnEnable()
        {
            OnChangeToDayOrNight();
        }

        // Update is called once per frame
        void Update()
        {
            if(DaySwitcher.Instance != null && DaySwitcher.Instance.IsDay != _dayOrNight)
                OnChangeToDayOrNight();
        }

        private void OnChangeToDayOrNight()
        {
            if (DaySwitcher.Instance == null)
                return;

            _dayOrNight = DaySwitcher.Instance.IsDay;

            var components = GetComponents<ISwitcherDayOrNight>();
            foreach (var switcherDayOrNight in components)
            {
                switcherDayOrNight.SwitchDayOrNight(_dayOrNight);

            }
        }
    }

}