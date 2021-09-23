using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowRainSpell : MonoBehaviour
{
    public float f_RRSize;
    [SerializeField] private int i_RRCost = 100;
    [SerializeField] private float timeRaining = 3f;
    [SerializeField] private ParticleSystem rainbowRain;
    private Collider[] collisionsInRain;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 rainPos;
    private bool b_RainbowRaining;
    private float f_orgTime;

    public void SpawnRainbow(Vector3 rainPos)
    {
        this.rainPos = rainPos;
        b_RainbowRaining = true;
        RainbowRaining();
        f_orgTime = timeRaining;
        rainbowRain.transform.position = new Vector3(rainPos.x, rainbowRain.transform.position.y, rainPos.z);
        rainbowRain.Play();
        rainbowRain.GetComponent<RandomRainColor>().startRaining = true;
        ManaManager.LoseMana(i_RRCost);
    }

    private void RainbowRaining()
    {
        enemies.Clear();
        collisionsInRain = Physics.OverlapSphere(rainPos, f_RRSize);
        foreach (var obj in collisionsInRain)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemies.Add(obj.gameObject);
        }

        if (timeRaining > 0)
        {
            //Attack
            Invoke("RainbowRaining", .5f);
        }
        else
        {
            rainbowRain.GetComponent<RandomRainColor>().startRaining = false;
            b_RainbowRaining = false;
            timeRaining = f_orgTime;
        }
    }

    private void Update()
    {
        if (b_RainbowRaining)
            timeRaining -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rainPos, f_RRSize);
    }
}
