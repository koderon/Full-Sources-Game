using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class MapRegionModel
    {
        public int Id;

        public Vector3 Position;

        public float scale;

        public int IndexInColorPalette = 0;
    }

    [Serializable]
    public class MapColorPalette
    {
        public Color[] Colors;
    }
}