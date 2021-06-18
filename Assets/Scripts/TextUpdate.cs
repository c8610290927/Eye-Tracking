using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text fixationTimer;
    public Text GameTimer;
    public Text fixationTimes;
    public Text fixationTimes_3D;
    public Text Mode;

    public void changeFixationText(float timer)
    {
        fixationTimer.text = "Fixation Time: "+timer.ToString("0.00");
    }
    public void changeGameText(float timer)
    {
        GameTimer.text = "Time: "+timer.ToString("0.00");
    }
    public void changeFixationTimesText(float times)
    {
        fixationTimes.text = "Fixation Times: "+times.ToString("0.00");
        fixationTimes_3D.text = "Fixation Times: "+times.ToString("0.00");
    }
    public void changeModeText(string mode)
    {
        Mode.text = "Mode: " + mode;
    }
    public void back2IInit()
    {
        Application.Quit();
    }
}
