using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gui
{


    public class UIMenuButtonImage : UIButton
    {
        [SerializeField]
        private UIStars _stars;

        public void Setup(int countStars, int countActiveStars)
        {
            _stars.SetMaxStars(countStars);
            _stars.SetActiveStars(countActiveStars);
        }

    }

}