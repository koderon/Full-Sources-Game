using System;
using System.Linq;
using Game;
using UnityEngine;

[CreateAssetMenu(menuName = "QUIZ/Map Setting")]
public class MapSettingScriptableObject : ScriptableObject
{
    public int IndexInPaletteSelectColor = 0;

    public int FirstNotSystemRegion;

    public MapColorPalette DayPalette;

    public MapColorPalette NightPalette;

    public MapRegionModel[] Regions;

    public MapRegionModel GetRegion(int id)
    {
        var r = Regions.ToList().Find(g => g.Id == id);
        return r;
    }

    public int GetMaxMapRegion()
    {
        return Regions.Length;
    }

    public Color GetColor(int indexInPalette, bool isDay)
    {
        var palette = isDay ? DayPalette : NightPalette;

        if (palette.Colors.Length <= indexInPalette)
            return Color.white;

        return palette.Colors[indexInPalette];
    }
}