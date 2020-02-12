
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    public class UIButtonAppIcon : UIButton, IUIListGridElement
    {
        [SerializeField] 
        private Image _image;

        [SerializeField]
        private UISwitchDayImages _switchDay;

        [SerializeField]
        private Shadow _shadow;

        public void SetImage(Sprite buttonImage)
        {
            if(_image != null)
                _image.sprite = buttonImage;

            _image.SetNativeSize();
        }

        public void SetColor(Color color)
        {
            if(_image != null)
                _image.color = color;
        }

        public void ActiveShadow(bool active)
        {
            _shadow.enabled = active;
        }

        public void ActiveSwitchDayNight(bool active = true)
        {
            if(!active)
                _switchDay.SetColors(Color.white, Color.white);

            _switchDay.SwitchDayOrNight(DaySwitcher.Instance.IsDay);
            
        }

        public void Refresh()
        {
            
        }
    }

}