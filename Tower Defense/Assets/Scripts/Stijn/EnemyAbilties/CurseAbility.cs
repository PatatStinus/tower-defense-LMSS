using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAbility : MonoBehaviour
{
    [SerializeField] private GameObject cursedObject;
    private GameObject theCurse;
    private bool isRaining;
    private bool doCurse;
    private EnemyMovement movement;
    private int curse;


    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        curse = Random.Range(1, 4);
        ColorRainWeather.onColorRaining += RainEvent;
        ColorRainWeather.onStopColorRaining += StopRainEvent;
        GetCurse();
    }

    private void Update()
    {
        if (movement.i_waypoitIndex > EnemyPathMaking.t_Points[movement.pathIndex].Length - 2 && !doCurse)
        {
            doCurse = true;
            SpawnCurse();
        }
    }

    private void OnDisable()
    {
        ColorRainWeather.onColorRaining -= RainEvent;
        ColorRainWeather.onStopColorRaining -= StopRainEvent;
        if (isRaining && !movement.reachedEnd)
            SpawnCurse();
    }

    private void SpawnCurse()
    {
        theCurse = Instantiate(cursedObject);
        theCurse.GetComponent<CurseEffect>().DoCurse(curse, isRaining);
    }

    private void GetCurse()
    {
        switch (curse)
        {
            case 1:
                //Cross
                break;
            case 2:
                //Circle
                break;
            case 3:
                //3 Lines
                break;
        }
    }

    private void RainEvent()
    {
        isRaining = true;
        ColorRainWeather.onColorRaining -= RainEvent;
    }

    private void StopRainEvent()
    {
        isRaining = false;
        ColorRainWeather.onColorRaining += RainEvent;
    }
}
