using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabVisualization;

public class MainSceneManager : MonoBehaviour
{
    float gameTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        VisualizationManager.Instance.StartDataVisualization();
        VisualizationManager.Instance.VisulizationInit();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        GameObject.Find("Canvas").GetComponent<timerUpdata>().changeGameText(gameTime);
    }
}
