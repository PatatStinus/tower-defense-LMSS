using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthParticles : MonoBehaviour
{
    [SerializeField] private GameObject blackFlower;
    [SerializeField] private GameObject colorFlower;
    private Quaternion orgRot;

    private void Start()
    {
        ColorRainWeather.onColorRaining += TurnColor;
        ColorRainWeather.onStopColorRaining += TurnDark;
        orgRot = transform.GetChild(0).GetChild(1).rotation;
    }

    private void Update()
    {
        transform.GetChild(0).GetChild(1).rotation = orgRot;
    }

    private void OnDisable()
    {
        ColorRainWeather.onColorRaining -= TurnColor;
        ColorRainWeather.onStopColorRaining -= TurnDark;
    }

    private void TurnColor()
    {
        Destroy(transform.GetChild(0).GetChild(1).gameObject);
        GetComponent<HealthAbility>().isRaining = true;
        Instantiate(colorFlower, transform.GetChild(0));
        ColorRainWeather.onColorRaining -= TurnColor;
    }

    private void TurnDark()
    {
        Destroy(transform.GetChild(0).GetChild(1).gameObject);
        GetComponent<HealthAbility>().isRaining = true;
        Instantiate(blackFlower, transform.GetChild(0));
        ColorRainWeather.onColorRaining += TurnColor;
    }
}
