using System.Collections;
using System.Collections.Generic;
using theGame;
using UnityEngine;
using UnityEngine.U2D;

public class GameStaticData : MonoBehaviour
{
    public static GameStaticData Instance;

    [SerializeField]
    private SpriteAtlas _spriteAtlas;

    [SerializeField] 
    private SpriteAtlas _mapSpriteAtlas;

    private void Start()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public static Sprite GetSprite(string nameSprite)
    {
        if (Instance == null)
            return null;

        return Instance._spriteAtlas.GetSprite(nameSprite);
    }

    public static Sprite GetMap(string nameSprite)
    {
        if (Instance == null)
            return null;

        return Instance._mapSpriteAtlas.GetSprite(nameSprite);
    }
}
