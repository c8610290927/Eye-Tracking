using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public void changeScene()
    {
        Debug.Log("按下去了");
        GameSceneManager.Instance.Change2MainScene();
    }
}
