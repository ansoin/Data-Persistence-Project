using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{
    public TMP_Text highScoreDisplay;

    private void Start()
    {
        // Set high score display on menu to saved high score string
        highScoreDisplay.text = HighscoreManager.instance.highScoreString;
    }

    // Save name from username input box
    public void GetName(string s)
    {
        HighscoreManager.instance.SetName(s);
        Debug.Log("Name set to: " + HighscoreManager.instance.userName);
    }

    // Start the game : start button
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}