using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAbility : EnemyMovement
{
    [SerializeField] private float radius;
    [SerializeField] private float regen;
    private bool isRaining = false;
    private Collider[] collisionsInRange;

    private void FixedUpdate()
    {
        collisionsInRange = Physics.OverlapSphere(transform.position, radius);
        foreach (var obj in collisionsInRange)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null && enemy.gameObject != gameObject)
            {
                if (isRaining)
                    enemy.TakeDamage(regen * Time.fixedDeltaTime);
                else
                    enemy.hp += regen * Time.fixedDeltaTime;

                if (enemy.hp >= enemy.startHealth) //Should be done in enemyhealth //Extra health that slowly degrates after might be fun
                    enemy.hp = enemy.startHealth;
            }
        }
    }

    [SerializeField] private GameObject blackFlower;
    [SerializeField] private GameObject colorFlower;
    private Quaternion orgRot;

    private void OnEnable()
    {
        ColorRainWeather.onColorRaining += TurnColor;
        ColorRainWeather.onStopColorRaining += TurnDark;
        orgRot = transform.GetChild(0).GetChild(1).rotation;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ColorRainWeather.onColorRaining -= TurnColor;
        ColorRainWeather.onStopColorRaining -= TurnDark;
        if (isRaining)
            ManaManager.GetMana(100);
    }

    protected override void Update()
    {
        base.Update();
        transform.GetChild(0).GetChild(1).rotation = orgRot;
    }

    private void TurnColor()
    {
        Destroy(transform.GetChild(0).GetChild(1).gameObject);
        isRaining = true;
        Instantiate(colorFlower, transform.GetChild(0));
        ColorRainWeather.onColorRaining -= TurnColor;
    }

    private void TurnDark()
    {
        Destroy(transform.GetChild(0).GetChild(1).gameObject);
        isRaining = true;
        Instantiate(blackFlower, transform.GetChild(0));
        ColorRainWeather.onColorRaining += TurnColor;
    }
}