using theGame;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{

    public class UIColorText : MonoBehaviour, ISwitcherDayOrNight
    { 
        [SerializeField]
        private Color _dayColor = Color.white;

        [SerializeField]
        private Color _nightColor = Color.white;

        private Text _text;

        
        void Start()
        {
            _text = GetComponent<Text>();

            UpdateColor(DaySwitcher.Instance.IsDay);
        }

        void UpdateColor(bool isDay)
        {
            if (_text == null)
                return;

            _text.color = isDay ? _dayColor : _nightColor;
        }

        public void SwitchDayOrNight(bool isDay)
        {
            UpdateColor(isDay);
        }
    }

}