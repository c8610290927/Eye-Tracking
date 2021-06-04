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


        private void Start()
        {
            if (!SRanipal_Eye_Framework.Instance.EnableEye)
            {
                enabled = false;
                return;
            }

            FocusName = "";
            FocusTag = "";
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

                    if (FocusTag == "target")
                    {
                        if(FocusInfo.collider.name == FocusName) Timer += Time.deltaTime;
                        FocusName = FocusInfo.collider.name;
                    }
                    else Timer = 0f;

                    if(Timer >= 3) Destroy(GameObject.Find(FocusInfo.collider.name));

                    /*DartBoard dartBoard = FocusInfo.transform.GetComponent<DartBoard>();
                    if (dartBoard != null) dartBoard.Focus(FocusInfo.point);*/
                    GameObject.Find("Canvas").GetComponent<timerUpdata>().changeText(Timer);
                    break;
                }
                else Timer = 0f;
            }
        }

    }
}