using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{

    public class UIFullScreen : MonoBehaviour
    {
        public bool FullVertical = true;
        public bool FullHorizontal = true;

        // Use this for initialization
        void Start()
        {
            var canvasScaler = gameObject.GetComponentInParent<CanvasScaler>();
            var canvas = canvasScaler.GetComponent<Canvas>();
            
            var canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
            var size = GetComponent<RectTransform>().sizeDelta;
            var newSize = new Vector2(FullHorizontal ? canvasSize.x : size.x, FullVertical ? canvasSize.y : size.y);

            GetComponent<RectTransform>().sizeDelta = newSize;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}