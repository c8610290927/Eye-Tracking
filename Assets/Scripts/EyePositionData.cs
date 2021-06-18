﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;
using System;

namespace LabData
{
    [Serializable]
    public class EyePositionData : LabDataBase
    {
        public float X;
        public float Y;
        public float Pupil_Diameter;
        public float Eye_Openness;
    }
}

