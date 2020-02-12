using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    public class UIButtonShadow : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _buttonUpSetting = new Vector2(5, -5);

        [SerializeField]
        private Vector2 _buttonDownSetting = new Vector2(0, 0);

        private Shadow _shadow;

        [SerializeField]
        private Transform _pressImage;

        void Start()
        {
            _shadow = GetComponent<Shadow>();
            if (_pressImage == null)
            {
                _pressImage = GetComponent<Transform>();
            }
        }

        public void PushButton(bool down = true)
        {
            if (_shadow == null)
                return;

            _shadow.effectDistance = down ? _buttonDownSetting : _buttonUpSetting;
            _pressImage.localPosition = down ? _buttonUpSetting : _buttonDownSetting;
        }
    }

}