using UnityEngine;
using UnityEngine.UI;

namespace newGame
{

    public class GameScaleFix : MonoBehaviour
    {
        [SerializeField] 
        private Canvas _canvas;

        void Start()
        {
            if (_canvas == null)
                return;

            var size = _canvas.GetComponent<RectTransform>().sizeDelta;
            if (size.y < 1920.0f)
            {
                _canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1.0f;
            }
        }
    }

}