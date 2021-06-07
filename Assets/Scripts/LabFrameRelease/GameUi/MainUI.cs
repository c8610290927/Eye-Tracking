using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static string mode;
    public void changeSceneEasy()
    {
        Debug.Log("按Easy");
        mode = "easy";
        GameSceneManager.Instance.Change2MainScene();
    }
    public void changeSceneNormal()
    {
        Debug.Log("按Normal");
        mode = "normal";
        GameSceneManager.Instance.Change2MainScene();
    }
    public void changeSceneHard()
    {
        Debug.Log("按Hard");
        mode = "hard";
        GameSceneManager.Instance.Change2MainScene();
    }
}
