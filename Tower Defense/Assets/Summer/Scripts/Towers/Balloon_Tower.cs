using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Tower : MonoBehaviour
{
    [SerializeField] GameObject bomb;

    float timer;
    public float fireRate;
    private void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
            timer = fireRate;
        }
    }
}
