using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGame : MonoBehaviour
{
    private bool spedUp;

    public void Speed()
    {
        if(!spedUp)
        {
            Time.timeScale = 3;
            spedUp = true;
        }
        else
        {
            Time.timeScale = 1;
            spedUp = false;
        }
    }
}
