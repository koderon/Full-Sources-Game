using System;
using System.Linq;
using theGame;
using UnityEngine;

[CreateAssetMenu(menuName = "QUIZ/Sound Setting")]
public class SoundSettingScriptableObject : ScriptableObject
{
    public SoundModel[] Sounds;

    public SoundModel GetSound(ESound soundId)
    {
        var s = Sounds.ToList().Find(g => g.SoundId == soundId);
        return s;
    }
}