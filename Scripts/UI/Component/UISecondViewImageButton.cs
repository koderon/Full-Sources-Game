using System;
using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    
    public class UISecondViewImageButton : UIButton
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private GameObject _maskBlock;

        private bool _block;

        public Action<GameObject> OnClickGameButton;

        public void Start()
        {
            base.Start();

            OnClick = go =>
            {
                if (!_block)
                    OnClickGameButton?.Invoke(gameObject);
            };
        }

        public void Setup(Sprite spr)
        {
            BlockButton(false);

            _image.sprite = spr;
        }

        public void BlockButton(bool block)
        {
            _block = block;
            _maskBlock.SetActive(block);
        }
    }

}