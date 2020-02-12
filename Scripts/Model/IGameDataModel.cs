
using System.Collections.Generic;
using UnityEngine;

namespace theGame
{
    public enum TypeDataModel
    {
        None,

        Capitals,
        States,
        Presidents,
        Map,
    }

    public enum ETypeGame
    {
        Presidents = 1,
        States = 2,
    }

    
    public interface IGameDataModel
    {
        void Init();

        List<IGameDataParticleModel> GetData();

        string GetNameSprite();
    }

    public interface IGameDataParticleModel
    {
        TypeDataModel GetTypeData();

        int GetID();

        string GetName();

        
    }
}