using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Return to the main menu
        StartCoroutine("ReturnToMainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: After beta add credits
	}

    IEnumerator ReturnToMainMenu()
    {
        //Return to the main menu after some time
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
