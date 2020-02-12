using System;
using System.Collections;
using System.Collections.Generic;
using theGame;
using TMPro;
using UnityEngine;

public class USAPresidentsDataModel : IGameDataModel
{
    private List<IGameDataParticleModel> _data = new List<IGameDataParticleModel>();

    public void Init()
    {
        var text = FileUtils.LoadTextFromResources("db/presidents");

        text = text.Replace("\r", "");
        var statesData = text.Split('\n');

        for (int i = 1; i < statesData.Length; i++)
        {
            var stateData = statesData[i].Split(',');

            for (int j = 0; j < stateData.Length; j++)
            {
                stateData[j] = stateData[j].Replace("#", ",");
            }

            var state = new USAPresidentDataModel();
            state.Id = int.Parse(stateData[0]);
            state.Level = stateData[1];

            var langIndex = 2;

            state.Names[SystemLanguage.English] = stateData[langIndex];
            state.Names[SystemLanguage.Finnish] = stateData[langIndex + 1];
            state.Names[SystemLanguage.Russian] = stateData[langIndex + 2];
            state.Names[SystemLanguage.German] = stateData[langIndex + 3];
            state.Names[SystemLanguage.French] = stateData[langIndex + 4];
            state.Names[SystemLanguage.Italian] = stateData[langIndex + 5];
            state.Names[SystemLanguage.Spanish] = stateData[langIndex + 6];
            state.Names[SystemLanguage.Polish] = stateData[langIndex + 7];
            state.Names[SystemLanguage.Portuguese] = stateData[langIndex + 8];
            state.Names[SystemLanguage.Norwegian] = stateData[langIndex + 9];
            state.Names[SystemLanguage.Danish] = stateData[langIndex + 10];
            state.Names[SystemLanguage.Indonesian] = stateData[langIndex + 11];
            state.Names[SystemLanguage.Korean] = stateData[langIndex + 12];
            state.Names[SystemLanguage.Japanese] = stateData[langIndex + 13];
            state.Names[SystemLanguage.Chinese] = stateData[langIndex + 14];

            _data.Add(state);
        }
    }
    
    public List<IGameDataParticleModel> GetData()
    {
        return _data;
    }

    public string GetNameSprite()
    {
        return "President_";
    }
}


public class USAPresidentDataModel : IGameDataParticleModel
{
    public int Id;
    public string Level;

    private TypeDataModel _type = TypeDataModel.Presidents;

    public Dictionary<SystemLanguage, string> Names = new Dictionary<SystemLanguage, string>();

    public TypeDataModel GetTypeData() => _type;

    public int GetID() => Id;

    public string GetName()
    {
        return Names[Lang.Instance.CurLang];
    }
}