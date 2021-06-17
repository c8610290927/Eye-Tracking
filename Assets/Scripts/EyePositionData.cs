using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;


namespace LabData
{
    public class EyePositionData : LabDataBase
    {
        public float X;
        public float Y;
        public float Z;
        public string FocusObject;
        public float Pupil_Diameter;
        public float Eye_Openness;
        /*public EyePositionData(float X_value, float Y_value, float Z_value, string FocusObject_value, float Pupil_Diameter_value, float Eye_Openness_value)
        {
            X = X_value;
            Y = Y_value;
            Z = Z_value;
            FocusObject = FocusObject_value;
            Pupil_Diameter = Pupil_Diameter_value;
            Eye_Openness = Eye_Openness_value;
        }
*/
    }
}

