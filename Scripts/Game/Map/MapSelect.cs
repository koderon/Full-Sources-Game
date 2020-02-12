using System;
using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{


    public class MapSelect : MonoBehaviour, ISwitcherDayOrNight
    {
        [SerializeField] 
        private Color[] _colors;
        
        [SerializeField] 
        private Vector2[] _points;

        [SerializeField]
        private Image _selectFrame;

        [SerializeField]
        private CanvasMesh[] _mesh;

        public void SetPosition(Vector3 position, float size)
        {
            _selectFrame.rectTransform.localPosition = position;
            _selectFrame.rectTransform.sizeDelta = Vector2.one * size;

            var halfSize = size / 2;
            var pointFrame1 = new Vector2(50, 150);
            var pointFrame2 = new Vector2(50, -350);

            var point1 = new Vector2(position.x - halfSize, position.y + halfSize);
            var point2 = new Vector2(position.x + halfSize, position.y + halfSize);
            var point3 = new Vector2(position.x + halfSize, position.y - halfSize);
            var point4 = new Vector2(position.x - halfSize, position.y - halfSize);

            var color = DaySwitcher.Instance.IsDay ? _colors[0] : _colors[1];
            
            _mesh[0].SetMesh(pointFrame1, point2, point1, pointFrame1, color);
            _mesh[1].SetMesh(pointFrame1, point1, point4, pointFrame2, color);
            _mesh[2].SetMesh(pointFrame2, point4, point3, pointFrame2, color);
        }

        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public void SwitchDayOrNight(bool isDay)
        {
            var color = DaySwitcher.Instance.IsDay ? _colors[0] : _colors[1];

            foreach (var canvasMesh in _mesh)
            {
                canvasMesh.SetColor(color);
            }
        }
    }

}