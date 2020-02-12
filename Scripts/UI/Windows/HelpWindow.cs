using System.Collections;
using System.Collections.Generic;
using theGame;
using UnityEngine;

namespace Gui
{
    
    public class HelpWindow : Window
    {
        [SerializeField]
        private GameObject _gameInfoPrefab;

        [SerializeField]
        private Transform _anchor;

        [SerializeField]
        private UIListGridComponent _gridComponent;

        private List<UIGameInfoPrefab> _prefabs = new List<UIGameInfoPrefab>();
        
        public override void Setup(IWindowManager windowManager, params string[] args)
        {
            base.Setup(windowManager);

            SetBackground("game");

            if (args == null || args.Length < 1)
            {
                WindowManager.SetGameScreen(EWindowType.MainWindow);
                return;
            }

            var mode = int.Parse(args[0]);

            Create(mode);
        }

        private void Create(int mode)
        {
            var type = (TypeDataModel)mode;

            var data = GameDataModel.GetData(type);
            var nameSprite = GameDataModel.GetNameSprite(type) + "normal_";
            
            CreatePrefabs(data.Count);

            for (int i = 0; i < data.Count; i++)
            {
                var url = nameSprite + data[i].GetID().ToString("d2");
                var name = data[i].GetName();

                _prefabs[i].Setup(url, name);

            }
        }

        private void CreatePrefabs(int count)
        {
            foreach (var uiGameInfoPrefab in _prefabs)
            {
                uiGameInfoPrefab.Show(false);
            }

            var createCounts = count - _prefabs.Count;

            for (int i = 0; i < createCounts; i++)
            {
                var prefab = Helper.Create<UIGameInfoPrefab>(_gameInfoPrefab, _anchor);
                prefab.Show(false);

                _prefabs.Add(prefab);
                
                _gridComponent.AddItem(prefab);
            }

            _gridComponent.Refresh();
        }
    }

}