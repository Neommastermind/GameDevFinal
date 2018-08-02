using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        //if (Application.isEditor)
        //{
            //UnityEditor.EditorApplication.isPlaying = false;
        //}
        //else
        //{
            Application.Quit();
        //}
    }
}
