using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theGame
{
    
    public class Swipe : MonoBehaviour
    {
        public enum EnumSwipeSide
        {
            Left,
            Right,
            Up,
            Down,
            None,
        }

        private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
        private bool isDraging;
        private Vector2 startTouch, swipeDelta;

        private float firstTime = 0;

        public Action<EnumSwipeSide> OnActionSwipeSide;

        void Update()
        {
            firstTime += Time.deltaTime;
            if (firstTime < 1.0f)  
                return;

            tap = swipeDown = swipeLeft = swipeDown = swipeRight = swipeUp = false;

#if UNITY_EDITOR
#region Standalone Inputs

            if (Input.GetMouseButtonDown(0))
            {
                tap = true;
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ChoiceToSwipeSide();
                Reset();
            }

            #endregion
#elif UNITY_ANDROID

#region Mobile Inputs

            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    isDraging = true;
                    tap = true;
                    startTouch = Input.touches[0].position;
                }
                else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    ChoiceToSwipeSide();
                    Reset();
                }
            }
#endregion

#endif
            swipeDelta = Vector2.zero;
            if (isDraging) 
            {
                if (Input.touches.Length > 0)
                {
                    swipeDelta = Input.touches[0].position - startTouch;
                }
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            if (swipeDelta.magnitude > 100)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    // left or right
                    if (x < 0)
                        swipeLeft = true;
                    else  
                    {
                        swipeRight = true;
                    }
                }
                else
                {
                    if (y < 0)
                        swipeDown = true;
                    else
                    {
                        swipeUp = true;
                    }
                }

                ChoiceToSwipeSide();
                Reset();
            }
        }

        private void Reset()
        {
            isDraging = false;
            startTouch = swipeDelta = Vector2.zero;
        }

        private void ChoiceToSwipeSide()
        {
            

            var eEdge = EnumSwipeSide.None;
            if(swipeDown)
                eEdge = EnumSwipeSide.Down;
            else if(swipeLeft)
                eEdge = EnumSwipeSide.Left;
            else if(swipeRight)
                eEdge = EnumSwipeSide.Right;
            else if(swipeUp)
                eEdge = EnumSwipeSide.Up;

            Debug.Log("edge := " + eEdge);

            if (OnActionSwipeSide != null)
            {
                OnActionSwipeSide(eEdge);
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        
    }

}