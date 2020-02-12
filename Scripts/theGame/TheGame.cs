using UnityEngine;

namespace theGame
{
    
    class TheGame : MonoBehaviour
    {
        public static bool Inited;

        #region Singleton
        private static TheGame _instance;
        
        public static TheGame Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("theGame").AddComponent<TheGame>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }
        #endregion

        #region Methods 
        public new static T GetComponent<T>() where T : TheGameComponent
        {
            return Instance.gameObject.GetComponent<T>();
        }

        public static T AddComponent<T>() where T : TheGameComponent
        {
            var go = Instance.gameObject.AddComponent<T>();
            go.Init();
            return go;
        }

        public static T ReloadComponent<T>() where T : TheGameComponent
        {
            var go = Instance.gameObject.GetComponent<T>();
            if (go != null)
            {
                Destroy(go);
                go = Instance.gameObject.AddComponent<T>();
                go.Init();
                return go;
            }
            return null;
        }

        public static void RemoveComponent<T>() where T : TheGameComponent
        {
            if(Instance.gameObject.GetComponent<T>())
                DestroyImmediate(Instance.gameObject.GetComponent<T>());
        }

#endregion

        private void OnDestroy()
        {
            Exit();
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Init()
        {
            Inited = true;

            var lang = new Lang();
            var daySwitcher = new Gui.DaySwitcher();
            lang.Load();

            AddComponent<GameData>();
            AddComponent<Admob>();
            //AddComponent<GamePlayer>();
            AddComponent<BackgroundData>();
            //AddComponent<SoundManager>();
            AddComponent<GameDataModel>();
        }
        
        public void Exit()
        {
        }
    }

}