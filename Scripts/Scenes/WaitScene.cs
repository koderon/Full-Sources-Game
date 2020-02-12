using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitScene : MonoBehaviour
{
    // Use this for initialization
	void Start () {
                
        StartCoroutine(StartNextScreen(0.1f));
    }

    IEnumerator StartNextScreen(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("Loading");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
