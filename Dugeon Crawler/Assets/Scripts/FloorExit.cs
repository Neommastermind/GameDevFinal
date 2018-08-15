using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FloorExit : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //TODO: Change this to another floor if we ever go beyond beta.
            //End screen for beta
            SceneManager.LoadScene("Credits", LoadSceneMode.Single);
        }
    }
}
