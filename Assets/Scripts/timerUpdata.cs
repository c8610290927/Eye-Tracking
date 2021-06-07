using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerUpdata : MonoBehaviour
{
    public Text fixationTimer;
    public Text GameTimer;

    public void changeFixationText(float timer)
    {
        fixationTimer.text = "Fixation Time: "+timer.ToString("0.00");
    }
    public void changeGameText(float timer)
    {
        GameTimer.text = "Time: "+timer.ToString("0.00");
    }
}
