using UnityEngine;
using UnityEngine.SceneManagement;

namespace theGame
{
    class TinyStart : MonoBehaviour
    {
        public static bool IsTinyStart;
        
        public static string LevelName = "farm";

        public static bool IsNeedLoadThisLevel = true;

        [SerializeField]
        private bool _isNeedLoadThisLevel = true;
        
        void Awake()
        {
            if (!TheGame.Inited)
            {
                IsTinyStart = true;

                IsNeedLoadThisLevel = _isNeedLoadThisLevel;

                LevelName = SceneManager.GetActiveScene().name;
                
                SceneManager.LoadScene("_Start");
                return;
            }
           
            Destroy(gameObject);
            
        }

    }
}