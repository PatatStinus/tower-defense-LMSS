using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthParticles : MonoBehaviour
{
    [SerializeField] private GameObject blackFlower;
    [SerializeField] private GameObject colorFlower;
    [HideInInspector] public bool raining;
    private Quaternion orgRot;

    private void Start()
    {
        orgRot = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = orgRot;
    }

    private void TurnColor()
    {
        transform.parent.transform.parent.GetComponent<HealthAbility>().isRaining = true;
        GameObject flower = Instantiate(colorFlower, transform.parent);
        flower.GetComponent<HealthParticles>().raining = true;
        Destroy(this.gameObject);
    }

    private void TurnDark()
    {
        transform.parent.transform.parent.GetComponent<HealthAbility>().isRaining = false;
        GameObject flower = Instantiate(blackFlower, transform.parent);
        flower.GetComponent<HealthParticles>().raining = false;
        Destroy(this.gameObject);
    }
}
