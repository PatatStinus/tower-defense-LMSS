using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGame : MonoBehaviour
{
    public void Speed()
    {
        if(Time.timeScale == 1)
            Time.timeScale = 3;
        else
            Time.timeScale = 1;
    }
}
