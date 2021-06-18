using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public GameObject LoginPage, ModePage;

    [Header("LoginPage")]
    #region LoginPage
    public InputField UserID;
    //public Text warningText;

    #endregion

    public static string mode;

    public void gameStartClick()
    {
        print("userID:"+ UserID.text);
        if (UserID.text == "")
        {
            print("Please enter your user ID/name ! ! !");
            UserID.text = "default";
            //warningText.text = "Please enter your user ID/name ! ! !";
        }
        ModePage.SetActive(true);
        LoginPage.SetActive(false);
        LabTools.CreateDataFolder<EyePositionData>(); //生成一個放labdata的資料夾

        GameDataManager.LabDataManager.LabDataCollectInit(() => UserID.text);
        
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
