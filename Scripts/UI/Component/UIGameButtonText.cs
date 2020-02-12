using System;
using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{


    public class UIGameButtonText : UIButton
    {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private GameObject _maskBlock;

        private bool _block;

        public Action<GameObject> OnClickGameButton;

        public void Start()
        {
            base.Start();

            OnClick = go =>
            {
                if(!_block)
                    OnClickGameButton?.Invoke(gameObject);
            };

        }

        public void Setup(string text)
        {
            BlockButton(false);

            _text.text = text;
        }

        public void BlockButton(bool block)
        {
            _block = block;
            _maskBlock.SetActive(block);
        }
    }

}