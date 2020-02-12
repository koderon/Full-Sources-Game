using System;
using theGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gui
{
    
    public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ISwitcherDayOrNight
    {
        [SerializeField]
        private ESound _soundOnClick = ESound.None;

        private UIButtonShadow _buttonShadow;
        private UIButtonPushEffect _buttonPushEffect;

        private bool _isDay;

        public Action<GameObject> OnClick;

        private bool _activeButton = true;

        public void Start()
        {
            //Debug.Log("UiButton Start");

            _buttonShadow = GetComponentInChildren<UIButtonShadow>();
            _buttonPushEffect = GetComponentInChildren<UIButtonPushEffect>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_activeButton)
                return;

            ActiveShadow(false);

            OnClick?.Invoke(gameObject);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_activeButton)
                return;

            ActiveShadow(true);
        }

        public void SwitchDayOrNight(bool isDay)
        {
            _isDay = isDay;
        }

        private void ActiveShadow(bool active)
        {
            if(_buttonShadow != null)
                _buttonShadow.PushButton(active);

            if(_buttonPushEffect != null)
                _buttonPushEffect.PushButton(active, _isDay);

            if(active)
                PlaySound();
        }

        private void PlaySound()
        {
            SoundController.PlaySound(_soundOnClick);
        }

        public void SetPosition(Vector3 pos)
        {
            GetComponent<RectTransform>().localPosition = pos;
        }

        public void ActiveButton(bool active)
        {
            _activeButton = active;
        }

        public Vector3 GetPosition()
        {
            return GetComponent<RectTransform>().localPosition;
        }
    }

}