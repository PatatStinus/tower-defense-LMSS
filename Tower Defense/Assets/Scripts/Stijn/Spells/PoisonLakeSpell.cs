using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonLakeSpell : MonoBehaviour
{
    public float size;
    [SerializeField] private int damage = 3;
    [SerializeField] private int cost = 100;
    [SerializeField] private float durationSpell = 3f;
    [SerializeField] private float slowness = 2;
    [SerializeField] private GameObject posionLakeEffect;
    private SphereCollider sc;
    private bool spellActive;

    public void SpawnLake(Vector3 spellPos)
    {
        spellActive = true;
        ManageMoney.LoseMoney(cost);
        posionLakeEffect.transform.position = new Vector3(spellPos.x, 0, spellPos.z);
        Instantiate(posionLakeEffect);
        sc = gameObject.AddComponent<SphereCollider>();
        sc.center = spellPos;
        sc.radius = size;
        sc.isTrigger = true;
        gameObject.layer = 3;
    }

    private void Update()
    {
        if(spellActive)
        {
            durationSpell -= Time.deltaTime;
            if (durationSpell < 0)
                sc.center = new Vector3(100f, 1000f, 100f);
            if (durationSpell < -0.3)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 14)
        {
            if (other.gameObject.GetComponent<EnemyMovement>())
                other.gameObject.GetComponent<EnemyMovement>().divideSpeed = slowness;
            if (other.gameObject.GetComponent<JumpingAbility>())
                other.gameObject.GetComponent<JumpingAbility>().canAbility = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            if(other.gameObject.GetComponent<EnemyMovement>())
                other.gameObject.GetComponent<EnemyMovement>().divideSpeed = 1;
            if (other.gameObject.GetComponent<JumpingAbility>())
                other.gameObject.GetComponent<JumpingAbility>().canAbility = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            if (other.gameObject.GetComponent<EnemyHealth>())
                other.gameObject.GetComponent<EnemyHealth>().hp -= damage * Time.fixedDeltaTime;
        }
    }
}
