using System.Collections;
using System.Collections.Generic;
using theGame;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController _instance;

    [SerializeField] 
    private AudioSource _audioSource;

    [SerializeField]
    private SoundSettingScriptableObject _soundSetting;

    private void Start()
    {
        _instance = this;

        DontDestroyOnLoad(this);
    }

    public static void PlaySound(ESound soundId)
    {
        if (_instance == null  || soundId == ESound.None)
            return;

        _instance.Play(soundId);
    }

    private void Play(ESound soundId)
    {
        var pd = GameData.GetPlayerData();
        if (!pd.activeSound)
            return;

        _audioSource.volume = pd.soundVolume;

        var clip = _soundSetting.GetSound(soundId);
        if(clip != null)
            _audioSource.PlayOneShot(clip.SoundClip);
    }
}
