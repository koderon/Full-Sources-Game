using UnityEngine;
using UnityEngine.SceneManagement;

namespace theGame
{
    class Startup : MonoBehaviour
    {
        public static string LoadLevelAfterAuth = "Main";

        [SerializeField] private bool _testAds = true;
        [SerializeField] private string _nameMainLevel = "Main";

        public GameObject SystemGameObject;

        public static bool TestADS = true;
        
        public void Awake()
        {
            Debug.Log("############### =- loading scene : " + SceneManager.GetActiveScene().name + " -= ###############");

            Screen.fullScreen = true;

            LoadLevelAfterAuth = _nameMainLevel;
            
            if (!TheGame.Inited)  
            {
                if (TinyStart.IsTinyStart)
                {
                    Debug.Log("Tiny start");

                    TinyStart.IsTinyStart = false;

                    if(TinyStart.IsNeedLoadThisLevel)
                        LoadLevelAfterAuth = TinyStart.LevelName;
                }

                TheGame.Instance.Init();

                TestADS = _testAds;
            }
            
            DontDestroyOnLoad(SystemGameObject);

            Debug.Log("scene name := " + LoadLevelAfterAuth);

            SceneManager.LoadScene("Loading");

            // заставляет работать проект даже вне фокуса!
            Application.runInBackground = true;  
        }
    }

}