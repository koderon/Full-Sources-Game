using UnityEngine;

namespace Gui
{


    public class UILogicHeartsContinueButton : MonoBehaviour
    {
        [SerializeField] 
        private GameObject[] _hearts;

        private float _time;

        private void OnEnable()
        {
            _time = 1.5f;
        }

        private void Update()
        {
            UpdateHeartsAnimation();
        }

        private void UpdateHeartsAnimation()
        {
            _time += Time.deltaTime;
            if (_time > 2.0f)
                _time = 0;

            for (int i = 0; i < _hearts.Length; i++)
            {
                _hearts[i].SetActive(((i + 1) * 0.5) <= _time);
            }
        }
    }

}