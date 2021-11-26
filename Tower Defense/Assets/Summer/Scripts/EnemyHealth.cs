using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class EnemyHealth : MonoBehaviour
{
    //Basic version for now

    public float startHealth = 4;
     public float hp;

    //[SerializeField] Color firstDamageColour;
    //[SerializeField] Color secondDamageColour;
    //[SerializeField] Color thirdDamageColour;

    //[SerializeField]Gradient firstGradient;

    Material enemyMaterial;
    Color enemyColour;

    [SerializeField] float lerpTime = 2;

    private void Start()
    {
        enemyMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        enemyColour = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
        hp = startHealth;
        BlackRainWeather.onBlackRaining += RainRegen;
        ColorRainWeather.onColorRaining += RainDamage;
    }

    private void OnDisable()
    {
        BlackRainWeather.onBlackRaining -= RainRegen;
        ColorRainWeather.onColorRaining -= RainDamage;
    }

    private void Update()
    {
        //float t = Mathf.PingPong(Time.time / strobeDuration, 1f);

        //enemyMaterial.color = firstGradient.Evaluate(t);
        //if (hp <= (startHealth /100f) * 75f && hp > (startHealth / 100f) * 50f)
        //{
        //    Debug.Log("75% hp");

        //    //enemyMaterial.color = Color.Lerp(enemyColour, firstDamageColour, 0.1f);
        //    enemyMaterial.color = Color.Lerp(enemyColour, firstDamageColour, Mathf.PingPong(Time.time, lerpTime));
        //}
        //else if (hp <= (startHealth / 100f) * 50f && hp > (startHealth / 100f) * 25f)
        //{
        //    Debug.Log("50% hp");
        //    lerpTime = 1.25f;
        //    enemyMaterial.color = Color.Lerp(firstDamageColour, secondDamageColour, Mathf.PingPong(Time.time, lerpTime));
        //}
        //else if (hp <= (startHealth / 100f) * 25f && hp > 0f)
        //{
        //    Debug.Log("25% hp");
        //    lerpTime = 0.75f;
        //    enemyMaterial.color = Color.Lerp(secondDamageColour, thirdDamageColour, Mathf.PingPong(Time.time, lerpTime));
        //}
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Projectile")
        //{
        //    TakeDamage(1);
        //}
    }

    public void TakeDamage(float dam)
    {
        hp -= dam;
    }

    private void RainDamage()
    {
        hp -= Time.deltaTime;
    }

    private void RainRegen()
    {
        hp += Time.deltaTime;
    }
}
