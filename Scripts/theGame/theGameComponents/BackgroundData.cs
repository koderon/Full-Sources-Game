using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theGame
{

    public class BackgroundData : TheGameComponent
    {
        private IDictionary<string, BackgroundDataModel> _backgroundDataModels = new Dictionary<string, BackgroundDataModel>();

        public override void Init()
        {
            base.Init();

            Load();
        }

        public void Load()
        {
            var res = Resources.Load<TextAsset>("db/background_setting");

            var backgrounds = JsonUtility.FromJson<BackgroundsDataModel>(res.text);

            foreach (var background in backgrounds.backgrounds)
            {
                _backgroundDataModels.Add(background.id, background);
            }
        }

        public BackgroundDataModel GetBackground(string id)
        {
            if (!_backgroundDataModels.ContainsKey(id))
            {
                Debug.LogError("Background not Found! id := " + id);
                return null;
            }

            return _backgroundDataModels[id];
        }

    }

}