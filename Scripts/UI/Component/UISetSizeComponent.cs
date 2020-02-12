using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{


    [ExecuteInEditMode]
    public class UISetSizeComponent : MonoBehaviour
    {
        public int Right;
        public int Left;
        public int Up;
        public int Bottom;

        public bool _debugView = false;

        public float _time;

        // Start is called before the first frame update
        void Start()
        {
            Refresh();
        }

        // Update is called once per frame
        void Update()
        {
            if (_debugView)
            {
                _debugView = false;
                Refresh();
            }

            _time += Time.deltaTime;
            if (_time < 0.5f)
            {
                _time = 0;

                Refresh();
            }
        }

        public void Refresh()
        {
            var canvasScaler = gameObject.GetComponentInParent<CanvasScaler>();
            var canvas = canvasScaler.GetComponent<Canvas>();

            var canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
            var size = GetComponent<RectTransform>().sizeDelta;

            var w = canvasSize.x - (Right + Left);
            var h = canvasSize.y - (Up + Bottom);

            var t = GetComponent<RectTransform>();
            t.localPosition = new Vector3(Right, -Up);
            t.sizeDelta = new Vector2(w, h);
        }
    }

}