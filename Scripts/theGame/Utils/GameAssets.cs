using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theGame
{

    public class GameAssets : MonoBehaviour
    {
        private static GameAssets _i;

        public static GameAssets i
        {
            get
            {
                if (_i == null)
                    _i = Instantiate(Resources.Load<GameAssets>("Prefabs/GameAssets"));
                    
                return _i;
            }
        }

        public GameObject GDamagePopup3DPrefabs;
        public GameObject GBullet3DPrefabs;

    }

}