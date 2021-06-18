using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabVisualization;
using LabVisualization.EyeTracing;
using System;
using LabData;
using GameData;
#if USE_SRANIPAL
using ViveSR.anipal.Eye;
#endif

public class EyeTrackEquipment : MonoSingleton<EyeTrackEquipment>, IEquipment, IEyeTracingPos
{
    public SRanipal_Eye_Framework Sranipal { get; set; }

    private bool IsOpenEye = false;
    
    public Vector2 EyeData { get; set; }
    
    SingleEyeData LeftData { get; set; }
    SingleEyeData RightData { get; set; }
    SingleEyeData Combined { get; set; }

    string FocusName { get; set; }

    private readonly GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };

    private FocusInfo FocusInfo;

    public bool IsStopEyeData = false;

    public void EquipmentInit()
    {
        Debug.Log("EquipmentInit");
        IsStopEyeData = false;
//        StartCoroutine(UpdateData());
#if USE_SRANIPAL
        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING)
        {
             
            if (Sranipal == null)
            {
                Sranipal = gameObject.AddComponent<SRanipal_Eye_Framework>();
            }
            Sranipal = SRanipal_Eye_Framework.Instance;
            Sranipal.StartFramework();
            IsOpenEye = true;

        }
#endif
    }
    
    public void EquipmentStart()
    {
        Debug.Log("EquipmentStart");
        StartCoroutine(UpdateData());
    }

    public void EquipmentStop()
    {
        IsStopEyeData = true;
        if (IsOpenEye)
        {
            Sranipal.StopFramework();
            IsOpenEye = false;
        }
    }

    public Func<Vector2> GetEyeTracingPos()
    {
        Debug.Log("EyeData");
        return () => EyeData;
    }

    private IEnumerator UpdateData()
    {
        Debug.Log("第一步");
        while (true)
        {
            Debug.Log("第二步");
#if USE_SRANIPAL
            if (IsStopEyeData)
            {
                yield break;
            }
            if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING)
            {
                Debug.Log("第三步");
                VerboseData data;
                if (SRanipal_Eye.GetVerboseData(out data) &&
                    data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY) &&
                    data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY)
                    )
                {
                    Debug.Log("第四步");
                    EyeData = data.left.pupil_position_in_sensor_area;
                    LeftData = data.left;
                    RightData = data.right;
                    Combined = data.combined.eye_data;
                    //Debug.Log("Left:" + data.left.pupil_position_in_sensor_area + "~O~O~" + "Right:" + data.right.pupil_position_in_sensor_area);
                    Ray GazeRay = new Ray();
                    if (SRanipal_Eye.Focus(GazePriority[0], out GazeRay, out FocusInfo, float.MaxValue))
                    {
                        FocusName = FocusInfo.collider.gameObject.name;
                        //Debug.Log("射線撞到: "+FocusName+"啦啦啦啦啦");
                        GameEventCenter.DispatchEvent("ShowEyeFocus", FocusName);
                        GameEventCenter.DispatchEvent("GetEyeContact", FocusName);

                        //回傳labdata的資料 要另外寫一個class
                        var eyepositiondata = new EyePositionData() //記錄eyedata
                        {
                            X = FocusInfo.point.x,
                            Y = FocusInfo.point.y,
                            Z = FocusInfo.point.z,
                            FocusObject = FocusName,
                            Pupil_Diameter = LeftData.pupil_diameter_mm,
                            Eye_Openness = LeftData.eye_openness
                        };
                        GameDataManager.LabDataManager.SendData(eyepositiondata);
                        Debug.Log("030");
                        LabTools.WriteData(eyepositiondata, "default", true);

                        Debug.Log("0303");

                        Debug.Log("FocusInfo:" + FocusName + " At (" + FocusInfo.point.x + "," + FocusInfo.point.y + "," + FocusInfo.point.z + ")");
                        Debug.Log("PupilSize :" + data.left.pupil_diameter_mm);
                        
                    }
                }
            }
            yield return new WaitForFixedUpdate();
#endif
        }
    }
}
