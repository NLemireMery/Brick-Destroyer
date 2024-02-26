using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Text playerName;

    private void Start()
    {
        DataManager.Instance.LoadNameHighScore();
    }

    /* Load Main scene when clicking Start in the Menu scene */
    public void StartNew()
    {
        SavePlayerName();
        SceneManager.LoadScene(1);
    }

    public void SavePlayerName()
    {
        DataManager.Instance.playerName = playerName;
    }

    public void Exit()
    {
        DataManager.Instance.SaveNameHighScore();

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
