using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gui
{
    
    public interface IUIListGridElement
    {
        void Refresh();

        void SetPosition(Vector3 pos);
        Vector3 GetPosition();
    }

}