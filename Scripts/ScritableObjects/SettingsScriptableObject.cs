using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

[CreateAssetMenu(menuName = "QUIZ/Settings")]
public class SettingsScriptableObject : ScriptableObject
{
    public bool IsPublishingBuild = false;

    public SystemLanguage DefaultLanguage = SystemLanguage.English;

    public string UrlToLikeGame;

    //public Sprite[] AppIcons;

    //public String[] AppUrl;

    /*
    public List<Sprite> GetAppIcons()
    {
        var index = AppUrl.ToList().FindIndex(g => g == UrlToLikeGame);
        if (index == -1 || !IsPublishingBuild)
            return AppIcons.ToList();

        var appIcons = new List<Sprite>();
        appIcons.AddRange(AppIcons);

        appIcons.RemoveAt(index);

        return appIcons;
    }

    public List<String> GetAppUrl()
    {
        var index = AppUrl.ToList().FindIndex(g => g == UrlToLikeGame);
        if (index == -1 || !IsPublishingBuild)
            return AppUrl.ToList();

        var appUrls = new List<string>();
        appUrls.AddRange(AppUrl);

        appUrls.RemoveAt(index);

        return appUrls;
    }
    */
}