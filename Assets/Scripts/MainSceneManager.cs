using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabVisualization;

public class MainSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VisualizationManager.Instance.StartDataVisualization();
        VisualizationManager.Instance.VisulizationInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
