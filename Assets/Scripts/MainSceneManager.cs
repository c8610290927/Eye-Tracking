using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LabVisualization;
using ViveSR.anipal.Eye;

public class MainSceneManager : MonoBehaviour
{
    public static float gameTime = 10f; //遊戲時間
    GameObject image;
    GameObject image_3D;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("啦啦啦ouq");
        image = GameObject.FindObjectOfType<Image>().gameObject;
        image_3D = GameObject.Find("Camera Image");
        print(image_3D.name);
        image.SetActive(false);    //隱藏PC上的結算畫面
        image_3D.SetActive(false); //隱藏頭盔中的結算畫面
        VisualizationManager.Instance.StartDataVisualization();
        VisualizationManager.Instance.VisulizationInit();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        GameObject.Find("Canvas").GetComponent<TextUpdate>().changeGameText(gameTime);

        if(gameTime <= 0)
        {
            //Time.timeScale = 0f; //時間暫停
            //Debug.Log(GameObject.FindObjectOfType<Image>().name);
            GameObject.Find("Canvas").GetComponent<TextUpdate>().changeFixationTimesText(SRanipal_EyeFocusSample.score);
            GameObject.Find("Canvas").GetComponent<TextUpdate>().changeModeText(MainUI.mode);
            image.SetActive(true); //顯示結算畫面
            image_3D.SetActive(true); //顯示頭盔結算畫面
            //刪除後方倒數
            GameObject.Find("FixationTimer").gameObject.SetActive(false); 
            GameObject.Find("GameTimer").gameObject.SetActive(false); 

            Destroy(GameObject.FindWithTag("target"));
        }
    }
}
