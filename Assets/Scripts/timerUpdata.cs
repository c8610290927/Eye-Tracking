using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerUpdata : MonoBehaviour
{
    public Text text;

    public void changeText(float timer)
    {
        text.text = "注視時間: "+timer.ToString("0.00");
    }
}
