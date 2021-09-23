using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowRainSpell : MonoBehaviour
{
    public float f_RRSize;
    [SerializeField] private int i_RRCost = 100;
    [SerializeField] private float timeRaining = 3f;
    private Vector3 rainPos;
    private bool b_RainbowRaining;
    private float f_orgTime;

    public void SpawnRainbow(Vector3 rainPos)
    {
        Debug.Log("HET WERKT");
        this.rainPos = rainPos;
        b_RainbowRaining = true;
        f_orgTime = timeRaining;
        ManaManager.LoseMana(i_RRCost);
    }

    private void RainbowRaining()
    {
        if (timeRaining > 0)
        {
            //Attack

            timeRaining -= Time.deltaTime;
        }
        else
        {
            b_RainbowRaining = false;
            timeRaining = f_orgTime;
        }
    }

    private void Update()
    {
        if (b_RainbowRaining)
            RainbowRaining();
    }
}
