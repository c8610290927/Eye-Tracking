using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class MainUI : MonoBehaviour
{
    public GameObject LoginPage, ModePage;

    [Header("LoginPage")]
    #region LoginPage
    public InputField UserID;
    #endregion

    public static string mode;

    public void gameStartClick()
    {
        ModePage.SetActive(true);
        LoginPage.SetActive(false);
        LabTools.CreateDataFolder<EyePositionData>(); //生成一個放labdata的資料夾
    }
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
