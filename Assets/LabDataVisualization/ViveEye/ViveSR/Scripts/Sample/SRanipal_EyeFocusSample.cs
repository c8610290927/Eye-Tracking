//========= Copyright 2018, HTC Corporation. All rights reserved. ===========
using System;
using System.Collections;
using UnityEngine;
using LabData;

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

        public Vector2 EyeData { get; set; }
        SingleEyeData LeftData { get; set; }
        SingleEyeData RightData { get; set; }
        SingleEyeData Combined { get; set; }

        //標靶生成位置(各30組)
        float[] positionX = new float[] {2.13f, -0.01f, 1.33f, -0.84f, 0.46f, -1.82f, 2.91f, -1.88f, 1.24f, -1.24f, 2.70f, -1.18f, 2.33f, -1.13f, 0.30f, -0.08f, 0.90f, -0.63f, -1.14f, 2.57f, 1.33f, -0.84f, 0.46f, -1.82f, 2.91f, -0.08f, 0.90f, -0.63f, -1.14f, 2.57f};
        float[] positionY = new float[] {9.98f, 9.67f, 8.93f, 9.88f, 9.40f, 8.99f, 8.83f, 8.84f, 8.07f, 8.15f, 9.80f, 9.46f, 9.49f, 8.95f, 8.90f, 9.40f, 8.34f, 9.48f, 9.75f, 9.35f, 9.88f, 9.40f, 8.99f, 8.83f, 8.84f, 8.07f, 8.15f, 9.80f, 9.46f, 9.49f};
        float[] positionZ = new float[] {-3.86f, -3.86f, -2.92f, -2.75f, -3.99f, -3.75f, -2.55f, -2.06f, -3.59f, -2.77f, -3.03f, -2.33f, -2.73f, -2.19f, -3.99f, -3.72f, -2.12f, 3.45f, -2.89f, -3.44f, -3.86f, -2.92f, -2.75f, -3.99f, -2.06f, -3.59f, -2.77f, -3.03f, -2.33f, -2.73f};


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
            if (MainUI.mode == "easy") reciprocal = 1f;
            else if (MainUI.mode == "normal") reciprocal = 2f;
            else reciprocal = 3f;
        }

        private void Update()
        {
            if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING)
            {
                print("RRRR");
                VerboseData data;
                if (SRanipal_Eye.GetVerboseData(out data) &&
                    data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY) &&
                    data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY)
                    )
                {
                    Debug.Log("第四步");
                    EyeData = data.left.pupil_position_in_sensor_area;
                    //LeftData = data.left;
                    //RightData = data.right;
                    Combined = data.combined.eye_data;
                    //Debug.Log("Left:" + data.left.pupil_position_in_sensor_area + "~O~O~" + "Right:" + data.right.pupil_position_in_sensor_area);
                    Ray GazeRay = new Ray();
                    if (SRanipal_Eye.Focus(GazePriority[0], out GazeRay, out FocusInfo, float.MaxValue))
                    {
                        //FocusName = FocusInfo.collider.gameObject.name;
                        //Debug.Log("射線撞到: "+FocusName+"啦啦啦啦啦");
                        GameEventCenter.DispatchEvent("ShowEyeFocus", FocusName);
                        GameEventCenter.DispatchEvent("GetEyeContact", FocusName);

                        //回傳labdata的資料 要另外寫一個class
                        EyePositionData eyepositiondata = new EyePositionData() //記錄eyedata
                        {
                            X = FocusInfo.point.x,
                            Y = FocusInfo.point.y,
                            Pupil_Diameter_Left = data.left.pupil_diameter_mm,
                            Pupil_Diameter_Right = data.right.pupil_diameter_mm,
                            Eye_Openness = data.left.eye_openness
                        };
                        //Debug.Log("PupilSize (R) :" + data.right.pupil_diameter_mm);
                        Debug.Log("eyepositiondata (XY) :" + eyepositiondata.X + eyepositiondata.Y);

                        if(MainSceneManager.gameTime > 0) GameDataManager.LabDataManager.SendData(eyepositiondata);

                       // Debug.Log("FocusInfo:" + FocusName + " At (" + FocusInfo.point.x + "," + FocusInfo.point.y + "," + FocusInfo.point.z + ")");
                        
                    }
                }
            }

            foreach (GazeIndex index in GazePriority)
            {
                Ray GazeRay;
                if (SRanipal_Eye.Focus(index, out GazeRay, out FocusInfo, MaxDistance))
                {
                    FocusTag = FocusInfo.collider.tag;
                    
                        //Debug.Log("射線撞到: " + FocusInfo.collider.name);
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
                        Instantiate(dartBoard, new Vector3(positionX[score%30], positionY[score%30], positionZ[score%30]), new Quaternion(0, 0, 0, 0));
                        //Instantiate(dartBoard, new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(8.5f, 10.0f), UnityEngine.Random.Range(-4.0f, -2.0f)), new Quaternion(0, 0, 0, 0));
                    }

                    GameObject.Find("Canvas").GetComponent<TextUpdate>().changeFixationText(Timer);
                    break;
                }
                else Timer = 0f;
            }
        }

    }
}