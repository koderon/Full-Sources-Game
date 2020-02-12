using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{

    public class UIListGridComponent : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _sizeElement;
        
        [SerializeField]
        private int _widthComponent;
 
        [SerializeField]
        private int _columns;

        [SerializeField]
        private bool _center;

        public bool Debug = false;

        private List<IUIListGridElement> _items = new List<IUIListGridElement>();

        private RectTransform _t;

        void Start()
        {
            _t = GetComponent<RectTransform>();
        }

        void Update()
        {
            //UpdateGrid();

            if (Debug)
            {
                Debug = false;

                Refresh();
            }
        }

        public void DestroyItems()
        {
            
            foreach (Object item in _items)
            {
                DestroyImmediate(item);
            }

            Clear();
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void AddItem(IUIListGridElement item)
        {
            _items.Add(item);
        }

        public void Refresh()
        {
            if (_t == null)
                _t = GetComponent<RectTransform>();

            var canvasScaler = gameObject.GetComponentInParent<CanvasScaler>();
            var canvas = canvasScaler.GetComponent<Canvas>();
            var canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

            var halfSizeCanvas = canvasSize.x / 2.0f;

            var sizeColumns = _sizeElement.x * _columns;
            if (_center)
                sizeColumns = _columns == 1 ? 0 : (_sizeElement.x * (_columns-1)) / 2;

            for (int i = 0; i < _items.Count; i++)
            {
                var offsetX = (i % _columns) * _sizeElement.x;
                var y = Mathf.FloorToInt(i / _columns) * _sizeElement.y;

                var x = offsetX;
                if (_center)
                {
                    x = /*-halfSizeCanvas + */-sizeColumns / 2 + offsetX;
                    x = -sizeColumns + offsetX;
                }

                _items[i].SetPosition(new Vector3(x, -y, 0));
            }

            var incrementHeight = _items.Count % _columns != 0 ? 0.5f : 0.0f;

            var height = (Mathf.Floor(_items.Count / _columns) + incrementHeight)  * _sizeElement.y;
    
            _t.localPosition = new Vector3(0, 0, 0);
            _t.sizeDelta = new Vector2(_widthComponent, height);

        }

        public void UpdateGrid()
        {
            var pos = _t.localPosition;
            var size = new Vector3(1000, 1500);
            var rect = new Rect(pos.x, pos.y-100, size.x, size.y);

            foreach (var item in _items)
            {
                var epos = item.GetPosition();
                epos.y *= -1;
                if (rect.Contains(epos))
                {
                    item.Refresh();
                }
            }
        }
    }

}