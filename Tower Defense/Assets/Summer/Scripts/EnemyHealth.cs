using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class EnemyHealth : MonoBehaviour
{
    //Basic version for now
    public int startHealth = 4;
    public int hp;

    //[SerializeField] private List<GameObject> decals;
    //[SerializeField] private Color[] colours;

    //GameObject currentDecal;

    private void Start()
    {
        //decal.GetComponent<DecalProjector>().material.color;
        hp = startHealth;
       
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int dam)
    {
        hp -= dam;
    }
}
