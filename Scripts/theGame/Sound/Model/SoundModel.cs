using System;
using UnityEngine;

public enum ESound
{
    None,

    Click,
    WrongClick,
    AcceptClick,
    LoseSound,
    WinSound,
}

[Serializable]
public class SoundModel
{
    public ESound SoundId;

    public AudioClip SoundClip;
}
