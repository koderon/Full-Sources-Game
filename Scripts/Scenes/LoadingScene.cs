using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

	void Start ()
    {
       StartCoroutine(StartNextScreen(0.001f));
    }

    IEnumerator StartNextScreen(float time)
    {
        Debug.LogWarning("Start Loading");

        yield return new WaitForSeconds(time);

        if (theGame.TheGame.GetComponent<theGame.Admob>())
            theGame.TheGame.GetComponent<theGame.Admob>().Loading();

        Debug.LogWarning("End Loading");

        SceneManager.LoadScene(theGame.Startup.LoadLevelAfterAuth);
    }
}
