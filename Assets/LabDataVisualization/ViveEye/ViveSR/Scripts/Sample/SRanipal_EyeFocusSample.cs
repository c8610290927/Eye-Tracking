//========= Copyright 2018, HTC Corporation. All rights reserved. ===========
using System;
using System.Collections;
using UnityEngine;

namespace ViveSR.anipal.Eye
{
    public class SRanipal_EyeFocusSample : MonoBehaviour
    {
        private FocusInfo FocusInfo;
        private readonly float MaxDistance = 20;
        private readonly GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };
        string FocusName { get; set; }
        string FocusTag { get; set; }
        float Timer = 0f;
        float reciprocal = 0f; //倒數秒數
        public static int score = 0; //注視成功次數


        private void Start()
        {
            if (!SRanipal_Eye_Framework.Instance.EnableEye)
            {
                enabled = false;
                return;
            }

            FocusName = "";
            FocusTag = "";

            //設定不同難易度的注視倒數時間
            if(MainUI.mode == "easy") reciprocal = 1f;
            else if(MainUI.mode == "normal") reciprocal = 2f;
            else reciprocal = 3f;
        }

        private void Update()
        {
            if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

            foreach (GazeIndex index in GazePriority)
            {
                Ray GazeRay;
                if (SRanipal_Eye.Focus(index, out GazeRay, out FocusInfo, MaxDistance))
                {
                    FocusTag = FocusInfo.collider.tag;
                    Debug.Log("射線撞到: " + FocusInfo.collider.name);
                    DartBoard dartBoard = FocusInfo.transform.GetComponent<DartBoard>();
                    if (dartBoard != null) dartBoard.Focus(FocusInfo.point);

                    if (FocusTag == "target")
                    {
                        if(FocusInfo.collider.name == FocusName) Timer += Time.deltaTime;
                        FocusName = FocusInfo.collider.name;
                    }
                    else Timer = 0f;

                    if(Timer >= reciprocal) 
                    {
                        score += 1;
                        Destroy(GameObject.Find(FocusInfo.collider.name));
                        Instantiate(dartBoard, new Vector3(UnityEngine.Random.Range(-3.0f, 3.0f), UnityEngine.Random.Range(8.5f, 10.0f), UnityEngine.Random.Range(-4.0f, -2.0f)), new Quaternion(0, 0, 0, 0));
                    }

                    GameObject.Find("Canvas").GetComponent<timerUpdata>().changeFixationText(Timer);
                    break;
                }
                else Timer = 0f;
            }
        }

    }
}