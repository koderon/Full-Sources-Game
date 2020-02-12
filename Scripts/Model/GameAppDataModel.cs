using System;
using System.Collections.Generic;
using theGame;
using UnityEngine;

public class GameAppDataModel
{
    private JsonAppInfoModels _app;

    public GameAppDataModel()
    {
        Init();
    }

    public void Init()
    {
        var text = FileUtils.LoadTextFromResources("db/appInfo");
        if (String.IsNullOrEmpty(text))
        {
            Debug.LogError("db/appInfo.json not found!");
            return;
        }

        _app = JsonUtility.FromJson<JsonAppInfoModels>(text);
    }

    public List<JsonAppInfoModel> GetApps(SettingsScriptableObject settings)
    {
        if (_app == null)
            return new List<JsonAppInfoModel>();

        var index = _app.apps.FindIndex(g => g.appUrl == settings.UrlToLikeGame);
        if (index == -1 || !settings.IsPublishingBuild)
            return _app.apps;

        var apps = new List<JsonAppInfoModel>();
        apps.AddRange(_app.apps);

        apps.RemoveAt(index);

        return apps;
    }
}

[Serializable]
public class JsonAppInfoModels
{
    public List<JsonAppInfoModel> apps = new List<JsonAppInfoModel>();
}


[Serializable]
public class JsonAppInfoModel
{
    public String nameAppIcon;

    public String appUrl;

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Sprites/AppIcons/" + nameAppIcon);
    }
}