using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRainColor : MonoBehaviour
{
    [HideInInspector] public bool startRaining = false;
    private Color newColor;
    private Color orgColor;
    private float time = 1;
    private int rainbow = 1;

    private void Update()
    {
        if (startRaining)
        {
            time += Time.deltaTime * 2.3f;
            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.Lerp(orgColor, newColor, time));
            if (time >= 1)
                GetNewColor();
        }
        else
            Invoke("DestroyParticle", 3f);
    }

    private void GetNewColor()
    {
        orgColor = newColor;
        switch (rainbow)
        {
            case 1:
                newColor = Color.red;
                break;
            case 2:
                newColor = new Color32(255, 98, 0, 255);
                break;
            case 3:
                newColor = Color.yellow;
                break;
            case 4:
                newColor = Color.green;
                break;
            case 5:
                newColor = Color.cyan;
                break;
            case 6:
                newColor = Color.blue;
                break;
            case 7:
                newColor = new Color32(255, 0, 255, 255);
                break;
        }
        time = 0;
        rainbow++;
        if(rainbow == 8)
        {
            rainbow = 1;
        }
    }

    private void DestroyParticle()
    {
        Destroy(this.gameObject);
    }
}
