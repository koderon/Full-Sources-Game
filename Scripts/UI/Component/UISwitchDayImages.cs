using UnityEngine;
using UnityEngine.UI;


namespace Gui
{


    public class UISwitchDayImages : MonoBehaviour, ISwitcherDayOrNight
    {
        [SerializeField]
        private Color _dayColor = Color.white;

        [SerializeField]
        private Color _nightColor = Color.white;

        [SerializeField]
        public Image[] _images;

        public void Start()
        {
            SwitchDayOrNight(DaySwitcher.Instance.IsDay);
        }

        public void SwitchDayOrNight(bool isDay)
        {
            foreach (var image in _images)
            {
                if(image == null)
                    continue;

                image.color = isDay ? _dayColor : _nightColor;
            }
        }

        public void SetColors(Color day, Color night)
        {
            _dayColor = day;
            _nightColor = night;
        }
    }

}