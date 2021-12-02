using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Tower : MonoBehaviour
{
    [SerializeField] GameObject bomb;

    float timer;
    public float fireRate;

    Balloon_Placement bp;
    private void Start()
    {
        timer = 0;
        bp = GetComponent<Balloon_Placement>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && bp.placedObject)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
            timer = fireRate;
        }
    }
}
